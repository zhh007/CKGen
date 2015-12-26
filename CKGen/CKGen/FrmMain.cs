using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;
using System.Globalization;
using System.Threading;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using CKGen.Temp.Adonet;
using CKGen.Controls;

namespace CKGen
{
    public partial class FrmMain : Form
    {
        private IDatabaseInfo DB = null;
        private TableDetail DetailPage = null;
        private ContextMenuStrip TableMenu = null;
        private TableDataAccessGen gen = new TableDataAccessGen();

        public FrmMain()
        {
            InitializeComponent();
        }

        [ImportMany("UserControl")]
        IEnumerable<UserControl> UIs { get; set; }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (SystemConfig.Instance.SrvInfo == null)
            {
                FrmLogin frmLogin = new FrmLogin();
                if (frmLogin.ShowDialog() == DialogResult.Cancel)
                {
                    this.Close();
                    return;
                }
            }

            this.DB = SystemConfig.Instance.Database;

            //详细信息
            this.tabControl1.TabPages.Add(new DetailTabPage());

            //
            InitTree();

            //菜单
            CreateMenu();

            //组合部件
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory));
            var container = new CompositionContainer(catalog);
            container.ComposeExportedValue("ModuleName", SystemConfig.Instance.Database);
            container.ComposeParts(this);

            LoadTemplates();
        }

        private void LoadTemplates()
        {
            TreeNode tbNode = new TreeNode("模板");
            int i = 0;
            foreach (var item in UIs)
            {
                item.Name = string.Format("TempUI_{0}", i);
                TreeNode node = new TreeNode(item.ToString());
                node.Tag = item;
                tbNode.Nodes.Add(node);
                i++;
            }
            this.tbTemp.Nodes.Add(tbNode);
            tbNode.Expand();
        }

        private void InitTree()
        {
            TreeNode tbNode = new TreeNode("表");
            tbNode.ImageIndex = 1;
            tbNode.SelectedImageIndex = 1;
            foreach (ITableInfo tbInfo in this.DB.Tables)
            {
                TreeNode node = new TreeNode(tbInfo.RawName);
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = tbInfo;

                var colHasNewDesc = (from p in tbInfo.Columns
                                     where p.Attributes.ContainsKey("new_desc") && !string.IsNullOrEmpty(p.Attributes["new_desc"])
                                     select p).Count() > 0;
                if (colHasNewDesc || (tbInfo.Attributes.ContainsKey("new_desc") && !string.IsNullOrEmpty(tbInfo.Attributes["new_desc"])))
                {
                    node.Text = tbInfo.RawName + "(*)";
                    node.ForeColor = Color.Red;
                }

                tbNode.Nodes.Add(node);
            }
            this.tvSchema.Nodes.Add(tbNode);
            tbNode.Expand();
            if (tbNode.Nodes.Count > 0)
            {
                this.tvSchema.SelectedNode = tbNode.Nodes[0];
            }

            //视图
            TreeNode vwRoot = new TreeNode("视图");
            vwRoot.ImageIndex = 1;
            vwRoot.SelectedImageIndex = 1;
            foreach (var vwInfo in this.DB.Views)
            {
                TreeNode node = new TreeNode(vwInfo.RawName);
                node.ImageIndex = 4;
                node.SelectedImageIndex = 4;
                node.Tag = vwInfo;

                var colHasNewDesc = (from p in vwInfo.Columns
                                     where p.Attributes.ContainsKey("new_desc") && !string.IsNullOrEmpty(p.Attributes["new_desc"])
                                     select p).Count() > 0;
                if (colHasNewDesc || (vwInfo.Attributes.ContainsKey("new_desc") && !string.IsNullOrEmpty(vwInfo.Attributes["new_desc"])))
                {
                    node.Text = vwInfo.RawName + "(*)";
                    node.ForeColor = Color.Red;
                }

                vwRoot.Nodes.Add(node);
            }
            this.tvSchema.Nodes.Add(vwRoot);

            //存储过程
            TreeNode procRoot = new TreeNode("存储过程");
            procRoot.ImageIndex = 1;
            procRoot.SelectedImageIndex = 1;
            foreach (var procInfo in this.DB.Procedures)
            {
                TreeNode node = new TreeNode(procInfo.Name);
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = procInfo;

                //var colHasNewDesc = (from p in procInfo.Columns
                //                     where !string.IsNullOrEmpty(p.Attributes["new_desc"])
                //                     select p).Count() > 0;
                //if (colHasNewDesc || !string.IsNullOrEmpty(procInfo.Attributes["new_desc"]))
                //{
                //    node.Text = procInfo.RawName + "(*)";
                //    node.ForeColor = Color.Red;
                //}

                procRoot.Nodes.Add(node);
            }
            this.tvSchema.Nodes.Add(procRoot);
        }

        private void CreateMenu()
        {
            this.TableMenu = new ContextMenuStrip();
            this.TableMenu.Items.Add("查看前 100 行", null, (s, e) =>
            {
                if (this.tvSchema.SelectedNode.Tag is ITableInfo)
                {
                    ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                    string sql = string.Format("select top 100 * from {0}", tbInfo.RawName);

                    var query = new UCQuery();
                    query.Dock = DockStyle.Fill;
                    TabPage tab1 = new TabPage("查询");
                    tab1.Controls.Add(query);
                    this.tabControl1.TabPages.Add(tab1);
                    this.tabControl1.SelectTab(tab1);
                    query.Query(sql);
                }
            });
            ToolStripSeparator spliter = new ToolStripSeparator();
            this.TableMenu.Items.Add(spliter);
            this.TableMenu.Items.Add("生成 - 实体类", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenModelCode(tbInfo);
                ShowCode(string.Format("{0}.cs", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - 数据访问类", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenDataAccessCode("Test", tbInfo);
                ShowCode(string.Format("{0}Access.cs", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Insert", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenInsertCode(tbInfo);
                ShowCode(string.Format("{0} - Insert", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Update", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenUpdateCode(tbInfo);
                ShowCode(string.Format("{0} - Update", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Delete", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenDeleteCode(tbInfo);
                ShowCode(string.Format("{0} - Delete", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Save", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenSaveCode(tbInfo);
                ShowCode(string.Format("{0} - Save", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Exist", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenExistCode(tbInfo);
                ShowCode(string.Format("{0} - Exist", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Get", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenGetCode(tbInfo);
                ShowCode(string.Format("{0} - Get", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - GetAll", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenGetAllCode(tbInfo);
                ShowCode(string.Format("{0} - GetAll", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Top", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenTopCode(tbInfo);
                ShowCode(string.Format("{0} - Top", tbInfo.PascalName), code);
            });
            this.TableMenu.Items.Add("生成 - Paged", null, (s, e) =>
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = gen.GenPagedCode(tbInfo);
                ShowCode(string.Format("{0} - Paged", tbInfo.PascalName), code);
            });
        }

        private void ShowCode(string title, string code)
        {
            if(!string.IsNullOrEmpty(code))
            {
                UCCodeShow codeShow = new UCCodeShow();
                codeShow.Dock = DockStyle.Fill;
                TabPage tab1 = new TabPage(title);
                tab1.Controls.Add(codeShow);
                this.tabControl1.TabPages.Add(tab1);
                this.tabControl1.SelectTab(tab1);
                codeShow.Show(code);
            }
        }

        private void tvSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SystemConfig.Instance.SelectedNode = e.Node;
        }

        /// <summary>
        /// 保存说明
        /// </summary>
        private void tsbtnSaveSchema_Click(object sender, EventArgs e)
        {
            this.DetailPage.Save();
            foreach (TreeNode node in tvSchema.Nodes)
            {
                UpdateNode(node);
            }
        }

        private void UpdateNode(TreeNode node)
        {
            if(node.Tag is ITableInfo)
            {
                ITableInfo tbInfo = node.Tag as ITableInfo;
                node.Text = tbInfo.RawName;
                node.ForeColor = Color.Black;
            }
            foreach (TreeNode item in node.Nodes)
            {
                UpdateNode(item);
            }
        }

        FrmLoading loadForm;
        /// <summary>
        /// 重新加载元数据
        /// </summary>
        private void tsBtnReloadSchema_Click(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();

            loadForm = new FrmLoading();
            loadForm.ShowDialog();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadForm.Close();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //保存说明
            //重新加载数据结构
            //更新界面
            for (int i = 0; i < 10; i++)
            {
                Debug.WriteLine(i);
                Thread.Sleep(1000);
            }
        }

        private void tvSchema_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.tvSchema.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right && e.Node.Tag is ITableInfo)
            {
                this.TableMenu.Show(this.tvSchema, e.Location);
            }
        }

        private void tbTemp_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is UserControl)
            {
                UserControl item = e.Node.Tag as UserControl;
                TabPage tab = HasShowControl(item.Name);
                if (tab == null)
                {
                    tab = new TabPage(item.ToString());
                    item.Dock = DockStyle.Fill;
                    tab.Controls.Add(item);
                    this.tabControl1.TabPages.Add(tab);
                }
                this.tabControl1.SelectedTab = tab;
            }
        }

        private TabPage HasShowControl(string ctrlName)
        {
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                var all = tab.Controls.Find(ctrlName, false);
                if (all != null && all.Length > 0)
                {
                    return tab;
                }
            }
            return null;
        }
    }
}

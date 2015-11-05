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

namespace CKGen
{
    public partial class FrmMain : Form
    {
        private IDatabaseInfo DB = null;
        private UCDetail DetailPage = null;
        private ContextMenuStrip TableMenu = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        [ImportMany("UserControl")]
        IEnumerable<UserControl> UIs { get; set; }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (SystemConfig.SrvInfo == null)
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
            this.DetailPage = new UCDetail();
            this.DetailPage.Dock = DockStyle.Fill;
            TabPage tab1 = new TabPage("详细信息");
            tab1.Controls.Add(this.DetailPage);
            this.tabControl1.TabPages.Add(tab1);

            //
            InitTree();

            //this.WindowState = FormWindowState.Maximized;

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
            foreach (ITableInfo item in this.DB.Tables)
            {
                TreeNode node = new TreeNode(item.RawName);
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = item;
                tbNode.Nodes.Add(node);
            }
            this.tvSchema.Nodes.Add(tbNode);
            tbNode.Expand();
            if (tbNode.Nodes.Count > 0)
            {
                this.tvSchema.SelectedNode = tbNode.Nodes[0];
            }

            //菜单
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
            this.TableMenu.Items.Add("生成代码 - Model", null, (s, e) =>
            {
                GenCode("Model.cshtml");
            });
            this.TableMenu.Items.Add("生成代码 - Insert", null, (s, e) =>
            {
                GenCode("Insert.cshtml");
            });
            this.TableMenu.Items.Add("生成代码 - Update", null, (s, e) =>
            {
                GenCode("Update.cshtml");
            });
            this.TableMenu.Items.Add("生成代码 - Delete", null, (s, e) =>
            {
                GenCode("Delete.cshtml");
            });
            this.TableMenu.Items.Add("生成代码 - Save", null, (s, e) =>
            {
                GenCode("Save.cshtml");
            });
            this.TableMenu.Items.Add("生成代码 - Exist", null, (s, e) =>
            {
                GenCode("Exist.cshtml");
            });
            this.TableMenu.Items.Add("生成代码 - Get", null, (s, e) =>
            {
                GenCode("Get.cshtml");
            });
            this.TableMenu.Items.Add("生成代码 - Top", null, (s, e) =>
            {
                GenCode("Top.cshtml");
            });
            this.TableMenu.Items.Add("生成代码 - Select", null, (s, e) =>
            {
                GenCode("Select.cshtml");
            });
            this.TableMenu.Items.Add("生成代码 - Paged", null, (s, e) =>
            {
                GenCode("Paged.cshtml");
            });
        }

        private void GenCode(string viewName)
        {
            if (this.tvSchema.SelectedNode.Tag is ITableInfo)
            {
                ITableInfo tbInfo = this.tvSchema.SelectedNode.Tag as ITableInfo;
                string code = CodeGen.GenForTable(viewName, tbInfo);
                UCCodeShow codeShow = new UCCodeShow();
                codeShow.Dock = DockStyle.Fill;
                TabPage tab1 = new TabPage("Model For " + tbInfo.Name);
                tab1.Controls.Add(codeShow);
                this.tabControl1.TabPages.Add(tab1);
                this.tabControl1.SelectTab(tab1);
                codeShow.Show(code);
            }
        }

        private void tvSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is ITableInfo)
            {
                ITableInfo tbInfo = e.Node.Tag as ITableInfo;
                SystemConfig.Instance.SelectedNode = e.Node;
                SystemConfig.Instance.CurrentTableName = tbInfo.RawName;
            }
        }

        /// <summary>
        /// 保存说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSaveSchema_Click(object sender, EventArgs e)
        {
            this.DetailPage.Save();
        }

        /// <summary>
        /// 重新加载元数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnReloadSchema_Click(object sender, EventArgs e)
        {

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

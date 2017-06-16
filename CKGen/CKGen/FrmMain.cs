using CKGen.Base.Events;
using CKGen.Controls;
using CKGen.Events;
using CKGen.Temp.DBDoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CKGen
{
    public partial class FrmMain : Form
    {
        [ImportMany("GenTemplate")]
        IEnumerable<UserControl> UIs { get; set; }

        [ImportMany("Tool")]
        IEnumerable<UserControl> ToolUIs { get; set; }
        public string Version { get; private set; }
        public bool CanUpdate { get; private set; }

        public FrmMain()
        {
            InitializeComponent();

            var v = Assembly.GetEntryAssembly().GetName().Version;
            this.Version = string.Format("v{0}.{1}", v.Major, v.Minor);
            string title = string.Format("编程辅助工具{0}", this.Version);
            this.Text = title;

            if(Util.IsAdministrator())
            {
                this.Text += "(管理员)";
            }

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();

            this.IsMdiContainer = true;
            this.dockPanel.DocumentStyle = DocumentStyle.DockingMdi;

            //详细信息
            DockContent dc = new DockContent();
            dc.CloseButtonVisible = false;
            dc.Text = "详细信息";
            DetailTabPage dtpage = new DetailTabPage();
            dtpage.Dock = DockStyle.Fill;
            dc.Controls.Add(dtpage);
            dc.Show(this.dockPanel, DockState.Document);

            //数据
            DockContent dc2 = new DockContent();
            dc2.CloseButtonVisible = false;
            dc2.Text = "数据";
            SchemaTreeView stpage = new SchemaTreeView();
            stpage.Dock = DockStyle.Fill;
            dc2.Controls.Add(stpage);
            dc2.Show(this.dockPanel, DockState.DockLeft);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(this.CanUpdate)
            {
                System.Windows.Forms.ToolStripButton tsBtnUpdate = new System.Windows.Forms.ToolStripButton();
                this.toolStrip1.Items.Add(tsBtnUpdate);
                tsBtnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                tsBtnUpdate.Image = global::CKGen.Properties.Resources.update;
                tsBtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
                tsBtnUpdate.Size = new System.Drawing.Size(23, 22);
                tsBtnUpdate.Text = "有新版本发布";
                tsBtnUpdate.Click += new System.EventHandler(this.tsBtnUpdate_Click);
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var files = Directory.GetFiles(Environment.CurrentDirectory, "*.old", SearchOption.TopDirectoryOnly); ;
            foreach (var oldfile in files)
            {
                if (File.Exists(oldfile))
                {
                    File.Delete(oldfile);
                }
            }

            UpdateCheck check = new UpdateCheck();
            this.CanUpdate = check.CanUpdate(this.Version);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //组合部件
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory));
            var container = new CompositionContainer(catalog);
            container.ComposeExportedValue("Database", App.Instance.Database);
            container.ComposeParts(this);

            LoadTemplates();

            tsBtnQuery.Enabled = false;
            tsBtnReloadSchema.Enabled = false;
            tsbtnSaveSchema.Enabled = false;
            tsBtnGenDbChm.Enabled = false;

            AppEvent.Subscribe<ShowCodeEvent>(p => this.ShowCode(p));
            AppEvent.Subscribe<ShowSQLQueryEvent>(p => this.ShowSQLQuery(p));
            AppEvent.Subscribe<DatabaseRefreshEvent>(p =>
            {
                tsBtnQuery.Enabled = true;
                tsBtnReloadSchema.Enabled = true;
                tsbtnSaveSchema.Enabled = true;
                tsBtnGenDbChm.Enabled = true;
            });
        }

        public void ShowCode(ShowCodeEvent e)
        {
            CodeView codeShow = new CodeView();
            codeShow.Dock = DockStyle.Fill;

            DockContent dc = new DockContent();
            dc.Text = e.Title;
            dc.Controls.Add(codeShow);
            dc.Show(this.dockPanel, DockState.Document);

            codeShow.Show(e.Code);
        }

        public void ShowSQLQuery(ShowSQLQueryEvent e)
        {
            var query = new SQLQueryView();
            query.Dock = DockStyle.Fill;

            DockContent dc = new DockContent();
            dc.Text = "查询";
            dc.Controls.Add(query);
            dc.Show(this.dockPanel, DockState.Document);

            if (!string.IsNullOrEmpty(e.SQL))
            {
                query.Query(e.SQL);
            }
        }

        private void LoadTemplates()
        {
            TreeView tbTemp = new TreeView();
            tbTemp.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tbTemp_NodeMouseDoubleClick);

            //模板
            TreeNode tmpNode = new TreeNode("模板");
            int i = 0;
            foreach (var item in UIs)
            {
                item.Name = string.Format("TempUI_{0}", i);
                TreeNode node = new TreeNode(item.ToString());
                node.Tag = item;
                tmpNode.Nodes.Add(node);
                i++;
            }
            tbTemp.Nodes.Add(tmpNode);
            tmpNode.Expand();

            //工具
            TreeNode toolNode = new TreeNode("工具");
            i = 0;
            foreach (var item in ToolUIs)
            {
                item.Name = string.Format("Tool_{0}", i);
                TreeNode node = new TreeNode(item.ToString());
                node.Tag = item;
                toolNode.Nodes.Add(node);
                i++;
            }
            tbTemp.Nodes.Add(toolNode);
            toolNode.Expand();

            DockContent dc2 = new DockContent();
            dc2.CloseButtonVisible = false;
            dc2.Text = "工具";
            tbTemp.Dock = DockStyle.Fill;
            dc2.Controls.Add(tbTemp);
            dc2.Show(this.dockPanel, DockState.DockLeft);
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        private void tsBtnLinkDb_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();
        }

        /// <summary>
        /// 重新加载元数据
        /// </summary>
        private void tsBtnReloadSchema_Click(object sender, EventArgs e)
        {
            if (App.Instance.DBLink == null)
            {
                return;
            }

            FrmLoading loadForm = new FrmLoading();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate (object s1, DoWorkEventArgs e1)
            {
                //保存说明
                //重新加载数据结构
                App.Instance.RefreshDbSchema();
            };
            worker.RunWorkerCompleted += delegate (object s2, RunWorkerCompletedEventArgs e2)
            {
                loadForm.Close();
                //更新界面
                AppEvent.Publish(new DatabaseRefreshEvent() { Database = App.Instance.Database });
            };
            worker.RunWorkerAsync();

            loadForm.ShowDialog();
        }

        /// <summary>
        /// 保存说明
        /// </summary>
        private void tsbtnSaveSchema_Click(object sender, EventArgs e)
        {
            if (App.Instance.DBLink == null)
            {
                return;
            }
            if (DialogResult.OK == MessageBox.Show("是否将新的说明同时保存到本地和数据库？", "保存提示", MessageBoxButtons.OKCancel))
            {
                DatabaseSchemaSetting.SaveDesc(App.Instance.Database);
                AppEvent.Publish(new SaveDescToDbEvent());
                MessageBox.Show("保存成功。");
            }
        }

        /// <summary>
        /// SQL查询
        /// </summary>
        private void tsBtnQuery_Click(object sender, EventArgs e)
        {
            if (App.Instance.DBLink == null)
            {
                return;
            }
            ShowSQLQueryEvent evt = new ShowSQLQueryEvent();
            AppEvent.Publish(evt);
        }

        /// <summary>
        /// 生成数据库文档
        /// </summary>
        private void tsBtnGenDbChm_Click(object sender, EventArgs e)
        {
            if (App.Instance.DBLink == null)
            {
                return;
            }

            DBDocBuilder builder = new DBDocBuilder(App.Instance.Database);
            FrmLoading loadForm = new FrmLoading();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate (object s1, DoWorkEventArgs e1)
            {
                builder.Build();
            };
            worker.RunWorkerCompleted += delegate (object s2, RunWorkerCompletedEventArgs e2)
            {
                loadForm.Close();
                Process.Start(builder.TargetFolder);
            };
            worker.RunWorkerAsync();

            loadForm.ShowDialog();
        }

        private void tbTemp_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is UserControl)
            {
                Type tp = e.Node.Tag.GetType();
                var item = Activator.CreateInstance(tp) as UserControl;

                DockContent dc = new DockContent();
                dc.Text = item.ToString();
                item.Dock = DockStyle.Fill;
                dc.Controls.Add(item);
                dc.Show(this.dockPanel, DockState.Document);
            }
        }

        /// <summary>
        /// 软件更新
        /// </summary>
        private void tsBtnUpdate_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("CKGenUpdater"))
            {
                process.Kill();
            }

            Process.Start(Path.Combine(Environment.CurrentDirectory, "CKGenUpdater.exe"));
        }
    }
}

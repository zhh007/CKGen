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
using CKGen.Events;

namespace CKGen
{
    public partial class FrmMain : Form
    {
        private IDatabaseInfo DB = null;
        private DbTableCodeGen gen = new DbTableCodeGen();

        public FrmMain()
        {
            InitializeComponent();
        }

        [ImportMany("GenTemplate")]
        IEnumerable<UserControl> UIs { get; set; }

        [ImportMany("Tool")]
        IEnumerable<UserControl> ToolUIs { get; set; }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (App.Instance.SrvInfo == null)
            {
                FrmLogin frmLogin = new FrmLogin();
                if (frmLogin.ShowDialog() == DialogResult.Cancel)
                {
                    this.Close();
                    return;
                }
            }

            this.DB = App.Instance.Database;

            //详细信息
            this.tabControl1.TabPages.Add(new DetailTabPage());

            this.tabPage1.Controls.Add(new SchemaTreeView());

            //组合部件
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory));
            var container = new CompositionContainer(catalog);
            container.ComposeExportedValue("Database", App.Instance.Database);
            container.ComposeParts(this);

            LoadTemplates();

            App.Instance.Subscribe<ShowCodeEvent>(p => this.ShowCode(p));
            App.Instance.Subscribe<ShowSQLQueryEvent>(p => this.ShowSQLQuery(p));
        }

        public void ShowCode(ShowCodeEvent e)
        {
            if (!string.IsNullOrEmpty(e.Code))
            {
                CodeView codeShow = new CodeView();
                codeShow.Dock = DockStyle.Fill;
                TabPage tab1 = new TabPage(e.Title);
                tab1.Controls.Add(codeShow);
                this.tabControl1.TabPages.Add(tab1);
                this.tabControl1.SelectTab(tab1);
                codeShow.Show(e.Code);
            }
        }

        public void ShowSQLQuery(ShowSQLQueryEvent e)
        {
            var query = new SQLQueryView();
            query.Dock = DockStyle.Fill;
            TabPage tab1 = new TabPage("查询");
            tab1.Controls.Add(query);
            this.tabControl1.TabPages.Add(tab1);
            this.tabControl1.SelectTab(tab1);
            if (!string.IsNullOrEmpty(e.SQL))
            {
                query.Query(e.SQL);
            }
        }

        private void LoadTemplates()
        {
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
            this.tbTemp.Nodes.Add(tmpNode);
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
            this.tbTemp.Nodes.Add(toolNode);
            toolNode.Expand();
        }

        /// <summary>
        /// 保存说明
        /// </summary>
        private void tsbtnSaveSchema_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("是否将新的说明同时保存到本地和数据库？", "保存提示", MessageBoxButtons.OKCancel))
            {
                DatabaseSchemaSetting.SaveDesc(App.Instance.Database);
                App.Instance.Publish<SaveDescToDbEvent>(new SaveDescToDbEvent());
                MessageBox.Show("保存成功。");
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
            this.DB = App.Instance.Database;
            //更新界面
            App.Instance.Publish<DatabaseRefreshEvent>(new DatabaseRefreshEvent());
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //保存说明
            //重新加载数据结构
            App.Instance.RefreshDbSchema();
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

        private void tsBtnQuery_Click(object sender, EventArgs e)
        {
            ShowSQLQueryEvent evt = new ShowSQLQueryEvent();
            App.Instance.Publish<ShowSQLQueryEvent>(evt);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;
using CKGen.Base;
using CKGen.Base.Events;

namespace CKGen.Temp.AspnetMVC
{
    public partial class ViewSetting : UserControl
    {
        public IDatabaseInfo Database { get; set; }
        private readonly object token = new object();
        private IGenUI parent;
        private string folderPath;
        private string tableName;
        private string nsString;
        private string webNSString;
        private ITableInfo SelectedTable;

        public ViewSetting(IGenUI _parent)
        {
            parent = _parent;
            InitializeComponent();
            btnView.Hide();

            AppEvent.Subscribe<GetDbInstanceResponseEvent>(p =>
            {
                if (p.Token == token)
                {
                    BindUI(p.Database);
                }
            });
            AppEvent.Publish(new GetDbInstanceRequestEvent() { Token = token });

            AppEvent.Subscribe<DatabaseRefreshEvent>(p =>
            {
                BindUI(p.Database);
            });
        }

        private void BindUI(IDatabaseInfo db)
        {
            this.Database = db;
            cbTables.Items.Clear();
            cbTables.Text = "";
            if (db != null)
            {
                foreach (var table in db.Tables)
                {
                    cbTables.Items.Add(table.Name);
                }

                AspnetMVCSetting setting = SettingStore.Instance.Get<AspnetMVCSetting>(db.Name + "_aspnetmvc.xml");
                if (setting != null)
                {
                    txtNamespace.Text = setting.Namespace;
                    txtWebProjNameSpace.Text = setting.WebProjNameSpace;
                }
            }
        }

        /// <summary>
        /// 生成
        /// </summary>
        private void btnGen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNamespace.Text) || string.IsNullOrWhiteSpace(txtNamespace.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtWebProjNameSpace.Text) || string.IsNullOrWhiteSpace(txtWebProjNameSpace.Text))
            {
                return;
            }
            string tbName = cbTables.Text.Trim();
            if (string.IsNullOrEmpty(tbName))
            {
                return;
            }

            bool hasTable = false;
            foreach (var item in this.Database.Tables)
            {
                if (item.Name == tbName)
                {
                    SelectedTable = item;
                    hasTable = true;
                    break;
                }
            }
            if (!hasTable)
            {
                return;
            }

            txtNamespace.Enabled = false;
            txtWebProjNameSpace.Enabled = false;
            cbTables.Enabled = false;
            btnGen.Enabled = false;
            btnGen.Text = "正在生成...";
            btnView.Hide();
            //lblTable.DataBindings.Remove(_tableNameBinding);
            //firstFileTreeNode = null;

            tableName = cbTables.Text.Trim();
            nsString = txtNamespace.Text.Trim();
            webNSString = txtWebProjNameSpace.Text.Trim();

            AspnetMVCSetting setting = new AspnetMVCSetting();
            setting.Namespace = nsString;
            setting.WebProjNameSpace = webNSString;
            SettingStore.Instance.Save<AspnetMVCSetting>(setting, this.Database.Name + "_aspnetmvc.xml");

            BackgroundWorker _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerSupportsCancellation = false;
            _bgWorker.WorkerReportsProgress = false;
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }

        private void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtNamespace.Enabled = true;
            txtWebProjNameSpace.Enabled = true;
            cbTables.Enabled = true;
            btnGen.Enabled = true;
            btnGen.Text = "生成";
            btnView.Show();

            parent.GoResult(folderPath);

            //_tableNameBinding = lblTable.DataBindings.Add("Text", SystemConfig.Instance, "CurrentTableName");
        }

        private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CodeBuilder mvcBuilder = new CodeBuilder();
            folderPath = mvcBuilder.Build(SelectedTable, nsString, webNSString);
        }

        /// <summary>
        /// 查看结果
        /// </summary>
        private void btnView_Click(object sender, EventArgs e)
        {
            parent.ShowResultView();
        }
    }
}

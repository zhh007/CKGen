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

namespace CKGen.Temp.AspnetMVC
{
    public partial class ViewSetting : UserControl
    {
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
            if(this.parent.Database != null)
            {
                foreach (var table in this.parent.Database.Tables)
                {
                    cbTables.Items.Add(table.Name);
                }
            }

            AspnetMVCSetting setting = SettingStore.Instance.Get<AspnetMVCSetting>(this.parent.Database.Name + "_aspnetmvc.xml");
            if(setting != null)
            {
                txtNamespace.Text = setting.Namespace;
                txtWebProjNameSpace.Text = setting.WebProjNameSpace;
            }
        }

        /// <summary>
        /// 生成
        /// </summary>
        private void btnGen_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtNamespace.Text) || string.IsNullOrWhiteSpace(txtNamespace.Text))
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
            foreach (var item in parent.Database.Tables)
            {
                if(item.Name == tbName)
                {
                    SelectedTable = item;
                    hasTable = true;
                    break;
                }
            }
            if(!hasTable)
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
            SettingStore.Instance.Save<AspnetMVCSetting>(setting, this.parent.Database.Name + "_aspnetmvc.xml");

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
            //IDatabaseInfo database = parent.Database;
            CodeBuilder mvcBuilder = new CodeBuilder();
            //folderPath = mvcBuilder.Build(database, tableName, nsString, webNSString);
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

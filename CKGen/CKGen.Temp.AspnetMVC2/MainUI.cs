using CKGen.Base;
using CKGen.Base.Events;
using CKGen.DBSchema;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace CKGen.Temp.AspnetMVC2
{
    [Export("GenTemplate", typeof(UserControl))]
    public partial class MainUI : UserControl, IGenUI
    {
        private readonly object token = new object();
        private IDatabaseInfo Database { get; set; }
        private ViewResult vResult = null;
        private string folderPath;
        private string tableName;
        private string nsString;
        private string dbContext;
        private string entityName;
        private ITableInfo SelectedTable;

        private Action<GetDbInstanceResponseEvent> dbResponseEventHandler = null;
        private Action<DatabaseRefreshEvent> dbRefreshEventHandler = null;
        public override string ToString()
        {
            return "ASP.NET MVC 模板";
        }

        public MainUI()
        {
            InitializeComponent();

            dbResponseEventHandler = p =>
            {
                if (p.Token == token)
                {
                    BindUI(p.Database);
                }
            };
            dbRefreshEventHandler = p =>
            {
                BindUI(p.Database);
            };
            AppEvent.Subscribe(dbResponseEventHandler);
            AppEvent.Subscribe(dbRefreshEventHandler);
            AppEvent.Publish(new GetDbInstanceRequestEvent() { Token = token });
            this.Disposed += SimpleChildUI_Disposed;
        }

        private void SimpleChildUI_Disposed(object sender, EventArgs e)
        {
            AppEvent.UnSubscribe(dbResponseEventHandler);
            AppEvent.UnSubscribe(dbRefreshEventHandler);
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

                AspnetMVCSetting setting = SettingStore.Instance.Get<AspnetMVCSetting>(db.Name + "_aspnetmvc2.xml");
                if (setting != null)
                {
                    if (!string.IsNullOrEmpty(setting.Namespace))
                    {
                        txtNamespace.Text = setting.Namespace;
                    }

                    if (!string.IsNullOrEmpty(setting.DBContextTypeName))
                    {
                        txtDBContextTypeName.Text = setting.DBContextTypeName;
                    }
                }
            }
        }

        private void BuildUI_Load(object sender, EventArgs e)
        {
            vResult = new ViewResult(this);
            this.Controls.Add(vResult);
            vResult.Hide();
            btnView.Hide();
        }

        public void GoResult(string dir)
        {
            vResult.Show(dir);
            ShowResultView();
        }

        public void ShowSettingView()
        {
            this.MainPanel.Show();
            this.vResult.Hide();
        }

        public void ShowResultView()
        {
            this.MainPanel.Hide();
            this.vResult.Show();
            this.vResult.Dock = DockStyle.Fill;
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

            string tbName = cbTables.Text.Trim();
            if (string.IsNullOrEmpty(tbName))
            {
                return;
            }
            entityName = txtEntityName.Text;
            if (string.IsNullOrEmpty(entityName) || string.IsNullOrWhiteSpace(entityName))
            {
                return;
            }

            dbContext = txtDBContextTypeName.Text;
            if (string.IsNullOrEmpty(dbContext) || string.IsNullOrWhiteSpace(dbContext))
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

            txtDBContextTypeName.Enabled = false;
            txtNamespace.Enabled = false;
            cbTables.Enabled = false;
            txtEntityName.Enabled = false;
            btnGen.Enabled = false;
            btnGen.Text = "正在生成...";
            btnView.Hide();
            //lblTable.DataBindings.Remove(_tableNameBinding);
            //firstFileTreeNode = null;

            tableName = cbTables.Text.Trim();
            nsString = txtNamespace.Text.Trim();
            entityName = txtEntityName.Text.Trim();

            AspnetMVCSetting setting = new AspnetMVCSetting();
            setting.Namespace = nsString;
            setting.DBContextTypeName = dbContext;
            SettingStore.Instance.Save<AspnetMVCSetting>(setting, this.Database.Name + "_aspnetmvc2.xml");

            BackgroundWorker _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerSupportsCancellation = false;
            _bgWorker.WorkerReportsProgress = false;
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }

        private void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtDBContextTypeName.Enabled = true;
            txtNamespace.Enabled = true;
            cbTables.Enabled = true;
            txtEntityName.Enabled = true;
            btnGen.Enabled = true;
            btnGen.Text = "生成";
            btnView.Show();

            GoResult(folderPath);
        }

        private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CodeBuilder mvcBuilder = new CodeBuilder();
            folderPath = mvcBuilder.Build(SelectedTable, nsString, entityName, dbContext);
        }

        /// <summary>
        /// 查看结果
        /// </summary>
        private void btnView_Click(object sender, EventArgs e)
        {
            ShowResultView();
        }

        private void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var str = StringHelper.SetPascalCase(cbTables.Text.Trim());
            txtEntityName.Text = str.Replace("_", "");
        }
    }
}

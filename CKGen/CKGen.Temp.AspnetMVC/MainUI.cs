using CKGen.Base.Events;
using CKGen.DBSchema;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace CKGen.Temp.AspnetMVC
{
    [Export("GenTemplate", typeof(UserControl))]
    public partial class MainUI : UserControl, IGenUI
    {
        [Import("Database")]
        public IDatabaseInfo Database { get; set; }

        private ViewSetting vSetting = null;
        private ViewResult vResult = null;

        public MainUI()
        {
            InitializeComponent();

            AppEvent.Subscribe<DatabaseRefreshEvent>(p => {
                this.Database = p.Database;
            });
        }

        private void BuildUI_Load(object sender, EventArgs e)
        {
            vSetting = new ViewSetting(this);
            vResult = new ViewResult(this);

            this.Controls.Clear();
            this.Controls.Add(vSetting);
        }

        public void GoResult(string dir)
        {
            vResult.Show(dir);
            ShowResultView();
        }

        public void ShowSettingView()
        {
            this.Controls.Clear();
            this.Controls.Add(vSetting);
        }

        public void ShowResultView()
        {
            this.Controls.Clear();
            this.Controls.Add(vResult);
            vResult.Dock = DockStyle.Fill;
        }

        public override string ToString()
        {
            return "ASP.NET MVC 模板";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using CKGen.DBSchema;

namespace CKGen.Temp.AspnetMVC
{
    [Export("UserControl", typeof(UserControl))]
    public partial class MainUI : UserControl
    {
        [Import("ModuleName")]
        public IDatabaseInfo Database { get; set; }

        private ViewSetting vSetting = null;
        private ViewResult vResult = null;

        public MainUI()
        {
            InitializeComponent();
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

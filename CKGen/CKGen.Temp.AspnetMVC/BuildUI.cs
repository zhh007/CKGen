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
    public partial class BuildUI : UserControl
    {
        [Import("ModuleName")]
        public IDatabaseInfo Database { get; set; }

        public BuildUI()
        {
            InitializeComponent();
        }

        private void BuildUI_Load(object sender, EventArgs e)
        {
            label1.Text = this.Database.Name;
        }
    }
}

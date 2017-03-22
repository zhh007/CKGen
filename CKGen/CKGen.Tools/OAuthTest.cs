using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Composition;

namespace CKGen.Tools
{
    [Export("Tool", typeof(UserControl))]
    public partial class OAuthTest : UserControl
    {
        private OAuth_ClientCredentials ctrlClientCredentials;
        private OAuth_Password ctrlPassword;

        public OAuthTest()
        {
            InitializeComponent();

            this.ctrlClientCredentials = new OAuth_ClientCredentials(this);
            this.ctrlPassword = new OAuth_Password(this);
        }

        public override string ToString()
        {
            return "OAuth测试";
        }

        private void OAuthTest_Load(object sender, EventArgs e)
        {
            this.tabPage2.Controls.Add(this.ctrlClientCredentials);
            this.ctrlClientCredentials.Dock = DockStyle.Fill;

            this.tabPage1.Controls.Add(this.ctrlPassword);
            this.ctrlPassword.Dock = DockStyle.Fill;
        }
    }
}

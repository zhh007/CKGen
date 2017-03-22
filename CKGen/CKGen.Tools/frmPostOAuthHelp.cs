using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CKGen.Tools
{
    public partial class frmPostOAuthHelp : Form
    {
        public frmPostOAuthHelp()
        {
            InitializeComponent();
        }

        private void frmPostOAuthHelp_Load(object sender, EventArgs e)
        {
            textBox1.Text = "在Headers中，添加Authorization参数，参数值为“Bearer +<access_token>”，如下次下图所示：";
            textBox1.ReadOnly = true;
            textBox1.BorderStyle = 0;
            textBox1.BackColor = this.BackColor;
            textBox1.TabStop = false;
        }
    }
}

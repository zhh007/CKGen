using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CKGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FrmModuleBuild f1 = new FrmModuleBuild();            
            f1.TopLevel = false;
            this.Controls.Add(f1);
            f1.Show();

            f1.Visible = true;
            f1.AutoScroll = true;
            f1.FormBorderStyle = FormBorderStyle.None;
            
            

        }

    }
}

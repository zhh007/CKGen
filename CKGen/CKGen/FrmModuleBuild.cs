using CKGen.Base.Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CKGen
{
    public partial class FrmModuleBuild : Form
    {
        private Module Entity { get; set; }

        public FrmModuleBuild()
        {
            InitializeComponent();
        }

        private void FrmModuleBuild_Load(object sender, EventArgs e)
        {
            if (this.Entity == null)
            {
                FrmSelectTable frm = new FrmSelectTable();
                if (frm.ShowDialog() == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 刷新元数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbRefresh_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbBuild_Click(object sender, EventArgs e)
        {

        }



        
    }
}

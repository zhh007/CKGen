using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;

namespace CKGen
{
    public partial class FrmMain : Form
    {
        private IDatabaseInfo DB = null;
        private UCDetail DetailPage = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (SystemConfig.SrvInfo == null)
            {
                FrmLogin frmLogin = new FrmLogin();
                if (frmLogin.ShowDialog() == DialogResult.Cancel)
                {
                    this.Close();
                    return;
                }
            }

            IDatabaseInfo dbi = SystemConfig.SrvInfo.GetDatabase(SystemConfig.DBName);
            this.DB = DatabaseSchemaSetting.Compute(dbi);
            SystemConfig.Instance.Database = this.DB;

            //详细信息
            this.DetailPage = new UCDetail();
            this.DetailPage.Dock = DockStyle.Fill;
            TabPage tab1 = new TabPage("详细信息");
            tab1.Controls.Add(this.DetailPage);
            this.tabControl1.TabPages.Add(tab1);

            //
            FrmCodeBuild frm = new FrmCodeBuild();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.FormBorderStyle = FormBorderStyle.None;
            TabPage tab = new TabPage(frm.Text);
            tab.Controls.Add(frm);
            this.tabControl1.TabPages.Add(tab);
            frm.Show();

            //
            TreeNode tbNode = new TreeNode("表");
            foreach (ITableInfo item in this.DB.Tables)
            {
                TreeNode node = new TreeNode(item.RawName);
                node.Tag = item;
                tbNode.Nodes.Add(node);
            }
            this.tvSchema.Nodes.Add(tbNode);
            tbNode.Expand();
            if (tbNode.Nodes.Count > 0)
            {
                this.tvSchema.SelectedNode = tbNode.Nodes[0];
            }

            this.WindowState = FormWindowState.Maximized;
        }

        private void tvSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is ITableInfo)
            {
                ITableInfo tbInfo = e.Node.Tag as ITableInfo;
                SystemConfig.Instance.SelectedNode = e.Node;
                SystemConfig.Instance.CurrentTableName = tbInfo.RawName;
            }
        }
        
        /// <summary>
        /// 保存说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSaveSchema_Click(object sender, EventArgs e)
        {
            this.DetailPage.Save();
        }

        /// <summary>
        /// 重新加载元数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnReloadSchema_Click(object sender, EventArgs e)
        {

        }
    }
}

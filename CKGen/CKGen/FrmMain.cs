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
using DotNet.DBSchema;

namespace CKGen
{
    public partial class FrmMain : Form
    {
        private DatabaseInfo DB = null;

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

            DatabaseInfo dbi = SystemConfig.SrvInfo.GetDatabase(SystemConfig.DBName);
            this.DB = DatabaseSchemaSetting.Compute(dbi);

            TreeNode tbNode = new TreeNode("表");
            foreach (TableInfo item in this.DB.Tables)
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

            FrmCodeBuild frm = new FrmCodeBuild();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.FormBorderStyle = FormBorderStyle.None;
            TabPage tab = new TabPage(frm.Text);
            tab.Controls.Add(frm);
            this.tabControl1.TabPages.Add(tab);
            frm.Show();
        }

        private void tvSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lblTableName.Text = "";
            txtTableDesc.Text = "";
            SystemConfig.Instance.CurrentTableName = "";
            dgvSchema.Rows.Clear();
            if (e.Node.Tag is TableInfo)
            {
                TableInfo tbInfo = e.Node.Tag as TableInfo;
                lblTableName.Text = tbInfo.RawName;
                txtTableDesc.Text = tbInfo.Description;

                SystemConfig.Instance.CurrentTableName = tbInfo.RawName;
                foreach (var item in tbInfo.Columns)
                {
                    string[] row = new string[] {
                        item.RawName,
                        item.DBType,
                        item.Nullable ? "Yes" : "No",
                        item.Description,
                        item.Attributes.ContainsKey("local_desc") ? item.Attributes["local_desc"] : "",
                        item.Attributes.ContainsKey("new_desc") ? item.Attributes["new_desc"] : ""
                    };

                    dgvSchema.Rows.Add(row);
                }
            }
        }
        
        /// <summary>
        /// 保存说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSaveSchema_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in this._EditNodes)
            {
                TableInfo tbInfo = node.Tag as TableInfo;
                tvSchema.SelectedNode.Text = tbInfo.RawName;
                tvSchema.SelectedNode.ForeColor = Color.Black;
            }
            this._EditNodes.Clear();

            foreach (DataGridViewRow row in dgvSchema.Rows)
            {
                string desc = row.Cells[row.Cells.Count - 2].Value as string;
                if (!string.IsNullOrEmpty(desc))
                {
                    row.Cells[row.Cells.Count - 2].Value = "";
                    row.Cells[row.Cells.Count - 3].Value = desc;
                    row.Cells[row.Cells.Count - 4].Value = desc;
                }
            }

            DatabaseSchemaSetting.Save(this.DB);

            MessageBox.Show("保存成功。");
        }

        private List<TreeNode> _EditNodes = new List<TreeNode>();

        private void dgvSchema_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string rawName = dgvSchema.Rows[e.RowIndex].Cells[0].Value.ToString();
            string value = dgvSchema.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (tvSchema.SelectedNode != null && tvSchema.SelectedNode.Tag is TableInfo)
            {
                this._EditNodes.Add(tvSchema.SelectedNode);
                TableInfo tbInfo = tvSchema.SelectedNode.Tag as TableInfo;

                var col = (from c in tbInfo.Columns
                           where c.RawName == rawName
                           select c).FirstOrDefault();
                if (col != null)
                {
                    col.Attributes["new_desc"] = value;
                }
                tvSchema.SelectedNode.Text = tbInfo.RawName + "(*)";
                tvSchema.SelectedNode.ForeColor = Color.Red;
            }
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

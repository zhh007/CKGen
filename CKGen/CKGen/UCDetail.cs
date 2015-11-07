using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;

namespace CKGen
{
    public partial class UCDetail : UserControl
    {
        public UCDetail()
        {
            InitializeComponent();
        }

        private void UCSchema_Load(object sender, EventArgs e)
        {
            SystemConfig.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            lblTableName.Text = "";
            txtTableDesc.Text = "";
            dgvSchema.Rows.Clear();

            if(SystemConfig.Instance.SelectedNode != null && SystemConfig.Instance.SelectedNode.Tag is ITableInfo)
            {
                ITableInfo tbInfo = SystemConfig.Instance.SelectedNode.Tag as ITableInfo;
                lblTableName.Text = tbInfo.RawName;
                txtTableDesc.Text = tbInfo.Description;

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

        private List<TreeNode> _EditNodes = new List<TreeNode>();

        private void dgvSchema_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string rawName = dgvSchema.Rows[e.RowIndex].Cells[0].Value.ToString();
            string value = "";
            if(dgvSchema.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                value = dgvSchema.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            if (SystemConfig.Instance.SelectedNode != null && SystemConfig.Instance.SelectedNode.Tag is ITableInfo)
            {
                this._EditNodes.Add(SystemConfig.Instance.SelectedNode);
                ITableInfo tbInfo = SystemConfig.Instance.SelectedNode.Tag as ITableInfo;

                var col = (from c in tbInfo.Columns
                           where c.RawName == rawName
                           select c).FirstOrDefault();
                if (col != null)
                {
                    col.Attributes["new_desc"] = value;
                }

                var colHasNewDesc = (from p in tbInfo.Columns
                                     where !string.IsNullOrEmpty(p.Attributes["new_desc"])
                                     select p).Count() > 0;
                if (colHasNewDesc)
                {
                    SystemConfig.Instance.SelectedNode.Text = tbInfo.RawName + "(*)";
                    SystemConfig.Instance.SelectedNode.ForeColor = Color.Red;
                }
                else
                {
                    SystemConfig.Instance.SelectedNode.Text = tbInfo.RawName;
                    SystemConfig.Instance.SelectedNode.ForeColor = Color.Black;
                }
            }
        }

        public void Save()
        {
            foreach (TreeNode node in this._EditNodes)
            {
                ITableInfo tbInfo = node.Tag as ITableInfo;
                node.Text = tbInfo.RawName;
                node.ForeColor = Color.Black;
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

            DatabaseSchemaSetting.Save(SystemConfig.Instance.Database);

            MessageBox.Show("保存成功。");
        }
    }
}

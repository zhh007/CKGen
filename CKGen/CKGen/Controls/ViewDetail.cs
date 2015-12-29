using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;

namespace CKGen.Controls
{
    public partial class ViewDetail : DetailBase
    {
        private TreeNode node = null;
        private IViewInfo view = null;

        public ViewDetail()
        {
            InitializeComponent();
        }

        public override void LoadDetail()
        {
            lblTableName.Text = "";
            //lblTableDBDesc.Text = "";//db_desc
            //lblTableLocalDesc.Text = "";//local_desc
            txtTableNewDesc.Text = "";//new_desc
            dgvSchema.Rows.Clear();

            this.node = App.Instance.SelectedNode;
            this.view = App.Instance.SelectedNode.Tag as IViewInfo;

            if (this.view != null)
            {
                lblTableName.Text = this.view.RawName;
                //lblTableDBDesc.Text = this.view.Description;//db_desc
                //lblTableLocalDesc.Text = this.view.Attributes.ContainsKey("local_desc") ? this.view.Attributes["local_desc"] : "";//local_desc
                //txtTableNewDesc.Text = this.view.Attributes.ContainsKey("new_desc") ? this.view.Attributes["new_desc"] : "";//new_desc
                txtTableNewDesc.Text = this.view["local_desc"] ?? "";//local_desc

                //foreach (var item in this.view.Columns)
                //{
                //    int index = dgvSchema.Rows.Add();
                //    DataGridViewRow row = dgvSchema.Rows[index];
                //    //row.Tag = item.IsPrimaryKey;
                //    row.Cells[0].Value = item.RawName;
                //    row.Cells[1].Value = SQLHelper.GetFullSqlType(item);
                //    row.Cells[2].Value = item.Nullable ? true : false;
                //    //row.Cells[3].Value = item.Description;
                //    //row.Cells[4].Value = item.Attributes.ContainsKey("local_desc") ? item.Attributes["local_desc"] : "";
                //    //row.Cells[5].Value = item.Attributes.ContainsKey("new_desc") ? item.Attributes["new_desc"] : "";
                //    row.Cells[3].Value = item["local_desc"] ?? "";
                //}

                List<DataGridViewRow> rlist = new List<DataGridViewRow>();
                foreach (var item in this.view.Columns)
                {
                    DataGridViewTextBoxCell col1 = new DataGridViewTextBoxCell();
                    col1.Value = item.RawName;

                    DataGridViewTextBoxCell col2 = new DataGridViewTextBoxCell();
                    col2.Value = SQLHelper.GetFullSqlType(item);

                    DataGridViewCheckBoxCell col3 = new DataGridViewCheckBoxCell();
                    col3.Value = item.Nullable ? true : false;

                    DataGridViewTextBoxCell col4 = new DataGridViewTextBoxCell();
                    col4.Value = item["local_desc"] ?? "";

                    DataGridViewTextBoxCell col5 = new DataGridViewTextBoxCell();

                    DataGridViewRow row = new DataGridViewRow();
                    row.Cells.AddRange(new DataGridViewCell[] { col1, col2, col3, col4, col5 });
                    rlist.Add(row);
                }

                dgvSchema.Rows.AddRange(rlist.ToArray());
            }
        }

        private void dgvSchema_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string rawName = dgvSchema.Rows[e.RowIndex].Cells[0].Value.ToString();
            string value = "";
            if (dgvSchema.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                value = dgvSchema.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            if (this.view != null)
            {
                var col = (from c in this.view.Columns
                           where c.RawName == rawName
                           select c).FirstOrDefault();
                if (col != null)
                {
                    string local_desc = col["local_desc"] ?? "";
                    if (local_desc != value)
                    {
                        col["new_desc"] = value;
                    }
                    else
                    {
                        col["new_desc"] = string.Empty;
                    }
                    UpdateNode();
                }
            }
        }

        private void txtTableNewDesc_TextChanged(object sender, EventArgs e)
        {
            if (this.view != null)
            {
                string local_desc = this.view["local_desc"] ?? "";
                if (local_desc != txtTableNewDesc.Text)
                {
                    this.view["new_desc"] = txtTableNewDesc.Text;
                }
                else
                {
                    this.view["new_desc"] = string.Empty;
                }
                UpdateNode();
            }
        }

        private void dgvSchema_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSchema.CurrentCell.ColumnIndex == 3)
            {
                dgvSchema.BeginEdit(true);
            }
        }

        private void UpdateNode()
        {
            var colHasNewDesc = (from p in this.view.Columns
                                 where !string.IsNullOrEmpty(p["new_desc"])
                                 select p).Count() > 0;
            if (colHasNewDesc || !string.IsNullOrEmpty(this.view["new_desc"]))
            {
                this.node.Text = this.view.RawName + "(*)";
                this.node.ForeColor = Color.Red;
            }
            else
            {
                this.node.Text = this.view.RawName;
                this.node.ForeColor = Color.Black;
            }
        }
    }
}

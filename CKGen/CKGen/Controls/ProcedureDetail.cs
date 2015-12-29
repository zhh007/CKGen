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
    public partial class ProcedureDetail : DetailBase
    {
        private TreeNode node = null;
        private IProcedureInfo proc = null;

        public ProcedureDetail()
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
            this.proc = App.Instance.SelectedNode.Tag as IProcedureInfo;

            if (this.proc != null)
            {
                lblTableName.Text = this.proc.Name;
                //lblTableDBDesc.Text = this.view.Description;//db_desc
                //lblTableLocalDesc.Text = this.view.Attributes.ContainsKey("local_desc") ? this.view.Attributes["local_desc"] : "";//local_desc
                //txtTableNewDesc.Text = this.view.Attributes.ContainsKey("new_desc") ? this.view.Attributes["new_desc"] : "";//new_desc
                txtTableNewDesc.Text = this.proc["local_desc"] ?? "";//local_desc

                //foreach (var item in this.proc.Parameters)
                //{
                //    int index = dgvSchema.Rows.Add();
                //    DataGridViewRow row = dgvSchema.Rows[index];
                //    //row.Tag = item.IsPrimaryKey;
                //    row.Cells[0].Value = item.Name;
                //    row.Cells[1].Value = item.DBTypeFull;
                //    row.Cells[2].Value = item.Nullable ? true : false;
                //    //row.Cells[3].Value = item.Description;
                //    //row.Cells[4].Value = item.Attributes.ContainsKey("local_desc") ? item.Attributes["local_desc"] : "";
                //    //row.Cells[5].Value = item.Attributes.ContainsKey("new_desc") ? item.Attributes["new_desc"] : "";
                //    row.Cells[3].Value = item["local_desc"] ?? "";
                //}

                List<DataGridViewRow> rlist = new List<DataGridViewRow>();
                foreach (var item in this.proc.Parameters)
                {
                    DataGridViewTextBoxCell col1 = new DataGridViewTextBoxCell();
                    col1.Value = item.Name;

                    DataGridViewTextBoxCell col2 = new DataGridViewTextBoxCell();
                    col2.Value = item.DBTypeFull;

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

            if (this.proc != null)
            {
                var col = (from c in this.proc.Parameters
                           where c.Name == rawName
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
            if (this.proc != null)
            {
                string local_desc = this.proc["local_desc"] ?? "";
                if (local_desc != txtTableNewDesc.Text)
                {
                    this.proc["new_desc"] = txtTableNewDesc.Text;
                }
                else
                {
                    this.proc["new_desc"] = string.Empty;
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
            var colHasNewDesc = (from p in this.proc.Parameters
                                 where !string.IsNullOrEmpty(p["new_desc"])
                                 select p).Count() > 0;
            if (colHasNewDesc || !string.IsNullOrEmpty(this.proc["new_desc"]))
            {
                this.node.Text = this.proc.Name + "(*)";
                this.node.ForeColor = Color.Red;
            }
            else
            {
                this.node.Text = this.proc.Name;
                this.node.ForeColor = Color.Black;
            }
        }
    }
}

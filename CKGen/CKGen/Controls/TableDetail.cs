using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;
using CKGen.Events;
using CKGen.Base.Events;

namespace CKGen.Controls
{
    public partial class TableDetail : DetailBase
    {
        private TreeNode node = null;
        private ITableInfo table = null;
        public TableDetail()
        {
            InitializeComponent();
            AppEvent.Subscribe<SaveDescToDbEvent>(p => SaveDescToDb());
        }

        private void SaveDescToDb()
        {
            //this.lblTableLocalDesc.Text = this.txtTableNewDesc.Text;
            //this.lblTableDBDesc.Text = this.txtTableNewDesc.Text;
            //this.txtTableNewDesc.Text = "";

            //foreach (DataGridViewRow row in dgvSchema.Rows)
            //{
            //    string desc = row.Cells[row.Cells.Count - 2].Value as string;
            //    if (!string.IsNullOrEmpty(desc))
            //    {
            //        row.Cells[row.Cells.Count - 2].Value = "";//new_desc
            //        row.Cells[row.Cells.Count - 3].Value = desc;//local_desc
            //        row.Cells[row.Cells.Count - 4].Value = desc;//db_desc
            //    }
            //}
        }

        public override void LoadDetail()
        {
            lblTableName.Text = "";
            //lblTableDBDesc.Text = "";//db_desc
            //lblTableLocalDesc.Text = "";//local_desc
            txtTableNewDesc.Text = "";//new_desc
            dgvSchema.Rows.Clear();

            this.node = App.Instance.SelectedNode;
            this.table = App.Instance.SelectedNode.Tag as ITableInfo;

            if (this.table != null)
            {
                lblTableName.Text = this.table.RawName;
                //lblTableDBDesc.Text = tbInfo.Description;//db_desc
                //lblTableLocalDesc.Text = tbInfo.Attributes.ContainsKey("local_desc") ? tbInfo.Attributes["local_desc"] : "";//local_desc
                //txtTableNewDesc.Text = tbInfo.Attributes.ContainsKey("new_desc") ? tbInfo.Attributes["new_desc"] : "";//new_desc
                txtTableNewDesc.Text = this.table["local_desc"] ?? "";//local_desc

                //foreach (var item in this.table.Columns)
                //{
                //    int index = dgvSchema.Rows.Add();
                //    DataGridViewRow row = dgvSchema.Rows[index];
                //    row.Tag = item.IsPrimaryKey;
                //    row.Cells[0].Value = item.RawName;
                //    row.Cells[1].Value = SQLHelper.GetFullSqlType(item);
                //    row.Cells[2].Value = item.Nullable ? true : false;
                //    //row.Cells[3].Value = item.Description;
                //    //row.Cells[4].Value = item.Attributes.ContainsKey("local_desc") ? item.Attributes["local_desc"] : "";
                //    //row.Cells[5].Value = item.Attributes.ContainsKey("new_desc") ? item.Attributes["new_desc"] : "";
                //    row.Cells[3].Value = item["local_desc"] ?? "";
                //}

                List<DataGridViewRow> rlist = new List<DataGridViewRow>();
                foreach (var item in this.table.Columns)
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
                    row.Tag = item.IsPrimaryKey;
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

            if (this.table != null)
            {
                var col = (from c in this.table.Columns
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
            if (this.table != null)
            {
                string local_desc = this.table["local_desc"] ?? "";
                if (local_desc != txtTableNewDesc.Text)
                {
                    this.table["new_desc"] = txtTableNewDesc.Text;
                }
                else
                {
                    this.table["new_desc"] = string.Empty;
                }
                UpdateNode();
            }
        }

        /// <summary>
        /// 显示主键图标
        /// </summary>
        private void dgvSchema_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewRow row = dgvSchema.Rows[e.RowIndex];

            if (row.Tag != null && row.Tag is bool && (bool)row.Tag)
            {
                Image img = CKGen.Properties.Resources.PrimaryKeyHS;
                Bitmap myBitmap = new Bitmap(img);
                Icon myIcon = Icon.FromHandle(myBitmap.GetHicon());

                Graphics graphics = e.Graphics;

                int iconHeight = 16;
                int iconWidth = 16;

                int xPosition = e.RowBounds.X + (dgvSchema.RowHeadersWidth / 2);
                int yPosition = e.RowBounds.Y + ((row.Height - iconHeight) / 2);

                Rectangle rectangle = new Rectangle(xPosition, yPosition, iconWidth, iconHeight);
                graphics.DrawIcon(myIcon, rectangle);
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
            var colHasNewDesc = (from p in this.table.Columns
                                 where !string.IsNullOrEmpty(p["new_desc"])
                                 select p).Count() > 0;
            if (colHasNewDesc || !string.IsNullOrEmpty(this.table["new_desc"]))
            {
                this.node.Text = this.table.RawName + "(*)";
                this.node.ForeColor = Color.Red;
            }
            else
            {
                this.node.Text = this.table.RawName;
                this.node.ForeColor = Color.Black;
            }
        }

        private void txtTableNewDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }
    }
}

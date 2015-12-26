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

namespace CKGen.Controls
{
    public partial class TableDetail : DetailBase
    {
        public TableDetail()
        {
            InitializeComponent();
            App.Instance.Events.GetEvent<SaveDescToDbEvent>().Subscribe(p => SaveDescToDb());
        }

        private void SaveDescToDb()
        {
            this.lblTableLocalDesc.Text = this.txtTableNewDesc.Text;
            this.lblTableDBDesc.Text = this.txtTableNewDesc.Text;
            this.txtTableNewDesc.Text = "";

            foreach (DataGridViewRow row in dgvSchema.Rows)
            {
                string desc = row.Cells[row.Cells.Count - 2].Value as string;
                if (!string.IsNullOrEmpty(desc))
                {
                    row.Cells[row.Cells.Count - 2].Value = "";//new_desc
                    row.Cells[row.Cells.Count - 3].Value = desc;//local_desc
                    row.Cells[row.Cells.Count - 4].Value = desc;//db_desc
                }
            }
        }

        public override void LoadDetail()
        {
            lblTableName.Text = "";
            lblTableDBDesc.Text = "";//db_desc
            lblTableLocalDesc.Text = "";//local_desc
            txtTableNewDesc.Text = "";//new_desc
            dgvSchema.Rows.Clear();

            if (App.Instance.SelectedNode != null && App.Instance.SelectedNode.Tag is ITableInfo)
            {
                ITableInfo tbInfo = App.Instance.SelectedNode.Tag as ITableInfo;
                lblTableName.Text = tbInfo.RawName;
                lblTableDBDesc.Text = tbInfo.Description;//db_desc
                lblTableLocalDesc.Text = tbInfo.Attributes.ContainsKey("local_desc") ? tbInfo.Attributes["local_desc"] : "";//local_desc
                txtTableNewDesc.Text = tbInfo.Attributes.ContainsKey("new_desc") ? tbInfo.Attributes["new_desc"] : "";//new_desc

                foreach (var item in tbInfo.Columns)
                {
                    int index = dgvSchema.Rows.Add();
                    DataGridViewRow row = dgvSchema.Rows[index];
                    row.Tag = item.IsPrimaryKey;
                    row.Cells[0].Value = item.RawName;
                    row.Cells[1].Value = SQLHelper.GetFullSqlType(item);
                    row.Cells[2].Value = item.Nullable ? true : false;
                    row.Cells[3].Value = item.Description;
                    row.Cells[4].Value = item.Attributes.ContainsKey("local_desc") ? item.Attributes["local_desc"] : "";
                    row.Cells[5].Value = item.Attributes.ContainsKey("new_desc") ? item.Attributes["new_desc"] : "";
                }
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

            if (App.Instance.SelectedNode != null && App.Instance.SelectedNode.Tag is ITableInfo)
            {
                ITableInfo tbInfo = App.Instance.SelectedNode.Tag as ITableInfo;

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
                if (colHasNewDesc || !string.IsNullOrEmpty(tbInfo.Attributes["new_desc"]))
                {
                    App.Instance.SelectedNode.Text = tbInfo.RawName + "(*)";
                    App.Instance.SelectedNode.ForeColor = Color.Red;
                }
                else
                {
                    App.Instance.SelectedNode.Text = tbInfo.RawName;
                    App.Instance.SelectedNode.ForeColor = Color.Black;
                }
            }
        }

        private void txtTableNewDesc_TextChanged(object sender, EventArgs e)
        {
            if (App.Instance.SelectedNode != null && App.Instance.SelectedNode.Tag is ITableInfo)
            {
                ITableInfo tbInfo = App.Instance.SelectedNode.Tag as ITableInfo;
                if (tbInfo != null)
                {
                    tbInfo.Attributes["new_desc"] = txtTableNewDesc.Text;

                    if (!string.IsNullOrEmpty(txtTableNewDesc.Text))
                    {
                        App.Instance.SelectedNode.Text = tbInfo.RawName + "(*)";
                        App.Instance.SelectedNode.ForeColor = Color.Red;
                    }
                    else
                    {
                        App.Instance.SelectedNode.Text = tbInfo.RawName;
                        App.Instance.SelectedNode.ForeColor = Color.Black;
                    }
                }
            }
        }

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
            if (dgvSchema.CurrentCell.ColumnIndex == 5)
            {
                dgvSchema.BeginEdit(true);
            }
        }
    }
}

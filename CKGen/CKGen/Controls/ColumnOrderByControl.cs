using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.DBSchema;
using CKGen.Base.CodeModel;

namespace CKGen.Controls
{
    public partial class ColumnOrderByControl : UserControl
    {
        public ColumnOrderByControl()
        {
            InitializeComponent();
        }

        public void SetOrderByItems(List<IColumnInfo> allColumns, List<OrderByItem> orderbyItems)
        {
            List<DataGridViewRow> rlist = new List<DataGridViewRow>();
            dgvColumns.Rows.Clear();
            foreach (var item in allColumns)
            {
                OrderByItem orderbyitem = null;
                if (orderbyItems != null)
                {
                    orderbyItems.Where(p => p.ColumnName == item.RawName).FirstOrDefault();
                }
                int rowIndex = dgvColumns.Rows.Add();
                DataGridViewRow row = dgvColumns.Rows[rowIndex];
                if (orderbyitem != null)
                {
                    row.Cells[0].Value = true;
                }
                else
                {
                    row.Cells[0].Value = false;
                }
                row.Cells[1].Value = item.RawName;
                if(orderbyitem != null)
                {
                    row.Cells[2].Value = orderbyitem.Direction == OrderDirection.Asc ? "ASC" : "DESC";
                }
                else
                {
                    row.Cells[2].Value = "ASC";
                }
                row.Cells[3].Value = item.DBType;
                row.Cells[4].Value = item.Nullable ? true : false;
                row.Cells[5].Value = item.Description;
                row.Tag = item;
            }
            dgvColumns.Rows.AddRange(rlist.ToArray());
        }

        public List<OrderByItem> GetOrderByItems()
        {
            List<OrderByItem> result = new List<OrderByItem>();

            foreach (DataGridViewRow item in dgvColumns.Rows)
            {
                DataGridViewCheckBoxCell col = item.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(col.Value))
                {
                    var colInfo = item.Tag as IColumnInfo;
                    OrderByItem obitem = new OrderByItem();
                    obitem.ColumnName = colInfo.RawName;
                    obitem.Direction = Convert.ToString(item.Cells[2].Value) == "ASC" ? OrderDirection.Asc : OrderDirection.Desc;
                    result.Add(obitem);
                }
            }

            return result;
        }
    }
}

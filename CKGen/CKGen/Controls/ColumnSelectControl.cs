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
    public partial class ColumnSelectControl : UserControl
    {
        public ColumnSelectControl()
        {
            InitializeComponent();
        }

        public void SetColumns(List<IColumnInfo> allColumns, List<IColumnInfo> selectedColumns)
        {
            List<DataGridViewRow> rlist = new List<DataGridViewRow>();
            dgvColumns.Rows.Clear();
            foreach (var item in allColumns)
            {
                int rowIndex = dgvColumns.Rows.Add();
                DataGridViewRow row = dgvColumns.Rows[rowIndex];
                if (selectedColumns != null && selectedColumns.Contains(item))
                {
                    row.Cells[0].Value = true;
                }
                else
                {
                    row.Cells[0].Value = false;
                }
                row.Cells[1].Value = item.RawName;
                row.Cells[2].Value = item.DBType;
                row.Cells[3].Value = item.Nullable ? true : false;
                row.Cells[4].Value = item.Description;
                row.Tag = item;
            }
            dgvColumns.Rows.AddRange(rlist.ToArray());
        }

        public List<IColumnInfo> GetSelectedColumns()
        {
            List<IColumnInfo> result = new List<IColumnInfo>();

            foreach (DataGridViewRow item in dgvColumns.Rows)
            {
                DataGridViewCheckBoxCell col = item.Cells[0] as DataGridViewCheckBoxCell;
                if(Convert.ToBoolean(col.Value))
                {
                    result.Add(item.Tag as IColumnInfo);
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.CodeModel
{
    public class OrderByItem
    {
        public string ColumnName { get; set; }
        public OrderDirection Direction { get; set; }

        public OrderByItem()
        {
            Direction = OrderDirection.Asc;
        }
    }

    public enum OrderDirection
    {
        Asc = 0,
        Desc = 1
    }
}

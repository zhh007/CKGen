using CKGen.DBSchema;
using System;

namespace CKGen.Temp.DataAccess.CodeTempletes
{
    public partial class TableSchemaTemplete
    {
        public string NameSpace;
        public ITableInfo DBTable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="t"></param>
        public TableSchemaTemplete(string n, ITableInfo t)
        {
            if (string.IsNullOrEmpty(n))
                throw new Exception("参数n，命名空间不能为空。");

            if (t == null)
                throw new Exception("参数t，不能为null。");

            this.NameSpace = n;
            this.DBTable = t;
            //this.DBTable.Refresh();
        }
    }
}

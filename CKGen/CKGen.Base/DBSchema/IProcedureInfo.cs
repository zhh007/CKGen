using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.DBSchema
{
    public interface IProcedureInfo
    {
        string Name { get; set; }

        string Schema { get; set; }

        IDatabaseInfo Database { get; }

        /// <summary>
        /// 参数集合
        /// </summary>
        List<ISqlParameter> Parameters { get; set; }

        void LoadParameters();

        string this[string key] { get; set; }
    }
}

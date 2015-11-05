using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.DBSchema
{
    public interface IDatabaseInfo
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 数据库id
        /// </summary>
        int Dbid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime Createtime { get; set; }

        /// <summary>
        /// 表
        /// </summary>
        List<ITableInfo> Tables { get; }

        /// <summary>
        /// 视图
        /// </summary>
        List<IViewInfo> Views { get; }

        /// <summary>
        /// 存储过程
        /// </summary>
        List<IProcedureInfo> Procedures { get; }

        /// <summary>
        /// 服务器
        /// </summary>
        IServerInfo Server { get; }
    }
}

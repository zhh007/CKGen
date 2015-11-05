using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.DBSchema
{
    public interface IServerInfo
    {
        /// <summary>
        /// 服务器名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string Connection { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        List<IDatabaseInfo> Databases { get; }
    }
}

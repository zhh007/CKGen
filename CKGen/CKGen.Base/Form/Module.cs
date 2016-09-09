using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Form
{
    /// <summary>
    /// 模块实体
    /// </summary>
    public class Module
    {
        /// <summary>
        /// 标题/说明（如：商品名称）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 原始名（如：good name）
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 英文名/代码变量名（如：GoodName）
        /// </summary>
        public string CodeName { get; set; }
        /// <summary>
        /// 字段集合
        /// </summary>
        public List<ModuleField> Fields { get; private set; }

        public Module(ITableInfo tbInfo)
        {
            this.CodeName = tbInfo.PascalName;

            var fields = new List<ModuleField>();
            foreach (var col in tbInfo.Columns)
            {
                ModuleField field = new ModuleField(this, col.Description, col.RawName, col.PascalName);
                field.LanguageType = col.LanguageType;
                field.Nullable = col.Nullable;
                if(col.MaxLength.HasValue && col.MaxLength > 0)
                {
                    field.StringLength = col.MaxLength.Value;
                }
                fields.Add(field);
            }
            this.Fields = fields;
        }

        public Module()
        {
            this.Fields = new List<ModuleField>();
        }
    }
}

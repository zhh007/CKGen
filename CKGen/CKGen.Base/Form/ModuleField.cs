using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Form
{
    public class ModuleField
    {
        private string _codename = "";
        public Guid Id { get; private set; }
        public Module Parent { get; private set; }
        /// <summary>
        /// 标题/说明（如：商品名称）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 原始名（如：good name）
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 英文名/代码变量名（如：GoodName）
        /// </summary>
        public string CodeName
        {
            get
            {
                return _codename;
            }
            set
            {
                _codename = StringHelper.SetPascalCase(value);
            }
        }
        /// <summary>
        /// C#类型，如DateTime,string等
        /// </summary>
        public string LanguageType { get; set; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool Nullable { get; set; }
        public Type DataType { get; set; }
        public bool IsKey { get; set; }
        public bool IsRequired { get; set; }

        public ModuleField(Module parent, string title, string name, string codename)
        {
            this.Id = Guid.NewGuid();
            this.Parent = parent;
            this.Title = title;
            this.FieldName = name;
            this.CodeName = codename;
        }
    }
}

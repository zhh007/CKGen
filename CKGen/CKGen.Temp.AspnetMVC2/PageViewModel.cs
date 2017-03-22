using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.AspnetMVC2
{
    public class PageViewModel
    {
        public ITableInfo DBTable { get; set; }

        public string NameSpacePR { get; set; }

        public string DatabaseName { get; set; }

        public string DBContextTypeName { get; set; }

        public string TableName { get; set; }

        public string TableCamelName
        {
            get
            {
                return StringHelper.SetCamelCase(this.TableName);
            }
        }

        public string GetKeyParamterString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.DBTable.Keys.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }
                var item = this.DBTable.Keys[i];
                sb.AppendFormat("{0} {1}", item.LanguageType, item.PascalName);
            }
            return sb.ToString();
        }

        public string BuildPropertieStr(IColumnInfo column)
        {
            StringBuilder sb = new StringBuilder();
            bool hasvalue = false;
            sb.AppendFormat("this.Property(t => t.{0})", column.PascalName);

            if (column.Identity)
            {
                hasvalue = true;
                sb.Append("\r\n                ");
                sb.Append(".HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)");
            }

            if (column.Computed)
            {
                hasvalue = true;
                sb.Append("\r\n                ");
                sb.Append(".HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)");
            }

            if (column.IsPrimaryKey)
            {
                if (!column.Identity)
                {
                    hasvalue = true;
                    sb.Append("\r\n                ");
                    sb.Append(".HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)");
                }
            }

            //if (!column.HasStringLength && column.Nullable)
            //    return string.Empty;

            if (column.HasStringLength)
            {
                hasvalue = true;
                sb.Append("\r\n                ");
                sb.AppendFormat(".HasMaxLength({0})", column.MaxLength);
            }

            if (!column.Nullable && !column.IsPrimaryKey && !column.Identity && !column.Computed)
            {
                hasvalue = true;
                sb.Append("\r\n                ");
                sb.Append(".IsRequired()");
            }

            if(!hasvalue)
            {
                return string.Empty;
            }

            sb.Append(";");
            return sb.ToString();
        }

        public string GetUpdateFilterString(string modelName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.DBTable.Keys.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" && ");
                }
                var item = this.DBTable.Keys[i];
                sb.AppendFormat("p.{0} == {1}.{0}", item.PascalName, modelName);
            }
            return sb.ToString();
        }

        //public string GetKeyParamterString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < this.DBTable.Keys.Count; i++)
        //    {
        //        if (i > 0)
        //        {
        //            sb.Append(", ");
        //        }
        //        var item = this.DBTable.Keys[i];
        //        sb.AppendFormat("{0} {1}", item.LanguageType, item.PascalName);
        //    }
        //    return sb.ToString();
        //}

        //public string GetUpdateFilterString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < this.DBTable.Keys.Count; i++)
        //    {
        //        if (i > 0)
        //        {
        //            sb.Append(" && ");
        //        }
        //        var item = this.DBTable.Keys[i];
        //        sb.AppendFormat("p.{0} == dto.{0}", item.PascalName);
        //    }
        //    return sb.ToString();
        //}

        public string GetDeleteFilterString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.DBTable.Keys.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" && ");
                }
                var item = this.DBTable.Keys[i];
                sb.AppendFormat("p.{0} == {0}", item.PascalName);
            }
            return sb.ToString();
        }

        public string GetCanUpdateFilterString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.DBTable.Keys.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" && ");
                }
                var item = this.DBTable.Keys[i];
                sb.AppendFormat("p.{0} != dto.{0}", item.PascalName);
            }
            return sb.ToString();
        }

        public string GetSearchTextFilterString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.DBTable.Columns.Count; i++)
            {
                var item = this.DBTable.Columns[i];
                if (item.LanguageType == "string")
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" || ");
                    }

                    sb.AppendFormat("p.{0}.Contains(text)", item.PascalName);
                }
            }
            return sb.ToString();
        }

        public string GetOrderByString()
        {
            if (this.DBTable.Keys != null && this.DBTable.Keys.Count == 1)
            {
                var item = this.DBTable.Keys[0];
                if (item.Identity)
                {
                    return string.Format("p.{0}", item.PascalName);
                }
            }

            List<IColumnInfo> list = new List<IColumnInfo>();
            IColumnInfo createTimeCI = null;
            IColumnInfo updateTimeCI = null;
            for (int i = 0; i < this.DBTable.Columns.Count; i++)
            {
                var item = this.DBTable.Columns[i];
                if (item.LanguageType == "DateTime")
                {
                    if (item.LowerName.Contains("create") || item.LowerName.Contains("add"))
                    {
                        createTimeCI = item;
                    }
                    if (item.LowerName.Contains("update") || item.LowerName.Contains("modified")
                        || item.LowerName.Contains("modify"))
                    {
                        updateTimeCI = item;
                    }
                    list.Add(item);
                }
            }

            if (updateTimeCI != null)
            {
                return string.Format("p.{0}", updateTimeCI.PascalName);
            }
            if (createTimeCI != null)
            {
                return string.Format("p.{0}", createTimeCI.PascalName);
            }

            if (list.Count > 0)
            {
                return string.Format("p.{0}", list[0].PascalName);
            }
            return "";
        }
    }
}

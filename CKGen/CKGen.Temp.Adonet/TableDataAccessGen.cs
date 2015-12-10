﻿using CKGen.Base;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CKGen.Temp.Adonet
{
    public class TableDataAccessGen
    {
        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();
        public string GenDataAccessCode(string nameSpace, ITableInfo tbInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Data.SqlClient;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine();
            sb.AppendLine("namespace " + nameSpace);
            sb.AppendLine("{");
            sb.AppendFormat("    public class {0}Access{1}", tbInfo.PascalName, Environment.NewLine);
            sb.AppendLine("    {");

            Gen(sb, "Insert.cshtml", tbInfo);
            Gen(sb, "Update.cshtml", tbInfo);
            Gen(sb, "Delete.cshtml", tbInfo);
            //Gen(sb, "Save.cshtml", tbInfo);
            //Gen(sb, "Exist.cshtml", tbInfo);
            Gen(sb, "Get.cshtml", tbInfo);
            //Gen(sb, "Top.cshtml", tbInfo);
            Gen(sb, "GetAll.cshtml", tbInfo);
            Gen(sb, "Paged.cshtml", tbInfo);

            if ((from col in tbInfo.Columns
                 where col.DbTargetType == "SqlDbType.Image" || col.DbTargetType == "SqlDbType.Binary"
                 || col.DbTargetType == "SqlDbType.VarBinary" || col.DbTargetType == "SqlDbType.Timestamp"
                 select col).Count() > 0)
            {
                Gen(sb, "_GetBytes.cshtml", tbInfo);
            }

            Gen(sb, "_GetMany.cshtml", tbInfo);

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private void Gen(StringBuilder sb, string viewName, ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\" + viewName);
            //string code = codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete(viewName);
            string code = codeGen.Gen(tmp, tbInfo);

            bool sqlLine = false;
            bool sqlBegin = false;
            using (StringReader sr = new StringReader(code))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.IndexOf(" sql = ") > -1 && sqlBegin == false)
                    {
                        sqlBegin = true;
                        sqlLine = true;
                        sb.Append("        ");
                    }

                    if (!string.IsNullOrEmpty(line) && !sqlLine)
                    {
                        sb.Append("        ");
                    }
                    sb.Append(line);
                    sb.Append(Environment.NewLine);

                    if (sqlBegin && line.EndsWith("\";"))
                    {
                        sqlBegin = false;
                        sqlLine = false;
                    }
                }//while
            }//using

            sb.AppendLine();
        }

        public string GenModelCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\Model.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("Model.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }

        public string GenInsertCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\Insert.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("Insert.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }

        public string GenUpdateCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\Update.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("Update.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }

        public string GenDeleteCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\Delete.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("Delete.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }

        public string GenSaveCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\Save.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("Save.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }

        public string GenExistCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\Exist.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("Exist.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }

        public string GenGetCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\Get.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("Get.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }

        public string GenGetAllCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\GetAll.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("GetAll.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }

        public string GenTopCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\Top.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("Top.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }

        public string GenPagedCode(ITableInfo tbInfo)
        {
            //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\Paged.cshtml");
            //return codeGen.GenByPath(viewPath, tbInfo);
            string tmp = Comm.GetTemplete("Paged.cshtml");
            return codeGen.Gen(tmp, tbInfo);
        }
    }
}

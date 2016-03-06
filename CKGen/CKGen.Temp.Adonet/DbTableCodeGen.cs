using CKGen.Base;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CKGen.Temp.Adonet
{
    public class DbTableCodeGen
    {
        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();
        
        private readonly string Temp_Table_Model = Comm.GetTemplete("Table.Model.cshtml");
        private readonly string Temp_Table_Insert = Comm.GetTemplete("Table.Insert.cshtml");
        private readonly string Temp_Table_Update = Comm.GetTemplete("Table.Update.cshtml");
        private readonly string Temp_Table_Delete = Comm.GetTemplete("Table.Delete.cshtml");
        private readonly string Temp_Table_Save = Comm.GetTemplete("Table.Save.cshtml");
        private readonly string Temp_Table_Exist = Comm.GetTemplete("Table.Exist.cshtml");
        private readonly string Temp_Table_Get = Comm.GetTemplete("Table.Get.cshtml");
        private readonly string Temp_Table_GetAll = Comm.GetTemplete("Table.GetAll.cshtml");
        private readonly string Temp_Table_Top = Comm.GetTemplete("Table.Top.cshtml");
        private readonly string Temp_Table_Paged = Comm.GetTemplete("Table.Paged.cshtml");
        private readonly string Temp_Table_GetBytes = Comm.GetTemplete("_GetBytes.cshtml");
        private readonly string Temp_Table_GetMany = Comm.GetTemplete("Table._GetMany.cshtml");

        public string GenDataAccessCode(string nameSpace, ITableInfo tbInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Data.SqlClient;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendFormat("using {0};{1}", nameSpace, Environment.NewLine);
            sb.AppendFormat("using {0}.Model;{1}", nameSpace, Environment.NewLine);
            sb.AppendLine();
            sb.AppendFormat("namespace {0}.DAL{1}", nameSpace, Environment.NewLine);
            sb.AppendLine("{");
            sb.AppendFormat("    public class {0}Access{1}", tbInfo.PascalName, Environment.NewLine);
            sb.AppendLine("    {");

            Gen(sb, this.Temp_Table_Insert, tbInfo);
            Gen(sb, this.Temp_Table_Update, tbInfo);
            Gen(sb, this.Temp_Table_Delete, tbInfo);
            //Gen(sb, this.Temp_Table_Save, tbInfo);
            //Gen(sb, this.Temp_Table_Exist, tbInfo);
            Gen(sb, this.Temp_Table_Get, tbInfo);
            //Gen(sb, this.Temp_Table_Top, tbInfo);
            Gen(sb, this.Temp_Table_GetAll, tbInfo);
            Gen(sb, this.Temp_Table_Paged, tbInfo);

            if ((from col in tbInfo.Columns
                 where col.DbTargetType == "SqlDbType.Image" || col.DbTargetType == "SqlDbType.Binary"
                 || col.DbTargetType == "SqlDbType.VarBinary" || col.DbTargetType == "SqlDbType.Timestamp"
                 select col).Count() > 0)
            {
                Gen(sb, this.Temp_Table_GetBytes, tbInfo);
            }

            Gen(sb, this.Temp_Table_GetMany, tbInfo);

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private void Gen(StringBuilder sb, string tmp, ITableInfo tbInfo)
        {
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
            return codeGen.Gen(this.Temp_Table_Model, tbInfo);
        }

        public string GenInsertCode(ITableInfo tbInfo)
        {
            return codeGen.Gen(this.Temp_Table_Insert, tbInfo);
        }

        public string GenUpdateCode(ITableInfo tbInfo)
        {
            return codeGen.Gen(this.Temp_Table_Update, tbInfo);
        }

        public string GenDeleteCode(ITableInfo tbInfo)
        {
            return codeGen.Gen(this.Temp_Table_Delete, tbInfo);
        }

        public string GenSaveCode(ITableInfo tbInfo)
        {
            return codeGen.Gen(this.Temp_Table_Save, tbInfo);
        }

        public string GenExistCode(ITableInfo tbInfo)
        {
            return codeGen.Gen(this.Temp_Table_Exist, tbInfo);
        }

        public string GenGetCode(ITableInfo tbInfo)
        {
            return codeGen.Gen(this.Temp_Table_Get, tbInfo);
        }

        public string GenGetAllCode(ITableInfo tbInfo)
        {
            return codeGen.Gen(this.Temp_Table_GetAll, tbInfo);
        }

        public string GenTopCode(ITableInfo tbInfo)
        {
            return codeGen.Gen(this.Temp_Table_Top, tbInfo);
        }

        public string GenPagedCode(ITableInfo tbInfo)
        {
            return codeGen.Gen(this.Temp_Table_Paged, tbInfo);
        }
    }
}

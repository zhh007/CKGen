using CKGen.Base;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CKGen.Temp.Adonet
{
    public class DbViewCodeGen
    {
        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();

        private readonly string Temp_View_Model = Comm.GetTemplete("View.Model.cshtml");
        private readonly string Temp_View_Exist = Comm.GetTemplete("View.Exist.cshtml");
        private readonly string Temp_View_Get = Comm.GetTemplete("View.Get.cshtml");
        private readonly string Temp_View_GetAll = Comm.GetTemplete("View.GetAll.cshtml");
        private readonly string Temp_View_Top = Comm.GetTemplete("View.Top.cshtml");
        private readonly string Temp_View_Paged = Comm.GetTemplete("View.Paged.cshtml");
        private readonly string Temp_View_GetBytes = Comm.GetTemplete("_GetBytes.cshtml");
        private readonly string Temp_View_GetMany = Comm.GetTemplete("View._GetMany.cshtml");

        public string GenDataAccessCode(string nameSpace, IViewInfo vwInfo)
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
            sb.AppendFormat("    public class {0}Access{1}", vwInfo.PascalName, Environment.NewLine);
            sb.AppendLine("    {");

            //Gen(sb, this.Temp_View_Exist, vwInfo);
            //Gen(sb, this.Temp_View_Get, vwInfo);
            //Gen(sb, this.Temp_View_Top, vwInfo);
            Gen(sb, this.Temp_View_GetAll, vwInfo);
            Gen(sb, this.Temp_View_Paged, vwInfo);

            if ((from col in vwInfo.Columns
                 where col.DbTargetType == "SqlDbType.Image" || col.DbTargetType == "SqlDbType.Binary"
                 || col.DbTargetType == "SqlDbType.VarBinary" || col.DbTargetType == "SqlDbType.Timestamp"
                 select col).Count() > 0)
            {
                Gen(sb, this.Temp_View_GetBytes, vwInfo);
            }

            Gen(sb, this.Temp_View_GetMany, vwInfo);

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private void Gen(StringBuilder sb, string tmp, IViewInfo vwInfo)
        {
            string code = codeGen.Gen(tmp, vwInfo);

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

        public string GenModelCode(IViewInfo vwInfo)
        {
            return codeGen.Gen(this.Temp_View_Model, vwInfo);
        }

        public string GenExistCode(IViewInfo vwInfo)
        {
            return codeGen.Gen(this.Temp_View_Exist, vwInfo);
        }

        public string GenGetCode(IViewInfo vwInfo)
        {
            return codeGen.Gen(this.Temp_View_Get, vwInfo);
        }

        public string GenGetAllCode(IViewInfo vwInfo)
        {
            return codeGen.Gen(this.Temp_View_GetAll, vwInfo);
        }

        public string GenTopCode(IViewInfo vwInfo)
        {
            return codeGen.Gen(this.Temp_View_Top, vwInfo);
        }

        public string GenPagedCode(IViewInfo vwInfo)
        {
            return codeGen.Gen(this.Temp_View_Paged, vwInfo);
        }
    }
}

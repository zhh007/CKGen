using CKGen.Base;
using CKGen.Base.Form;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CKGen.Temp.Adonet
{
    public class DbQueryGetListModel
    {
        public string SQL { get; set; }
        public string ConnectionString { get; set; }
        public Module Module { get; set; }

        public DbQueryGetListModel()
        {
            this.Module = new Module();
        }
    }

    public class DbQueryCodeGen
    {
        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();

        private readonly string Temp_Query_Model = Comm.GetTemplete("Query.Model.cshtml");
        private readonly string Temp_Query_GetList = Comm.GetTemplete("Query.GetList.cshtml");

        public string Gen(string query, DataSet ds, string connstr)
        {
            DbQueryGetListModel gModel = new DbQueryGetListModel();
            gModel.SQL = query;
            gModel.ConnectionString = connstr;
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable dt = ds.Tables[i];
                    Module module = new Module();
                    module.ModuleName = string.Format("Result{0}", i);
                    module.CodeName = string.Format("Result{0}", i);
                    foreach (DataColumn dc in dt.Columns)
                    {
                        ModuleField mf = new ModuleField(module, "", dc.ColumnName, dc.ColumnName);
                        mf.Nullable = dc.AllowDBNull;
                        mf.LanguageType = LanguageConvert.GetCSharpType(dc.DataType, dc.AllowDBNull);
                        //Debug.WriteLine("{0} ---> {1}", mf.FieldName, mf.LanguageType);

                        module.Fields.Add(mf);
                    }

                    gModel.Module = module;
                }
            }

            string modelCode = GenModelCode(gModel.Module);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(modelCode);

            string queryCode = codeGen.Gen(this.Temp_Query_GetList, gModel);
            queryCode = queryCode.Replace("'conn_name'", "Program.TestConnection");
            sb.AppendLine(queryCode);

            return sb.ToString();
        }

        private string GenModelCode(Module module)
        {
            return codeGen.Gen(this.Temp_Query_Model, module);
        }
    }
}

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

    public class DbQueryExecuteScalarModel
    {
        public string SQL { get; set; }
        public string ConnectionString { get; set; }
        public string TypeString { get; set; }
        public string ReturnString { get; set; }
    }

    public class DbQueryForMultiListModel
    {
        public string SQL { get; set; }
        public string ConnectionString { get; set; }
        public List<Module> Modules { get; set; }

        public DbQueryForMultiListModel()
        {
            this.Modules = new List<Module>();
        }
    }

    public class DbQueryCodeGen
    {
        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();

        private readonly string Temp_Query_Model = Comm.GetTemplete("Query.Model.cshtml");
        private readonly string Temp_Query_GetList = Comm.GetTemplete("Query.GetList.cshtml");
        private readonly string Temp_Query_GetOne = Comm.GetTemplete("Query.GetOne.cshtml");
        private readonly string Temp_Query_ExecuteNonQuery = Comm.GetTemplete("Query.ExecuteNonQuery.cshtml");
        private readonly string Temp_Query_ExecuteScalar = Comm.GetTemplete("Query.ExecuteScalar.cshtml");
        private readonly string Temp_Query_GetMultiList = Comm.GetTemplete("Query.GetMultiList.cshtml");

        public string GenForQueryList(string query, Module module, string connstr)
        {
            DbQueryGetListModel gModel = new DbQueryGetListModel();
            gModel.SQL = query;
            gModel.ConnectionString = connstr;
            gModel.Module = module;
            //if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables.Count; i++)
            //    {
            //        DataTable dt = ds.Tables[i];
            //        Module module = new Module();
            //        module.ModuleName = string.Format("Result{0}", i);
            //        module.CodeName = string.Format("Result{0}", i);
            //        foreach (DataColumn dc in dt.Columns)
            //        {
            //            ModuleField mf = new ModuleField(module, "", dc.ColumnName, dc.ColumnName);
            //            mf.DataType = dc.DataType;
            //            mf.Nullable = dc.AllowDBNull;
            //            mf.LanguageType = LanguageConvert.GetCSharpType(dc.DataType, dc.AllowDBNull);
            //            //Debug.WriteLine("{0} ---> {1}", mf.FieldName, mf.LanguageType);
            //            module.Fields.Add(mf);
            //        }

            //        gModel.Module = module;
            //    }
            //}

            string modelCode = GenModelCode(gModel.Module);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(modelCode);

            string queryCode = codeGen.Gen(this.Temp_Query_GetList, gModel);
            queryCode = queryCode.Replace("'conn_name'", "Program.TestConnection");
            sb.AppendLine(queryCode);

            return sb.ToString();
        }

        public string GenForQueryOne(string query, Module module, string connstr)
        {
            DbQueryGetListModel gModel = new DbQueryGetListModel();
            gModel.SQL = query;
            gModel.ConnectionString = connstr;
            gModel.Module = module;
            //if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables.Count; i++)
            //    {
            //        DataTable dt = ds.Tables[i];
            //        Module module = new Module();
            //        module.ModuleName = string.Format("Result{0}", i);
            //        module.CodeName = string.Format("Result{0}", i);
            //        foreach (DataColumn dc in dt.Columns)
            //        {
            //            ModuleField mf = new ModuleField(module, "", dc.ColumnName, dc.ColumnName);
            //            mf.DataType = dc.DataType;
            //            mf.Nullable = dc.AllowDBNull;
            //            mf.LanguageType = LanguageConvert.GetCSharpType(dc.DataType, dc.AllowDBNull);
            //            //Debug.WriteLine("{0} ---> {1}", mf.FieldName, mf.LanguageType);
            //            module.Fields.Add(mf);
            //        }

            //        gModel.Module = module;
            //    }
            //}

            string modelCode = GenModelCode(gModel.Module);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(modelCode);

            string queryCode = codeGen.Gen(this.Temp_Query_GetOne, gModel);
            queryCode = queryCode.Replace("'conn_name'", "Program.TestConnection");
            sb.AppendLine(queryCode);

            return sb.ToString();
        }

        public string GenForExecuteNoQuery(string query, string connstr)
        {
            DbQueryGetListModel gModel = new DbQueryGetListModel();
            gModel.SQL = query;
            gModel.ConnectionString = connstr;

            StringBuilder sb = new StringBuilder();

            string queryCode = codeGen.Gen(this.Temp_Query_ExecuteNonQuery, gModel);
            queryCode = queryCode.Replace("'conn_name'", "Program.TestConnection");
            sb.AppendLine(queryCode);

            return sb.ToString();
        }

        public string GenForExecuteScalar(string query, Type t, bool allowDBNull, string connstr)
        {
            //Type t = null;
            //bool allowDBNull = false;
            //if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            //{
            //    var dc = ds.Tables[0].Columns[0];
            //    t = dc.DataType;
            //    allowDBNull = dc.AllowDBNull;
            //}

            DbQueryExecuteScalarModel gModel = new DbQueryExecuteScalarModel();
            gModel.SQL = query;
            gModel.ConnectionString = connstr;
            gModel.TypeString = LanguageConvert.GetCSharpType(t, allowDBNull);
            if (allowDBNull || t.IsClass || gModel.TypeString.Contains("?") || gModel.TypeString.Contains("Nullable<"))
            {
                gModel.ReturnString = string.Format("cmd.ExecuteScalar() as {0};", gModel.TypeString);
            }
            else
            {
                gModel.ReturnString = string.Format("({0})cmd.ExecuteScalar();", gModel.TypeString);
            }

            StringBuilder sb = new StringBuilder();

            string queryCode = codeGen.Gen(this.Temp_Query_ExecuteScalar, gModel);
            queryCode = queryCode.Replace("'conn_name'", "Program.TestConnection");
            sb.AppendLine(queryCode);

            return sb.ToString();
        }

        private string GenModelCode(Module module)
        {
            return codeGen.Gen(this.Temp_Query_Model, module);
        }

        public string GenForMultiQuery(string query, List<Module> modules, string connstr)
        {
            DbQueryForMultiListModel gModel = new DbQueryForMultiListModel();
            gModel.SQL = query;
            gModel.ConnectionString = connstr;
            if (modules != null)
            {
                gModel.Modules = modules;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var module in gModel.Modules)
            {
                string modelCode = GenModelCode(module);
                sb.AppendLine(modelCode);
            }

            string queryCode = codeGen.Gen(this.Temp_Query_GetMultiList, gModel);
            queryCode = queryCode.Replace("'conn_name'", "Program.TestConnection");
            sb.AppendLine(queryCode);

            return sb.ToString();
        }
    }
}

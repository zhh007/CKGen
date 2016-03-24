using CKGen.Base;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.Adonet.Snippet
{
    public class DbSnippetGen
    {
        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();

        private readonly string Temp_Snippet_Count = Comm.GetTemplete("Snippet.Count.cshtml");
        private readonly string Temp_Snippet_Paged = Comm.GetTemplete("Snippet.Paged.cshtml");

        public string GenTableQueryCode(TableQueryGenModel model)
        {
            string code = "";
            switch (model.GenType)
            {
                case TableQueryGenType.Get:
                    break;
                case TableQueryGenType.GetList:
                    break;
                case TableQueryGenType.Paged:
                    code = codeGen.Gen(this.Temp_Snippet_Paged, model);
                    break;
                case TableQueryGenType.Top:
                    break;
                case TableQueryGenType.Exist:
                    break;
                case TableQueryGenType.Count:
                    code = codeGen.Gen(this.Temp_Snippet_Count, model);
                    break;
                default:
                    break;
            }

            return code;
        }

        //public string GenCountCode(TableQueryGenModel model)
        //{
        //    return codeGen.Gen(this.Temp_Snippet_Count, model);
        //}

        //public string GenPagedCode(TableQueryGenModel model)
        //{
        //    return codeGen.Gen(this.Temp_Snippet_Paged, model);
        //}
    }
}

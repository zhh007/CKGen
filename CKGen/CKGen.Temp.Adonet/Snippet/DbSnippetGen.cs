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

        private readonly string Temp_Snippet_Get = Comm.GetTemplete("Snippet.Get.cshtml");
        private readonly string Temp_Snippet_GetList = Comm.GetTemplete("Snippet.GetList.cshtml");
        private readonly string Temp_Snippet_Paged = Comm.GetTemplete("Snippet.Paged.cshtml");
        private readonly string Temp_Snippet_Top = Comm.GetTemplete("Snippet.Top.cshtml");
        private readonly string Temp_Snippet_Exist = Comm.GetTemplete("Snippet.Exist.cshtml");
        private readonly string Temp_Snippet_Count = Comm.GetTemplete("Snippet.Count.cshtml");

        public string GenTableQueryCode(TableQueryGenModel model)
        {
            string code = "";
            switch (model.GenType)
            {
                case TableQueryGenType.Get:
                    code = codeGen.Gen(this.Temp_Snippet_Get, model);
                    break;
                case TableQueryGenType.GetList:
                    code = codeGen.Gen(this.Temp_Snippet_GetList, model);
                    break;
                case TableQueryGenType.Paged:
                    code = codeGen.Gen(this.Temp_Snippet_Paged, model);
                    break;
                case TableQueryGenType.Top:
                    code = codeGen.Gen(this.Temp_Snippet_Top, model);
                    break;
                case TableQueryGenType.Exist:
                    code = codeGen.Gen(this.Temp_Snippet_Exist, model);
                    break;
                case TableQueryGenType.Count:
                    code = codeGen.Gen(this.Temp_Snippet_Count, model);
                    break;
                default:
                    break;
            }

            return code;
        }
    }
}

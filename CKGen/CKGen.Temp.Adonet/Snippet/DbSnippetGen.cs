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

        private readonly string Temp_TableSnippet_Get = Comm.GetTemplete("TableSnippet.Get.cshtml");
        private readonly string Temp_TableSnippet_GetList = Comm.GetTemplete("TableSnippet.GetList.cshtml");
        private readonly string Temp_TableSnippet_Paged = Comm.GetTemplete("TableSnippet.Paged.cshtml");
        private readonly string Temp_TableSnippet_Top = Comm.GetTemplete("TableSnippet.Top.cshtml");
        private readonly string Temp_TableSnippet_Exist = Comm.GetTemplete("TableSnippet.Exist.cshtml");
        private readonly string Temp_TableSnippet_Count = Comm.GetTemplete("TableSnippet.Count.cshtml");

        private readonly string Temp_ViewSnippet_Get = Comm.GetTemplete("ViewSnippet.Get.cshtml");
        private readonly string Temp_ViewSnippet_GetList = Comm.GetTemplete("ViewSnippet.GetList.cshtml");
        private readonly string Temp_ViewSnippet_Paged = Comm.GetTemplete("ViewSnippet.Paged.cshtml");
        private readonly string Temp_ViewSnippet_Top = Comm.GetTemplete("ViewSnippet.Top.cshtml");
        private readonly string Temp_ViewSnippet_Exist = Comm.GetTemplete("ViewSnippet.Exist.cshtml");
        private readonly string Temp_ViewSnippet_Count = Comm.GetTemplete("ViewSnippet.Count.cshtml");

        public string GenTableQueryCode(TableQueryGenModel model)
        {
            string code = "";
            switch (model.GenType)
            {
                case TableQueryGenType.Get:
                    code = codeGen.Gen(this.Temp_TableSnippet_Get, model);
                    break;
                case TableQueryGenType.GetList:
                    code = codeGen.Gen(this.Temp_TableSnippet_GetList, model);
                    break;
                case TableQueryGenType.Paged:
                    code = codeGen.Gen(this.Temp_TableSnippet_Paged, model);
                    break;
                case TableQueryGenType.Top:
                    code = codeGen.Gen(this.Temp_TableSnippet_Top, model);
                    break;
                case TableQueryGenType.Exist:
                    code = codeGen.Gen(this.Temp_TableSnippet_Exist, model);
                    break;
                case TableQueryGenType.Count:
                    code = codeGen.Gen(this.Temp_TableSnippet_Count, model);
                    break;
                default:
                    break;
            }
            return code;
        }

        public string GenViewQueryCode(ViewQueryGenModel model)
        {
            string code = "";
            switch (model.GenType)
            {
                case ViewQueryGenType.Get:
                    code = codeGen.Gen(this.Temp_ViewSnippet_Get, model);
                    break;
                case ViewQueryGenType.GetList:
                    code = codeGen.Gen(this.Temp_ViewSnippet_GetList, model);
                    break;
                case ViewQueryGenType.Paged:
                    code = codeGen.Gen(this.Temp_ViewSnippet_Paged, model);
                    break;
                case ViewQueryGenType.Top:
                    code = codeGen.Gen(this.Temp_ViewSnippet_Top, model);
                    break;
                case ViewQueryGenType.Exist:
                    code = codeGen.Gen(this.Temp_ViewSnippet_Exist, model);
                    break;
                case ViewQueryGenType.Count:
                    code = codeGen.Gen(this.Temp_ViewSnippet_Count, model);
                    break;
                default:
                    break;
            }
            return code;
        }
    }
}

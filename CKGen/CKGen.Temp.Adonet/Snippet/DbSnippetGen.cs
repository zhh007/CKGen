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

        public string GenCountCode(CountModel model)
        {
            return codeGen.Gen(this.Temp_Snippet_Count, model);
        }
    }
}

using CKGen.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKGen.Temp.AspnetForm
{
    public class PageFormCodeGen
    {
        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();

        public string Build(PageFormModel model)
        {
            Guid pathID = Guid.NewGuid();

            string root = Path.Combine(Path.GetTempPath(), pathID.ToString("P"));
            Directory.CreateDirectory(root);

            //page.aspx
            //string pagePath = Path.Combine(root, string.Format("{0}.aspx", model.FormModule.CodeName));
            //string pageContent = codeGen.Gen(Comm.GetTemplete("WebForm.Page.cshtml"), model.FormModule);
            //File.WriteAllText(pagePath, pageContent);

            //page.aspx.cs
            string pageCodePath = Path.Combine(root, string.Format("{0}.aspx.cs", model.FormModule.CodeName));
            string pageCodeContent = codeGen.Gen(Comm.GetTemplete("WebForm.PageCode.cshtml"), model.FormModule);
            File.WriteAllText(pageCodePath, pageCodeContent);

            return root;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base
{
    public interface ICodeGenService
    {
        string GenByPath(string viewpath, object model);

        void GenSave(string viewpath, object model, string filepath);

        string Gen(string view, object model);
    }
}

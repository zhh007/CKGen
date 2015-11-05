using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base
{
    public interface ICodeGenService
    {
        string Gen(string viewpath, object model);

        void GenSave(string viewpath, object model, string filepath);
    }
}

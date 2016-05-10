using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.AspnetMVC
{
    public interface IGenUI
    {
        IDatabaseInfo Database { get; set; }
        void ShowSettingView();

        void ShowResultView();

        void GoResult(string dir);
    }
}

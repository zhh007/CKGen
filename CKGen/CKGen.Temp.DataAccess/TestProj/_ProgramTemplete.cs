// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DataAccess.TestProj
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class ProgramTemplete
    {
        public string NameSpace;

        public ProgramTemplete(string n)
        {
            if (string.IsNullOrEmpty(n))
                throw new Exception("参数n，命名空间不能为空。");
            
            this.NameSpace = n;
        }
    }
}

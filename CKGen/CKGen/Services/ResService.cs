using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CKGen.Base;

namespace CKGen.Services
{
    public class ResService : IResService
    {
        public Image GetPrimaryKeyImage()
        {
            return CKGen.Properties.Resources.PrimaryKeyHS;
        }
    }
}

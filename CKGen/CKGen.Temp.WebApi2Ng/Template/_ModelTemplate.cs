using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.WebApi2Ng.Template
{
    public partial class ModelTemplate
    {
        public WebApiEntity Entity { get; private set; }

        public ModelTemplate(WebApiEntity entity)
        {
            Entity = entity;
        }
    }
}

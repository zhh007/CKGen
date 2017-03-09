using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.WebApi2Ng
{
    public class WebApiDef
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }

        public List<MethodDef> Methods { get; set; }

        public WebApiDef()
        {
            this.Methods = new List<MethodDef>();
        }
    }

    public class MethodDef
    {
        public string WebApiName { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// post, get,...etc
        /// </summary>
        public string HttpMethod { get; set; }
        public string ResponseEntity { get; set; }

        public List<MethodParam> Params { get; set; }

        public MethodDef()
        {
            this.Params = new List<MethodParam>();
        }
    }

    public class MethodParam
    {
        public string Name { get; set; }
        public string EntityName { get; set; }
    }

    public class WebApiEntity
    {
        public string Name { get; set; }

        public List<WebApiEntityProperty> Properties { get; set; }

        public WebApiEntity()
        {
            this.Properties = new List<WebApiEntityProperty>();
        }
    }

    public class WebApiEntityProperty
    {
        public string Name { get; set; }
        public string TypeString { get; set; }
        public bool IsSample { get; set; }

        public bool IsObject { get; set; }
        public bool IsArray { get; set; }
    }


}

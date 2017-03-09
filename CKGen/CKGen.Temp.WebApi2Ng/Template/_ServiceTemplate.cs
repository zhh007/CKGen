using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CKGen.Temp.WebApi2Ng.Template
{
    public partial class ServiceTemplate
    {
        public WebApiDef Api { get; private set; }

        public string ImportModels { get; private set; }
        public string BaseUrl { get; set; }

        public ServiceTemplate(WebApiDef api)
        {
            this.Api = api;

            List<string> list = new List<string>();
            foreach (var item in api.Methods)
            {
                if(!list.Contains(item.ResponseEntity))
                {
                    list.Add(item.ResponseEntity);
                }

                foreach (var p in item.Params)
                {
                    if(!list.Contains(p.EntityName))
                    {
                        list.Add(p.EntityName);
                    }
                }
            }

            this.ImportModels = string.Join(", ", list.ToArray());
            this.BaseUrl = api.BaseUrl + "/api/" + api.Name + "/";

        }
    }
}

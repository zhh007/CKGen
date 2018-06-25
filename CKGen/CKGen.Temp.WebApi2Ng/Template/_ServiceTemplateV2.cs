using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CKGen.Temp.WebApi2Ng.Template
{
    public partial class ServiceTemplateV2
    {
        public WebApiDef Api { get; private set; }

        public string ImportModels { get; private set; }
        public string BaseUrl { get; set; }
        public bool IsWorkflowApi { get; set; }

        //以下流程方法，请求参数改为any
        public string[] wfMethods = new string[] { "Delete", "Change", "UnChange" };

        public ServiceTemplateV2(WebApiDef api)
        {
            this.Api = api;

            List<string> list = new List<string>();
            foreach (var item in api.Methods)
            {
                if(!list.Contains(item.ResponseEntity) && item.ResponseEntity != "string")
                {
                    list.Add(item.ResponseEntity);
                }

                foreach (var p in item.Params)
                {
                    if(p.IsSampleType)
                    {
                        continue;
                    }

                    if(!list.Contains(p.EntityName))
                    {
                        list.Add(p.EntityName);
                    }
                }
            }

            this.ImportModels = string.Join(",\r\n  ", list.ToArray());
            this.BaseUrl = "/api/" + api.Name;

            this.IsWorkflowApi = api.Methods.Select(p => wfMethods.Contains(p.Name)).Count() > 0;


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CKGen.Base.Log
{
    public class Log4NetUserNameProvider
    {
        private bool IsWebApp = false;
        public Log4NetUserNameProvider()
        {
            IsWebApp = HttpRuntime.AppDomainAppId != null;
        }

        private string getUserIP()
        {
            HttpContext context = HttpContext.Current;
            if (context == null || context.Request == null)
                return string.Empty;

            string ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                   ?? context.Request.UserHostAddress;

            if (ip == "::1")
                ip = "127.0.0.1";

            return ip;
        }

        public override string ToString()
        {
            if (IsWebApp)
            {
                //is web app
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    if (context.User != null && context.User.Identity.IsAuthenticated)
                    {
                        int userid = 0;
                        if (HttpContext.Current.Items["__UserID__"] != null)
                        {
                            int.TryParse(HttpContext.Current.Items["__UserID__"].ToString(), out userid);
                        }
                        if (userid > 0)
                        {
                            return string.Format("{0}[{1}/{2}]", getUserIP(), context.User.Identity.Name, userid);
                        }
                        else
                        {
                            return string.Format("{0}[{1}]", getUserIP(), context.User.Identity.Name);
                        }
                    }
                    else
                    {
                        return getUserIP();
                    }
                }
                else
                {
                    return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                }
            }
            else
            {
                //is windows app
                return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
        }
    }
}

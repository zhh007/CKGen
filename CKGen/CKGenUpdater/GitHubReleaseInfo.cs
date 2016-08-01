using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CKGenUpdater
{
    public class GitHubReleaseInfo
    {
        public string Version { get; set; }
        public string Body { get; set; }
        public string DownloadUrl { get; set; }
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        public GitHubReleaseInfo(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;

            string responseString = "";

            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
                    {
                        responseString = readStream.ReadToEnd();
                    }
                }
            }

            if (!string.IsNullOrEmpty(responseString))
            {
                string span = "\"tag_name\":\"";
                int pos = responseString.IndexOf(span) + span.Length;
                int pos2 = responseString.IndexOf("\"", pos);
                this.Version = responseString.Substring(pos, pos2 - pos);

                span = "\"browser_download_url\":\"";
                pos = responseString.IndexOf(span) + span.Length;
                pos2 = responseString.IndexOf("\"", pos);
                this.DownloadUrl = responseString.Substring(pos, pos2 - pos);

                span = "\"body\":\"";
                pos = responseString.IndexOf(span) + span.Length;
                pos2 = responseString.IndexOf("\"", pos);
                this.Body = responseString.Substring(pos, pos2 - pos);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web;

namespace CKGen.Tools
{
    public partial class OAuth_Password : UserControl
    {
        private OAuthTest parent;
        public OAuth_Password(OAuthTest _parent)
        {
            InitializeComponent();
            this.parent = _parent;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            string url = parent.txtUrl.Text;
            string clientId = parent.txtClientId.Text;
            string clientSecret = parent.txtClientSecret.Text;

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            string encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret));
            request.Headers.Add("Authorization", "Basic " + encoded);

            string str = string.Format("grant_type=password&username={0}&password={1}"
                , HttpUtility.UrlEncode(txtUserName.Text)
                , HttpUtility.UrlEncode(txtPassword.Text));
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            request.ContentLength = bytes.Length;
            using (Stream outputStream = request.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            using (WebResponse webResponse = request.GetResponse())
            using (var s = webResponse.GetResponseStream())
            using (var sr = new StreamReader(s, Encoding.UTF8))
            {
                string content = sr.ReadToEnd();
                var json = JObject.Parse(content);

                this.txtAccessToken.Text = json["access_token"].Value<string>();
                this.txtRefreshToken.Text = json["refresh_token"].Value<string>();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string url = parent.txtUrl.Text;
            string clientId = parent.txtClientId.Text;
            string clientSecret = parent.txtClientSecret.Text;

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            string encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret));
            request.Headers.Add("Authorization", "Basic " + encoded);

            string str = string.Format("grant_type=refresh_token&refresh_token={0}"
                , HttpUtility.UrlEncode(txtRefreshToken.Text));
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            request.ContentLength = bytes.Length;
            using (Stream outputStream = request.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            using (WebResponse webResponse = request.GetResponse())
            using (var s = webResponse.GetResponseStream())
            using (var sr = new StreamReader(s, Encoding.UTF8))
            {
                string content = sr.ReadToEnd();
                var json = JObject.Parse(content);

                this.txtAccessToken.Text = json["access_token"].Value<string>();
                this.txtRefreshToken.Text = json["refresh_token"].Value<string>();
            }
        }

        private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPostOAuthHelp frm = new frmPostOAuthHelp();
            frm.ShowDialog();
        }
    }
}

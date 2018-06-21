using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.Web;
using System.Net;
using CKGen.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CKGen.Temp.WebApi2Ng.Template;

namespace CKGen.Temp.WebApi2Ng
{
    [Export("GenTemplate", typeof(UserControl))]
    public partial class MainUI : UserControl
    {

        private string HostUrl = "";
        private List<MethodDef> Methods = new List<MethodDef>();
        private List<WebApiEntity> Entities = new List<WebApiEntity>();

        public override string ToString()
        {
            return "WebAPI生成angular2代理类";
        }

        public MainUI()
        {
            InitializeComponent();
        }

        private void btnGetSchema_Click(object sender, EventArgs e)
        {
            this.Methods.Clear();
            this.Entities.Clear();
            lbWebAPI.Items.Clear();
            lvEntity.Clear();
            lvMethod.Clear();

            try
            {
                var jsonstr = HttpUtil.Get(txtURL.Text);
                JObject json = JsonConvert.DeserializeObject(jsonstr) as JObject;
                string host = json["host"].ToString();
                string schema = json["schemes"][0].ToString();
                JToken paths = json["paths"] as JToken;
                JToken definitions = json["definitions"] as JToken;

                this.HostUrl = schema + "://" + host;

                foreach (JProperty item in paths.Children())
                {
                    MethodDef mdef = new MethodDef();

                    string name = item.Name;
                    JToken val = item.Value;
                    string[] arr = name.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                    mdef.WebApiName = arr[1];
                    mdef.Name = arr[2];
                    JToken val2 = val["post"];
                    if (val2 != null)
                    {
                        mdef.HttpMethod = "post";
                    }

                    if (val2 == null)
                    {
                        val2 = val["get"];
                        if (val2 != null)
                        {
                            mdef.HttpMethod = "get";
                        }
                    }

                    try
                    {
                        if (val2["responses"]["200"] != null)
                        {
                            if (val2["responses"]["200"]["schema"]["$ref"] != null)
                            {
                                var s2 = val2["responses"]["200"]["schema"]["$ref"].ToString().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                                mdef.ResponseEntity = s2[2];
                            }
                            else if (val2["responses"]["200"]["schema"]["type"] != null)
                            {
                                var s2 = val2["responses"]["200"]["schema"]["type"].ToString();
                                if (s2 == "array")
                                {
                                    if (val2["responses"]["200"]["schema"]["items"]["type"] != null)
                                    {
                                        mdef.ResponseEntity = "List<" + val2["responses"]["200"]["schema"]["items"]["type"] + ">";
                                    }
                                }
                                else if(s2 == "string")
                                {
                                    mdef.ResponseEntity = "string";
                                }
                                else
                                {
                                    mdef.ResponseEntity = "string";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        if (val2["parameters"] != null)
                        {
                            var plist = val2["parameters"] as JArray;
                            if (plist != null)
                            {
                                foreach (JToken p in plist)
                                {
                                    if(p["in"] != null && p["in"].ToString() == "query")
                                    {
                                        continue;
                                    }

                                    MethodParam mp = new MethodParam();
                                    mp.Name = p["name"].ToString();
                                    try
                                    {
                                        var s = p["schema"]["$ref"].ToString().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                                        mp.EntityName = s[2];
                                    }
                                    catch (Exception)
                                    {
                                        mp.IsSampleType = true;
                                        mp.EntityName = "any";
                                    }
                                    mdef.Params.Add(mp);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    this.Methods.Add(mdef);
                }

                foreach (JProperty item in definitions.Children())
                {
                    WebApiEntity wpEntity = new WebApiEntity();
                    wpEntity.Name = item.Name;

                    if (item.Value["properties"] != null)
                    {
                        var pp = item.Value["properties"].Children();
                        foreach (JProperty ip in pp)
                        {
                            WebApiEntityProperty ep = new WebApiEntityProperty();
                            ep.Name = ip.Name;

                            var first = ip.First();

                            if (first["$ref"] != null)
                            {
                                ep.IsObject = true;
                                var ss = first["$ref"].ToString().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                                ep.TypeString = ss[2];
                            }

                            if (first["type"] != null)
                            {
                                ep.TypeString = first["type"].ToString();
                                if (ep.TypeString == "integer")
                                {
                                    ep.TypeString = "number";
                                }
                                ep.IsSample = true;
                            }

                            if (ep.TypeString == "array")
                            {
                                ep.IsArray = true;
                                if (first["items"]["$ref"] != null)
                                {
                                    var str = first["items"]["$ref"].ToString();
                                    var ss = str.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                                    ep.TypeString = ss[2];
                                }
                                else if (first["items"]["type"] != null)
                                {
                                    var str = first["items"]["type"].ToString();
                                    if (str == "integer")
                                    {
                                        ep.TypeString = "number";
                                    }
                                    else if( str == "string")
                                    {
                                        ep.TypeString = "string";
                                    }
                                }
                            }

                            wpEntity.Properties.Add(ep);
                        }
                    }
                    Entities.Add(wpEntity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var wpNames = (from p in this.Methods
                           group p by p.WebApiName into pg
                           select pg.Key).ToList();
            this.lbWebAPI.Items.Clear();
            foreach (var name in wpNames)
            {
                this.lbWebAPI.Items.Add(name);
            }
        }

        private void lbWebAPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbWebAPI.SelectedIndex < 0)
            {
                return;
            }
            var name = lbWebAPI.SelectedItem.ToString();
            lvEntity.Clear();
            lvMethod.Clear();

            var ms = (from p in this.Methods
                      where p.WebApiName == name
                      select p).ToList();
            foreach (var item in ms)
            {
                string[] itemArr = { item.Name };
                var lvi = new ListViewItem(itemArr) { };
                lvi.Tag = item;
                lvMethod.Items.Add(lvi);
            }


            List<string> es = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (var item in ms)
            {
                if (item.Params != null && item.Params.Count > 0)
                {
                    foreach (var p in item.Params)
                    {
                        if (!es.Contains(p.EntityName))
                        {
                            es.Add(p.EntityName);
                            stack.Push(p.EntityName);
                        }
                    }
                }

                if (!es.Contains(item.ResponseEntity))
                {
                    es.Add(item.ResponseEntity);
                    stack.Push(item.ResponseEntity);
                }
            }

            //子对象
            while (stack.Any())
            {
                var str = stack.Pop();
                var entity = this.Entities.FirstOrDefault(p => p.Name == str);
                if (entity == null || entity.Properties == null || entity.Properties.Count == 0)
                {
                    continue;
                }

                foreach (var p in entity.Properties)
                {
                    if (p.IsObject)
                    {
                        if (!es.Contains(p.TypeString))
                        {
                            es.Add(p.TypeString);
                            stack.Push(p.TypeString);
                        }
                    }
                    if (p.IsArray)
                    {
                        if (!es.Contains(p.TypeString))
                        {
                            es.Add(p.TypeString);
                            stack.Push(p.TypeString);
                        }
                    }
                }
            }

            //
            foreach (var item in es)
            {
                var entity = this.Entities.FirstOrDefault(p => p.Name == item);
                if (entity == null)
                {
                    continue;
                }
                string[] itemArr = { entity.Name };
                var lvi = new ListViewItem(itemArr) { };
                lvi.Tag = entity;
                lvEntity.Items.Add(lvi);
            }

        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            WebApiDef apidef = new WebApiDef();
            apidef.BaseUrl = this.HostUrl;
            apidef.Name = lbWebAPI.SelectedItem.ToString();

            foreach (ListViewItem item in lvMethod.Items)
            {
                var m = item.Tag as MethodDef;
                if(string.IsNullOrEmpty(m.ResponseEntity))
                {
                    MessageBox.Show("接口" + m.Name + "，没有返回值，无法生成。");
                    return;
                }
                apidef.Methods.Add(m);
            }

            string srvString = "";
            if(radV1.Checked)
            {
                ServiceTemplate st = new Template.ServiceTemplate(apidef);
                srvString = st.TransformText();
            }
            else
            {
                ServiceTemplateV2 st = new Template.ServiceTemplateV2(apidef);
                srvString = st.TransformText();
            }

            txtSrevice.Text = srvString;

            List<string> entityCodeList = new List<string>();
            txtEntity.Text = "";
            foreach (ListViewItem item in lvEntity.Items)
            {
                ModelTemplate modelCode = new ModelTemplate(item.Tag as WebApiEntity);
                string autoCodeStr = modelCode.TransformText();

                txtEntity.Text += autoCodeStr + "\r\n";
                //entityCodeList.Add(autoCodeStr);
            }

            tabControl1.SelectTab(1);
        }

        private void txtSrevice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }
    }
}

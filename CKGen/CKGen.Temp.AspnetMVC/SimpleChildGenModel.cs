using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CKGen.Temp.AspnetMVC
{
    public class SimpleChildGenModel
    {
        public ITableInfo ChildModel { get; set; }
        public ITableInfo ParentModel { get; set; }
        public string ChildCollectionName { get; set; }
        public List<InputItem> Items { get; set; }
        public string NameSpacePR { get; set; }
        public string WebProjNameSpace { get; set; }
        public SimpleChildGenModel()
        {
            this.Items = new List<InputItem>();
        }

        public int GetColumnCount()
        {
            return Items.Count(p => p.InputType != "hidden");
        }

        public string GetRowHtmlString(int index)
        {
            StringBuilder sb = new StringBuilder();
            if (index == 0)
            {
                var list = Items.Where(p => p.InputType == "hidden").ToList();
                foreach (var item in list)
                {
                    sb.Append(_getRowString(item));
                    sb.Append("\r\n");
                }
            }
            InputItem inp = Items.Where(p => p.InputType != "hidden").ToList()[index];
            sb.Append(_getRowString(inp));
            return sb.ToString();
        }

        public string GetRowForJS(int index)
        {
            StringBuilder sb = new StringBuilder();
            if (index == 0)
            {
                var list = Items.Where(p => p.InputType == "hidden").ToList();
                foreach (var item in list)
                {
                    sb.AppendLine("h += '" + clearRowForJs(_getRowString(item)) + "';");
                }
            }
            InputItem inp = Items.Where(p => p.InputType != "hidden").ToList()[index];
            sb.Append("h += '" + clearRowForJs(_getRowString(inp)) + "';");
            return sb.ToString();
        }

        private string clearRowForJs(string txt)
        {
            string str = Regex.Replace(txt, @"value=""([^""]*)""\s", "");
            return str.Replace("@(i)", "0");
        }

        public string GetRowForJSUpdateId(int index)
        {
            StringBuilder sb = new StringBuilder();
            if (index == 0)
            {
                var list = Items.Where(p => p.InputType == "hidden").ToList();
                foreach (var item in list)
                {
                    sb.AppendFormat("$(\".{2}row_{1}\", o).attr(\"id\", \"{1}_\" + i).attr(\"name\", \"{0}[\" + i + \"].{1}\");", this.ChildCollectionName, item.PascalName, this.ChildCollectionName.ToLower());
                    sb.Append("\r\n");
                }
            }
            InputItem inp = Items.Where(p => p.InputType != "hidden").ToList()[index];
            sb.AppendFormat("$(\".{2}row_{1}\", o).attr(\"id\", \"{1}_\" + i).attr(\"name\", \"{0}[\" + i + \"].{1}\");", this.ChildCollectionName, inp.PascalName, this.ChildCollectionName.ToLower());
            return sb.ToString();
        }

        public string _getRowString(InputItem inp)
        {
            StringBuilder sb = new StringBuilder();
            switch (inp.InputType)
            {
                case "text":
                    sb.AppendFormat("<input name=\"{0}[@(i)].{1}\" id=\"{1}_@(i)\" type=\"text\" value=\"@Model.{0}[i].{1}\" class=\"form-control {2}row_{1}\" />", this.ChildCollectionName, inp.PascalName, this.ChildCollectionName.ToLower());
                    break;
                case "hidden":
                    sb.AppendFormat("<input name=\"{0}[@(i)].{1}\" id=\"{1}_@(i)\" type=\"hidden\" value=\"@Model.{0}[i].{1}\" class=\"{2}row_{1}\" />", this.ChildCollectionName, inp.PascalName, this.ChildCollectionName.ToLower());
                    break;
                case "select":
                    sb.AppendFormat("<select name=\"{0}[@(i)].{1}\" id=\"{1}_@(i)\" class=\"form-control {2}row_{1}\">", this.ChildCollectionName, inp.PascalName, this.ChildCollectionName.ToLower());
                    sb.Append("<option value=\"\">全部</option>");
                    sb.Append("</select>");
                    break;
                case "radio":
                    sb.AppendFormat("<input name=\"{0}[@(i)].{1}\" id=\"{1}_@(i)\" type=\"radio\" class=\"form-control {2}row_{1}\" />", this.ChildCollectionName, inp.PascalName, this.ChildCollectionName.ToLower());
                    break;
                case "checkbox":
                    sb.AppendFormat("<input name=\"{0}[@(i)].{1}\" id=\"{1}_@(i)\" type=\"checkbox\" class=\"form-control {2}row_{1}\" />", this.ChildCollectionName, inp.PascalName, this.ChildCollectionName.ToLower());
                    break;
                case "textarea":
                    sb.AppendFormat("<textarea name=\"{0}[@(i)].{1}\" id=\"{1}_@(i)\" class=\"form-control {2}row_{1}\" ></textarea>", this.ChildCollectionName, inp.PascalName, this.ChildCollectionName.ToLower());
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }
    }

    public class InputItem
    {
        public string Title { get; set; }
        public string PascalName { get; set; }
        public string CamelName { get; set; }
        public string InputType { get; set; }
    }
}

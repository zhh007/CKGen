using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.IO;

namespace CKGen.Tools
{
    [Export("Tool", typeof(UserControl))]
    public partial class CtrlHtmlToJSString : UserControl
    {
        public CtrlHtmlToJSString()
        {
            InitializeComponent();

            cbList.Text = "JS";
            BuildOutput();
        }

        public override string ToString()
        {
            return "HTML转代码";
        }

        private void txtHTML_TextChanged(object sender, EventArgs e)
        {
            BuildOutput();
        }

        private void BuildOutput()
        {
            string output = "";
            if (!string.IsNullOrEmpty(txtHTML.Text) && !string.IsNullOrWhiteSpace(txtHTML.Text))
            {
                if (cbList.Text == "JS")
                {
                    output = BuildJS();
                }
                else if (cbList.Text == "C#")
                {
                    output = BuildCSharp();
                }
            }
            txtJS.Text = output;
        }

        private string BuildJS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var h = '';");
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(txtHTML.Text)))
            using (StreamReader r = new StreamReader(stream))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line.Trim()))
                    {
                        sb.AppendLine("h += " + ToJsString(line) + ";");
                    }
                }
            }

            return sb.ToString();
        }

        private string BuildCSharp()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("StringBuilder sb = new StringBuilder();");
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(txtHTML.Text)))
            using (StreamReader r = new StreamReader(stream))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line.Trim()))
                    {
                        sb.AppendLine("sb.AppendLine(" + ToCSharpString(line) + ");");
                    }
                }
            }

            return sb.ToString();
        }

        public static string ToJsString(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'");
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\'':
                        sb.Append("\\\'");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            sb.Append("'");

            return sb.ToString();
        }

        public static string ToCSharpString(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            sb.Append("\"");

            return sb.ToString();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }

        private void cbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildOutput();
        }
    }
}

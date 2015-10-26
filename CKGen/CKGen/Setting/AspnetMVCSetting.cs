using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

/*
<mvc>
    <setting DatabaseName="" NameSpace="" WebProjNameSpace=""  />
</mvc>
*/
namespace CKGen
{
    public class AspnetMVCSetting
    {
        private static string FileName = "aspnetmvc.xml";
        private static string _namespace = "";
        private static string _webprojNameSpace = "";

        public static string NameSpace
        {
            get
            {
                return _namespace;
            }
        }

        public static string WebProjNameSpace
        {
            get
            {
                return _webprojNameSpace;
            }
        }

        static AspnetMVCSetting()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Settings\\" + FileName);
            if (File.Exists(filePath))
            {
                string txt = File.ReadAllText(filePath);
                XElement root = XElement.Parse(txt);

                var el = (from p in root.Elements()
                          where p.Attribute("DatabaseName").Value == SystemConfig.DBName
                          select p).FirstOrDefault();

                if (el != null)
                {
                    _namespace = el.Attribute("NameSpace").Value;
                    _webprojNameSpace = el.Attribute("WebProjNameSpace").Value;
                }
            }
        }

        public static void Save(string nsString, string webNsString)
        {
            _namespace = nsString;
            _webprojNameSpace = webNsString;
            string filePath = Path.Combine(Environment.CurrentDirectory, "Settings\\" + FileName);
            if (File.Exists(filePath))
            {
                string txt = File.ReadAllText(filePath);
                XElement root = XElement.Parse(txt);

                var el = (from p in root.Elements()
                          where p.Attribute("DatabaseName").Value == SystemConfig.DBName
                          select p).FirstOrDefault();

                if (el != null)
                {
                    el.Attribute("NameSpace").Value = nsString;
                    el.Attribute("WebProjNameSpace").Value = webNsString;
                }
                else
                {
                    XElement setting = new XElement("setting");
                    setting.Add(new XAttribute("DatabaseName", SystemConfig.DBName));
                    setting.Add(new XAttribute("NameSpace", nsString));
                    setting.Add(new XAttribute("WebProjNameSpace", webNsString));

                    root.Add(setting);
                }

                XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                xdoc.Save(filePath);
            }
            else
            {
                XElement root = new XElement("mvc");

                XElement setting = new XElement("setting");
                setting.Add(new XAttribute("DatabaseName", SystemConfig.DBName));
                setting.Add(new XAttribute("NameSpace", nsString));
                setting.Add(new XAttribute("WebProjNameSpace", webNsString));

                root.Add(setting);

                XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                xdoc.Save(filePath);
            }
        }
    }
}

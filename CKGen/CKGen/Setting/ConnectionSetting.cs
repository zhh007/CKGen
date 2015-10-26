using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

/*
<Connections>
    <Connection Type="" ServerName="" DatabaseName="" LoginName="" LoginPassword="" IsWindowsLogin=""  />
</Connections>
*/
namespace CKGen
{
    public class ConnectionSetting
    {
        private static string FileName = "conn.xml";

        private static List<DatabaseLink> Connections = new List<DatabaseLink>();

        static ConnectionSetting()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Settings\\" + FileName);
            if (File.Exists(filePath))
            {
                string txt = File.ReadAllText(filePath);
                XElement config = XElement.Parse(txt);
                foreach (XElement el in config.Elements())
                {
                    DatabaseLink link = new DatabaseLink(
                        EnumHelper.ParseEnum<DatabaseType>(el.Attribute("Type").Value),
                        el.Attribute("ServerName").Value,
                        el.Attribute("LoginName").Value,
                        el.Attribute("LoginPassword").Value,
                        (bool)el.Attribute("IsWindowsLogin"));
                    link.SetDatabaseName(el.Attribute("DatabaseName").Value);
                    Connections.Add(link);
                }
            }
        }

        public static List<DatabaseLink> GetList()
        {
            return Connections;
        }

        public static void Add(DatabaseLink link)
        {
            var oldLink = (from p in Connections
                           where p.ServerName == link.ServerName
                           select p).FirstOrDefault();
            if (oldLink != null)
            {
                Connections.Remove(oldLink);
            }
            Connections.Insert(0, link);

            Save();
        }

        public static DatabaseLink GetByServerName(string srvname)
        {
            var oldLink = (from p in Connections
                           where p.ServerName == srvname
                           select p).FirstOrDefault();
            if (oldLink != null)
            {
                return oldLink;
            }
            return null;
        }

        private static void Save()
        {
            XElement connList = new XElement("Connections");
            foreach (var item in Connections)
            {
                XElement conn = new XElement("Connection");
                conn.Add(new XAttribute("Type", item.Type));
                conn.Add(new XAttribute("ServerName", item.ServerName));
                conn.Add(new XAttribute("LoginName", item.LoginName));
                conn.Add(new XAttribute("LoginPassword", item.LoginPassword));
                conn.Add(new XAttribute("IsWindowsLogin", item.IsWindowsLogin));
                conn.Add(new XAttribute("DatabaseName", item.DatabaseName));

                connList.Add(conn);
            }

            XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), connList);
            string filePath = Path.Combine(Environment.CurrentDirectory, "Settings\\" + FileName);
            xdoc.Save(filePath);
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen
{
    public class DatabaseLink
    {
        public DatabaseType Type { get; private set; }

        public string ServerName { get; private set; }

        public string DatabaseName { get; private set; }

        public string LoginName { get; private set; }

        public string LoginPassword { get; private set; }

        public bool IsWindowsLogin { get; private set; }

        public DatabaseLink(DatabaseType type, string srv, string uid, string pwd, bool isWinLogin)
        {
            this.Type = type;
            this.ServerName = srv;
            this.LoginName = uid;
            this.LoginPassword = pwd;
            this.IsWindowsLogin = isWinLogin;
        }

        public DatabaseLink(DatabaseType type, string srv, string dbname, string uid, string pwd)
        {
            this.Type = type;
            this.ServerName = srv;
            this.DatabaseName = dbname;
            this.LoginName = uid;
            this.LoginPassword = pwd;
            this.IsWindowsLogin = false;
        }

        public void SetDatabaseName(string dbName)
        {
            this.DatabaseName = dbName;
        }

        public string SchemaConnectionString
        {
            get
            {
                string str = "";
                string dbName = this.DatabaseName;
                switch (Type)
                {
                    case DatabaseType.MSSQLServer:
                        dbName = dbName ?? "master";
                        if (!this.IsWindowsLogin)
                        {
                            str = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3};Provider={4};Persist Security Info=False;", this.ServerName, dbName, this.LoginName, this.LoginPassword, GetSqlDBLinkProvider());
                        }
                        else
                        {
                            str = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Provider={2};Persist Security Info=False;", this.ServerName, dbName, GetSqlDBLinkProvider());
                        }
                        break;
                    default:
                        break;
                }

                return str;
            }
        }

        public string ConnectionString
        {
            get
            {
                string str = "";
                string dbName = this.DatabaseName;
                switch (Type)
                {
                    case DatabaseType.MSSQLServer:
                        dbName = dbName ?? "master";
                        if (!this.IsWindowsLogin)
                        {
                            str = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3};Persist Security Info=False;", this.ServerName, dbName, this.LoginName, this.LoginPassword);
                        }
                        else
                        {
                            str = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Persist Security Info=False;Application Name=CKGen"
                                        , this.ServerName, dbName, this.LoginName, this.LoginPassword);
                        }
                        break;
                    default:
                        break;
                }

                return str;
            }
        }

        private string GetSqlDBLinkProvider()
        {
            string result = "SQLNCLI10";
            var sfkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server Native Client 11.0");
            if (sfkey != null)
            {
                sfkey.Close();
                result = "SQLNCLI11";
            }

            return result;
        }
    }
}

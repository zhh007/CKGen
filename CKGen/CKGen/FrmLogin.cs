using CKGen.DBLoader;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CKGen
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.cbServerType.DataSource = typeof(DatabaseType).ToList();
            this.cbServerType.DisplayMember = "Value";
            this.cbServerType.ValueMember = "Key";

            this.btnOK.Enabled = false;

            var connList = ConnectionSetting.GetList();
            if (connList != null && connList.Count > 0)
            {
                cbServerName.DataSource = connList;
                cbServerName.DisplayMember = "ServerName";
                cbServerName.ValueMember = "ServerName";

                ShowServer(connList[0].ServerName);
            }
        }

        private string curDatabaseName = "";

        private void ShowServer(string srvName)
        {
            DatabaseLink link = ConnectionSetting.GetByServerName(cbServerName.Text);
            if (link != null)
            {
                cbServerName.Text = srvName;
                if (link.IsWindowsLogin)
                {
                    rbWindows.Checked = true;
                    rbSQLServer.Checked = false;
                    txtLoginName.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    rbWindows.Checked = false;
                    rbSQLServer.Checked = true;
                    txtLoginName.Text = link.LoginName;
                    txtPassword.Text = link.LoginPassword;
                }
                cbDatabases.DataSource = null;
                cbDatabases.Items.Clear();
                cbDatabases.DataSource = new string[] { link.DatabaseName };
                cbDatabases.Enabled = false;
                this.DBLink = link;
                this.curDatabaseName = link.DatabaseName;
            }

            btnOK.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 使用 Windows 身份验证
        /// </summary>
        private void rbWindows_CheckedChanged(object sender, EventArgs e)
        {
            txtLoginName.Text = "";
            txtPassword.Text = "";
            txtLoginName.Enabled = false;
            txtPassword.Enabled = false;
            
            if (this.DBLink != null)
            {
                cbDatabases.DataSource = new string[] { this.DBLink.DatabaseName };
            }
            DisabledOKBtn();
        }

        /// <summary>
        /// 使用 SQL Server 身份验证
        /// </summary>
        private void rbSQLServer_CheckedChanged(object sender, EventArgs e)
        {
            txtLoginName.Enabled = true;
            txtPassword.Enabled = true;
            if (!string.IsNullOrEmpty(cbServerName.Text))
            {
                DatabaseLink link = ConnectionSetting.GetByServerName(cbServerName.Text);
                if (link != null)
                {
                    txtLoginName.Text = link.LoginName;
                    txtPassword.Text = link.LoginPassword;
                }
            }

            DisabledOKBtn();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.cbServerType.Enabled = false;
            this.cbServerName.Enabled = false;
            this.rbWindows.Enabled = false;
            this.rbSQLServer.Enabled = false;
            this.txtLoginName.Enabled = false;
            this.txtPassword.Enabled = false;
            this.btnLogin.Enabled = false;
            this.btnOK.Enabled = false;
            this.cbDatabases.Enabled = false;

            var link = new DatabaseLink((DatabaseType)this.cbServerType.SelectedValue, this.cbServerName.Text, this.txtLoginName.Text, this.txtPassword.Text, rbWindows.Checked);
            this.DBLink = link;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.cbServerType.Enabled = true;
            this.cbServerName.Enabled = true;
            this.rbWindows.Enabled = true;
            this.rbSQLServer.Enabled = true;
            if (rbSQLServer.Checked)
            {
                this.txtLoginName.Enabled = true;
                this.txtPassword.Enabled = true;
            }
            this.btnOK.Enabled = true;
            this.cbDatabases.Enabled = true;

            if (this.SrvInfo != null && this.SrvInfo.Databases != null && this.SrvInfo.Databases.Count > 0)
            {
                cbDatabases.DataSource = this.SrvInfo.Databases;
                cbDatabases.DisplayMember = "Name";
                cbDatabases.ValueMember = "Name";
                if (!string.IsNullOrEmpty(this.curDatabaseName))
                {
                    cbDatabases.SelectedValue = this.curDatabaseName;
                }
            }
        }

        private DatabaseLink DBLink;
        private ServerInfo SrvInfo;
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.SrvInfo = new ServerInfo(this.DBLink.ConnectionString, this.DBLink.ServerName);
                this.SrvInfo.LoadDatabases();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DBLink.SetDatabaseName(cbDatabases.Text);
            ConnectionSetting.Add(this.DBLink);

            SystemConfig.DBLink = this.DBLink;
            SystemConfig.SrvInfo = this.SrvInfo;
            SystemConfig.DBName = cbDatabases.Text;

            //IDatabaseInfo dbi = SystemConfig.SrvInfo.GetDatabase(SystemConfig.DBName);
            //SystemConfig.Instance.Database = DatabaseSchemaSetting.Compute(dbi);

            //this.DialogResult = DialogResult.OK;
            //this.Close();

            cbServerType.Enabled = false;
            cbServerName.Enabled = false;
            rbWindows.Enabled = false;
            rbSQLServer.Enabled = false;
            txtLoginName.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Enabled = false;
            cbDatabases.Enabled = false;
            btnOK.Enabled = false;
            btnCancel.Enabled = false;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork2);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted2);
            worker.RunWorkerAsync();
        }

        void worker_RunWorkerCompleted2(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void worker_DoWork2(object sender, DoWorkEventArgs e)
        {
            try
            {
                IDatabaseInfo dbi = SystemConfig.SrvInfo.GetDatabase(SystemConfig.DBName);
                SystemConfig.Instance.Database = DatabaseSchemaSetting.Compute(dbi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK);
            }
        }

        private void cbServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisabledOKBtn();
        }

        private void DisabledOKBtn()
        {
            cbDatabases.DataSource = null;
            cbDatabases.Items.Clear();
            btnOK.Enabled = false;
            btnLogin.Enabled = true;
        }

        private void cbServerName_TextChanged(object sender, EventArgs e)
        {
            DisabledOKBtn();
        }

        private void cbServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisabledOKBtn();
        }


    }
}

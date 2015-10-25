using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotNet.DBSchema;
using System.Diagnostics;

namespace CKGen
{
    public partial class FrmSelectTable : Form
    {
        //disable close button
        //private const int CP_NOCLOSE_BUTTON = 0x200;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams myCp = base.CreateParams;
        //        myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
        //        return myCp;
        //    }
        //}

        //removing the entire system menu
        private const int WS_SYSMENU = 0x80000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_SYSMENU;
                return cp;
            }
        }

        public DatabaseLink DBLink;
        private ServerInfo SrvInfo;
        public TableInfo DBTable;

        public FrmSelectTable()
        {
            InitializeComponent();
        }

        private void ShowLinkForm()
        {
            this.Text = "连接数据库";
            this.panel2.Visible = false;

            this.cbType.DataSource = typeof(DatabaseType).ToList();
            this.cbType.DisplayMember = "Value";
            this.cbType.ValueMember = "Key";

            this.panel1.Visible = true;
            this.panel1.Dock = DockStyle.Fill;
            this.Width = 500;
            this.Height = 250;

            this.CancelButton = this.btnLinkCancel;
            this.AcceptButton = this.btnLink;
        }

        private void ShowSelectForm()
        {
            this.Text = "选择主表";
            this.panel1.Visible = false;

            this.panel2.Visible = true;
            this.panel2.Dock = DockStyle.Fill;
            this.Width = 500;
            this.Height = 500;

            this.CancelButton = this.btnCancel;
            this.AcceptButton = this.btnOk;
        }

        private void FrmSelectTable_Load(object sender, EventArgs e)
        {
            if (this.DBLink != null)
            {
                this.ShowSelectForm();
            }
            else
            {
                this.ShowLinkForm();
            }
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLink_Click(object sender, EventArgs e)
        {
            this.cbType.Enabled = false;
            this.txtDBName.Enabled = false;
            this.txtLoginName.Enabled = false;
            this.txtPassword.Enabled = false;
            this.btnLink.Enabled = false;

            this.DBLink = new DatabaseLink((DatabaseType)this.cbType.SelectedValue, this.txtDBName.Text, this.txtLoginName.Text, this.txtPassword.Text, false);

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.cbType.Enabled = true;
            this.txtDBName.Enabled = true;
            this.txtLoginName.Enabled = true;
            this.txtPassword.Enabled = true;
            this.btnLink.Enabled = true;

            if (this.SrvInfo != null && this.SrvInfo.Databases != null && this.SrvInfo.Databases.Count > 0)
            {
                ShowSelectForm();

                cbDatabases.DataSource = this.SrvInfo.Databases;
                cbDatabases.DisplayMember = "Name";
                cbDatabases.ValueMember = "Name";

                lbTables.DataSource = this.SrvInfo.Databases[0].Tables;
                lbTables.DisplayMember = "RawName";
                lbTables.ValueMember = "RawName";
                SetColumnWith(lbTables);
                
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ServerInfo serverInfo = new ServerInfo(this.DBLink.ConnectionString, this.DBLink.ServerName);
                serverInfo.LoadDatabases();                
                this.SrvInfo = serverInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 取消连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbDatabases.SelectedValue != null && lbTables.SelectedValue != null)
            {
                string dbname = cbDatabases.SelectedValue.ToString();
                this.DBLink.SetDatabaseName(dbname);
                this.DBTable = lbTables.SelectedValue as TableInfo;
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SrvInfo != null)
            {
                lbTables.DataSource = null;
                foreach (var db in this.SrvInfo.Databases)
                {
                    if (db.Name == cbDatabases.SelectedValue.ToString())
                    {
                        lbTables.Items.Clear();
                        lbTables.DataSource = db.Tables;
                        lbTables.DisplayMember = "RawName";
                        lbTables.ValueMember = "RawName";
                    }
                }
                SetColumnWith(lbTables);
            }
        }

        private void SetColumnWith(ListBox lb)
        {
            List<TableInfo> ds = lb.DataSource as List<TableInfo>;
            if (ds != null)
            {
                string txt = (from p in ds
                              orderby p.RawName.Length descending
                              select p.RawName).First();
                int width = (int)lb.CreateGraphics().MeasureString(txt, lb.Font).Width;
                lb.ColumnWidth = width;
            }
        }

        private void lbTables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbTables.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string dbname = cbDatabases.SelectedValue.ToString();
                this.DBLink.SetDatabaseName(dbname);
                this.DBTable = lbTables.Items[index] as TableInfo;
            }
        }

        private void txtDBName_Enter(object sender, EventArgs e)
        {
            SetEnglishLanguage();
        }

        private void txtLoginName_Enter(object sender, EventArgs e)
        {
            SetEnglishLanguage();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            SetEnglishLanguage();
        }

        private void SetEnglishLanguage()
        {
            foreach (InputLanguage item in InputLanguage.InstalledInputLanguages)
            {
                if (item.Culture.LCID == 1033)
                {
                    InputLanguage.CurrentInputLanguage = item;
                    break;
                }
            }
        }
    }
}

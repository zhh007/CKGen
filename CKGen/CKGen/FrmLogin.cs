using CKGen.DBLoader;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CKGen
{
    public partial class FrmLogin : Form
    {
        private System.Windows.Forms.ProgressBar bar;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Image img = CKGen.Properties.Resources.link;
            Bitmap myBitmap = new Bitmap(img);
            Icon myIcon = Icon.FromHandle(myBitmap.GetHicon());
            this.Icon = myIcon;

            this.bar = new ProgressBar();
            this.bar.Location = new System.Drawing.Point(18, 0);
            this.bar.Size = new System.Drawing.Size(367, 10);
            this.bar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.bar.Visible = false;
            this.Controls.Add(bar);

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
            bar.Visible = true;
            bar.Location = new Point(0, (this.ClientSize.Height - bar.Height));
            bar.Width = this.Width;

            IDatabaseInfo dbi = SystemConfig.SrvInfo.GetDatabase(SystemConfig.DBName);
            SystemConfig.Instance.Database = dbi;

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
                Stopwatch _stopWatch = new Stopwatch();
                _stopWatch.Start();
                LoadData();
                _stopWatch.Stop();
                Debug.WriteLine("执行时间:" + (_stopWatch.Elapsed.TotalMilliseconds * 1.0 / 1000).ToString(CultureInfo.InvariantCulture) + "秒");

                _stopWatch = new Stopwatch();
                _stopWatch.Start();
                DatabaseSchemaSetting.SyncToLocal(SystemConfig.Instance.Database);
                _stopWatch.Stop();
                Debug.WriteLine("执行时间:" + (_stopWatch.Elapsed.TotalMilliseconds * 1.0 / 1000).ToString(CultureInfo.InvariantCulture) + "秒");

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

        private void LoadData()
        {
            int size = SystemConfig.Instance.Database.Tables.Count;
            int N = size;
            int P = Environment.ProcessorCount; // assume twice the procs for 
                                                // good work distribution
            int Chunk = (int)Math.Round((double)N / (double)P, 0);// size of a work chunk
            AutoResetEvent signal = new AutoResetEvent(false);
            int counter = P;                        // use a counter to reduce 
                                                    // kernel transitions    

            Debug.WriteLine("size:{0}", size);
            Debug.WriteLine("P:{0}", P);
            Debug.WriteLine("Chunk:{0}", Chunk);

            List<Thread> list = new List<Thread>();
            List<int> args = new List<int>();

            for (int c = 0; c < P; c++)
            {           // for each chunk
                Thread th = new Thread(new ParameterizedThreadStart((o) =>
                {
                    int lc = (int)o;
                    int start = lc * Chunk;// iterate through a work chunk
                    int end = (lc + 1 == P ? N : (lc + 1) * Chunk);// respect upper bound
                    Debug.WriteLine("{0} : {1} ~ {2}", lc, start, end);

                    for (int i = start; i < end; i++)
                    {
                        if (i < SystemConfig.Instance.Database.Tables.Count)
                        {
                            ITableInfo tb = SystemConfig.Instance.Database.Tables[i];
                            tb.LoadColumns();
                        }
                    }
                    if (Interlocked.Decrement(ref counter) == 0)
                    { // use efficient 
                      // interlocked 
                      // instructions      
                        signal.Set();  // and kernel transition only when done
                    }
                }));
                th.IsBackground = true;
                list.Add(th);
                args.Add(c);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Start(args[i]);
            }
            
            signal.WaitOne();
        }
        
    }
}

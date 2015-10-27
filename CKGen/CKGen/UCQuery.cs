using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CKGen
{
    public partial class UCQuery : UserControl
    {
        DataGridView dgv = null;
        public UCQuery()
        {
            InitializeComponent();

            dgv = new DataGridView();
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;
            dgv.AutoGenerateColumns = true;
            dgv.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(dgv);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _query();
        }

        public void Query(string txt)
        {
            this.txtCode.Text = txt;
            _query();
        }

        public void _query()
        {
            dgv.DataSource = null;

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(SystemConfig.DBLink.ConnectionStringForExecute))
            {
                SqlCommand cmd = new SqlCommand(this.txtCode.Text, conn);
                try
                {
                    SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                    sdr.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                    if (conn != null)
                        conn.Dispose();
                }
            }

            dgv.DataSource = ds.Tables[0].DefaultView;
        }
    }
}

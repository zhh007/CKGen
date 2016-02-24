using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CKGen.Controls
{
    public partial class SQLQueryView : UserControl
    {
        private DataGridView dgv = null;
        private TextBox txtMsg = null;

        public SQLQueryView()
        {
            InitializeComponent();
        }

        private void UCQuery_Load(object sender, EventArgs e)
        {
            Tool2.Items.Clear();

            dgv = new DataGridView();
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;
            dgv.AutoGenerateColumns = true;
            dgv.Dock = DockStyle.Fill;

            txtMsg = new TextBox();
            txtMsg.Multiline = true;
            txtMsg.Dock = DockStyle.Fill;
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
            Tool2.Items.Clear();
            pBox.Controls.Clear();
            DataSet ds = new DataSet();
            int? count = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(App.Instance.DBLink.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(this.txtCode.Text, conn);
                    try
                    {
                        SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                        count = sdr.Fill(ds);
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
            }
            catch (Exception ex)
            {
                txtMsg.ForeColor = System.Drawing.Color.Red;
                txtMsg.Text = ex.Message;
                StatusLabel.Image = global::CKGen.Properties.Resources.error;
                StatusLabel.Text = "查询已完成，但有错误。";
            }

            if(count.HasValue)
            {
                dgv.DataSource = ds.Tables[0].DefaultView;
                txtMsg.ForeColor = System.Drawing.SystemColors.WindowText;
                txtMsg.Text = string.Format("返回{0}条记录。", count);
                StatusLabel.Image = global::CKGen.Properties.Resources.success;
                StatusLabel.Text = "查询已成功执行。";
            }

            List<ToolStripItem> titems = new List<ToolStripItem>();
            if(count.HasValue)
            {
                titems.Add(btnResult);
                pBox.Controls.Add(dgv);
            }
            else
            {
                pBox.Controls.Add(txtMsg);
            }
            titems.Add(btnMessage);
            titems.Add(btnGenCode);
            if(count.HasValue)
            {
                titems.Add(btnExport);
            }

            Tool2.Items.Clear();
            Tool2.Items.AddRange(titems.ToArray());
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            pBox.Controls.Clear();
            pBox.Controls.Add(dgv);
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            pBox.Controls.Clear();
            pBox.Controls.Add(txtMsg);
        }

        private void btnGenCode_Click(object sender, EventArgs e)
        {
            
        }

        private void menuExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void menuExportWord_Click(object sender, EventArgs e)
        {

        }

        private void menuExportPDF_Click(object sender, EventArgs e)
        {

        }

        private void menuExportHTML_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CKGen.Temp.Adonet;

namespace CKGen.Controls
{
    public partial class SQLQueryView : UserControl
    {
        private DataGridView dgv = null;
        private TextBox txtMsg = null;
        private DataSet queryDataSet = null;
        private DataSet ds = null;

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
            StatusLabel.Text = "就绪。";
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
            if (!string.IsNullOrEmpty(txt))
            {
                this.txtCode.Text = txt;
                _query();
            }
        }

        public void _query()
        {
            dgv.DataSource = null;
            Tool2.Items.Clear();
            pBox.Controls.Clear();
            ds = new DataSet();
            queryDataSet = new DataSet();
            int? count = null;
            bool hasError = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(App.Instance.DBLink.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(this.txtCode.Text, conn);
                    try
                    {
                        SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                        count = sdr.Fill(ds);
                        sdr.FillSchema(queryDataSet, SchemaType.Mapped);
                        if(ds != null && ds.Tables != null && ds.Tables.Count == 0)
                        {
                            count = null;
                        }
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
                hasError = true;
                txtMsg.ForeColor = System.Drawing.Color.Red;
                txtMsg.Text = ex.Message;
                StatusLabel.Image = global::CKGen.Properties.Resources.error;
                StatusLabel.Text = "查询已完成，但有错误。";
            }

            List<ToolStripItem> titems = new List<ToolStripItem>();
            if (!hasError)
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dgv.DataSource = ds.Tables[0].DefaultView;
                    txtMsg.ForeColor = System.Drawing.SystemColors.WindowText;
                    txtMsg.Text = string.Format("返回{0}条记录。", count);
                    StatusLabel.Image = global::CKGen.Properties.Resources.success;
                    StatusLabel.Text = "查询已成功执行。";

                    titems.Add(btnResult);
                    pBox.Controls.Add(dgv);
                    titems.Add(btnMessage);
                    titems.Add(btnGenCode);

                    if (count.HasValue && count > 0)
                    {
                        titems.Add(btnExport);
                    }
                }
                else
                {
                    txtMsg.ForeColor = System.Drawing.SystemColors.WindowText;
                    txtMsg.Text = string.Format("执行成功。");
                    StatusLabel.Image = global::CKGen.Properties.Resources.success;
                    StatusLabel.Text = "执行成功。";

                    pBox.Controls.Add(txtMsg);
                    titems.Add(btnMessage);
                    titems.Add(btnGenCode);
                }
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

        //取消查询
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        //生成代码
        private void btnGenCode_Click(object sender, EventArgs e)
        {
            DbQueryCodeGen gen = new DbQueryCodeGen();
            string code = "";
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                code = gen.GenForQueryList(txtCode.Text, queryDataSet, App.Instance.DBLink.ConnectionString);
            }
            else
            {
                code = gen.GenForExecuteNoQuery(txtCode.Text, App.Instance.DBLink.ConnectionString);
            }

            FrmShowCode frm = new FrmShowCode();
            frm.SetCode(code);
            frm.Show();

        }

        //导出数据
        private void btnExport_Click(object sender, EventArgs e)
        {

        }
    }
}

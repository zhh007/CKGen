﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CKGen.Temp.Adonet;
using System.IO;
using System.Diagnostics;
using System.Threading;
using CKGen.DBSchema;
using CKGen.Base.Log;

namespace CKGen.Controls
{
    public partial class SQLQueryView : UserControl
    {
        private Panel resultPanel = null;
        private TextBox txtMsg = null;
        private DataSet queryDataSet = null;
        private DataSet ds = null;
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private string sql = "";
        private bool cancel = false;

        public SQLQueryView()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                Query(this.txtCode.Text);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                CancelQuery();
            }
            else if (keyData == (Keys.Control | Keys.R))
            {
                splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UCQuery_Load(object sender, EventArgs e)
        {
            btnRun.Enabled = true;
            btnCancel.Enabled = false;

            Tool2.Items.Clear();
            List<ToolStripItem> titems2 = new List<ToolStripItem>();
            titems2.Add(btnMessage);
            Tool2.Items.AddRange(titems2.ToArray());

            txtMsg = new TextBox();
            txtMsg.ReadOnly = true;
            txtMsg.Multiline = true;
            txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtMsg.Dock = DockStyle.Fill;
            pBox.Controls.Add(txtMsg);

            StatusLabel.Image = global::CKGen.Properties.Resources.link;
            StatusLabel.Text = "就绪。";

            splitContainer1.Panel2Collapsed = true;

            resultPanel = new Panel();
            resultPanel.Dock = DockStyle.Fill;
            resultPanel.BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            Query(this.txtCode.Text);
        }

        public void Query(string txt)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                btnRun.Enabled = false;
                btnCancel.Enabled = true;
                sql = txt;
                this.txtCode.Text = txt;
                Thread th = new Thread(new ThreadStart(Thread_Query));
                th.Start();
            }
        }

        private void Thread_Query()
        {
            this.BeginInvoke(new Action(() =>
            {
                Tool2.Items.Clear();
                pBox.Controls.Clear();

                StatusLabel.Image = global::CKGen.Properties.Resources.loading;
                StatusLabel.Text = "正在查询...";
            }));

            ds = new DataSet();
            queryDataSet = new DataSet();
            this.cancel = false;
            int? count = null;
            bool hasError = false;
            try
            {
                using (conn = new SqlConnection(App.Instance.DBLink.ConnectionString))
                {
                    try
                    {
                        cmd = new SqlCommand(this.sql, conn);
                        SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                        count = sdr.Fill(ds);
                        sdr.FillSchema(queryDataSet, SchemaType.Mapped);
                        if (ds != null && ds.Tables != null && ds.Tables.Count == 0)
                        {
                            count = null;
                        }
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
                if (!this.cancel)
                {
                    hasError = true;
                    this.BeginInvoke(new Action(() =>
                    {
                        btnRun.Enabled = true;
                        btnCancel.Enabled = false;
                        txtMsg.ForeColor = System.Drawing.Color.Red;
                        txtMsg.Text = ex.Message;
                        pBox.Controls.Add(txtMsg);

                        StatusLabel.Image = global::CKGen.Properties.Resources.Error;
                        StatusLabel.Text = "查询已完成，但有错误。";

                        List<ToolStripItem> titems2 = new List<ToolStripItem>();
                        titems2.Add(btnMessage);
                        Tool2.Items.AddRange(titems2.ToArray());
                    }));
                }
            }

            this.BeginInvoke(new Action(() =>
            {
                splitContainer1.Panel2Collapsed = false;
            }));

            if (!this.cancel)
            {
                this.BeginInvoke(new Action(() =>
                {
                    btnRun.Enabled = true;
                    btnCancel.Enabled = false;
                    List<ToolStripItem> titems = new List<ToolStripItem>();
                    if (!hasError)
                    {
                        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                        {
                            showDataView(ds);

                            txtMsg.ForeColor = System.Drawing.SystemColors.WindowText;
                            txtMsg.Text = string.Format("返回{0}条记录。", count);

                            StatusLabel.Image = global::CKGen.Properties.Resources.success;
                            StatusLabel.Text = "查询已成功执行。";

                            titems.Add(btnResult);
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
                            pBox.Controls.Add(txtMsg);

                            StatusLabel.Image = global::CKGen.Properties.Resources.success;
                            StatusLabel.Text = "执行成功。";

                            titems.Add(btnMessage);
                            titems.Add(btnGenCode);
                        }
                    }

                    Tool2.Items.AddRange(titems.ToArray());
                }));
            }
        }

        private void showDataView(DataSet ds)
        {
            if (resultPanel.Controls.Count > 0)
            {
                foreach (var ctrl in resultPanel.Controls)
                {
                    if (ctrl is DataGridView)
                    {
                        var dgv = resultPanel.Controls[0] as DataGridView;
                        if (dgv != null)
                        {
                            dgv.CellFormatting -= Dgv_CellFormatting;
                            dgv.DataError -= Dgv_DataError;
                        }
                    }
                }
                resultPanel.Controls.Clear();
            }

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataGridView dgv = new DataGridView();
                dgv.CellFormatting += Dgv_CellFormatting;
                dgv.DataError += Dgv_DataError;
                dgv.BorderStyle = BorderStyle.None;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.MultiSelect = false;
                dgv.AutoGenerateColumns = false;

                foreach (DataColumn col in ds.Tables[i].Columns)
                {
                    var newcol = new DataGridViewTextBoxColumn();
                    newcol.DataPropertyName = col.ColumnName;
                    newcol.HeaderText = col.ColumnName;
                    newcol.ValueType = typeof(string);
                    dgv.Columns.Add(newcol);
                }
                dgv.DataSource = ds.Tables[i].DefaultView;

                if (ds.Tables.Count == 1)
                {
                    dgv.Dock = DockStyle.Fill;
                }
                else
                {
                    dgv.Width = pBox.Width;
                    dgv.Height = 200;
                    dgv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    dgv.Top = 205 * i;
                }
                resultPanel.Controls.Add(dgv);
            }
            pBox.Controls.Add(resultPanel);
        }

        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is byte[])
            {
                e.Value = ((byte[])e.Value).ToHexString();//"<二进制数据>";
                e.FormattingApplied = true;
            }
        }

        private void Dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            //view.Rows[e.RowIndex].ErrorText = "an error";
            view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "解析失败";
            LogHelper.Error(e.Exception);
            e.ThrowException = false;
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            pBox.Controls.Clear();
            pBox.Controls.Add(resultPanel);
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            pBox.Controls.Clear();
            pBox.Controls.Add(txtMsg);
        }

        //取消查询
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelQuery();
        }

        private void CancelQuery()
        {
            btnCancel.Enabled = false;
            Thread th = new Thread(new ThreadStart(Thread_Cancel));
            th.Start();
        }

        private void Thread_Cancel()
        {
            if (conn != null && cmd != null && conn.State != ConnectionState.Closed)
            {
                this.cancel = true;
                cmd.Cancel();
                this.BeginInvoke(new Action(() =>
                {
                    txtMsg.ForeColor = System.Drawing.SystemColors.WindowText;
                    txtMsg.Text = "查询已取消。";
                    StatusLabel.Image = global::CKGen.Properties.Resources.Error;
                    StatusLabel.Text = "查询已取消。";

                    List<ToolStripItem> titems = new List<ToolStripItem>();
                    titems.Add(btnMessage);
                    Tool2.Items.Clear();
                    Tool2.Items.AddRange(titems.ToArray());

                    pBox.Controls.Add(txtMsg);
                }));
            }
            this.BeginInvoke(new Action(() =>
            {
                btnRun.Enabled = true;
                btnCancel.Enabled = false;
            }));
        }

        //生成代码
        private void btnGenCode_Click(object sender, EventArgs e)
        {
            SQLQueryCodeGenWizard wizard = new SQLQueryCodeGenWizard();
            wizard.SQL = txtCode.Text;
            wizard.ds = this.ds;
            wizard.ShowDialog();
        }

        //导出数据
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                Guid folderID = Guid.NewGuid();
                string folder = Path.Combine(Path.GetTempPath(), folderID.ToString("P"));
                Directory.CreateDirectory(folder);

                string filepath = Path.Combine(folder, string.Format("file_{0:yyyyMMddHHmmss}.xls", DateTime.Now));
                ExcelHelper.ExportFile(ds, filepath);
                Process.Start(folder);
            }
        }

        private void txtCode_DragEnter(object sender, DragEventArgs e)
        {
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (draggedNode != null && (draggedNode.Tag is ITableInfo || draggedNode.Tag is IViewInfo || draggedNode.Tag is IProcedureInfo))
            {
                e.Effect = e.AllowedEffect;
            }
        }

        private void txtCode_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (draggedNode != null)
            {
                string txt = GetDragText(draggedNode, (e.KeyState & 8) == 8);
                int pos = txtCode.SelectionStart;
                txtCode.Text = txtCode.Text.Insert(pos, txt);
                txtCode.SelectionStart = pos + txt.Length;
                txtCode.SelectionLength = 0;
            }
        }

        /// <remarks>
        /// GetCharIndexFromPosition is missing one caret position, as there is one extra caret
        /// position than there are characters (an extra one at the end).
        /// </remarks>
        private int GetCaretIndexFromPoint(System.Windows.Forms.TextBox box, int x, int y)
        {
            Point realPoint = box.PointToClient(new Point(x, y));
            int index = box.GetCharIndexFromPosition(realPoint);
            if (index == box.Text.Length - 1)
            {
                Point caretPoint = box.GetPositionFromCharIndex(index);
                if (realPoint.X > caretPoint.X)
                {
                    index += 1;
                }
            }
            return index;
        }

        private void txtCode_DragOver(object sender, DragEventArgs e)
        {
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (draggedNode != null)
            {
                // fake moving the text caret
                txtCode.SelectionStart = GetCaretIndexFromPoint(txtCode, e.X, e.Y);
                txtCode.SelectionLength = 0;
                // don't forget to set focus to the text box to make the caret visible!
                txtCode.Focus();
            }
        }

        private string GetDragText(TreeNode draggedNode, bool ctrlDown)
        {
            StringBuilder sb = new StringBuilder();
            if (draggedNode.Tag is ITableInfo)
            {
                var tInfo = draggedNode.Tag as ITableInfo;
                if (ctrlDown)
                {
                    for (int i = 0; i < tInfo.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            sb.Append(", ");
                        }
                        sb.Append(tInfo.Columns[i].Name);
                    }
                }
                else
                {
                    sb.Append(tInfo.Name);
                }
            }
            else if (draggedNode.Tag is IViewInfo)
            {
                var vInfo = draggedNode.Tag as IViewInfo;
                if (ctrlDown)
                {
                    for (int i = 0; i < vInfo.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            sb.Append(", ");
                        }
                        sb.Append(vInfo.Columns[i].Name);
                    }
                }
                else
                {
                    sb.Append(vInfo.Name);
                }
            }
            else if (draggedNode.Tag is IProcedureInfo)
            {
                var procInfo = draggedNode.Tag as IProcedureInfo;
                sb.Append(procInfo.Name);
            }
            else
            {

            }

            return sb.ToString();
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }
    }
}

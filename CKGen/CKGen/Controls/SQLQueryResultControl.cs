using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CKGen.Base.CodeModel;

namespace CKGen.Controls
{
    public partial class SQLQueryResultControl : UserControl
    {
        private string _bakRowClassName = "";
        private DataTable _dbtable = null;

        public string RowClassName
        {
            get
            {
                return this.txtRowClassName.Text;
            }
        }

        public DataTable DataTable
        {
            get
            {
                return this._dbtable;
            }
        }

        public bool HasMoreResult
        {
            set
            {
                if (value)
                {
                    rbExecuteNonQuery.Hide();
                    rbExecuteScalar.Hide();
                }
            }
        }

        public SQLQueryExecuteType QueryExecuteType
        {
            get
            {
                if(rbReadRows.Checked)
                {
                    return SQLQueryExecuteType.ReadRows;
                }
                else if(rbReadOneRow.Checked)
                {
                    return SQLQueryExecuteType.ReadOneRow;
                }
                else if(rbExecuteScalar.Checked)
                {
                    return SQLQueryExecuteType.ExecuteSclor;
                }
                else
                {
                    return SQLQueryExecuteType.ExecuteNoQuery;
                }
            }
        }

        public SQLQueryResultControl()
        {
            InitializeComponent();
            this.dgvSchema.AutoGenerateColumns = true;
        }

        public SQLQueryResultControl(DataTable dt, int index)
            :this()
        {
            this._dbtable = dt;
            if (this._dbtable != null)
            {
                if(dt == null || dt.Columns == null || dt.Columns.Count == 0)
                {
                    rbReadRows.Enabled = false;
                    rbReadOneRow.Enabled = false;
                    rbExecuteScalar.Enabled = false;
                    rbExecuteNonQuery.Checked = true;
                }
                else
                {
                    rbExecuteNonQuery.Enabled = false;
                    if (dt.Columns.Count == 1)
                    {
                        txtRowClassName.Enabled = false;
                        if (dt.Rows.Count <= 1)
                        {
                            rbReadOneRow.Enabled = false;
                            rbExecuteScalar.Checked = true;
                        }
                        else
                        {
                            rbExecuteScalar.Enabled = false;
                            rbReadRows.Checked = true;
                        }
                    }
                    
                    if(dt.Columns.Count > 1)
                    {
                        rbExecuteScalar.Enabled = false;
                        if (dt.Rows.Count <= 1)
                        {
                            rbReadOneRow.Checked = true;
                            txtRowClassName.Text = string.Format("Result{0}", index);
                        }
                        else
                        {
                            rbReadRows.Checked = true;
                            txtRowClassName.Text = string.Format("Result{0}", index);
                        }
                    }
                }

                this._dbtable.Rows.Clear();
                dgvSchema.DataSource = this._dbtable.DefaultView;
            }

            groupBox1.Text = string.Format("返回值{0}", index);
        }

        private void QueryExecuteTypeChanged(object sender, EventArgs e)
        {
            txtRowClassName.Enabled = rbReadRows.Checked || rbReadOneRow.Checked;
            if (!txtRowClassName.Enabled)
            {
                if (!string.IsNullOrEmpty(txtRowClassName.Text))
                {
                    this._bakRowClassName = txtRowClassName.Text;
                    txtRowClassName.Text = "";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtRowClassName.Text))
                {
                    txtRowClassName.Text = this._bakRowClassName;
                    this._bakRowClassName = "";
                }
            }
        }
    }
}

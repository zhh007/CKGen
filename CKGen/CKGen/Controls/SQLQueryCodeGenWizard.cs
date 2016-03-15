using CKGen.Base;
using CKGen.Base.CodeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CKGen.Controls
{
    public partial class SQLQueryCodeGenWizard : Form
    {
        public string SQL { get; set; }
        public MethodDefine MethodInfo = new MethodDefine();
        public List<ClassDefine> ResultTypeList = new List<ClassDefine>();
        public SQLQueryCodeGenWizard()
        {
            InitializeComponent();

            codePage.Initialize += CodePage_Initialize;
            codePage.Commit += CodePage_Commit;

            //if (MethodInfo == null || MethodInfo.Count == 0)
            //{
            //    QueryParamPage.Suppress = true;//隐藏参数页
            //}
        }

        #region [1] Code Page
        private void CodePage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            txtSQL.Text = this.SQL;
        }

        private void CodePage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            this.SQL = txtSQL.Text;
            MethodInfo.Params = GetMethodParams(this.SQL);

            List<DataGridViewRow> rlist = new List<DataGridViewRow>();
            dgvParam.Rows.Clear();
            foreach (var item in MethodInfo.Params)
            {
                int rowIndex = dgvParam.Rows.Add();
                DataGridViewRow row = dgvParam.Rows[rowIndex];
                row.Cells[0].Value = item.ParamName;

                DataGridViewComboBoxCell cboCol = (DataGridViewComboBoxCell)row.Cells[1];
                if (!cboCol.Items.Contains(item.ParamType))
                {
                    cboCol.Items.Add(item.ParamType);
                }
                row.Cells[1].Value = item.ParamType;

                row.Cells[2].Value = item.AllowNull ? true : false;
                row.Cells[3].Value = item.Remark;
            }
            dgvParam.Rows.AddRange(rlist.ToArray());
        }

        private List<ParamDefine> GetMethodParams(string sql)
        {
            List<ParamDefine> list = new List<ParamDefine>();
            try
            {
                Regex regexObj = new Regex(@"\s+@([a-z_0-9]+)\s+([a-z_0-9()]+)");
                Match matchResults = regexObj.Match(sql);
                while (matchResults.Success)
                {
                    ParamDefine pd = new ParamDefine();
                    pd.ParamName = matchResults.Groups[1].Value;
                    pd.ParamType = matchResults.Groups[2].Value;
                    list.Add(pd);
                    matchResults = matchResults.NextMatch();
                }
            }
            catch (Exception)
            {
            }
            return list;
        }
        #endregion

        #region [2] Query Param Page
        //增加查询参数
        private void btnAddParam_Click(object sender, EventArgs e)
        {
            dgvParam.Rows.Add();
        }

        //删除查询参数
        private void btnRemoveParam_Click(object sender, EventArgs e)
        {
            var rows = dgvParam.SelectedRows;
            if (rows != null && rows.Count > 0)
            {
                for (int i = rows.Count - 1; i >= 0; i--)
                {
                    dgvParam.Rows.Remove(rows[i]);
                }
            }
        }

        private void dgvParam_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvParam.CurrentCell.ColumnIndex == 1)
            {
                ComboBox combo = e.Control as ComboBox;
                if (combo == null)
                    return;
                combo.DropDownStyle = ComboBoxStyle.DropDown;
                combo.Validating -= new CancelEventHandler(cbo_Validating);
                combo.Validating += new CancelEventHandler(cbo_Validating);
            }
        }

        private void cbo_Validating(object sender, CancelEventArgs e)
        {
            DataGridViewComboBoxEditingControl cbo = sender as DataGridViewComboBoxEditingControl;
            DataGridView grid = cbo.EditingControlDataGridView;
            object value = cbo.Text;
            if (cbo.Items.IndexOf(value) == -1)
            {
                DataGridViewComboBoxCell cboCol = (DataGridViewComboBoxCell)grid.CurrentCell;
                cbo.Items.Add(value);
                cboCol.Items.Add(value);
                grid.CurrentCell.Value = value;
            }
        }

        private void dgvParam_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvParam.CurrentCell is DataGridViewComboBoxCell)
            {
                dgvParam.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgvParam.EndEdit();
            }
        }
        #endregion
    }
}

using CKGen.Base;
using CKGen.Base.CodeModel;
using CKGen.Base.Form;
using CKGen.DBSchema;
using CKGen.Temp.Adonet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CKGen.Controls
{
    public partial class SQLQueryCodeGenWizard : Form
    {
        public string SQL { get; set; }
        public MethodDefine MethodInfo = new MethodDefine();
        public List<ClassDefine> ResultTypeList = new List<ClassDefine>();
        public DataSet ds { get; set; }
        public List<SQLQueryResultControl> ResultList = new List<SQLQueryResultControl>();
        public SQLQueryCodeGenWizard()
        {
            InitializeComponent();

            codePage.Initialize += CodePage_Initialize;
            codePage.Commit += CodePage_Commit;
            QueryParamPage.Initialize += QueryParamPage_Initialize;
            QueryParamPage.Commit += QueryParamPage_Commit;
            ResultSettingPage.Initialize += ResultSettingPage_Initialize;
            ResultSettingPage.Commit += ResultSettingPage_Commit;

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
        }

        private List<MethodParamDefine> GetMethodParams(string sql)
        {
            List<MethodParamDefine> list = new List<MethodParamDefine>();
            try
            {
                Regex regexObj = new Regex(@"\s+@([a-z_0-9]+)\s+([a-z_0-9()]+)");
                Match matchResults = regexObj.Match(sql);
                while (matchResults.Success)
                {
                    MethodParamDefine pd = new MethodParamDefine();
                    pd.ParamName = matchResults.Groups[1].Value;
                    pd.ParamType = matchResults.Groups[2].Value.ToLower();
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
        private void QueryParamPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
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

        private void QueryParamPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {

        }

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

        #region [3] Result Setting Page
        private void ResultSettingPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                resultSettingPanel.Controls.Clear();
                int dtcount = ds.Tables.Count;
                for (int i = 0; i < dtcount; i++)
                {
                    var dt = ds.Tables[i].Copy();
                    var mctrl = new SQLQueryResultControl(dt, i + 1);
                    ResultList.Add(mctrl);
                    mctrl.Location = new System.Drawing.Point(0, 160 * i);
                    mctrl.Size = new System.Drawing.Size(600, 160);
                    mctrl.HasMoreResult = dtcount > 1;
                    resultSettingPanel.Controls.Add(mctrl);
                }
            }
        }

        private void ResultSettingPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (ResultList == null || ResultList.Count == 0)
                return;

            DbQueryCodeGen gen = new DbQueryCodeGen();
            string code = string.Empty;
            string connstr = App.Instance.DBLink.ConnectionString;
            if (ResultList.Count == 1)
            {
                var ctrl = ResultList[0];
                Module module = new Module();
                module.ModuleName = ctrl.RowClassName;
                module.CodeName = ctrl.RowClassName;
                if (ctrl.DataTable != null)
                {
                    foreach (DataColumn dc in ctrl.DataTable.Columns)
                    {
                        ModuleField mf = new ModuleField(module, "", dc.ColumnName, dc.ColumnName);
                        mf.DataType = dc.DataType;
                        mf.Nullable = dc.AllowDBNull;
                        mf.LanguageType = LanguageConvert.GetCSharpType(dc.DataType, dc.AllowDBNull);
                        //Debug.WriteLine("{0} ---> {1}", mf.FieldName, mf.LanguageType);
                        module.Fields.Add(mf);
                    }
                }
                if (ctrl.QueryExecuteType == SQLQueryExecuteType.ReadRows)
                {
                    code = gen.GenForQueryList(this.SQL, module, connstr);
                }
                else if (ctrl.QueryExecuteType == SQLQueryExecuteType.ReadOneRow)
                {
                    code = gen.GenForQueryOne(this.SQL, module, connstr);
                }
                else if (ctrl.QueryExecuteType == SQLQueryExecuteType.ExecuteSclor)
                {
                    if (ctrl.DataTable != null && ctrl.DataTable.Columns != null && ctrl.DataTable.Columns.Count > 0)
                    {
                        var dc = ctrl.DataTable.Columns[0];
                        Type t = dc.DataType;
                        bool allowDBNull = dc.AllowDBNull;
                        code = gen.GenForExecuteScalar(this.SQL, t, allowDBNull, connstr);
                    }
                }
                else
                {
                    code = gen.GenForExecuteNoQuery(this.SQL, connstr);
                }
            }
            else
            {

            }

            CodeView codeView = new CodeView();
            codeView.Dock = DockStyle.Fill;
            ResultPage.Controls.Add(codeView);
            codeView.Show(code);
        }
        #endregion
    }
}

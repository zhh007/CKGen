using CKGen.Base.CodeModel;
using CKGen.DBSchema;
using CKGen.Temp.Adonet.Snippet;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CKGen.Controls
{
    public partial class FrmTableSnippetGen : Form
    {
        public ITableInfo Table { get; set; }
        
        private ColumnSelectControl ctrlWhere = null;
        private ColumnOrderByControl ctrlOrderBy = null;
        private CodeView ctrlCodeView = null;
        private List<IColumnInfo> WhereColumns = new List<IColumnInfo>();
        private List<OrderByItem> OrderByItems = new List<OrderByItem>();
        private TableQueryGenType GenType = TableQueryGenType.Get;

        public FrmTableSnippetGen()
        {
            InitializeComponent();

            ctrlOrderBy = new ColumnOrderByControl();
            ctrlOrderBy.Dock = DockStyle.Fill;
            OrderByPage.Controls.Add(ctrlOrderBy);

            ctrlWhere = new ColumnSelectControl();
            ctrlWhere.Dock = DockStyle.Fill;
            WhereParamPage.Controls.Add(ctrlWhere);

            ctrlCodeView = new CodeView();
            ctrlCodeView.Dock = DockStyle.Fill;
            ResultPage.Controls.Add(ctrlCodeView);

            GenTypePage.Initialize += GenTypePage_Initialize;
            GenTypePage.Commit += GenTypePage_Commit;

            WhereParamPage.Initialize += WhereParamPage_Initialize;
            WhereParamPage.Commit += WhereParamPage_Commit;

            OrderByPage.Initialize += OrderByPage_Initialize;
            OrderByPage.Commit += OrderByPage_Commit;
        }

        private void GenTypePage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            switch (GenType)
            {
                case TableQueryGenType.Get:
                    rbGet.Checked = true;
                    break;
                case TableQueryGenType.GetList:
                    rbGetList.Checked = true;
                    break;
                case TableQueryGenType.Paged:
                    rbPaged.Checked = true;
                    break;
                case TableQueryGenType.Top:
                    rbTop.Checked = true;
                    break;
                case TableQueryGenType.Exist:
                    rbExist.Checked = true;
                    break;
                case TableQueryGenType.Count:
                    rbCount.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void GenTypePage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if(rbGet.Checked)
            {
                GenType = TableQueryGenType.Get;
            }
            if(rbGetList.Checked)
            {
                GenType = TableQueryGenType.GetList;
            }
            if(rbPaged.Checked)
            {
                GenType = TableQueryGenType.Paged;
            }
            if(rbTop.Checked)
            {
                GenType = TableQueryGenType.Top;
            }
            if(rbExist.Checked)
            {
                GenType = TableQueryGenType.Exist;
            }
            if(rbCount.Checked)
            {
                GenType = TableQueryGenType.Count;
            }
        }

        private void WhereParamPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            ctrlWhere.SetColumns(Table.Columns, WhereColumns);
        }

        private void WhereParamPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            WhereColumns = ctrlWhere.GetSelectedColumns();
        }

        private void OrderByPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            ctrlOrderBy.SetOrderByItems(Table.Columns, OrderByItems);
        }

        private void OrderByPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            OrderByItems = ctrlOrderBy.GetOrderByItems();

            DbSnippetGen gen = new DbSnippetGen();
            TableQueryGenModel model = new TableQueryGenModel();
            model.Table = this.Table;
            model.GenType = this.GenType;
            model.WhereColumns = this.WhereColumns;
            model.OrderBy = this.OrderByItems;
            model.ResultItemClassName = this.Table.PascalName;
            string code = gen.GenTableQueryCode(model);

            ctrlCodeView.Show(code);
        }
    }
}

using CKGen.Base.CodeModel;
using CKGen.DBSchema;
using CKGen.Temp.Adonet.Snippet;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CKGen.Controls
{
    public partial class FrmViewSnippetGen : Form
    {
        public IViewInfo View { get; set; }

        private ColumnSelectControl ctrlWhere = null;
        private ColumnOrderByControl ctrlOrderBy = null;
        private CodeView ctrlCodeView = null;
        private List<IColumnInfo> WhereColumns = new List<IColumnInfo>();
        private List<OrderByItem> OrderByItems = new List<OrderByItem>();
        private ViewQueryGenType GenType = ViewQueryGenType.Get;

        public FrmViewSnippetGen()
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
                case ViewQueryGenType.Get:
                    rbGet.Checked = true;
                    break;
                case ViewQueryGenType.GetList:
                    rbGetList.Checked = true;
                    break;
                case ViewQueryGenType.Paged:
                    rbPaged.Checked = true;
                    break;
                case ViewQueryGenType.Top:
                    rbTop.Checked = true;
                    break;
                case ViewQueryGenType.Exist:
                    rbExist.Checked = true;
                    break;
                case ViewQueryGenType.Count:
                    rbCount.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void GenTypePage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (rbGet.Checked)
            {
                GenType = ViewQueryGenType.Get;
            }
            if (rbGetList.Checked)
            {
                GenType = ViewQueryGenType.GetList;
            }
            if (rbPaged.Checked)
            {
                GenType = ViewQueryGenType.Paged;
            }
            if (rbTop.Checked)
            {
                GenType = ViewQueryGenType.Top;
            }
            if (rbExist.Checked)
            {
                GenType = ViewQueryGenType.Exist;
            }
            if (rbCount.Checked)
            {
                GenType = ViewQueryGenType.Count;
            }
        }

        private void WhereParamPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            ctrlWhere.SetColumns(View.Columns, WhereColumns);
        }

        private void WhereParamPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            WhereColumns = ctrlWhere.GetSelectedColumns();
        }

        private void OrderByPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            ctrlOrderBy.SetOrderByItems(View.Columns, OrderByItems);
        }

        private void OrderByPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            OrderByItems = ctrlOrderBy.GetOrderByItems();

            DbSnippetGen gen = new DbSnippetGen();
            ViewQueryGenModel model = new ViewQueryGenModel();
            model.View = this.View;
            model.GenType = this.GenType;
            model.WhereColumns = this.WhereColumns;
            model.OrderBy = this.OrderByItems;
            model.ResultItemClassName = this.View.PascalName;
            string code = gen.GenViewQueryCode(model);

            ctrlCodeView.Show(code);
        }
    }
}

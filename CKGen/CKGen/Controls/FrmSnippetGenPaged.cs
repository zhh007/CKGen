using CKGen.Base.CodeModel;
using CKGen.DBSchema;
using CKGen.Temp.Adonet.Snippet;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CKGen.Controls
{
    public partial class FrmSnippetGenPaged : Form
    {
        public ITableInfo Table { get; set; }
        
        private ColumnSelectControl ctrlWhere = null;
        private ColumnOrderByControl ctrlOrderBy = null;
        private CodeView ctrlCodeView = null;
        private List<IColumnInfo> WhereColumns = new List<IColumnInfo>();
        private List<OrderByItem> OrderByItems = new List<OrderByItem>();

        public FrmSnippetGenPaged()
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

            WhereParamPage.Initialize += WhereParamPage_Initialize;
            WhereParamPage.Commit += WhereParamPage_Commit;

            OrderByPage.Initialize += OrderByPage_Initialize;
            OrderByPage.Commit += OrderByPage_Commit;
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
            TablePagedModel model = new TablePagedModel();
            model.Table = this.Table;
            model.ParamColumns = this.WhereColumns;
            model.OrderBy = this.OrderByItems;
            model.ResultItemClassName = this.Table.PascalName;
            string code = gen.GenPagedCode(model);

            ctrlCodeView.Show(code);
        }
    }
}

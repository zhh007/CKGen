using CKGen.DBSchema;
using CKGen.Temp.Adonet.Snippet;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CKGen.Controls
{
    public partial class FrmSnippetGenCount : Form
    {
        public ITableInfo Table { get; set; }
        private List<IColumnInfo> SelectedColumns = new List<IColumnInfo>();
        private ColumnSelectControl ctrlColumnSelect = null;
        private CodeView ctrlCodeView = null;

        public FrmSnippetGenCount()
        {
            InitializeComponent();

            ctrlColumnSelect = new ColumnSelectControl();
            ctrlColumnSelect.Dock = DockStyle.Fill;
            ColumnSelectPage.Controls.Add(ctrlColumnSelect);

            ctrlCodeView = new CodeView();
            ctrlCodeView.Dock = DockStyle.Fill;
            CodePage.Controls.Add(ctrlCodeView);

            ColumnSelectPage.Initialize += ColumnSelectPage_Initialize;
            ColumnSelectPage.Commit += ColumnSelectPage_Commit;
        }

        private void ColumnSelectPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            ctrlColumnSelect.SetColumns(this.Table.Columns, this.SelectedColumns);
        }

        private void ColumnSelectPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SelectedColumns = ctrlColumnSelect.GetSelectedColumns();

            DbSnippetGen gen = new DbSnippetGen();
            TableCountModel model = new TableCountModel();
            model.Table = this.Table;
            model.ParamColumns = this.SelectedColumns;
            string code = "";// gen.GenCountCode(model);

            ctrlCodeView.Show(code);
        }
    }
}

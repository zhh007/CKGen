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

        public FrmSnippetGenCount()
        {
            InitializeComponent();

            ctrlColumnSelect = new ColumnSelectControl();
            ColumnSelectPage.Controls.Add(ctrlColumnSelect);

            ColumnSelectPage.Initialize += ColumnSelectPage_Initialize;
            ColumnSelectPage.Commit += ColumnSelectPage_Commit;

            CodePage.Initialize += CodePage_Initialize;
            CodePage.Commit += CodePage_Commit;
        }

        private void ColumnSelectPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            ctrlColumnSelect.SetColumns(this.Table.Columns, this.SelectedColumns);
        }

        private void ColumnSelectPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SelectedColumns = ctrlColumnSelect.GetSelectedColumns();

            DbSnippetGen gen = new DbSnippetGen();
            CountModel model = new CountModel();
            model.Table = this.Table;
            model.ParamColumns = this.SelectedColumns;
            string code = gen.GenCountCode(model);

            CodeView codeView = new CodeView();
            codeView.Dock = DockStyle.Fill;
            CodePage.Controls.Add(codeView);
            codeView.Show(code);
        }

        private void CodePage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            
        }

        private void CodePage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            
        }

        
    }
}

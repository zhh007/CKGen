namespace CKGen.Controls
{
    partial class FrmSnippetGenPaged
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.OrderByPage = new AeroWizard.WizardPage();
            this.WhereParamPage = new AeroWizard.WizardPage();
            this.ResultPage = new AeroWizard.WizardPage();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackColor = System.Drawing.Color.White;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.WhereParamPage);
            this.wizardControl1.Pages.Add(this.OrderByPage);
            this.wizardControl1.Pages.Add(this.ResultPage);
            this.wizardControl1.Size = new System.Drawing.Size(808, 550);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "生成代码";
            // 
            // OrderByPage
            // 
            this.OrderByPage.Name = "OrderByPage";
            this.OrderByPage.Size = new System.Drawing.Size(761, 394);
            this.OrderByPage.TabIndex = 0;
            this.OrderByPage.Text = "设置排序";
            // 
            // WhereParamPage
            // 
            this.WhereParamPage.Name = "WhereParamPage";
            this.WhereParamPage.Size = new System.Drawing.Size(761, 394);
            this.WhereParamPage.TabIndex = 1;
            this.WhereParamPage.Text = "设置查询条件";
            // 
            // ResultPage
            // 
            this.ResultPage.Name = "ResultPage";
            this.ResultPage.Size = new System.Drawing.Size(761, 394);
            this.ResultPage.TabIndex = 2;
            this.ResultPage.Text = "结果";
            // 
            // FrmSnippetGenPaged
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 550);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmSnippetGenPaged";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage OrderByPage;
        private AeroWizard.WizardPage WhereParamPage;
        private AeroWizard.WizardPage ResultPage;
    }
}
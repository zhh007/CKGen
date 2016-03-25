namespace CKGen.Controls
{
    partial class FrmViewSnippetGen
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
            this.WhereParamPage = new AeroWizard.WizardPage();
            this.OrderByPage = new AeroWizard.WizardPage();
            this.ResultPage = new AeroWizard.WizardPage();
            this.GenTypePage = new AeroWizard.WizardPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbGet = new System.Windows.Forms.RadioButton();
            this.rbGetList = new System.Windows.Forms.RadioButton();
            this.rbPaged = new System.Windows.Forms.RadioButton();
            this.rbTop = new System.Windows.Forms.RadioButton();
            this.rbExist = new System.Windows.Forms.RadioButton();
            this.rbCount = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.GenTypePage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackColor = System.Drawing.Color.White;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.GenTypePage);
            this.wizardControl1.Pages.Add(this.WhereParamPage);
            this.wizardControl1.Pages.Add(this.OrderByPage);
            this.wizardControl1.Pages.Add(this.ResultPage);
            this.wizardControl1.Size = new System.Drawing.Size(808, 550);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "生成代码";
            // 
            // WhereParamPage
            // 
            this.WhereParamPage.Name = "WhereParamPage";
            this.WhereParamPage.Size = new System.Drawing.Size(761, 394);
            this.WhereParamPage.TabIndex = 1;
            this.WhereParamPage.Text = "设置查询条件";
            // 
            // OrderByPage
            // 
            this.OrderByPage.Name = "OrderByPage";
            this.OrderByPage.Size = new System.Drawing.Size(761, 394);
            this.OrderByPage.TabIndex = 0;
            this.OrderByPage.Text = "设置排序";
            // 
            // ResultPage
            // 
            this.ResultPage.Name = "ResultPage";
            this.ResultPage.Size = new System.Drawing.Size(761, 394);
            this.ResultPage.TabIndex = 2;
            this.ResultPage.Text = "结果";
            // 
            // GenTypePage
            // 
            this.GenTypePage.Controls.Add(this.panel1);
            this.GenTypePage.Name = "GenTypePage";
            this.GenTypePage.Size = new System.Drawing.Size(761, 394);
            this.GenTypePage.TabIndex = 3;
            this.GenTypePage.Text = "设置生成方式";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbCount);
            this.panel1.Controls.Add(this.rbExist);
            this.panel1.Controls.Add(this.rbTop);
            this.panel1.Controls.Add(this.rbPaged);
            this.panel1.Controls.Add(this.rbGetList);
            this.panel1.Controls.Add(this.rbGet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(761, 394);
            this.panel1.TabIndex = 0;
            // 
            // rbGet
            // 
            this.rbGet.AutoSize = true;
            this.rbGet.Checked = true;
            this.rbGet.Location = new System.Drawing.Point(28, 28);
            this.rbGet.Name = "rbGet";
            this.rbGet.Size = new System.Drawing.Size(46, 21);
            this.rbGet.TabIndex = 0;
            this.rbGet.TabStop = true;
            this.rbGet.Text = "Get";
            this.rbGet.UseVisualStyleBackColor = true;
            // 
            // rbGetList
            // 
            this.rbGetList.AutoSize = true;
            this.rbGetList.Location = new System.Drawing.Point(28, 74);
            this.rbGetList.Name = "rbGetList";
            this.rbGetList.Size = new System.Drawing.Size(65, 21);
            this.rbGetList.TabIndex = 1;
            this.rbGetList.Text = "GetList";
            this.rbGetList.UseVisualStyleBackColor = true;
            // 
            // rbPaged
            // 
            this.rbPaged.AutoSize = true;
            this.rbPaged.Location = new System.Drawing.Point(28, 125);
            this.rbPaged.Name = "rbPaged";
            this.rbPaged.Size = new System.Drawing.Size(63, 21);
            this.rbPaged.TabIndex = 2;
            this.rbPaged.Text = "Paged";
            this.rbPaged.UseVisualStyleBackColor = true;
            // 
            // rbTop
            // 
            this.rbTop.AutoSize = true;
            this.rbTop.Location = new System.Drawing.Point(28, 178);
            this.rbTop.Name = "rbTop";
            this.rbTop.Size = new System.Drawing.Size(49, 21);
            this.rbTop.TabIndex = 3;
            this.rbTop.Text = "Top";
            this.rbTop.UseVisualStyleBackColor = true;
            // 
            // rbExist
            // 
            this.rbExist.AutoSize = true;
            this.rbExist.Location = new System.Drawing.Point(28, 235);
            this.rbExist.Name = "rbExist";
            this.rbExist.Size = new System.Drawing.Size(52, 21);
            this.rbExist.TabIndex = 4;
            this.rbExist.Text = "Exist";
            this.rbExist.UseVisualStyleBackColor = true;
            // 
            // rbCount
            // 
            this.rbCount.AutoSize = true;
            this.rbCount.Location = new System.Drawing.Point(28, 286);
            this.rbCount.Name = "rbCount";
            this.rbCount.Size = new System.Drawing.Size(60, 21);
            this.rbCount.TabIndex = 5;
            this.rbCount.Text = "Count";
            this.rbCount.UseVisualStyleBackColor = true;
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
            this.GenTypePage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage OrderByPage;
        private AeroWizard.WizardPage WhereParamPage;
        private AeroWizard.WizardPage ResultPage;
        private AeroWizard.WizardPage GenTypePage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbExist;
        private System.Windows.Forms.RadioButton rbTop;
        private System.Windows.Forms.RadioButton rbPaged;
        private System.Windows.Forms.RadioButton rbGetList;
        private System.Windows.Forms.RadioButton rbGet;
        private System.Windows.Forms.RadioButton rbCount;
    }
}
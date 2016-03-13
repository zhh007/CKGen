namespace CKGen.Controls
{
    partial class SQLQueryCodeGenWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLQueryCodeGenWizard));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.QueryParamPage = new AeroWizard.WizardPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddParam = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveParam = new System.Windows.Forms.ToolStripButton();
            this.dgvParam = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colAllowNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultSettingPage = new AeroWizard.WizardPage();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.QueryParamPage.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParam)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackColor = System.Drawing.Color.White;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.QueryParamPage);
            this.wizardControl1.Pages.Add(this.ResultSettingPage);
            this.wizardControl1.Size = new System.Drawing.Size(574, 383);
            this.wizardControl1.SuppressParentFormCaptionSync = true;
            this.wizardControl1.SuppressParentFormIconSync = true;
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "代码生成";
            this.wizardControl1.TitleIcon = global::CKGen.Properties.Resources.move;
            // 
            // QueryParamPage
            // 
            this.QueryParamPage.Controls.Add(this.toolStrip1);
            this.QueryParamPage.Controls.Add(this.dgvParam);
            this.QueryParamPage.Name = "QueryParamPage";
            this.QueryParamPage.Size = new System.Drawing.Size(527, 227);
            this.QueryParamPage.TabIndex = 0;
            this.QueryParamPage.Text = "查询参数设置";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddParam,
            this.btnRemoveParam});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(527, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddParam
            // 
            this.btnAddParam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddParam.Image = ((System.Drawing.Image)(resources.GetObject("btnAddParam.Image")));
            this.btnAddParam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddParam.Name = "btnAddParam";
            this.btnAddParam.Size = new System.Drawing.Size(23, 22);
            this.btnAddParam.Text = "增加参数";
            // 
            // btnRemoveParam
            // 
            this.btnRemoveParam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveParam.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveParam.Image")));
            this.btnRemoveParam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveParam.Name = "btnRemoveParam";
            this.btnRemoveParam.Size = new System.Drawing.Size(23, 22);
            this.btnRemoveParam.Text = "删除参数";
            // 
            // dgvParam
            // 
            this.dgvParam.AllowUserToAddRows = false;
            this.dgvParam.AllowUserToDeleteRows = false;
            this.dgvParam.AllowUserToResizeRows = false;
            this.dgvParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colType,
            this.colAllowNull,
            this.colRemark});
            this.dgvParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvParam.Location = new System.Drawing.Point(0, 0);
            this.dgvParam.Name = "dgvParam";
            this.dgvParam.RowTemplate.Height = 23;
            this.dgvParam.Size = new System.Drawing.Size(527, 227);
            this.dgvParam.TabIndex = 0;
            // 
            // colName
            // 
            this.colName.HeaderText = "参数名";
            this.colName.Name = "colName";
            // 
            // colType
            // 
            this.colType.HeaderText = "类型";
            this.colType.Items.AddRange(new object[] {
            "string",
            "char",
            "int",
            "long",
            "float",
            "double",
            "bool",
            "DateTime",
            "TimeSpan",
            "Guid"});
            this.colType.Name = "colType";
            this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colAllowNull
            // 
            this.colAllowNull.HeaderText = "允许空";
            this.colAllowNull.Name = "colAllowNull";
            // 
            // colRemark
            // 
            this.colRemark.HeaderText = "说明";
            this.colRemark.Name = "colRemark";
            // 
            // ResultSettingPage
            // 
            this.ResultSettingPage.Name = "ResultSettingPage";
            this.ResultSettingPage.Size = new System.Drawing.Size(527, 227);
            this.ResultSettingPage.TabIndex = 1;
            this.ResultSettingPage.Text = "查询结果设置页";
            // 
            // SQLQueryCodeGenWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 383);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SQLQueryCodeGenWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.SQLQueryCodeGenWizard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.QueryParamPage.ResumeLayout(false);
            this.QueryParamPage.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage QueryParamPage;
        private AeroWizard.WizardPage ResultSettingPage;
        private System.Windows.Forms.DataGridView dgvParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAllowNull;
        private System.Windows.Forms.DataGridViewComboBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddParam;
        private System.Windows.Forms.ToolStripButton btnRemoveParam;
    }
}
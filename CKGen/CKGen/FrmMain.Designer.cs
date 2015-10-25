namespace CKGen
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnSaveSchema = new System.Windows.Forms.ToolStripButton();
            this.tsBtnReloadSchema = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvSchema = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpDetail = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTableDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvSchema = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDBType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBlank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchema)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSaveSchema,
            this.tsBtnReloadSchema});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1045, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnSaveSchema
            // 
            this.tsbtnSaveSchema.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSaveSchema.Image")));
            this.tsbtnSaveSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSaveSchema.Name = "tsbtnSaveSchema";
            this.tsbtnSaveSchema.Size = new System.Drawing.Size(76, 22);
            this.tsbtnSaveSchema.Text = "保存说明";
            this.tsbtnSaveSchema.ToolTipText = "保存说明";
            this.tsbtnSaveSchema.Click += new System.EventHandler(this.tsbtnSaveSchema_Click);
            // 
            // tsBtnReloadSchema
            // 
            this.tsBtnReloadSchema.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnReloadSchema.Image")));
            this.tsBtnReloadSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnReloadSchema.Name = "tsBtnReloadSchema";
            this.tsBtnReloadSchema.Size = new System.Drawing.Size(76, 22);
            this.tsBtnReloadSchema.Text = "重新加载";
            this.tsBtnReloadSchema.Click += new System.EventHandler(this.tsBtnReloadSchema_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl2);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1045, 488);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(3, 5);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(197, 483);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvSchema);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(189, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tvSchema
            // 
            this.tvSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSchema.HideSelection = false;
            this.tvSchema.Location = new System.Drawing.Point(3, 3);
            this.tvSchema.Name = "tvSchema";
            this.tvSchema.Size = new System.Drawing.Size(183, 451);
            this.tvSchema.TabIndex = 0;
            this.tvSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSchema_AfterSelect);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 462);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "模板";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpDetail);
            this.tabControl1.Location = new System.Drawing.Point(0, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(836, 476);
            this.tabControl1.TabIndex = 0;
            // 
            // tpDetail
            // 
            this.tpDetail.BackColor = System.Drawing.SystemColors.Control;
            this.tpDetail.Controls.Add(this.label7);
            this.tpDetail.Controls.Add(this.label6);
            this.tpDetail.Controls.Add(this.lblTableName);
            this.tpDetail.Controls.Add(this.label5);
            this.tpDetail.Controls.Add(this.txtTableDesc);
            this.tpDetail.Controls.Add(this.label4);
            this.tpDetail.Controls.Add(this.dgvSchema);
            this.tpDetail.Location = new System.Drawing.Point(4, 22);
            this.tpDetail.Name = "tpDetail";
            this.tpDetail.Size = new System.Drawing.Size(828, 450);
            this.tpDetail.TabIndex = 0;
            this.tpDetail.Text = "详细信息";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(216, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "说明(新):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "[说明]";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(45, 8);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(41, 12);
            this.lblTableName.TabIndex = 4;
            this.lblTableName.Text = "[表名]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "说明(本地):";
            // 
            // txtTableDesc
            // 
            this.txtTableDesc.Location = new System.Drawing.Point(281, 39);
            this.txtTableDesc.Name = "txtTableDesc";
            this.txtTableDesc.Size = new System.Drawing.Size(187, 21);
            this.txtTableDesc.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "表名:";
            // 
            // dgvSchema
            // 
            this.dgvSchema.AllowUserToAddRows = false;
            this.dgvSchema.AllowUserToDeleteRows = false;
            this.dgvSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSchema.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvSchema.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSchema.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSchema.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchema.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.ColDBType,
            this.ColNull,
            this.colRemark,
            this.colDescLocal,
            this.colDescNew,
            this.ColBlank});
            this.dgvSchema.Location = new System.Drawing.Point(0, 73);
            this.dgvSchema.MultiSelect = false;
            this.dgvSchema.Name = "dgvSchema";
            this.dgvSchema.RowHeadersVisible = false;
            this.dgvSchema.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvSchema.RowTemplate.Height = 23;
            this.dgvSchema.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSchema.Size = new System.Drawing.Size(828, 378);
            this.dgvSchema.TabIndex = 0;
            this.dgvSchema.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchema_CellEndEdit);
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colName.HeaderText = "名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 54;
            // 
            // ColDBType
            // 
            this.ColDBType.HeaderText = "数据类型";
            this.ColDBType.Name = "ColDBType";
            this.ColDBType.ReadOnly = true;
            this.ColDBType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColNull
            // 
            this.ColNull.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColNull.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColNull.HeaderText = "允许Null值";
            this.ColNull.Name = "ColNull";
            this.ColNull.ReadOnly = true;
            this.ColNull.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColNull.Width = 90;
            // 
            // colRemark
            // 
            this.colRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colRemark.HeaderText = "说明";
            this.colRemark.Name = "colRemark";
            this.colRemark.ReadOnly = true;
            this.colRemark.Width = 54;
            // 
            // colDescLocal
            // 
            this.colDescLocal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDescLocal.HeaderText = "说明(本地)";
            this.colDescLocal.Name = "colDescLocal";
            this.colDescLocal.ReadOnly = true;
            this.colDescLocal.Width = 90;
            // 
            // colDescNew
            // 
            this.colDescNew.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDescNew.HeaderText = "说明(新)";
            this.colDescNew.MaxInputLength = 100;
            this.colDescNew.MinimumWidth = 200;
            this.colDescNew.Name = "colDescNew";
            this.colDescNew.Width = 200;
            // 
            // ColBlank
            // 
            this.ColBlank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.ColBlank.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColBlank.HeaderText = "";
            this.ColBlank.Name = "ColBlank";
            this.ColBlank.ReadOnly = true;
            this.ColBlank.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColBlank.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 513);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpDetail.ResumeLayout(false);
            this.tpDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchema)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton tsbtnSaveSchema;
        private System.Windows.Forms.TreeView tvSchema;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpDetail;
        private System.Windows.Forms.DataGridView dgvSchema;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDBType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBlank;
        private System.Windows.Forms.ToolStripButton tsBtnReloadSchema;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTableDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
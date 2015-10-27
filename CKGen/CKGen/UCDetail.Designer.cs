namespace CKGen
{
    partial class UCDetail
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchema)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(216, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "说明(新):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "[说明]";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(45, 9);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(41, 12);
            this.lblTableName.TabIndex = 11;
            this.lblTableName.Text = "[表名]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "说明(本地):";
            // 
            // txtTableDesc
            // 
            this.txtTableDesc.Location = new System.Drawing.Point(281, 40);
            this.txtTableDesc.Name = "txtTableDesc";
            this.txtTableDesc.Size = new System.Drawing.Size(187, 21);
            this.txtTableDesc.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 8;
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
            this.dgvSchema.Location = new System.Drawing.Point(0, 74);
            this.dgvSchema.MultiSelect = false;
            this.dgvSchema.Name = "dgvSchema";
            this.dgvSchema.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvSchema.RowTemplate.Height = 23;
            this.dgvSchema.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSchema.Size = new System.Drawing.Size(834, 419);
            this.dgvSchema.TabIndex = 7;
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
            // UCDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTableDesc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvSchema);
            this.Name = "UCDetail";
            this.Size = new System.Drawing.Size(834, 493);
            this.Load += new System.EventHandler(this.UCSchema_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchema)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTableDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvSchema;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDBType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBlank;
    }
}

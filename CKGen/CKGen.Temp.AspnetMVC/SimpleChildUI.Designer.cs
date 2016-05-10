namespace CKGen.Temp.AspnetMVC
{
    partial class SimpleChildUI
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.btnGen = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtWebProjNameSpace = new System.Windows.Forms.TextBox();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtChildName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTablesForParent = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gvFields = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDBType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDescNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColInputType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColBlank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbTablesForChild = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtParentObjectName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbForeignKey = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFields)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.btnView);
            this.MainPanel.Controls.Add(this.btnGen);
            this.MainPanel.Controls.Add(this.groupBox2);
            this.MainPanel.Controls.Add(this.groupBox1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(836, 572);
            this.MainPanel.TabIndex = 0;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(152, 446);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 15;
            this.btnView.Text = "查看结果";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(11, 446);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(123, 23);
            this.btnGen.TabIndex = 14;
            this.btnGen.Text = "生成";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtWebProjNameSpace);
            this.groupBox2.Controls.Add(this.txtNamespace);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtChildName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbTablesForParent);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(4, 336);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(829, 94);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "父对象设置";
            // 
            // txtWebProjNameSpace
            // 
            this.txtWebProjNameSpace.Location = new System.Drawing.Point(480, 51);
            this.txtWebProjNameSpace.Name = "txtWebProjNameSpace";
            this.txtWebProjNameSpace.Size = new System.Drawing.Size(243, 21);
            this.txtWebProjNameSpace.TabIndex = 17;
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(84, 51);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(243, 21);
            this.txtNamespace.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(343, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "Web NameSpace:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "NameSpace:";
            // 
            // txtChildName
            // 
            this.txtChildName.Location = new System.Drawing.Point(480, 20);
            this.txtChildName.Name = "txtChildName";
            this.txtChildName.Size = new System.Drawing.Size(243, 21);
            this.txtChildName.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(343, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "ChildCollectionName：";
            // 
            // cbTablesForParent
            // 
            this.cbTablesForParent.FormattingEnabled = true;
            this.cbTablesForParent.Location = new System.Drawing.Point(84, 20);
            this.cbTablesForParent.Name = "cbTablesForParent";
            this.cbTablesForParent.Size = new System.Drawing.Size(243, 20);
            this.cbTablesForParent.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "Table：";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbForeignKey);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtParentObjectName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.gvFields);
            this.groupBox1.Controls.Add(this.cbTablesForChild);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(830, 327);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "子对象设置";
            // 
            // gvFields
            // 
            this.gvFields.AllowUserToAddRows = false;
            this.gvFields.AllowUserToDeleteRows = false;
            this.gvFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvFields.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvFields.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvFields.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.ColDBType,
            this.ColNull,
            this.colDescNew,
            this.ColInputType,
            this.ColBlank});
            this.gvFields.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvFields.Location = new System.Drawing.Point(8, 78);
            this.gvFields.MultiSelect = false;
            this.gvFields.Name = "gvFields";
            this.gvFields.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvFields.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.gvFields.RowTemplate.Height = 23;
            this.gvFields.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gvFields.Size = new System.Drawing.Size(816, 243);
            this.gvFields.TabIndex = 7;
            this.gvFields.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.gvFields_RowPostPaint);
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colName.HeaderText = "名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colName.Width = 35;
            // 
            // ColDBType
            // 
            this.ColDBType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColDBType.HeaderText = "数据类型";
            this.ColDBType.MinimumWidth = 100;
            this.ColDBType.Name = "ColDBType";
            this.ColDBType.ReadOnly = true;
            this.ColDBType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColNull
            // 
            this.ColNull.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColNull.HeaderText = "允许Null值";
            this.ColNull.Name = "ColNull";
            this.ColNull.ReadOnly = true;
            this.ColNull.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColNull.Width = 71;
            // 
            // colDescNew
            // 
            this.colDescNew.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDescNew.HeaderText = "说明";
            this.colDescNew.MaxInputLength = 100;
            this.colDescNew.MinimumWidth = 300;
            this.colDescNew.Name = "colDescNew";
            this.colDescNew.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDescNew.Width = 300;
            // 
            // ColInputType
            // 
            this.ColInputType.FillWeight = 90F;
            this.ColInputType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ColInputType.HeaderText = "input类型";
            this.ColInputType.Items.AddRange(new object[] {
            "text",
            "hidden",
            "select",
            "radio",
            "checkbox",
            "textarea"});
            this.ColInputType.MinimumWidth = 90;
            this.ColInputType.Name = "ColInputType";
            this.ColInputType.Width = 90;
            // 
            // ColBlank
            // 
            this.ColBlank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.ColBlank.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColBlank.HeaderText = "";
            this.ColBlank.Name = "ColBlank";
            this.ColBlank.ReadOnly = true;
            this.ColBlank.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColBlank.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cbTablesForChild
            // 
            this.cbTablesForChild.FormattingEnabled = true;
            this.cbTablesForChild.Location = new System.Drawing.Point(59, 23);
            this.cbTablesForChild.Name = "cbTablesForChild";
            this.cbTablesForChild.Size = new System.Drawing.Size(243, 20);
            this.cbTablesForChild.TabIndex = 15;
            this.cbTablesForChild.SelectedIndexChanged += new System.EventHandler(this.cbTablesForChild_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table：";
            // 
            // txtParentObjectName
            // 
            this.txtParentObjectName.Location = new System.Drawing.Point(450, 22);
            this.txtParentObjectName.Name = "txtParentObjectName";
            this.txtParentObjectName.Size = new System.Drawing.Size(243, 21);
            this.txtParentObjectName.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "ParentObjectName：";
            // 
            // cbForeignKey
            // 
            this.cbForeignKey.FormattingEnabled = true;
            this.cbForeignKey.Location = new System.Drawing.Point(85, 52);
            this.cbForeignKey.Name = "cbForeignKey";
            this.cbForeignKey.Size = new System.Drawing.Size(217, 20);
            this.cbForeignKey.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "ForeignKey：";
            // 
            // SimpleChildUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Name = "SimpleChildUI";
            this.Size = new System.Drawing.Size(836, 572);
            this.Load += new System.EventHandler(this.SimpleChildUI_Load);
            this.MainPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFields)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Button btnView;
        internal System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTablesForChild;
        private System.Windows.Forms.ComboBox cbTablesForParent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gvFields;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtChildName;
        private System.Windows.Forms.TextBox txtWebProjNameSpace;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDBType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColNull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescNew;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColInputType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBlank;
        private System.Windows.Forms.TextBox txtParentObjectName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbForeignKey;
        private System.Windows.Forms.Label label7;
    }
}

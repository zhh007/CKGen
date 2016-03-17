namespace CKGen.Controls
{
    partial class SQLQueryResultControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRowClassName = new System.Windows.Forms.TextBox();
            this.dgvSchema = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbReadRows = new System.Windows.Forms.RadioButton();
            this.rbReadOneRow = new System.Windows.Forms.RadioButton();
            this.rbExecuteScalar = new System.Windows.Forms.RadioButton();
            this.rbExecuteNonQuery = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchema)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "返回值设置";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgvSchema, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(594, 150);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtRowClassName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 98);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(594, 42);
            this.panel2.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "行类名：";
            // 
            // txtRowClassName
            // 
            this.txtRowClassName.Location = new System.Drawing.Point(66, 3);
            this.txtRowClassName.Margin = new System.Windows.Forms.Padding(0);
            this.txtRowClassName.Name = "txtRowClassName";
            this.txtRowClassName.Size = new System.Drawing.Size(256, 21);
            this.txtRowClassName.TabIndex = 7;
            // 
            // dgvSchema
            // 
            this.dgvSchema.AllowUserToAddRows = false;
            this.dgvSchema.AllowUserToDeleteRows = false;
            this.dgvSchema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSchema.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSchema.Location = new System.Drawing.Point(3, 3);
            this.dgvSchema.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.dgvSchema.MultiSelect = false;
            this.dgvSchema.Name = "dgvSchema";
            this.dgvSchema.RowTemplate.Height = 23;
            this.dgvSchema.Size = new System.Drawing.Size(588, 75);
            this.dgvSchema.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbReadRows);
            this.panel1.Controls.Add(this.rbReadOneRow);
            this.panel1.Controls.Add(this.rbExecuteScalar);
            this.panel1.Controls.Add(this.rbExecuteNonQuery);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 78);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 20);
            this.panel1.TabIndex = 8;
            // 
            // rbReadRows
            // 
            this.rbReadRows.AutoSize = true;
            this.rbReadRows.Checked = true;
            this.rbReadRows.Location = new System.Drawing.Point(65, 3);
            this.rbReadRows.Margin = new System.Windows.Forms.Padding(0);
            this.rbReadRows.Name = "rbReadRows";
            this.rbReadRows.Size = new System.Drawing.Size(71, 16);
            this.rbReadRows.TabIndex = 2;
            this.rbReadRows.TabStop = true;
            this.rbReadRows.Text = "ReadRows";
            this.rbReadRows.UseVisualStyleBackColor = true;
            this.rbReadRows.CheckedChanged += new System.EventHandler(this.QueryExecuteTypeChanged);
            // 
            // rbReadOneRow
            // 
            this.rbReadOneRow.AutoSize = true;
            this.rbReadOneRow.Location = new System.Drawing.Point(142, 3);
            this.rbReadOneRow.Margin = new System.Windows.Forms.Padding(0);
            this.rbReadOneRow.Name = "rbReadOneRow";
            this.rbReadOneRow.Size = new System.Drawing.Size(83, 16);
            this.rbReadOneRow.TabIndex = 3;
            this.rbReadOneRow.Text = "ReadOneRow";
            this.rbReadOneRow.UseVisualStyleBackColor = true;
            this.rbReadOneRow.CheckedChanged += new System.EventHandler(this.QueryExecuteTypeChanged);
            // 
            // rbExecuteScalar
            // 
            this.rbExecuteScalar.AutoSize = true;
            this.rbExecuteScalar.Location = new System.Drawing.Point(233, 3);
            this.rbExecuteScalar.Margin = new System.Windows.Forms.Padding(0);
            this.rbExecuteScalar.Name = "rbExecuteScalar";
            this.rbExecuteScalar.Size = new System.Drawing.Size(101, 16);
            this.rbExecuteScalar.TabIndex = 4;
            this.rbExecuteScalar.Text = "ExecuteScalar";
            this.rbExecuteScalar.UseVisualStyleBackColor = true;
            this.rbExecuteScalar.CheckedChanged += new System.EventHandler(this.QueryExecuteTypeChanged);
            // 
            // rbExecuteNonQuery
            // 
            this.rbExecuteNonQuery.AutoSize = true;
            this.rbExecuteNonQuery.Location = new System.Drawing.Point(341, 3);
            this.rbExecuteNonQuery.Margin = new System.Windows.Forms.Padding(0);
            this.rbExecuteNonQuery.Name = "rbExecuteNonQuery";
            this.rbExecuteNonQuery.Size = new System.Drawing.Size(113, 16);
            this.rbExecuteNonQuery.TabIndex = 5;
            this.rbExecuteNonQuery.Text = "ExecuteNonQuery";
            this.rbExecuteNonQuery.UseVisualStyleBackColor = true;
            this.rbExecuteNonQuery.CheckedChanged += new System.EventHandler(this.QueryExecuteTypeChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查询方式：";
            // 
            // SQLQueryResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SQLQueryResultControl";
            this.Size = new System.Drawing.Size(600, 160);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchema)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvSchema;
        private System.Windows.Forms.RadioButton rbExecuteNonQuery;
        private System.Windows.Forms.RadioButton rbExecuteScalar;
        private System.Windows.Forms.RadioButton rbReadOneRow;
        private System.Windows.Forms.RadioButton rbReadRows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRowClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
    }
}

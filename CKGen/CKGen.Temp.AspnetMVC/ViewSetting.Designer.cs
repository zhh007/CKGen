namespace CKGen.Temp.AspnetMVC
{
    partial class ViewSetting
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
            this.step1Panel = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGen = new System.Windows.Forms.Button();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWebProjNameSpace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.step1Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // step1Panel
            // 
            this.step1Panel.Controls.Add(this.cbTables);
            this.step1Panel.Controls.Add(this.btnView);
            this.step1Panel.Controls.Add(this.label1);
            this.step1Panel.Controls.Add(this.btnGen);
            this.step1Panel.Controls.Add(this.txtNamespace);
            this.step1Panel.Controls.Add(this.label3);
            this.step1Panel.Controls.Add(this.txtWebProjNameSpace);
            this.step1Panel.Controls.Add(this.label2);
            this.step1Panel.Location = new System.Drawing.Point(3, 3);
            this.step1Panel.Name = "step1Panel";
            this.step1Panel.Size = new System.Drawing.Size(545, 242);
            this.step1Panel.TabIndex = 14;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(167, 125);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 13;
            this.btnView.Text = "查看结果";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "NameSpace:";
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(26, 125);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(123, 23);
            this.btnGen.TabIndex = 0;
            this.btnGen.Text = "生成";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(123, 13);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(243, 21);
            this.txtNamespace.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Table:";
            // 
            // txtWebProjNameSpace
            // 
            this.txtWebProjNameSpace.Location = new System.Drawing.Point(123, 45);
            this.txtWebProjNameSpace.Name = "txtWebProjNameSpace";
            this.txtWebProjNameSpace.Size = new System.Drawing.Size(243, 21);
            this.txtWebProjNameSpace.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Web NameSpace:";
            // 
            // cbTables
            // 
            this.cbTables.FormattingEnabled = true;
            this.cbTables.Location = new System.Drawing.Point(123, 80);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(243, 20);
            this.cbTables.TabIndex = 14;
            // 
            // ViewSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.step1Panel);
            this.Name = "ViewSetting";
            this.Size = new System.Drawing.Size(602, 382);
            this.step1Panel.ResumeLayout(false);
            this.step1Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel step1Panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWebProjNameSpace;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button btnView;
        internal System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.ComboBox cbTables;
    }
}

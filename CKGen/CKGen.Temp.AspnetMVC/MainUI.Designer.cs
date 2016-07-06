namespace CKGen.Temp.AspnetMVC
{
    partial class MainUI
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.btnView = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGen = new System.Windows.Forms.Button();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWebProjNameSpace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModalName = new System.Windows.Forms.TextBox();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.txtModalName);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.cbTables);
            this.MainPanel.Controls.Add(this.btnView);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.btnGen);
            this.MainPanel.Controls.Add(this.txtNamespace);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.txtWebProjNameSpace);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(860, 511);
            this.MainPanel.TabIndex = 0;
            // 
            // cbTables
            // 
            this.cbTables.FormattingEnabled = true;
            this.cbTables.Location = new System.Drawing.Point(127, 82);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(243, 20);
            this.cbTables.TabIndex = 22;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(171, 186);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 21;
            this.btnView.Text = "查看结果";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "命名空间:";
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(30, 186);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(123, 23);
            this.btnGen.TabIndex = 15;
            this.btnGen.Text = "生成";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(127, 12);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(243, 21);
            this.txtNamespace.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "表:";
            // 
            // txtWebProjNameSpace
            // 
            this.txtWebProjNameSpace.Location = new System.Drawing.Point(127, 47);
            this.txtWebProjNameSpace.Name = "txtWebProjNameSpace";
            this.txtWebProjNameSpace.Size = new System.Drawing.Size(243, 21);
            this.txtWebProjNameSpace.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "Web项目命名空间:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "模块名称:";
            // 
            // txtModalName
            // 
            this.txtModalName.Location = new System.Drawing.Point(127, 117);
            this.txtModalName.Name = "txtModalName";
            this.txtModalName.Size = new System.Drawing.Size(243, 21);
            this.txtModalName.TabIndex = 24;
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Name = "MainUI";
            this.Size = new System.Drawing.Size(860, 511);
            this.Load += new System.EventHandler(this.BuildUI_Load);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ComboBox cbTables;
        internal System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWebProjNameSpace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModalName;
        private System.Windows.Forms.Label label4;
    }
}

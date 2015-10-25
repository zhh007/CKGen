namespace CKGen
{
    partial class FrmCodeBuild
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
            this.button1 = new System.Windows.Forms.Button();
            this.lblTable = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWebProjNameSpace = new System.Windows.Forms.TextBox();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.step1Panel = new System.Windows.Forms.Panel();
            this.step2Panel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvRoot = new System.Windows.Forms.TreeView();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnOpenDir = new System.Windows.Forms.Button();
            this.scintilla1 = new ScintillaNET.Scintilla();
            this.step1Panel.SuspendLayout();
            this.step2Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(120, 80);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(29, 12);
            this.lblTable.TabIndex = 12;
            this.lblTable.Text = "表名";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Web NameSpace:";
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
            // txtWebProjNameSpace
            // 
            this.txtWebProjNameSpace.Location = new System.Drawing.Point(123, 45);
            this.txtWebProjNameSpace.Name = "txtWebProjNameSpace";
            this.txtWebProjNameSpace.Size = new System.Drawing.Size(243, 21);
            this.txtWebProjNameSpace.TabIndex = 8;
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(123, 13);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(243, 21);
            this.txtNamespace.TabIndex = 7;
            // 
            // step1Panel
            // 
            this.step1Panel.Controls.Add(this.btnView);
            this.step1Panel.Controls.Add(this.label1);
            this.step1Panel.Controls.Add(this.button1);
            this.step1Panel.Controls.Add(this.lblTable);
            this.step1Panel.Controls.Add(this.txtNamespace);
            this.step1Panel.Controls.Add(this.label3);
            this.step1Panel.Controls.Add(this.txtWebProjNameSpace);
            this.step1Panel.Controls.Add(this.label2);
            this.step1Panel.Location = new System.Drawing.Point(12, 12);
            this.step1Panel.Name = "step1Panel";
            this.step1Panel.Size = new System.Drawing.Size(545, 242);
            this.step1Panel.TabIndex = 13;
            // 
            // step2Panel
            // 
            this.step2Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.step2Panel.Controls.Add(this.splitContainer1);
            this.step2Panel.Location = new System.Drawing.Point(12, 289);
            this.step2Panel.Name = "step2Panel";
            this.step2Panel.Size = new System.Drawing.Size(1080, 428);
            this.step2Panel.TabIndex = 14;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnOpenDir);
            this.splitContainer1.Panel1.Controls.Add(this.btnBack);
            this.splitContainer1.Panel1.Controls.Add(this.tvRoot);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scintilla1);
            this.splitContainer1.Size = new System.Drawing.Size(1080, 428);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvRoot
            // 
            this.tvRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvRoot.HideSelection = false;
            this.tvRoot.Location = new System.Drawing.Point(3, 38);
            this.tvRoot.Name = "tvRoot";
            this.tvRoot.Size = new System.Drawing.Size(193, 387);
            this.tvRoot.TabIndex = 0;
            this.tvRoot.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRoot_AfterSelect);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(10, 9);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "返回";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
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
            // btnOpenDir
            // 
            this.btnOpenDir.Location = new System.Drawing.Point(91, 9);
            this.btnOpenDir.Name = "btnOpenDir";
            this.btnOpenDir.Size = new System.Drawing.Size(75, 23);
            this.btnOpenDir.TabIndex = 2;
            this.btnOpenDir.Text = "打开文件夹";
            this.btnOpenDir.UseVisualStyleBackColor = true;
            this.btnOpenDir.Click += new System.EventHandler(this.btnOpenDir_Click);
            // 
            // scintilla1
            // 
            this.scintilla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintilla1.Location = new System.Drawing.Point(3, 3);
            this.scintilla1.Name = "scintilla1";
            this.scintilla1.Size = new System.Drawing.Size(872, 422);
            this.scintilla1.TabIndex = 1;
            this.scintilla1.Text = "scintilla1";
            // 
            // FrmCodeBuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 729);
            this.Controls.Add(this.step2Panel);
            this.Controls.Add(this.step1Panel);
            this.Name = "FrmCodeBuild";
            this.Text = "代码生成";
            this.Load += new System.EventHandler(this.FrmCodeBuild_Load);
            this.step1Panel.ResumeLayout(false);
            this.step1Panel.PerformLayout();
            this.step2Panel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWebProjNameSpace;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Panel step1Panel;
        private System.Windows.Forms.Panel step2Panel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvRoot;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnOpenDir;
        private ScintillaNET.Scintilla scintilla1;
    }
}
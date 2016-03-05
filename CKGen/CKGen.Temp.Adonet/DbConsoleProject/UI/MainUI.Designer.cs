namespace CKGen.Temp.Adonet.DbConsoleProject.UI
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvMain = new System.Windows.Forms.ListView();
            this.btnBuild = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjName = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lvMain);
            this.groupBox1.Location = new System.Drawing.Point(15, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(646, 326);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择数据库对象";
            // 
            // lvMain
            // 
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.Location = new System.Drawing.Point(3, 17);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(640, 306);
            this.lvMain.TabIndex = 0;
            this.lvMain.TileSize = new System.Drawing.Size(180, 28);
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Tile;
            this.lvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMain_KeyDown);
            // 
            // btnBuild
            // 
            this.btnBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBuild.Location = new System.Drawing.Point(18, 382);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(96, 30);
            this.btnBuild.TabIndex = 1;
            this.btnBuild.Text = "生成";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 353);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "项目名称：";
            // 
            // txtProjName
            // 
            this.txtProjName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtProjName.Location = new System.Drawing.Point(90, 350);
            this.txtProjName.Name = "txtProjName";
            this.txtProjName.Size = new System.Drawing.Size(220, 21);
            this.txtProjName.TabIndex = 3;
            this.txtProjName.Validating += new System.ComponentModel.CancelEventHandler(this.txtProjName_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // MainUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtProjName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainUI";
            this.Size = new System.Drawing.Size(677, 430);
            this.Load += new System.EventHandler(this.MainUI_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainUI_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainUI_DragEnter);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProjName;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

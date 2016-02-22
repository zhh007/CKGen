namespace CKGen.Temp.Adonet.TestProj
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvMain = new System.Windows.Forms.ListView();
            this.btnBuild = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            this.btnBuild.Location = new System.Drawing.Point(15, 349);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 1;
            this.btnBuild.Text = "生成";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // MainUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainUI";
            this.Size = new System.Drawing.Size(677, 430);
            this.Load += new System.EventHandler(this.MainUI_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainUI_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainUI_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.ListView lvMain;
    }
}

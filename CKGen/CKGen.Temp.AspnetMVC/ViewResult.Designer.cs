namespace CKGen.Temp.AspnetMVC
{
    partial class ViewResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewResult));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.stBtnBack = new System.Windows.Forms.ToolStripButton();
            this.stBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.tvRoot = new System.Windows.Forms.TreeView();
            this.txtCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.tvRoot);
            this.splitContainer1.Panel1MinSize = 220;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtCode);
            this.splitContainer1.Size = new System.Drawing.Size(996, 453);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stBtnBack,
            this.stBtnOpen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(220, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // stBtnBack
            // 
            this.stBtnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stBtnBack.Image = ((System.Drawing.Image)(resources.GetObject("stBtnBack.Image")));
            this.stBtnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stBtnBack.Name = "stBtnBack";
            this.stBtnBack.Size = new System.Drawing.Size(36, 22);
            this.stBtnBack.Text = "返回";
            this.stBtnBack.Click += new System.EventHandler(this.stBtnBack_Click);
            // 
            // stBtnOpen
            // 
            this.stBtnOpen.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("stBtnOpen.Image")));
            this.stBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stBtnOpen.Name = "stBtnOpen";
            this.stBtnOpen.Size = new System.Drawing.Size(72, 22);
            this.stBtnOpen.Text = "打开文件夹";
            this.stBtnOpen.Click += new System.EventHandler(this.stBtnOpen_Click);
            // 
            // tvRoot
            // 
            this.tvRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvRoot.HideSelection = false;
            this.tvRoot.Location = new System.Drawing.Point(0, 28);
            this.tvRoot.Name = "tvRoot";
            this.tvRoot.Size = new System.Drawing.Size(220, 425);
            this.tvRoot.TabIndex = 0;
            this.tvRoot.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRoot_AfterSelect);
            this.tvRoot.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvRoot_KeyUp);
            // 
            // txtCode
            // 
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Location = new System.Drawing.Point(0, 0);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(772, 453);
            this.txtCode.TabIndex = 0;
            this.txtCode.WordWrap = false;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // ViewResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ViewResult";
            this.Size = new System.Drawing.Size(996, 453);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvRoot;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton stBtnBack;
        private System.Windows.Forms.ToolStripButton stBtnOpen;
    }
}

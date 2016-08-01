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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnLinkDb = new System.Windows.Forms.ToolStripButton();
            this.tsBtnReloadSchema = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSaveSchema = new System.Windows.Forms.ToolStripButton();
            this.tsBtnGenDbChm = new System.Windows.Forms.ToolStripButton();
            this.tsBtnQuery = new System.Windows.Forms.ToolStripButton();
            this.tsBtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnLinkDb,
            this.tsBtnReloadSchema,
            this.tsbtnSaveSchema,
            this.tsBtnGenDbChm,
            this.tsBtnQuery,
            this.tsBtnUpdate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1061, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnLinkDb
            // 
            this.tsBtnLinkDb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnLinkDb.Image = global::CKGen.Properties.Resources.link;
            this.tsBtnLinkDb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnLinkDb.Name = "tsBtnLinkDb";
            this.tsBtnLinkDb.Size = new System.Drawing.Size(23, 22);
            this.tsBtnLinkDb.Text = "连接数据库";
            this.tsBtnLinkDb.Click += new System.EventHandler(this.tsBtnLinkDb_Click);
            // 
            // tsBtnReloadSchema
            // 
            this.tsBtnReloadSchema.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnReloadSchema.Image = global::CKGen.Properties.Resources.fresh;
            this.tsBtnReloadSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnReloadSchema.Name = "tsBtnReloadSchema";
            this.tsBtnReloadSchema.Size = new System.Drawing.Size(23, 22);
            this.tsBtnReloadSchema.Text = "重新加载";
            this.tsBtnReloadSchema.Click += new System.EventHandler(this.tsBtnReloadSchema_Click);
            // 
            // tsbtnSaveSchema
            // 
            this.tsbtnSaveSchema.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSaveSchema.Image = global::CKGen.Properties.Resources.saveHS;
            this.tsbtnSaveSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSaveSchema.Name = "tsbtnSaveSchema";
            this.tsbtnSaveSchema.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSaveSchema.Text = "保存说明";
            this.tsbtnSaveSchema.ToolTipText = "保存说明";
            this.tsbtnSaveSchema.Click += new System.EventHandler(this.tsbtnSaveSchema_Click);
            // 
            // tsBtnGenDbChm
            // 
            this.tsBtnGenDbChm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnGenDbChm.Image = global::CKGen.Properties.Resources.NewChm;
            this.tsBtnGenDbChm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnGenDbChm.Name = "tsBtnGenDbChm";
            this.tsBtnGenDbChm.Size = new System.Drawing.Size(23, 22);
            this.tsBtnGenDbChm.Text = "生成数据库文档";
            this.tsBtnGenDbChm.Click += new System.EventHandler(this.tsBtnGenDbChm_Click);
            // 
            // tsBtnQuery
            // 
            this.tsBtnQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnQuery.Image = global::CKGen.Properties.Resources.Sqlfile;
            this.tsBtnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnQuery.Name = "tsBtnQuery";
            this.tsBtnQuery.Size = new System.Drawing.Size(23, 22);
            this.tsBtnQuery.Text = "新建查询";
            this.tsBtnQuery.Click += new System.EventHandler(this.tsBtnQuery_Click);
            // 
            // tsBtnUpdate
            // 
            this.tsBtnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnUpdate.Image = global::CKGen.Properties.Resources.update;
            this.tsBtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnUpdate.Name = "tsBtnUpdate";
            this.tsBtnUpdate.Size = new System.Drawing.Size(23, 22);
            this.tsBtnUpdate.Text = "有新版本发布";
            this.tsBtnUpdate.Click += new System.EventHandler(this.tsBtnUpdate_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "database.png");
            this.imageList1.Images.SetKeyName(1, "folder.png");
            this.imageList1.Images.SetKeyName(2, "proc.png");
            this.imageList1.Images.SetKeyName(3, "table.png");
            this.imageList1.Images.SetKeyName(4, "view.png");
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 25);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1061, 563);
            this.dockPanel.TabIndex = 1;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 588);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1061, 22);
            this.statusBar.TabIndex = 3;
            this.statusBar.Text = "statusStrip1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 610);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编程辅助工具v1.4";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton tsBtnQuery;
        private System.Windows.Forms.ToolStripButton tsBtnLinkDb;
        private System.Windows.Forms.ToolStripButton tsBtnGenDbChm;
        private System.Windows.Forms.ToolStripButton tsbtnSaveSchema;
        private System.Windows.Forms.ToolStripButton tsBtnReloadSchema;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripButton tsBtnUpdate;
    }
}
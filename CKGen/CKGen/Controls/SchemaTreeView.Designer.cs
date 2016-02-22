namespace CKGen.Controls
{
    partial class SchemaTreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaTreeView));
            this.tvSchema = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tvSchema
            // 
            this.tvSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSchema.HideSelection = false;
            this.tvSchema.ImageIndex = 0;
            this.tvSchema.ImageList = this.imageList1;
            this.tvSchema.Location = new System.Drawing.Point(0, 0);
            this.tvSchema.Name = "tvSchema";
            this.tvSchema.SelectedImageIndex = 0;
            this.tvSchema.Size = new System.Drawing.Size(150, 150);
            this.tvSchema.TabIndex = 0;
            this.tvSchema.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvSchema_ItemDrag);
            this.tvSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSchema_AfterSelect);
            this.tvSchema.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSchema_NodeMouseClick);
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
            // SchemaTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvSchema);
            this.Name = "SchemaTreeView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvSchema;
        private System.Windows.Forms.ImageList imageList1;
    }
}

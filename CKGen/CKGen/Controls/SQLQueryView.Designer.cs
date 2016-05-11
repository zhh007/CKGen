namespace CKGen.Controls
{
    partial class SQLQueryView
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pBox = new System.Windows.Forms.Panel();
            this.Tool2 = new System.Windows.Forms.ToolStrip();
            this.btnGenCode = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnResult = new System.Windows.Forms.ToolStripButton();
            this.btnMessage = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.Tool2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtCode);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pBox);
            this.splitContainer1.Panel2.Controls.Add(this.Tool2);
            this.splitContainer1.Size = new System.Drawing.Size(808, 405);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtCode
            // 
            this.txtCode.AllowDrop = true;
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCode.Location = new System.Drawing.Point(0, 25);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(808, 152);
            this.txtCode.TabIndex = 1;
            this.txtCode.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtCode_DragDrop);
            this.txtCode.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtCode_DragEnter);
            this.txtCode.DragOver += new System.Windows.Forms.DragEventHandler(this.txtCode_DragOver);
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRun,
            this.btnCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(808, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRun
            // 
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRun.Image = global::CKGen.Properties.Resources.PlayHS;
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(23, 22);
            this.btnRun.Text = "执行";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancel.Image = global::CKGen.Properties.Resources.StopHS;
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23, 22);
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pBox
            // 
            this.pBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBox.Location = new System.Drawing.Point(0, 25);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(808, 202);
            this.pBox.TabIndex = 3;
            // 
            // Tool2
            // 
            this.Tool2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Tool2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGenCode,
            this.btnExport,
            this.btnResult,
            this.btnMessage});
            this.Tool2.Location = new System.Drawing.Point(0, 0);
            this.Tool2.Name = "Tool2";
            this.Tool2.Size = new System.Drawing.Size(808, 25);
            this.Tool2.TabIndex = 0;
            this.Tool2.Text = "toolStrip2";
            // 
            // btnGenCode
            // 
            this.btnGenCode.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnGenCode.Image = global::CKGen.Properties.Resources.CSCodefile;
            this.btnGenCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenCode.Name = "btnGenCode";
            this.btnGenCode.Size = new System.Drawing.Size(76, 22);
            this.btnGenCode.Text = "生成代码";
            this.btnGenCode.Click += new System.EventHandler(this.btnGenCode_Click);
            // 
            // btnExport
            // 
            this.btnExport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExport.Image = global::CKGen.Properties.Resources.PageNumber;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(76, 22);
            this.btnExport.Text = "导出数据";
            this.btnExport.ToolTipText = "导出数据";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnResult
            // 
            this.btnResult.Image = global::CKGen.Properties.Resources.TableHS;
            this.btnResult.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(52, 22);
            this.btnResult.Text = "结果";
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // btnMessage
            // 
            this.btnMessage.Image = global::CKGen.Properties.Resources.Alerts;
            this.btnMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(52, 22);
            this.btnMessage.Text = "消息";
            this.btnMessage.Click += new System.EventHandler(this.btnMessage_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 408);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(808, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Image = global::CKGen.Properties.Resources.success;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(120, 17);
            this.StatusLabel.Text = "查询已成功执行。";
            // 
            // SQLQueryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SQLQueryView";
            this.Size = new System.Drawing.Size(808, 430);
            this.Load += new System.EventHandler(this.UCQuery_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Tool2.ResumeLayout(false);
            this.Tool2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip Tool2;
        private System.Windows.Forms.ToolStripButton btnGenCode;
        private System.Windows.Forms.ToolStripButton btnResult;
        private System.Windows.Forms.ToolStripButton btnMessage;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.Panel pBox;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripButton btnExport;
    }
}

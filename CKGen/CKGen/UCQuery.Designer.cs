namespace CKGen
{
    partial class UCQuery
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
            this.Tool2 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pBox = new System.Windows.Forms.Panel();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.btnGenCode = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripSplitButton();
            this.menuExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportWord = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.btnResult = new System.Windows.Forms.ToolStripButton();
            this.btnMessage = new System.Windows.Forms.ToolStripButton();
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
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.Tool2);
            this.splitContainer1.Size = new System.Drawing.Size(808, 430);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtCode
            // 
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCode.Location = new System.Drawing.Point(0, 25);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(808, 152);
            this.txtCode.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(808, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 227);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(808, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pBox
            // 
            this.pBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBox.Location = new System.Drawing.Point(0, 25);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(808, 202);
            this.pBox.TabIndex = 3;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Image = global::CKGen.Properties.Resources.success;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(120, 17);
            this.StatusLabel.Text = "查询已成功执行。";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::CKGen.Properties.Resources.PlayHS;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "执行";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::CKGen.Properties.Resources.StopHS;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "取消";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // btnGenCode
            // 
            this.btnGenCode.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnGenCode.Image = global::CKGen.Properties.Resources.EditCodeHS;
            this.btnGenCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenCode.Name = "btnGenCode";
            this.btnGenCode.Size = new System.Drawing.Size(76, 22);
            this.btnGenCode.Text = "生成代码";
            this.btnGenCode.Click += new System.EventHandler(this.btnGenCode_Click);
            // 
            // btnExport
            // 
            this.btnExport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExportExcel,
            this.menuExportWord,
            this.menuExportPDF,
            this.menuExportHTML});
            this.btnExport.Image = global::CKGen.Properties.Resources.PageNumber;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(64, 22);
            this.btnExport.Text = "导出";
            // 
            // menuExportExcel
            // 
            this.menuExportExcel.Name = "menuExportExcel";
            this.menuExportExcel.Size = new System.Drawing.Size(134, 22);
            this.menuExportExcel.Text = "导出Excel";
            this.menuExportExcel.Click += new System.EventHandler(this.menuExportExcel_Click);
            // 
            // menuExportWord
            // 
            this.menuExportWord.Name = "menuExportWord";
            this.menuExportWord.Size = new System.Drawing.Size(134, 22);
            this.menuExportWord.Text = "导出Word";
            this.menuExportWord.Click += new System.EventHandler(this.menuExportWord_Click);
            // 
            // menuExportPDF
            // 
            this.menuExportPDF.Name = "menuExportPDF";
            this.menuExportPDF.Size = new System.Drawing.Size(134, 22);
            this.menuExportPDF.Text = "导出PDF";
            this.menuExportPDF.Click += new System.EventHandler(this.menuExportPDF_Click);
            // 
            // menuExportHTML
            // 
            this.menuExportHTML.Name = "menuExportHTML";
            this.menuExportHTML.Size = new System.Drawing.Size(134, 22);
            this.menuExportHTML.Text = "导出HTML";
            this.menuExportHTML.Click += new System.EventHandler(this.menuExportHTML_Click);
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
            // UCQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCQuery";
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

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip Tool2;
        private System.Windows.Forms.ToolStripSplitButton btnExport;
        private System.Windows.Forms.ToolStripMenuItem menuExportExcel;
        private System.Windows.Forms.ToolStripMenuItem menuExportWord;
        private System.Windows.Forms.ToolStripMenuItem menuExportPDF;
        private System.Windows.Forms.ToolStripMenuItem menuExportHTML;
        private System.Windows.Forms.ToolStripButton btnGenCode;
        private System.Windows.Forms.ToolStripButton btnResult;
        private System.Windows.Forms.ToolStripButton btnMessage;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel pBox;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    }
}

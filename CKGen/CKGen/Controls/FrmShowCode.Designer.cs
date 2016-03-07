namespace CKGen.Controls
{
    partial class FrmShowCode
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
            this.codeView1 = new CKGen.Controls.CodeView();
            this.SuspendLayout();
            // 
            // codeView1
            // 
            this.codeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeView1.Location = new System.Drawing.Point(0, 0);
            this.codeView1.Name = "codeView1";
            this.codeView1.Size = new System.Drawing.Size(950, 646);
            this.codeView1.TabIndex = 0;
            // 
            // FrmShowCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 646);
            this.Controls.Add(this.codeView1);
            this.Name = "FrmShowCode";
            this.ResumeLayout(false);

        }

        #endregion

        private CodeView codeView1;
    }
}
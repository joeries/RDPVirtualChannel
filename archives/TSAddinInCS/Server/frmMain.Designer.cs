namespace TSAddinInCSServer
{
    partial class frmMain
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
            this.btLoadAndSend = new System.Windows.Forms.Button();
            this.ofdPickUpFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btLoadAndSend
            // 
            this.btLoadAndSend.Location = new System.Drawing.Point(12, 12);
            this.btLoadAndSend.Name = "btLoadAndSend";
            this.btLoadAndSend.Size = new System.Drawing.Size(147, 37);
            this.btLoadAndSend.TabIndex = 0;
            this.btLoadAndSend.Text = "Load && Send";
            this.btLoadAndSend.UseVisualStyleBackColor = true;
            this.btLoadAndSend.Click += new System.EventHandler(this.btLoadAndSend_Click);
            // 
            // ofdPickUpFile
            // 
            this.ofdPickUpFile.Filter = "Text files|*.txt";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 60);
            this.Controls.Add(this.btLoadAndSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TSAddinInCSServer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btLoadAndSend;
        private System.Windows.Forms.OpenFileDialog ofdPickUpFile;
    }
}


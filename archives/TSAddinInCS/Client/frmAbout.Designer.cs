namespace TSAddinInCS
{
    partial class frmAbout
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
            this.btOK = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblRights = new System.Windows.Forms.Label();
            this.lblVer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btOK.Location = new System.Drawing.Point(208, 9);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "OK";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(12, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(120, 16);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "TS addin in C#";
            // 
            // lblRights
            // 
            this.lblRights.Location = new System.Drawing.Point(12, 41);
            this.lblRights.Name = "lblRights";
            this.lblRights.Size = new System.Drawing.Size(283, 16);
            this.lblRights.TabIndex = 2;
            this.lblRights.Text = "©Copyright Selvin 2008.";
            // 
            // lblVer
            // 
            this.lblVer.Location = new System.Drawing.Point(12, 25);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(128, 16);
            this.lblVer.TabIndex = 1;
            this.lblVer.Text = "Version: ";
            // 
            // frmAbout
            // 
            this.CancelButton = this.btOK;
            this.ClientSize = new System.Drawing.Size(303, 70);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.lblVer);
            this.Controls.Add(this.lblRights);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About...";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblRights;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.Button btOK;
    }
}
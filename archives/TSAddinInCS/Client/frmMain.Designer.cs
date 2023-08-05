namespace TSAddinInCS
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuNIContext = new System.Windows.Forms.ContextMenu();
            this.mnuNICShow = new System.Windows.Forms.MenuItem();
            this.mnuNICAuto = new System.Windows.Forms.MenuItem();
            this.mnuNICSeparator = new System.Windows.Forms.MenuItem();
            this.mnuNICAbout = new System.Windows.Forms.MenuItem();
            this.niMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.mnuMain = new System.Windows.Forms.MainMenu(this.components);
            this.mnuOptions = new System.Windows.Forms.MenuItem();
            this.mnuAuto = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.mnuLVCOpen = new System.Windows.Forms.MenuItem();
            this.mnuLVCSaveAs = new System.Windows.Forms.MenuItem();
            this.mnuLVContext = new System.Windows.Forms.ContextMenu();
            this.ilMain = new System.Windows.Forms.ImageList(this.components);
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.chTime = new System.Windows.Forms.ColumnHeader();
            this.chFile = new System.Windows.Forms.ColumnHeader();
            this.lvMain = new System.Windows.Forms.ListView();
            this.mnuSendFile = new System.Windows.Forms.MenuItem();
            this.ofdMain = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // mnuNIContext
            // 
            this.mnuNIContext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNICShow,
            this.mnuNICAuto,
            this.mnuNICSeparator,
            this.mnuNICAbout});
            // 
            // mnuNICShow
            // 
            this.mnuNICShow.DefaultItem = true;
            this.mnuNICShow.Index = 0;
            this.mnuNICShow.Text = "Show";
            this.mnuNICShow.Click += new System.EventHandler(this.ShowMe);
            // 
            // mnuNICAuto
            // 
            this.mnuNICAuto.Index = 1;
            this.mnuNICAuto.Text = "Auto-open";
            this.mnuNICAuto.Click += new System.EventHandler(this.mnuAuto_Click);
            // 
            // mnuNICSeparator
            // 
            this.mnuNICSeparator.Index = 2;
            this.mnuNICSeparator.Text = "-";
            // 
            // mnuNICAbout
            // 
            this.mnuNICAbout.Index = 3;
            this.mnuNICAbout.Text = "About...";
            this.mnuNICAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // niMain
            // 
            this.niMain.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.niMain.BalloonTipTitle = "Kliknij aby otworzyæ plik";
            this.niMain.ContextMenu = this.mnuNIContext;
            this.niMain.Icon = ((System.Drawing.Icon)(resources.GetObject("niMain.Icon")));
            this.niMain.Text = "EORDP";
            this.niMain.Visible = true;
            this.niMain.BalloonTipClicked += new System.EventHandler(this.niMain_BalloonTipClicked);
            this.niMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niMain_MouseDoubleClick);
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuOptions,
            this.mnuHelp});
            // 
            // mnuOptions
            // 
            this.mnuOptions.Index = 0;
            this.mnuOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAuto,
            this.mnuSendFile});
            this.mnuOptions.Text = "Options";
            // 
            // mnuAuto
            // 
            this.mnuAuto.Index = 0;
            this.mnuAuto.Text = "Auto-open";
            this.mnuAuto.Click += new System.EventHandler(this.mnuAuto_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 1;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAbout});
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Index = 0;
            this.mnuAbout.Text = "About...";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuLVCOpen
            // 
            this.mnuLVCOpen.Index = 0;
            this.mnuLVCOpen.Text = "Open";
            this.mnuLVCOpen.Click += new System.EventHandler(this.mnuLVCOpen_Click);
            // 
            // mnuLVCSaveAs
            // 
            this.mnuLVCSaveAs.Index = 1;
            this.mnuLVCSaveAs.Text = "Save As...";
            this.mnuLVCSaveAs.Click += new System.EventHandler(this.mnuLVCSaveAs_Click);
            // 
            // mnuLVContext
            // 
            this.mnuLVContext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuLVCOpen,
            this.mnuLVCSaveAs});
            // 
            // ilMain
            // 
            this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
            this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMain.Images.SetKeyName(0, "txt.png");
            // 
            // pbMain
            // 
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbMain.Location = new System.Drawing.Point(0, 142);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(474, 23);
            this.pbMain.TabIndex = 4;
            // 
            // chTime
            // 
            this.chTime.Text = "Time";
            this.chTime.Width = 130;
            // 
            // chFile
            // 
            this.chFile.Text = "File";
            this.chFile.Width = 320;
            // 
            // lvMain
            // 
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTime,
            this.chFile});
            this.lvMain.ContextMenu = this.mnuLVContext;
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.FullRowSelect = true;
            this.lvMain.Location = new System.Drawing.Point(0, 0);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(474, 142);
            this.lvMain.SmallImageList = this.ilMain;
            this.lvMain.TabIndex = 5;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseDoubleClick);
            // 
            // mnuSendFile
            // 
            this.mnuSendFile.Index = 1;
            this.mnuSendFile.Text = "Send file...";
            this.mnuSendFile.Click += new System.EventHandler(this.mnuSendFile_Click);
            // 
            // ofdMain
            // 
            this.ofdMain.Filter = "Text files|*.txt";
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(474, 165);
            this.Controls.Add(this.lvMain);
            this.Controls.Add(this.pbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Menu = this.mnuMain;
            this.Name = "frmMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TS addin in C#:";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.VisibleChanged += new System.EventHandler(this.frmMain_VisibleChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenu mnuNIContext;
        private System.Windows.Forms.MenuItem mnuNICShow;
        private System.Windows.Forms.MenuItem mnuNICAuto;
        private System.Windows.Forms.MenuItem mnuNICSeparator;
        private System.Windows.Forms.MenuItem mnuNICAbout;
        private System.Windows.Forms.NotifyIcon niMain;
        private System.Windows.Forms.MainMenu mnuMain;
        private System.Windows.Forms.MenuItem mnuOptions;
        private System.Windows.Forms.MenuItem mnuAuto;
        private System.Windows.Forms.MenuItem mnuHelp;
        private System.Windows.Forms.MenuItem mnuAbout;
        private System.Windows.Forms.MenuItem mnuLVCOpen;
        private System.Windows.Forms.MenuItem mnuLVCSaveAs;
        private System.Windows.Forms.ContextMenu mnuLVContext;
        private System.Windows.Forms.ImageList ilMain;
        public System.Windows.Forms.ProgressBar pbMain;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ColumnHeader chFile;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.MenuItem mnuSendFile;
        private System.Windows.Forms.OpenFileDialog ofdMain;
    }
}
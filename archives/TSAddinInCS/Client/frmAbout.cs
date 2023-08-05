using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TSAddinInCS
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, System.EventArgs e)
        {
            this.lblVer.Text += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
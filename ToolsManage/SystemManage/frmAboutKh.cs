using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ToolsManage.SystemManage
{
    public partial class frmAboutKh : DevExpress.XtraEditors.XtraForm
    {
        public frmAboutKh()
        {
            InitializeComponent();
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ToolsManage.DoorManage
{
    public partial class frmPowerIn : DevExpress.XtraEditors.XtraForm
    {
        public frmPowerIn()
        {
            InitializeComponent();
        }

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            fbDialogFile.ShowDialog();
            txtDSPath.Text = fbDialogFile.SelectedPath.ToString().Trim() + "\\";
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {

        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
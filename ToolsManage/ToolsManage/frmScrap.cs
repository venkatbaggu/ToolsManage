using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ToolsManage.ToolsManage
{
    public partial class frmScrap : DevExpress.XtraEditors.XtraForm
    {
        public static string strScrapInfo = "";

        public frmScrap()
        {
            InitializeComponent();
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            strScrapInfo = cbbScrapInfo.Text;
            DialogResult = DialogResult.OK;
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmScrap_Load(object sender, EventArgs e)
        {

        }




    }
}
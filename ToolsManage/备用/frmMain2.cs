using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;

using ToolsManage.ToolsManage;

namespace ToolsManage
{
    public partial class frmMain2 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain2()
        {
            InitializeComponent();

            InitSkinGallery();
        }

        void InitSkinGallery()
        {
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkins, true);
            this.ribbonControl.Toolbar.ItemLinks.Clear();
            this.ribbonControl.Toolbar.ItemLinks.Add(rgbiSkins);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnTools_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmToolsManage frm = new frmToolsManage();
            frm.ShowDialog(this );
            frm.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }











    }
}
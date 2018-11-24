using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ToolsManage.BaseClass;

using System.IO;

namespace ToolsManage.SystemManage
{
    public partial class frmDataReturn : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        public frmDataReturn()
        {
            InitializeComponent();
        }

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                ofDialogFile.ShowDialog();
                txtDRPath.Text = ofDialogFile.FileName.ToString().Trim();
            }
            catch
            { 
            
            }
        
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            string str = txtDRPath.Text.Trim();
            if (string.IsNullOrEmpty(str))
            {
                MessageUtil.ShowTips("请选择数据库备份文件位置!");
            }

            if (MessageUtil.ShowYesNoAndTips("还原数据库会造成现所有数据文件被覆盖，确认还原数据库？") == DialogResult.Yes)
            {
                try
                {
                    datalogic.SqlComNonQuery("BACKUP DATABASE ToolsManage to disk='" + str
                        + "' use master restore database ToolsManage from disk='" + str + "'");
                    MessageUtil.ShowTips("数据还原成功!");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowTips(ex.Message);
                }
            }
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
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
    public partial class frmDataBack : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        public frmDataBack()
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
            string str = txtDSPath.Text.Trim();
            if (string.IsNullOrEmpty(str))
            {
                MessageUtil.ShowTips("请选择数据库文件备份位置!");
            }
            else
            {
                try
                {
                    if (File.Exists(str + ".bak"))
                    {
                        MessageUtil.ShowTips("该文件已经存在!");
                        txtDSPath.Text = "";
                        txtDSPath.Focus();
                    }
                    else
                    {
                        datalogic.SqlComNonQuery("backup database ToolsManage to disk='" + txtDSPath.Text.Trim() + ".bak'");
                        MessageUtil.ShowTips("数据备份成功!");
                    }
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
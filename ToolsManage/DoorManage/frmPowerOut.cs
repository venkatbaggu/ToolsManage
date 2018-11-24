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

namespace ToolsManage.DoorManage
{
    public partial class frmPowerOut : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        public frmPowerOut()
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
                MessageUtil.ShowTips("请选择导出权限文件存放位置位置!");
            }
            else
            {
                try
                {
                    string filePath = str + "DoorPower.txt";     // "C:\\Test.txt";

                    if (FileUtil.IsExistFile(filePath))
                        FileUtil.ClearFile(filePath);
                    else 
                    FileUtil.AppendText(filePath, "");

                    string strSql = "select ID,tvParent,tvChildId,IsGroup,GroupName,UserName,IcNo,IsPower from tb_DoorUser  ";
                    DataTable dt = datalogic.GetDataTable(strSql);
                    foreach (DataRow datarow in dt.Rows)
                    {
                        string strTxt = "";
                        strTxt = datarow["ID"].ToString();
                        strTxt += ",";
                        strTxt += datarow["tvParent"].ToString();
                        strTxt += ",";
                        strTxt += datarow["tvChildId"].ToString();
                        strTxt += ",";
                        strTxt += datarow["IsGroup"].ToString();
                        strTxt += ",";
                        strTxt += datarow["GroupName"].ToString();
                        strTxt += ",";
                        strTxt += datarow["IcNo"].ToString();
                        strTxt += ",";
                        strTxt += datarow["IsPower"].ToString();
                        strTxt += "\r\n";

                        FileUtil.AppendText(filePath, strTxt);
                    }
                    MessageUtil.ShowTips("导出权限成功!");
                    
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
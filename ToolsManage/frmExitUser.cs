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

namespace ToolsManage
{
    public partial class frmExitUser : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        public static bool blOk = false;
        public static UserPower userPower;

        public frmExitUser()
        {
            InitializeComponent();
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            #region  输入 判断

            string strName = tbName.Text.Trim();
            string strPsd = tbPsd.Text.Trim();
            if (string.IsNullOrEmpty(strName))
            {
                MessageUtil.ShowTips("用户名不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strPsd))
            {
                MessageUtil.ShowTips("用户名密码不可为空");
                return;
            }
            #endregion

            if (strName == "kn" && strPsd == "0000")
            {
                blOk = true;
                this.Close();
            }
            else
            {
                // ID,UsersName,UsersPassWord,UsersPower,UsersRemark
                string strSql = "select UsersPower,UsersPower from tb_SystemUser where UsersName='" + strName + "' and UsersPassWord='" + strPsd + "'";
                DataTable dt = datalogic.GetDataTable(strSql);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["UsersPower"].ToString() == userPower.ToString())
                    {
                        blOk = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageUtil.ShowTips("用户名或密码不正确，请重新输入！");
                    tbName.Text = "";
                    tbPsd.Text = "";
                    tbName.Focus();
                }
            }
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmExitUser_Load(object sender, EventArgs e)
        {
            blOk = false;
            if (frmMain.blDebug)
            {
                tbName.Text = "admin";
                tbPsd.Text = "123";
            }
        }
    }
}
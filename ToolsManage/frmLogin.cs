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
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        /// <summary>
        /// 有登录事件
        /// </summary>
        public static bool blLoginEvent=false ;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

            blLoginEvent = true;
            if (strName == "kn" && strPsd == "0000")
            {
                frmMain.strUserName = strName;
                frmMain.systemPower = UserPower.厂家;
                this.Close();
                //this.Hide();
                //frmMain frm = new frmMain();
                //frm.Show();
                //return;
            }
            else
            {
                // ID,UsersName,UsersPassWord,UsersPower,UsersRemark
                string strSql = "select ID,UsersPower from tb_SystemUser where UsersName='" + strName + "' and UsersPassWord='" + strPsd + "'";
                DataTable dt = datalogic.GetDataTable(strSql);
                if (dt.Rows.Count > 0)
                {
                    frmMain.tiemLogin = DateTime.Now;
                    frmMain.strUserName = strName;
                    frmMain.strUserID = dt.Rows[0]["ID"].ToString();
                    if (UserPower.系统用户.ToString() == dt.Rows[0]["UsersPower"].ToString())
                    {
                        frmMain.systemPower = UserPower.系统用户;
                        this.Close();
                    }
                    else if (UserPower.普通用户.ToString() == dt.Rows[0]["UsersPower"].ToString())
                    {
                        frmMain.systemPower = UserPower.普通用户;
                        this.Close();
                    }

                    //this.Hide();
                    //frmMain frm = new frmMain();
                    //frm.Show();
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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            frmMain.tiemLogin = DateTime.Now;
            tbName.Focus();
            if (frmMain.blDebug)
            {
                tbName.Text = "kn";
                tbPsd.Text = "0000";
            }
        }
    }
}
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

namespace ToolsManage.SystemManage
{
    public partial class frmUsersEdit : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        public string strID = "";
        public string strName = "";
        public string strPsd = "";
        public string strPower = "";
        public string strRemark = "";

        public frmUsersEdit()
        {
            InitializeComponent();
        }

        private void frmUsersEdit_Load(object sender, EventArgs e)
        {
            if (this.Tag.ToString() == TagType.添加.ToString())
                this.Text = "添加用户";
            else if (this.Tag.ToString() == TagType.修改.ToString())
            {
                this.Text = "修改用户";
                tbUser.Text  = strName;
                tbPsd.Text = strPsd;
                cbbPower.Text = strPower;
                tbRemark.Text = strRemark;
            } 
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            #region  输入 判断

            strName = tbUser.Text.Trim();
            strPsd = tbPsd.Text.Trim();
            strPower = cbbPower.Text.Trim();
            strRemark = tbRemark.Text.Trim();
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
            if (string.IsNullOrEmpty(strPower))
            {
                MessageUtil.ShowTips("用户名权限不可为空");//tb_SystemUser ID,UsersName,UsersPassWord,UsersPower,UsersRemark
                return;
            }

            #endregion

            if (this.Text == "添加用户")
            {
                string strSql = "insert into tb_SystemUser (UsersName,UsersPassWord,UsersPower,UsersRemark)" +
                "values ('" + strName + "','" + strPsd + "','" + strPower + "','" + strRemark + "')";
                datalogic.SqlComNonQuery(strSql);
            }
            else if (this.Text == "修改用户")
            {
                string strSql = "update tb_SystemUser set UsersName='" + strName + "',UsersPassWord='" + strPsd + "',UsersPower='" + strPower + "'," +
                      "UsersRemark='" + strRemark + "' where ID='" + strID + "' ";
                datalogic.SqlComNonQuery(strSql);
            }
            this.Close();
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }







    }
}
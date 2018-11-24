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

namespace ToolsManage.ToolsManage
{
    public partial class frmInRoomPower : DevExpress.XtraEditors.XtraForm
    {
        private DataOperate operatedata = new DataOperate();
        private DataLogic datalogic = new DataLogic();

        private string strTreeSeleParent = "";

        public string sId = "";
        public string sToolType = "";
        public string sToolName = "";
        public string sToolId = "";
        public string sPlace = "";
        public string sRFID = "";

        public frmInRoomPower()
        {
            InitializeComponent();

        }

        private void frmInRoom_Load(object sender, EventArgs e)
        {
            cbbOverPeople.Text = frmMain.strUserName;

            #region  绑定 工具种类

            string strSql = "select ToolType from tb_TypeAndName where tvParent='0'";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                cbbToolType.Items.Add(dr["ToolType"].ToString());
            }
            if (cbbToolType.Items .Count >0)
                cbbToolType.SelectedIndex = 0;
            #endregion

            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();

            if (this.Tag != null)
            {
                if (this.Tag.ToString() == TagType.修改.ToString())
                {
                    cbbToolType.Text = sToolType;
                    cbbToolName.Text = sToolName;
                    tbToolID.Text = sToolId;
                    tbPlace.Text = sPlace;
                    tbRFID.Text = sRFID;
                }
                else if (this.Tag.ToString() == TagType.添加.ToString())
                {
                    strSql = "select top 1 RFIDCoding from tb_Tools where IsArea='"+ToolAreaType .工具 .ToString ()+"' order by id desc ";// select top 1 * from table order by id desc
                    object ob = datalogic.SqlComScalar(strSql);

                    int iNo = 0;
                    try
                    {
                        if (ob != null)
                        {
                            iNo = Convert.ToInt32(ob.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        if (frmMain.blDebug)
                            MessageUtil.ShowTips(ex.Message);
                    }

                    iNo++;
                    string strNo = iNo.ToString();
                    int iLength = 6 - strNo.Length;
                    strNo = clsCommon.FormatString(iLength) + strNo;
                    tbRFID.Text = strNo;
                    
                }
            }
        }

        private void AddTreeView(string strParent, TreeNode parentNode)
        {
            //  tvParent tvChildId  IsArea  AreaName
            string strSql = "select tvParent,tvChildId,IsArea,AreaName,PlaceName,ToolID,IsInStore from tb_Tools where tvParent='" + strParent + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);

            foreach (DataRow datarow in dt.Rows)
            {
                TreeNode node = new TreeNode();
                //处理根节点
                if (parentNode == null)
                {
                    node.Name = datarow["tvChildId"].ToString();// node.Name 为本节点的编号
                    node.Tag = strParent;                       // node.Tag 为本节点父节点的编号
                    if (datarow["IsArea"].ToString() == ToolAreaType.区域.ToString())
                    {
                        node.Text = datarow["AreaName"].ToString();
                        node.ImageIndex = 0;
                        node.SelectedImageIndex = 0;
                    }
                    treeView1.Nodes.Add(node);
                    AddTreeView(datarow["tvChildId"].ToString(), node);
                }
                //处理子节点
                else
                {
                    node.Name = datarow["tvChildId"].ToString();
                    node.Tag = strParent;
                    if (datarow["IsArea"].ToString() == ToolAreaType.区域.ToString())
                    {
                        node.Text = datarow["AreaName"].ToString();
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                    }
                    else if (datarow["IsArea"].ToString() == ToolAreaType.位置.ToString())
                    {
                        node.Text = datarow["PlaceName"].ToString();
                        node.ImageIndex = 2;
                        node.SelectedImageIndex = 2;
                    }
                    else if (datarow["IsArea"].ToString() == "工具")
                    {
                        node.Text = datarow["ToolID"].ToString();
                        node.ImageIndex = 3;
                        node.SelectedImageIndex = 3;
                    }
                    parentNode.Nodes.Add(node);
           
                    AddTreeView(datarow["tvChildId"].ToString(), node);
                }
            }

        }

        private void cbbToolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region  自动生成工具ID

            string strName = cbbToolName.Text;
            string strSql = "select top 1 ToolID from tb_Tools where IsArea='"+ToolAreaType .工具 .ToString ()+"' and ToolName='" + cbbToolName.Text + "' order by id desc ";// select top 1 * from table order by id desc
            object ob = datalogic.SqlComScalar(strSql);

            int iNo = 0;
            if (ob != null)
            {
                int iLength = strName.Length;
                string strToolId = ob.ToString();
                string strName2 = strToolId.Substring(0,iLength);
                if (strName == strName2)
                {
                    if ((strToolId.Length - iLength) == 3)//数字部分的长度
                    {
                        string strNo = strToolId.Substring(iLength,3);
                        try
                        {
                             iNo = Convert.ToInt32(strNo);
                        }
                        catch(Exception ex)
                        {
                            //iNo = 0;
                            if (frmMain.blDebug)
                                MessageUtil.ShowTips(ex.Message);
                        }
                    }
                }
            }

            iNo++;
            string striNo = iNo.ToString();
            int iLen = 3 - striNo.Length;
            striNo = clsCommon.FormatString(iLen) + striNo;
            tbToolIdAdd.Text = striNo;
            tbToolID.Text = strName + striNo;

            #endregion

            strSql = "select ToolsCycle from tb_TypeAndName where ToolName='" + cbbToolName.Text.Trim() + "' ";
            ob = datalogic.SqlComScalar(strSql);
            if (ob != null)
            {
                tbCycle.Text = ob.ToString();
            }



            #region   自动生成 工具ID 

            //string str = cbbToolName.Text;
            //string strSql = "select ToolID from tb_Tools where ToolName='" + str + "'";
            //DataTable dt = datalogic.GetDataTable(strSql);
            //int i = dt.Rows.Count;
            //i++;
            //str = i.ToString();
            //if (str.Length == 1)
            //{
            //    str = "00" + str;
            //}
            //else if (str.Length == 2)
            //{
            //    str = "0" + str;
            //}
            //tbToolID.Text = cbbToolName.Text + str;

            #endregion

            #region  自动生成 RFID 

            //if (str.Length == 3)
            //{
            //    str = "0" + str;
            //}
            //tbRFID.Text = str;

            //strSql = "select ToolsNameCoding from tb_TypeAndName where ToolName='" + cbbToolName.Text + "' ";
            //object ob = datalogic.SqlComScalar(strSql);
            //if (ob != null)
            //{
            //    tbRFID1 .Text =ob.ToString();
            //}

            #endregion

        }

        private void cbbToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbToolName.Items.Clear();
            string strSql = "select tvChildId from tb_TypeAndName where ToolType='" + cbbToolType.Text.Trim() + "' ";
            object ob = datalogic.SqlComScalar(strSql);
            if (ob != null)
            {
                strSql = "select ToolName from tb_TypeAndName where tvParent='" + ob.ToString() + "'";
                DataTable dt = datalogic.GetDataTable(strSql);
                foreach (DataRow dr in dt.Rows)
                {
                    cbbToolName.Items.Add(dr["ToolName"].ToString());
                }
                cbbToolName.SelectedIndex = 0;
            }
       }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            #region  输入 判断

            string strToolId = tbToolID.Text.Trim();//strToolId
            string strRFID = tbRFID.Text.Trim();
            string strToolType = cbbToolType.Text.Trim();
            string strToolName = cbbToolName.Text.Trim();
            string strPlace = tbPlace.Text.Trim();
            string strPeople = cbbOverPeople.Text.Trim();
            string strTime = dtpTime.Text;

            if (string.IsNullOrEmpty(strToolId))
            {
                MessageUtil.ShowTips("工具ID不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strPlace))
            {
                MessageUtil.ShowTips("工具存放位置不可为空");
                return;
            }
            if (this.Tag != null)
            {
                string strSql = "";
                if (this.Tag.ToString() == TagType.修改.ToString())
                {
                    strSql = "Select ID From tb_Tools where ToolID='" + strToolId + "' and ID<>'" + sId + "'";
                }
                else if (this.Tag.ToString() == TagType.添加.ToString())
                {
                    strSql = "Select ID From tb_Tools where ToolID='" + strToolId + "' ";
                }
                if (!string.IsNullOrEmpty(strSql))
                {
                    if (operatedata.blCheckHas(strSql))
                    {
                        MessageUtil.ShowTips("工具ID重复，请重新输入");
                        return;
                    }
                }

            }

            if (string.IsNullOrEmpty(strRFID))
            {
                MessageUtil.ShowTips("RFID编号不可为空");
                return;
            }
            else
            {
                if (strRFID.Length != 6)
                {
                    MessageUtil.ShowTips("RFID编号格式错误，请重新输入");
                    return;
                }
                else
                {

                    if (this.Tag != null)
                    {
                        string strSql = "";
                        if (this.Tag.ToString() == TagType.修改.ToString())
                        {
                            strSql = "Select ID From tb_Tools where RFIDCoding='" + strRFID + "' and ID<>'" + sId + "'";
                        }
                        else if (this.Tag.ToString() == TagType.添加.ToString())
                        {
                            strSql = "Select ID From tb_Tools where RFIDCoding='" + strRFID + "' ";
                        }
                        if (!string.IsNullOrEmpty(strSql))
                        {
                            if (operatedata.blCheckHas(strSql))
                            {
                                MessageUtil.ShowTips("RFID编号重复，请重新输入");
                                return;
                            }
                        }
                    }

                }
            }
            #endregion


            if (this.Tag != null)
            {
                if (this.Tag.ToString() == TagType.修改.ToString())
                {
                    string strSql = "update tb_Tools set tvParent='" + strTreeSeleParent + "',ToolType='" + strToolType + "',ToolName='" + strToolName + "'" +
                        ",ToolID='" + strToolId + "',RFIDCoding='" + strRFID + "',StoragePlace='" + strPlace + "' ,TestCycle='" + tbCycle.Text + "'" +
                        "where ID='" + sId + "' ";
                    datalogic.SqlComNonQuery(strSql);
                    this.Close();
                    //treeView1.Nodes.Clear();
                    //AddTreeView("0", (TreeNode)null);
                    //treeView1.ExpandAll();
                }
                else if (this.Tag.ToString() == TagType.添加.ToString())
                {
                    TreeNode node = new TreeNode();

                    #region  生成tvChildId
                    string sql = "select top 1 tvChildId from tb_Tools order by id desc ";// select top 1 * from table order by id desc
                    object ob = datalogic.SqlComScalar(sql);
                    int iNo = 0;
                    if (ob != null)
                    {
                        iNo = Convert.ToInt32(ob.ToString());
                    }
                    iNo++;
                    #endregion

                    string strChild = iNo.ToString();
                    node.Name = strChild;// node.Name 为本节点的编号 
                    string strParent = treeView1.SelectedNode.Name.ToString();
                    node.Tag = strParent;
                    node.Text = strToolId;
                    node.ImageIndex = 3;
                    node.SelectedImageIndex = 3;
                    treeView1.SelectedNode.Nodes.Add(node);
                    treeView1.ExpandAll();

                    string strSql = "insert into tb_Tools (tvParent,tvChildId,IsArea,ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,InPeople,InTime,IsInStore,TestCycle,TestState)" +
                                    "values ('" + strParent + "','" + strChild + "','"+ToolAreaType .工具 .ToString ()+"','" + strToolType + "','" + strToolName + "'," +
                                    "'" + strToolId + "','" + strRFID + "','" + strPlace + "','" + strPeople + "','" + strTime + "','" + ToolsState.在库.ToString() + "','" + tbCycle.Text + "','未录入试验记录')";
                    datalogic.SqlComNonQuery(strSql);

                    strSql = "insert into tb_RecordInRoom (ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,InPeople,InTime) values ('" + strToolType + "'," +
                        "'" + strToolName + "','" + strToolId + "','" + strRFID + "','" + strPlace + "','" + strPeople + "','" + strTime + "')";
                    datalogic.SqlComNonQuery(strSql);

                    string str = tbToolIdAdd.Text;
                    if (!string.IsNullOrEmpty(str))
                    {
                        int i = Convert.ToInt32(str);
                        i++;
                        str = i.ToString();
                        tbToolIdAdd.Text = str;
                        int iLength = 3 - str.Length;
                        str = clsCommon.FormatString(iLength) + str;

                        tbToolID.Text = cbbToolName.Text + str;
                    }

                    str = tbRFID.Text;
                    if (!string.IsNullOrEmpty(str))
                    {
                        int i = Convert.ToInt32(str);
                        i++;
                        str = i.ToString();
                        int iLength = 6 - str.Length;
                        str = clsCommon.FormatString(iLength) + str;
                        tbRFID.Text = str;
                    }
                }
            }

        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private byte[] HexStringToByteArray(string s)//字符串转16进制数组
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }
            return buffer;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.ImageIndex == 2)
            {
                strTreeSeleParent = treeView1.SelectedNode.Name;
                tbPlace.Text = treeView1.SelectedNode.Text.ToString();
                sbtnOk.Enabled = true;
            }
            else
            {
                sbtnOk.Enabled = false;
            }
        }

        private void sbtnExpand_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
            {
                if (treeView1.Nodes[0].IsExpanded == true)
                    treeView1.CollapseAll();
                else
                    treeView1.ExpandAll();
            }
        }







    }
}
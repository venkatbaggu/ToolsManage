using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Threading;

using ToolsManage.BaseClass;
using ToolsManage.UDP;

namespace ToolsManage.ToolsManage
{
    public partial class frmBoxPower : DevExpress.XtraEditors.XtraForm
    {
        public UDPClientManager manager;

        DataLogic datalogic = new DataLogic();
        DataTable dataTable = new DataTable();
        private string strAlterChildId = "";
        private string strSn = "";
        private string strIp = "";
        private bool blRet = false;

        private byte[] bSendDoor = new byte[64] { 0x19, 0x8C, 0x00, 0x00, 0x1F, 0x9B, 0x57, 0x07, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00 };

        public frmBoxPower()
        {
            InitializeComponent();
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }

        private void frmBoxPower_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();

            TableInit();
            ShowDataTable();

            if (manager == null)
            {
                manager = new UDPClientManager("61003");
                manager.Start(int.Parse("61003"));  //开启端口监听
                manager.UDPMessageReceived += new UDPMessageReceivedEventHandler(manager_UDPMessageReceived);
            }


            //if (UDPClientManager.ClientExist("61003"))
            //{
            //    MessageBox.Show("客户端已存在！");
            //    return;
            //}
            //else
            //{

            //}
        }

        #region  子程序

        void manager_UDPMessageReceived(string csID, UDPMessageReceivedEventArgs args)
        {
            try
            {
                if (args.Data.Length >= 64)
                {
                    if (args.RemoteIP == strIp && args.Data[8] == 0x01 && args.Data[1] == 0x8C)
                    {
                        blRet = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获得数据库信息放入TreeView中 (递归)   
        /// </summary>
        /// <param name="strParent"></param>
        /// <param name="parentNode"></param>
        private void AddTreeView(string strParent, TreeNode parentNode)
        {
            //  tvParent tvChildId  IsArea  AreaName
            string strSql = "select tvParent,tvChildId,IsArea,AreaName,PlaceName,ToolID from tb_Tools where tvParent='" + strParent + "' ";
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
                        treeView1.Nodes.Add(node);
                        AddTreeView(datarow["tvChildId"].ToString(), node);
                    }
                    else if (datarow["IsArea"].ToString() == ToolAreaType.工具柜.ToString())
                    {
                        node.Text = datarow["PlaceName"].ToString();
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 4;
                        treeView1.Nodes.Add(node);
                        AddTreeView(datarow["tvChildId"].ToString(), node);
                    }
       
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
                        parentNode.Nodes.Add(node);
                        AddTreeView(datarow["tvChildId"].ToString(), node);
                    }
                    //else if (datarow["IsArea"].ToString() == ToolAreaType.工具.ToString())
                    //{
                    //    node.Text = datarow["ToolID"].ToString();
                    //    node.ImageIndex = 3;
                    //    node.SelectedImageIndex = 3;
                    //    parentNode.Nodes.Add(node);
                    //    AddTreeView(datarow["tvChildId"].ToString(), node);
                    //}
                    else if (datarow["IsArea"].ToString() == ToolAreaType.工具柜.ToString())
                    {
                        node.Text = datarow["PlaceName"].ToString();
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 4;
                        parentNode.Nodes.Add(node);
                        AddTreeView(datarow["tvChildId"].ToString(), node);
                    }
                }
            }

        }

        private void TableInit()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PlaceName";
            column.Caption = "工具柜名称";
            dataTable.Columns.Add(column);//DoorPsw1,DoorPsw2,DoorPsw3,DoorPsw4

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "DoorPsw1";
            column.Caption = "柜门密码1";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "DoorPsw2";
            column.Caption = "柜门密码2";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "DoorPsw3";
            column.Caption = "柜门密码3";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "DoorPsw4";
            column.Caption = "柜门密码4";
            dataTable.Columns.Add(column);
        }

        private void ShowDataTable()//   
        {
            dataTable.Rows.Clear();

            string strSql = "select PlaceName,DoorPsw1,DoorPsw2,DoorPsw3,DoorPsw4 from tb_Tools where  " +
                "IsArea='" + ToolAreaType.工具柜.ToString() + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                DataRow row = dataTable.NewRow();
                row["PlaceName"] = datarow["PlaceName"].ToString();
                row["DoorPsw1"] = datarow["DoorPsw1"].ToString();
                row["DoorPsw2"] = datarow["DoorPsw2"].ToString();
                row["DoorPsw3"] = datarow["DoorPsw3"].ToString();
                row["DoorPsw4"] = datarow["DoorPsw4"].ToString();
                dataTable.Rows.Add(row);
            }
            this.gridControl1.DataSource = dataTable;
        }

        #endregion

        private void sbtnNew_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();

            //EditType = TagType.初值;
            ShowDataTable();
            treeView1.Enabled = true;
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

        private void sbtnSetPsw_Click(object sender, EventArgs e)
        {
            treeView1.Enabled = false;
            if (treeView1.SelectedNode == null||treeView1.SelectedNode.ImageIndex != 4)
            {
                MessageUtil.ShowTips("请选择工具柜");
                treeView1.Enabled = true;
            }
            else
            {
                strAlterChildId = treeView1.SelectedNode.Name.ToString();
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
                string strSql = "select AreaName,PlaceName,DoorPsw1,DoorPsw2,DoorPsw3,DoorPsw4,DoorIp,DoorSn  from tb_Tools where tvChildId='" + strAlterChildId + "' ";
                DataTable dt = datalogic.GetDataTable(strSql);//
                if (dt.Rows.Count > 0)
                {
                    tbArea.Text = dt.Rows[0]["AreaName"].ToString(); 
                    tbBoxName.Text = dt.Rows[0]["PlaceName"].ToString();
                    tbPsw1.Text = dt.Rows[0]["DoorPsw1"].ToString();
                    tbPsw2.Text = dt.Rows[0]["DoorPsw2"].ToString();
                    tbPsw3.Text = dt.Rows[0]["DoorPsw3"].ToString();
                    tbPsw4.Text = dt.Rows[0]["DoorPsw4"].ToString();
                    strIp = dt.Rows[0]["DoorIp"].ToString();
                    strSn = dt.Rows[0]["DoorSn"].ToString();
                }
            }
        }

        private void sbtnPswOk_Click(object sender, EventArgs e)
        {
            sbtnPswOk.Enabled = false;

            #region 输入 判断

            string strArea = tbArea.Text.Trim();
            string strName = tbBoxName.Text.Trim();
            string strPsw1 = tbPsw1.Text.Trim();
            string strPsw2 = tbPsw2.Text.Trim();
            string strPsw3 = tbPsw3.Text.Trim();
            string strPsw4 = tbPsw4.Text.Trim();
            int iPsw1=0;
            int iPsw2=0;
            int iPsw3=0;
            int iPsw4=0;

            if (!string.IsNullOrEmpty(strPsw1))
            {
                if (strPsw1.Length < 4 || strPsw1.Length > 6)
                {
                    MessageUtil.ShowTips("密码1长度错误，长度应为4-6位");
                    sbtnPswOk.Enabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        iPsw1 = Convert.ToInt32(strPsw1);
                    }
                    catch
                    {
                        MessageUtil.ShowTips("密码1格式错误，应为4-6位数字");
                        sbtnPswOk.Enabled = true;
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(strPsw2))
            {
                if (strPsw2.Length < 4 || strPsw2.Length > 6)
                {
                    MessageUtil.ShowTips("密码2长度错误，长度应为4-6位");
                    sbtnPswOk.Enabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        iPsw2 = Convert.ToInt32(strPsw2);
                    }
                    catch
                    {
                        MessageUtil.ShowTips("密码2格式错误，应为4-6位数字");
                        sbtnPswOk.Enabled = true;
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(strPsw3))
            {
                if (strPsw3.Length < 4 || strPsw3.Length > 6)
                {
                    MessageUtil.ShowTips("密码3长度错误，长度应为4-6位");
                    sbtnPswOk.Enabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        iPsw3 = Convert.ToInt32(strPsw3);
                    }
                    catch
                    {
                        MessageUtil.ShowTips("密码3格式错误，应为4-6位数字");
                        sbtnPswOk.Enabled = true;
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(strPsw4))
            {
                if (strPsw4.Length < 4 || strPsw4.Length > 6)
                {
                    MessageUtil.ShowTips("密码4长度错误，长度应为4-6位");
                    sbtnPswOk.Enabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        iPsw4 = Convert.ToInt32(strPsw4);
                    }
                    catch
                    {
                        MessageUtil.ShowTips("密码4格式错误，应为4-6位数字");
                        sbtnPswOk.Enabled = true;
                        return;
                    }
                }
            }

            #endregion

            try 
            {
                if (!string.IsNullOrEmpty(strSn))
                {
                    //SN
                    int iSn = Convert.ToInt32(strSn);
                    bSendDoor[4] = (byte)iSn;//1F 9B 57 07
                    iSn = (iSn >> 8);
                    bSendDoor[5] = (byte)iSn;
                    iSn = (iSn >> 8);
                    bSendDoor[6] = (byte)iSn;
                    iSn = (iSn >> 8);
                    bSendDoor[7] = (byte)iSn;
                    //第一组 密码
                    bSendDoor[12] = (byte)iPsw1;
                    iPsw1 = (iPsw1 >> 8);
                    bSendDoor[13] = (byte)iPsw1;
                    iPsw1 = (iPsw1 >> 8);
                    bSendDoor[14] = (byte)iPsw1;
                    iPsw1 = (iPsw1 >> 8);
                    bSendDoor[15] = (byte)iPsw1;
                    //第二组 密码
                    bSendDoor[16] = (byte)iPsw2;
                    iPsw2 = (iPsw2 >> 8);
                    bSendDoor[17] = (byte)iPsw2;
                    iPsw2 = (iPsw2 >> 8);
                    bSendDoor[18] = (byte)iPsw2;
                    iPsw2 = (iPsw2 >> 8);
                    bSendDoor[19] = (byte)iPsw2;
                    //第3组 密码
                    bSendDoor[20] = (byte)iPsw3;
                    iPsw3 = (iPsw3 >> 8);
                    bSendDoor[21] = (byte)iPsw3;
                    iPsw3 = (iPsw3 >> 8);
                    bSendDoor[22] = (byte)iPsw3;
                    iPsw3 = (iPsw3 >> 8);
                    bSendDoor[23] = (byte)iPsw3;
                    //第4组 密码
                    bSendDoor[24] = (byte)iPsw4;
                    iPsw4 = (iPsw4 >> 8);
                    bSendDoor[25] = (byte)iPsw4;
                    iPsw4 = (iPsw4 >> 8);
                    bSendDoor[26] = (byte)iPsw4;
                    iPsw4 = (iPsw4 >> 8);
                    bSendDoor[27] = (byte)iPsw4; 

                    manager.SendTo(bSendDoor, strIp, 60000);
                    blRet = false;
                    Thread.Sleep(300);
                    if (blRet)
                    {
                        string strSql = "update tb_Tools set DoorPsw1='" + strPsw1 + "',DoorPsw2='" + strPsw2 + "'"+
                            ",DoorPsw3='" + strPsw3 + "',DoorPsw4='" + strPsw4 + "' where tvChildId='" + strAlterChildId + "' ";
                        datalogic.SqlComNonQuery(strSql);
                        //tb_RecordBoxPower BoxArea,BoxName,Psw1,Psw2,Psw3,Psw4,Time
                        if (string.IsNullOrEmpty(strPsw1))
                            strPsw1 = "未设置";
                        if (string.IsNullOrEmpty(strPsw2))
                            strPsw2 = "未设置";
                        if (string.IsNullOrEmpty(strPsw3))
                            strPsw3 = "未设置";
                        if (string.IsNullOrEmpty(strPsw4))
                            strPsw4 = "未设置";

                        string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        strSql = "insert into tb_RecordBoxPower (BoxArea,BoxName,Psw1,Psw2,Psw3,Psw4,Time)" +
                                        "values ('" + strArea + "','" + strName + "','" + strPsw1 + "','" + strPsw2 + "',"+
                                        "'" + strPsw3 + "','" + strPsw4 + "','" + strTime + "')";
                        datalogic.SqlComNonQuery(strSql);
                    }
                    else
                    {
                        MessageUtil.ShowTips("设置密码失败，请检查网络或IP、SN设置值");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowTips(ex.ToString());
                sbtnPswOk.Enabled = true;
            }
            sbtnNew_Click(sender, e);
            sbtnPswOk.Enabled = true;
        }

        private void sbtnPswExit_Click(object sender, EventArgs e)
        {
            sbtnNew_Click(sender, e);
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }








    }
}
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
using ToolsManage.BaseClass.DoorClass;

using System.Threading;
using System.Runtime.InteropServices;
using WG3000_COMM.Core;
using ToolsManage.Domain;
using NewLife.Web;
using ElecPower.WaitingBox;

namespace ToolsManage.DoorManage
{
    public partial class frmDoorUser : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        DataOperate operatedata = new DataOperate();
        DataTable dataTable = new DataTable();

        private TagType EditType = TagType.初值;
        private string strAlterChildId = "";
        private TbSysdevice CurMJInfo = null;

        //public wgMjController control = new wgMjController();
        //private int iSn;
        //private string strIp;
        //private DoorCount doorCount = DoorCount.未设置;

        //DeviceUsing UsingIc = DeviceUsing.未启用 ;
        //DeviceUsing UsingFinger = DeviceUsing.未启用;
        clsZkControl ZKControl = null;

        clsWgInfo wgControl;
        //Dictionary<string, clsZkDoor> dicZKDoor = new Dictionary<string, clsZkDoor>();
        //clsZkDoor zkdoor;

        public frmDoorUser(clsZkControl zkControl)
        {
            ZKControl = zkControl;
            InitializeComponent();
            this.gridView1.IndicatorWidth = 40;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }

        /// <summary>
        /// 获取门禁信息
        /// </summary>
        private void GetMJInfo()
        {
            CurMJInfo= TbSysdevice.Find("ID=(SELECT MAX(ID) FROM tb_SysDevice)");
            //IList<TbSysdevice> list = TbSysdevice.Search(null, new Pager() { PageIndex = 1, PageSize = 10,  Desc = true });
            //if (list != null && list.Count > 0)
            //{
            //    CurMJInfo = list[0];
            //}
        }

        private void frmDoorUser_Load(object sender, EventArgs e)
        {
            GetMJInfo();
            if (CurMJInfo != null)
            {
                if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString())
                {
                    chbMJ1.Enabled = true;
                    chbMJ1.Text = CurMJInfo.FingerDoorName;
                }
                if (CurMJInfo.UsingFinger2 == DeviceUsing.启用.ToString())
                {
                    chbMJ2.Enabled = true;
                    chbMJ2.Text = CurMJInfo.FingerDoorName2;
                }
            }
            Cursor = Cursors.WaitCursor;
            try
            {
                //UsingIc = frmMain.UsingDoor;
                //UsingFinger = frmMain.UsingFinger;
                if (CurMJInfo.DoorUsing== DeviceUsing.启用.ToString())
                {
                    if (wgControl == null)
                    {
                        wgControl = new clsWgInfo();
                    }
                    gboxIc.Enabled = true;
                    LoadWgInfo();
                }
                else
                {
                    gboxIc.Enabled = false;
                }

                AsyZKOper();
                xtraTabControl1.SelectedTabPage = xpageUserAll;
                AddTreeView("0", (TreeNode)null);
                treeView1.ExpandAll();

                TableInit();
                ShowDataTable();
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
            Cursor = Cursors.Default;
        }

        #region 连接中控指纹机
        private int MaxWaitTime = 10;
        private void AsyZKOper()
        {
            //string res;
            //frmWaitingBox fwb = null;
            //fwb = new frmWaitingBox((obj, args) =>
            //{
            //    Thread.Sleep(1000);
            //    fwb.IsComplete = false;
            //    ConnectZKMJ();
            //    fwb.IsComplete = true;
            //    fwb._WaitTime = MaxWaitTime * 1000;
            //}, MaxWaitTime, "正在连接门禁，请稍候。。。", false, false);
            //fwb.IsGenColor = false;
            //fwb.ShowDialog(this);
            //res = fwb.Message;
            ConnectZKMJ();
        }

        private void ConnectZKMJ()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(()=>ConnectZKMJ()));
            }
            else
            {
                if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString() ||
                        CurMJInfo.UsingFinger2 == DeviceUsing.启用.ToString())
                {
                    gboxFinger.Enabled = true;
                    foreach (clsZkDoor zkdoor in ZKControl.listZk)
                    {
                        if (zkdoor.BlConnoct == false)
                        {
                            if (!zkdoor.OnlyConnect())
                            {
                                MessageUtil.ShowError("连接指纹机失败，请检查设备");
                            }
                        }
                    }
                }
                else
                {
                    gboxFinger.Enabled = false;
                }
            }
        }
        #endregion

        #region  子程序

        /// <summary>
        /// 加载大门 指纹控制器信息
        /// </summary>
        private void LoadFingerInfo()
        {
            //string strSql = "select FingerDoorIp,FingerPort from tb_SysDevice ";
            //DataTable dt = datalogic.GetDataTable(strSql);//wgPort,WgDoorName3,WgDoorName4,UsingFinger,FingerDoorName
            //if (dt.Rows.Count > 0)////FingerDoorIp,FingerPort
            //{
            //    string strIp = "";
            //    int iPort = 0;
            //    strIp = dt.Rows[0]["FingerDoorIp"].ToString();
            //    if (string.IsNullOrEmpty(strIp))
            //    {
            //        if (frmMain.blDebug)
            //            MessageUtil.ShowTips("指纹门禁 IP为空");
            //    }
            //    string str = dt.Rows[0]["FingerPort"].ToString();
            //    if (string.IsNullOrEmpty(str))
            //    {
            //        if (frmMain.blDebug)
            //            MessageUtil.ShowTips("指纹门禁 端口号为空");
            //    }
            //    else
            //        iPort = Convert.ToInt32(str);
            //    zkdoor.StrIp = strIp;
            //    zkdoor.IPort = iPort;
            //}
            //else
            //{
            //    MessageUtil.ShowError("指纹门禁 控制器未配置，请检查系统设置");
            //}
        }

        /// <summary>
        /// 加载大门 门禁控制器信息
        /// </summary>
        private void LoadWgInfo()
        {
            string strSql = "select DoorSN,DoorIP,wgPort,DoorName1,DoorName2,WgDoorName3,WgDoorName4 from tb_SysDevice ";
            DataTable dt = datalogic.GetDataTable(strSql);//wgPort,WgDoorName3,WgDoorName4,UsingFinger,FingerDoorName
            if (dt.Rows.Count > 0)
            {
                int iSn = 0;
                string strIp = "";
                int iPort = 0;
                strIp = dt.Rows[0]["DoorIP"].ToString();
                if (string.IsNullOrEmpty(strIp))
                {
                    if (frmMain.blDebug)
                        MessageUtil.ShowTips("IC门禁 IP为空");
                }
                string str = dt.Rows[0]["wgPort"].ToString();
                if (string.IsNullOrEmpty(str))
                {
                    if (frmMain.blDebug)
                        MessageUtil.ShowTips("IC门禁 端口号为空");
                }
                else
                    iPort = Convert.ToInt32(str);
                str = dt.Rows[0]["DoorSN"].ToString();
                if (string.IsNullOrEmpty(str))
                {
                    if (frmMain.blDebug)
                        MessageUtil.ShowTips("IC门禁 SN为空");
                }
                else
                    iSn = Convert.ToInt32(str);
                wgControl.SetWgIpSnPort(strIp, iPort, iSn);

                if (wgControl.listDoor.Count > 0)
                    wgControl.listDoor.Clear();

                str = dt.Rows[0]["DoorName1"].ToString();//DoorName1,DoorName2,WgDoorName3,WgDoorName4
                if (!string.IsNullOrEmpty(str))
                {
                    clsDoorInfo door = new clsDoorInfo(str, 1);
                    wgControl.listDoor.Add(door);
                }
                str = dt.Rows[0]["DoorName2"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    clsDoorInfo door = new clsDoorInfo(str, 2);
                    wgControl.listDoor.Add(door);
                }
                str = dt.Rows[0]["WgDoorName3"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    clsDoorInfo door = new clsDoorInfo(str, 3);
                    wgControl.listDoor.Add(door);
                }
                str = dt.Rows[0]["WgDoorName4"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    clsDoorInfo door = new clsDoorInfo(str, 4);
                    wgControl.listDoor.Add(door);
                }
            }
            else
            {
                MessageUtil.ShowError("IC门禁控制器未配置，请检查系统设置");
            }
        }

        /// <summary>
        /// 获得 用户ID
        /// </summary>
        /// <returns></returns>
        private string  GetUserId()
        {
            string strRet = "";
            int iNo = 0;
            string strSql = "select top 1 UserId from tb_DoorUser order by UserId desc ";// select top 1 * from table order by id desc
            DataTable dt = datalogic.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                string str = dt.Rows[0]["UserId"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    iNo = Convert.ToInt32(str );
                }
            }
            iNo++;
            strRet = iNo.ToString();
            return strRet;
        }

        /// <summary>
        /// 获得数据库信息放入TreeView中 (递归)   
        /// </summary>
        /// <param name="strParent"></param>
        /// <param name="parentNode"></param>
        private void AddTreeView(string strParent, TreeNode parentNode)
        {
            //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo
            string strSql = "select tvParent,tvChildId,IsGroup,GroupName,UserName,IsPower from tb_DoorUser where tvParent='" + strParent + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);

            foreach (DataRow datarow in dt.Rows)
            {
                TreeNode node = new TreeNode();
                //处理根节点
                if (parentNode == null)
                {
                    node.Name = datarow["tvChildId"].ToString();// node.Name 为本节点的编号
                    node.Tag = strParent;                       // node.Tag 为本节点父节点的编号
                    string strIsGroup=datarow["IsGroup"].ToString();
                    if (strIsGroup == GroupPeoType.班组.ToString())
                    {
                        node.Text = datarow["GroupName"].ToString();
                        node.ImageIndex = 0;
                        node.SelectedImageIndex = 0;
                    }
                    else if (strIsGroup == GroupPeoType.人员.ToString())
                    {
                        node.Text = datarow["UserName"].ToString();
                        string str = datarow["IsPower"].ToString();
                        node.ImageIndex = 2;
                        node.SelectedImageIndex = 2;
                    }
                    treeView1.Nodes.Add(node);
                    AddTreeView(datarow["tvChildId"].ToString(), node);
                }
                //处理子节点
                else
                {
                    node.Name = datarow["tvChildId"].ToString();
                    node.Tag = strParent;
                    string strIsGroup = datarow["IsGroup"].ToString();
                    if (strIsGroup == GroupPeoType.班组.ToString())
                    {
                        node.Text = datarow["GroupName"].ToString();
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                    }
                    else if (strIsGroup == GroupPeoType.人员.ToString())
                    {
                        node.Text = datarow["UserName"].ToString();
                        string str = datarow["IsPower"].ToString();
                        node.ImageIndex = 2;
                        node.SelectedImageIndex = 2;
                    }
                    parentNode.Nodes.Add(node);
                    AddTreeView(datarow["tvChildId"].ToString(), node);
                }
            }

        }

        private void TableInit() //dataTable   GroupName UserName IcNo CardNo  
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "GroupName";
            column.Caption = "班组";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UserName";
            column.Caption = "姓名";
            dataTable.Columns.Add(column);

            if (CurMJInfo.DoorUsing ==DeviceUsing .启用.ToString() )
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "IcNo";
                column.Caption = "IC卡号";
                dataTable.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "CardNo";
                column.Caption = "IC卡编号";
                dataTable.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "IsPower";
                column.Caption = "IC权限";
                dataTable.Columns.Add(column);
            }
            if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString())
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "UserId";
                column.Caption = "指纹ID";
                dataTable.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "PassWord";
                column.Caption = "指纹密码";
                dataTable.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "PowerType";
                column.Caption = "指纹权限";
                dataTable.Columns.Add(column);
            }
        }

        private void ShowDataTable()   //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo
        {
            dataTable.Rows.Clear();

            //string strSql = "select GroupName,UserName,IcNo,CardNo,IsPower,UserId,PassWord,PowerType "+
            //                "from tb_DoorUser where IsGroup='" + GroupPeoType.人员.ToString() + "' ";
            //DataTable dt = datalogic.GetDataTable(strSql);
            string selects = "GroupName,UserName,IcNo,CardNo,IsPower,UserId,PassWord,PowerType";
            IList<TbDooruser> list = TbDooruser.FindAll("IsGroup='" + GroupPeoType.人员.ToString() + "'", "", selects, 0, 0);
            if (list != null && list.Count > 0)
            {
                foreach (TbDooruser dooruser in list)
                {
                    DataRow row = dataTable.NewRow();
                    row["GroupName"] = dooruser.GroupName;
                    row["UserName"] = dooruser.UserName;
                    if (CurMJInfo.DoorUsing == DeviceUsing.启用.ToString())
                    {
                        row["IcNo"] = dooruser.IcNo;
                        row["CardNo"] = dooruser.CardNo;
                        string str = dooruser.IsPower;
                        if (string.IsNullOrEmpty(str))
                            row["IsPower"] = DoorPowerType.未授权.ToString();
                        else
                            row["IsPower"] = str;
                    }
                    if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString() ||
                        CurMJInfo.UsingFinger2== DeviceUsing.启用.ToString())
                    {
                        row["UserId"] = dooruser.UserId;
                        row["PassWord"] = dooruser.PassWord;
                        row["PowerType"] = dooruser.PowerType;
                    }
                    dataTable.Rows.Add(row);
                }
            }
            //foreach (DataRow datarow in dt.Rows)
            //{
            //    DataRow row = dataTable.NewRow();
            //    row["GroupName"] = datarow["GroupName"].ToString();
            //    row["UserName"] = datarow["UserName"].ToString();
            //    if (UsingIc == DeviceUsing.启用)
            //    {
            //        row["IcNo"] = datarow["IcNo"].ToString();
            //        row["CardNo"] = datarow["CardNo"].ToString();
            //        string str = datarow["IsPower"].ToString();
            //        if (string.IsNullOrEmpty(str))
            //            row["IsPower"] = DoorPowerType.未授权.ToString();
            //        else
            //            row["IsPower"] = str;
            //    }
            //    if (UsingFinger == DeviceUsing.启用)
            //    {
            //        row["UserId"] = datarow["UserId"].ToString();
            //        row["PassWord"] = datarow["PassWord"].ToString();
            //        row["PowerType"] = datarow["PowerType"].ToString();
            //    }
            //    dataTable.Rows.Add(row);
            //}
            this.gridControl1.DataSource = dataTable;
            this.gridView1.BestFitColumns();
        }

        /// <summary>
        /// 刷新显示
        /// </summary>
        private void ShowNew()
        {
            //UsingIc = frmMain.UsingDoor;
            //UsingFinger = frmMain.UsingFinger;
            xtraTabControl1.SelectedTabPage = xpageUserAll;
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            treeView1.Enabled = true;
            EditType = TagType.初值;
            ShowDataTable();

            sbtnExpand.Enabled = true;
            sbtnAddGroup.Enabled = true;
            sbtnAddUser.Enabled = true;
            sbtnAlter.Enabled = true;
            sbtnDelete.Enabled = true;
        }

        /// <summary>
        /// 修改或添加时 别的按钮不可操作
        /// </summary>
        private void SetBtnFalse()
        {
            sbtnExpand.Enabled = false;
            sbtnAddGroup.Enabled = false;
            sbtnAddUser.Enabled = false;
            sbtnAlter.Enabled = false;
            sbtnDelete.Enabled = false;
        }

        /// <summary>
        /// 判断子节点 是否有授权的
        /// </summary>
        /// <param name="currNode"></param>
        /// <returns></returns>
        private bool JudgePowerChildNode(TreeNode currNode)
        {
            bool bl = false;
            TreeNodeCollection nodes = currNode.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeNode tn in nodes)
                {
                    if (tn.ImageIndex == 2)
                    {
                        bl = true;
                        return bl;
                    }
                    bl = JudgePowerChildNode(tn);
                    if (bl)
                        return bl;
                }
            }
            return bl;
        }

        /// <summary>
        /// 选中节点之后，选中节点的所有子节点
        /// </summary>
        /// <param name="currNode"></param>
        private void DeleteChildNode(TreeNode currNode)//   tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName UserCard UserNo
        {
            TreeNodeCollection nodes = currNode.Nodes;
            bool blResult = true;
            foreach (TreeNode tn in nodes)
            {
                string str = tn.Name.ToString();

                #region 删除权限

                TbDooruser dooruser = TbDooruser.Find("tvChildId='"+str+"'");
                if (dooruser!=null)
                {
                    #region  IC门禁

                    if (CurMJInfo.DoorUsing  == DeviceUsing .启用.ToString() )
                    {
                        if (dooruser.IsPower == DoorPowerType.有权限.ToString())
                        {
                            if (!string.IsNullOrEmpty(dooruser.IcNo))
                            {
                                bool blRet=wgControl.DeleteCardPower(dooruser.IcNo);
                                if (blRet)
                                {
                                    string strsql = "delete from tb_DoorUser where tvChildId='" + str + "'";
                                    datalogic.SqlComNonQuery(strsql);

                                    strsql = "delete from tb_BoxIcPower where UserId='" + str + "'";
                                    datalogic.SqlComNonQuery(strsql);
                                }
                            }
                        }
                        else
                        {
                            string strsql = "delete from tb_DoorUser where tvChildId='" + str + "'";
                            datalogic.SqlComNonQuery(strsql);
                        }
                    }

                    #endregion

                    #region  指纹门禁
                    //判断用户权限有几扇门
                    if (CurMJInfo.UsingFinger  == DeviceUsing.启用.ToString())
                    {  
                        if (!string.IsNullOrEmpty(dooruser.UserId))
                        {
                            if (!String.IsNullOrEmpty(dooruser.DoorNo1))
                            {
                                clsZkDoor zkdoor = ZKControl.listZk.Find(p => p.StrIp == dooruser.DoorNo1);
                                if (zkdoor != null)
                                {
                                    blResult = zkdoor.DeleteUserInfo(dooruser.UserId);
                                    if (blResult)
                                    {
                                        dooruser.DoorNo1 = null;
                                        if (String.IsNullOrEmpty(dooruser.DoorNo1) && String.IsNullOrEmpty(dooruser.DoorNo2))
                                        {
                                            dooruser.Delete();
                                        }
                                        else
                                        {
                                            dooruser.Update();
                                        }
                                    }
                                }
                            }                            
                        }
                    }
                    if (CurMJInfo.UsingFinger2 == DeviceUsing.启用.ToString())
                    {
                        if (!string.IsNullOrEmpty(dooruser.UserId))
                        {
                            if (!String.IsNullOrEmpty(dooruser.DoorNo2))
                            {
                                clsZkDoor zkdoor = ZKControl.listZk.Find(p => p.StrIp == dooruser.DoorNo2);
                                if (zkdoor != null)
                                {
                                    blResult = zkdoor.DeleteUserInfo(dooruser.UserId);
                                    if (blResult)
                                    {
                                        dooruser.DoorNo2 = null;
                                        if (String.IsNullOrEmpty(dooruser.DoorNo1) && String.IsNullOrEmpty(dooruser.DoorNo2))
                                        {
                                            dooruser.Delete();
                                        }
                                        else
                                        {
                                            dooruser.Update();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }

                #endregion

                DeleteChildNode(tn);
            }
            if (blResult)
            {
                datalogic.SqlComNonQuery("delete from tb_DoorUser where tvChildId='" + currNode.Name.ToString() + "'");
            }
        }

        /// <summary>
        /// 点击 树结构后 表显示子内容
        /// </summary>
        /// <param name="strChildId"></param>
        private void QueryChildNode(string strChildId)   //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo
        {
            string strSql = "select IsGroup,GroupName,UserName,tvChildId,IcNo,CardNo,IsPower,UserId,PassWord,PowerType from tb_DoorUser" +
                            " where tvParent='" + strChildId + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                if (datarow["IsGroup"].ToString() == GroupPeoType.人员.ToString())
                {
                    DataRow row = dataTable.NewRow();
                    row["GroupName"] = datarow["GroupName"].ToString();
                    row["UserName"] = datarow["UserName"].ToString();
                    if (CurMJInfo.DoorUsing == DeviceUsing.启用.ToString())
                    {
                        row["IcNo"] = datarow["IcNo"].ToString();
                        row["CardNo"] = datarow["CardNo"].ToString();
                        string str = datarow["IsPower"].ToString();
                        if (string.IsNullOrEmpty(str))
                            row["IsPower"] = DoorPowerType.未授权.ToString();
                        else
                            row["IsPower"] = str;
                    }
    
                    if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString() ||
                        CurMJInfo.UsingFinger2==DeviceUsing.启用.ToString())
                    {
                        row["UserId"] = datarow["UserId"].ToString();
                        row["PassWord"] = datarow["PassWord"].ToString();
                        row["PowerType"] = datarow["PowerType"].ToString();
                    }
                    dataTable.Rows.Add(row);
                }
                string strChild = datarow["tvChildId"].ToString();
                QueryChildNode(strChild);
            }
        }


        #endregion


        private void sbtnNew_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void sbtnExpand_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
            {
                if (treeView1.Nodes[0].IsExpanded == true)
                    treeView1.CollapseAll();
                else
                    treeView1.ExpandAll();
                xtraTabControl1.SelectedTabPage = xpageUserAll;
                EditType = TagType.初值;
                treeView1.Enabled = true;
                ShowDataTable();
            }
        }

        private void sbtnAddGroup_Click(object sender, EventArgs e)
        {
            bool blJudge = frmMain.PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;
                

            treeView1.Enabled = false;
            EditType = TagType.添加;
            gcGroup.Text = "添加班组";
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.ImageIndex == 2 || treeView1.SelectedNode.ImageIndex == 3)
                {
                    MessageUtil.ShowTips("不可在用户节点下添加班组");
                    treeView1.Enabled = true;
                    return;
                }
            }
            xtraTabControl1.SelectedTabPage = xpageGroup;
            SetBtnFalse();
        }

        private void sbtnAddUser_Click(object sender, EventArgs e)
        {
            bool blJudge = frmMain.PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            treeView1.Enabled = false;
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.ImageIndex == 2 || treeView1.SelectedNode.ImageIndex == 3)
                {
                    MessageUtil.ShowTips("不可在用户节点下添加用户");
                    treeView1.Enabled = true;
                    return;
                }
                else
                {
                    tbGroup.Text = treeView1.SelectedNode.Text.ToString();
                    tbFingerGroup.Text = treeView1.SelectedNode.Text.ToString();
                }
            }
            ShowIcFingerCheckBox();
            xtraTabControl1.SelectedTabPage = xpageUserIc;
            gcUserIc.Text = "添加门禁用户";
            if (CurMJInfo.DoorUsing == DeviceUsing.启用.ToString())
            {
                gboxIc.Enabled = true;
                labInfo.Visible = true;
                labInfo.Text = "点击此文本框，制卡器上刷卡    ";
                tbCard.Enabled = true;
            }
            else
            {
                gboxIc.Enabled = false;
            }
            if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString() ||
                CurMJInfo.UsingFinger2== DeviceUsing.启用.ToString())
            {
                gboxFinger.Enabled = true;
                tbFingerUserId.Text = GetUserId();
            }
            else
            {
                gboxFinger.Enabled = false ;
            }
            EditType = TagType.添加;
            SetBtnFalse();
        }

        /// <summary>
        /// 显示 IC 中控门禁 是否 权限下发
        /// </summary>
        private void ShowIcFingerCheckBox()
        {
            //UsingIc = frmMain.UsingDoor;
            //UsingFinger = frmMain.UsingFinger;
            if (CurMJInfo.DoorUsing == DeviceUsing.启用.ToString() && (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString()||
                CurMJInfo.UsingFinger2==DeviceUsing.启用.ToString()))
            {
                cboxFinger.Visible = true;
                cboxIc.Visible = true;
                cboxIc.Checked = true;
                cboxFinger.Checked = true;
            }
            else
            {
                cboxFinger.Visible = false;
                cboxIc.Visible = false;
            }
        }

        private void sbtnAlter_Click(object sender, EventArgs e)   //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo
        {
            bool blJudge = frmMain.PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            treeView1.Enabled = false;
            if (treeView1.SelectedNode == null)
            {
                MessageUtil.ShowTips("请选择需要修改的内容");
                treeView1.Enabled = true;
                return;
            }
            else
            {
                EditType = TagType.修改;
                strAlterChildId = treeView1.SelectedNode.Name.ToString();
                if (treeView1.SelectedNode.ImageIndex == 2 || treeView1.SelectedNode.ImageIndex == 3)
                {
                    ShowIcFingerCheckBox();

                    gcUserIc.Text = "修改门禁用户信息";
                    xtraTabControl1.SelectedTabPage = xpageUserIc;
                    string strSql = "select GroupName,UserName,DoorNo1,DoorNo2,IcNo,CardNo,UserId,PassWord,PowerType " +
                          "from tb_DoorUser where tvChildId='" + strAlterChildId + "' ";
                    DataTable dt = datalogic.GetDataTable(strSql);

                    if (dt.Rows.Count > 0)
                    {
                        tbGroup.Text = dt.Rows[0]["GroupName"].ToString();//tbGroup
                        tbName.Text = dt.Rows[0]["UserName"].ToString();

                        #region IC

                        if (CurMJInfo.DoorUsing == DeviceUsing.启用.ToString())
                        {
                            gboxIc.Enabled = true;
                            string strIc = dt.Rows[0]["IcNo"].ToString();
                            if (string.IsNullOrEmpty(strIc))
                            {
                                tbCard.Enabled = true;
                                labInfo.Visible = true;
                            }
                            else
                            {
                                tbCard.Enabled = false;
                                labInfo.Visible = false;
                            }
                            tbCard.Text = strIc;
                            tbNo.Text = dt.Rows[0]["CardNo"].ToString();
                        }
                        else
                        {
                            gboxIc.Enabled = false;
                        }

                        #endregion

                        #region  指纹

                        if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString()||
                            CurMJInfo.UsingFinger2== DeviceUsing.启用.ToString())
                        {
                            gboxFinger.Enabled = true;
                            string strId = dt.Rows[0]["UserId"].ToString();
                            if (string.IsNullOrEmpty(strId))
                            {
                                tbFingerUserId.Text = GetUserId();
                            }
                            else
                                tbFingerUserId.Text = strId;
                            tbFingerPsw.Text = dt.Rows[0]["PassWord"].ToString();
                            cbbFingerPower.Text = dt.Rows[0]["PowerType"].ToString();
                        }
                        else
                        {
                            gboxFinger.Enabled = false;
                        }

                        string doorno1 = dt.Rows[0]["DoorNo1"].ToString();
                        string doorno2 = dt.Rows[0]["DoorNo2"].ToString();
                        if (!String.IsNullOrEmpty(doorno1))
                            chbMJ1.Checked = true;
                        else
                            chbMJ1.Checked = false;
                        if (!String.IsNullOrEmpty(doorno2))
                            chbMJ2.Checked = true;
                        else
                            chbMJ2.Checked = false;

                        #endregion
                       
                    }

                }
                else if (treeView1.SelectedNode.ImageIndex == 1 || treeView1.SelectedNode.ImageIndex == 0)
                {
                    gcGroup.Text = "修改班组名称";
                    xtraTabControl1.SelectedTabPage = xpageGroup;
                    tbGroupName.Text = treeView1.SelectedNode.Text.ToString();
                }
            }
            SetBtnFalse();
        }

        private void sbtnDelete_Click(object sender, EventArgs e)//      //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo
        {
            bool blJudge = frmMain.PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            sbtnDelete.Enabled = false;
            if (treeView1.SelectedNode == null)
            {
                MessageUtil.ShowTips("请选择需要删除的内容");
            }
            else
            {
                if (treeView1.SelectedNode.ImageIndex == 0 || treeView1.SelectedNode.ImageIndex == 1)
                {
                    if (MessageUtil.ShowYesNoAndTips("确定删除该班组，及该班组的所有成员？") == DialogResult.Yes)
                    {
                        frmExitUser.userPower = UserPower.系统用户;
                        frmExitUser frm = new frmExitUser();
                        frm.ShowDialog(this);
                        frm.Dispose();
                        if (frmExitUser.blOk)
                        {
                            DeleteChildNode(treeView1.SelectedNode);
                        }
                    }
                }
                else
                {
                    if (MessageUtil.ShowYesNoAndTips("确定删除该用户？") == DialogResult.Yes)
                    {
                        bool blDelete = true;
                        string str = treeView1.SelectedNode.Name.ToString();
                        if (treeView1.SelectedNode.ImageIndex == 2)
                        {
                            #region 删权限
                            TbDooruser dooruser = TbDooruser.Find("tvChildId='" + str + "'");

                            if (dooruser!=null)
                            {
                                #region  IC门禁

                                if (CurMJInfo.DoorUsing  ==DeviceUsing .启用.ToString())
                                {
                                    string strIc = dooruser.IcNo;
                                    if (!string.IsNullOrEmpty(strIc))
                                    {
                                        blDelete = wgControl.DeleteCardPower(strIc);
                                    }
                                }

                                #endregion

                                #region  指纹门禁

                                if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString())
                                {
                                    if (!string.IsNullOrEmpty(dooruser.UserId))
                                    {
                                        if (!String.IsNullOrEmpty(dooruser.DoorNo1))
                                        {
                                            clsZkDoor zkdoor = ZKControl.listZk.Find(p => p.StrIp == dooruser.DoorNo1);
                                            if (zkdoor != null)
                                            {
                                                bool blResult = zkdoor.DeleteUserInfo(dooruser.UserId);
                                                if (blResult)
                                                {
                                                    dooruser.DoorNo1 = null;
                                                    if (String.IsNullOrEmpty(dooruser.DoorNo1) && String.IsNullOrEmpty(dooruser.DoorNo2))
                                                    {
                                                        dooruser.Delete();
                                                    }
                                                    else
                                                    {
                                                        dooruser.Update();
                                                        blDelete = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (CurMJInfo.UsingFinger2 == DeviceUsing.启用.ToString())
                                {
                                    if (!string.IsNullOrEmpty(dooruser.UserId))
                                    {
                                        if (!String.IsNullOrEmpty(dooruser.DoorNo2))
                                        {
                                            clsZkDoor zkdoor = ZKControl.listZk.Find(p => p.StrIp == dooruser.DoorNo2);
                                            if (zkdoor != null)
                                            {
                                                bool blResult = zkdoor.DeleteUserInfo(dooruser.UserId);
                                                if (blResult)
                                                {
                                                    dooruser.DoorNo2 = null;
                                                    if (String.IsNullOrEmpty(dooruser.DoorNo1) && String.IsNullOrEmpty(dooruser.DoorNo2))
                                                    {
                                                        dooruser.Delete();
                                                    }
                                                    else
                                                    {
                                                        dooruser.Update();
                                                        blDelete = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                #endregion
                            }
                            else 
                            {
                                blDelete = false;
                            }

                            #endregion
                        }
                        if (blDelete)
                        {
                            string strsql = "delete from tb_DoorUser where tvChildId='" + str + "'";
                            datalogic.SqlComNonQuery(strsql);
                            treeView1.SelectedNode.Remove();

                            strsql = "delete from tb_BoxIcPower where UserId='" + str + "'";
                            datalogic.SqlComNonQuery(strsql);
                        }
                    }
                }
            }
            ShowNew();
            sbtnDelete.Enabled = true;
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            //if (UsingFinger == DeviceUsing.启用)
            //{
            //    if (zkdoor != null)
            //    {
            //        if (zkdoor.BlConnoct)
            //        {
            //            zkdoor.OnlyDisConnect ();
            //        }
            //        zkdoor = null;
            //    }
            //}


            foreach (clsZkDoor zkdoor in ZKControl.listZk)
            {
                if(zkdoor!=null)
                {
                    if(zkdoor.BlConnoct)
                    {
                        zkdoor.OnlyDisConnect();
                    }
                }
            }
            this.Close();
        }

        private void sbtnGroupOk_Click(object sender, EventArgs e)   //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo
        {
            #region 输入 判断

            string strName = tbGroupName.Text.Trim();
            if (string.IsNullOrEmpty(strName))
            {
                MessageUtil.ShowTips("班组名称不可为空");
                return;
            }
            if (strName.Length > 50)
            {
                strName = strName.Substring(0, 50);
            }

            string strSame = "";
            if (EditType == TagType.添加)
            {
                strSame = "Select ID From tb_DoorUser where GroupName='" + strName + "' and  IsGroup='" + GroupPeoType.班组.ToString() + "'";
            }
            else if (EditType == TagType.修改)
            {
                strSame = "Select ID From tb_DoorUser where GroupName='" + strName + "' and  IsGroup='" + GroupPeoType.班组.ToString() + "' and tvChildId<>'" + strAlterChildId + "' ";
            }
            if (!string.IsNullOrEmpty(strSame))
            {
                if (operatedata.blCheckHas(strSame))
                {
                    MessageUtil.ShowTips("班组名称重复，请重新输入");
                    return;
                }
            }

            #endregion

            sbtnGroupOk.Enabled = false;

            string strParent = "";
            string strChild = "";
            if (EditType == TagType.添加)
            {
                TreeNode node = new TreeNode();
                //获取ChildId
                strChild = operatedata.GetChildId("tb_DoorUser", "tvChildId"); 
                node.Name = strChild;// node.Name 为本节点的编号 
                strParent = "0";
                node.Tag = strParent;
                node.Text = strName;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                treeView1.Nodes.Add(node);

                //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo
                string strSql = "insert into tb_DoorUser (tvParent,tvChildId,IsGroup,GroupName) values ('" + strParent + "'," +
                   "'" + strChild + "','" + GroupPeoType.班组.ToString() + "','" + strName + "')";
                datalogic.SqlComNonQuery(strSql);
                treeView1.ExpandAll();
            }
            else if (EditType == TagType.修改)
            {
                treeView1.SelectedNode.Text = strName;
                string strSql = "update tb_DoorUser set GroupName='" + strName + "' where tvChildId='" + strAlterChildId + "' ";
                datalogic.SqlComNonQuery(strSql);
                if (treeView1.SelectedNode.FirstNode != null)
                {
                    if (treeView1.SelectedNode.FirstNode.ImageIndex == 2 || treeView1.SelectedNode.FirstNode.ImageIndex==3)
                    {
                        strSql = "update tb_DoorUser set GroupName='" + strName + "' where tvParent='" + strAlterChildId + "' ";
                        datalogic.SqlComNonQuery(strSql);
                    }
                }
                ShowNew();
            }
            sbtnGroupOk.Enabled = true;
        }

        private void sbtnGroupExit_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void sbtnUserOk_Click(object sender, EventArgs e)
        {
            #region 输入判断

            string strName = tbName.Text.Trim();
            if (string.IsNullOrEmpty(strName))
            {
                MessageUtil.ShowTips("姓名不能为空");
                return;
            }
            string strSame = "";
            if (EditType == TagType.添加)
            {
                strSame = "Select ID From tb_DoorUser where UserName='" + strName + "'  and  IsGroup='" + GroupPeoType.人员.ToString() + "' ";
            }
            else if (EditType == TagType.修改)
            {
                strSame = "Select ID From tb_DoorUser where UserName='" + strName + "'  and  IsGroup='" + GroupPeoType.人员.ToString() + "'" +
                    " and tvChildId<>'" + strAlterChildId + "'";
            }
            if (!string.IsNullOrEmpty(strSame))
            {
                if (operatedata.blCheckHas(strSame))
                {
                    MessageUtil.ShowTips("用户名称 重复，请重新输入");
                    return;
                }
            }


            #region  IC门禁

            string strCard = tbCard.Text.Trim();
            string strNo = tbNo.Text.Trim();
            if (CurMJInfo.DoorUsing == DeviceUsing.启用.ToString())
            {
                if (string.IsNullOrEmpty(strCard))
                {
                    MessageUtil.ShowTips("门禁卡卡号不能为空");
                    return;
                }
                if (strCard.Length != 8)
                {
                    MessageUtil.ShowTips("门禁卡卡号格式错误，请重新刷卡");
                    tbCard.Text = "";
                    return;
                }
                if (strName.Length > 50)
                    strName = strName.Substring(0, 50);
                if (strCard.Length > 8)
                    strCard = strCard.Substring(0, 8);

                if (EditType == TagType.添加)
                {
                    strSame = "Select ID From tb_DoorUser where  IcNo='" + strCard + "' and  IsGroup='" + GroupPeoType.人员.ToString() + "' ";
                }
                else if (EditType == TagType.修改)
                {
                    strSame = "Select ID From tb_DoorUser where  IcNo='" + strCard + "' and  IsGroup='" + GroupPeoType.人员.ToString() + "'" +
                        " and tvChildId<>'" + strAlterChildId + "'";
                }
                if (!string.IsNullOrEmpty(strSame))
                {
                    if (operatedata.blCheckHas(strSame))
                    {
                        MessageUtil.ShowTips("IC号重复，请重新输入");
                        return;
                    }
                }
            }

            #endregion

            #region 指纹门禁

            string strPsw = tbFingerPsw.Text.Trim();
            string strPowerType = cbbFingerPower.Text.Trim();
            int iPower = 0;
            string strUserId = tbFingerUserId.Text.Trim();
            string strSql = "";

            if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString() ||
                CurMJInfo.UsingFinger2 == DeviceUsing.启用.ToString())
            {
                #region  用户ID

                if (string.IsNullOrEmpty(strUserId))
                {
                    MessageUtil.ShowTips("用户ID 不能为空");
                    return;
                }
                if (EditType == TagType.添加)
                {
                    strSql = "Select ID From tb_DoorUser where UserId='" + strUserId + "' and  IsGroup='" + GroupPeoType.人员.ToString() + "' ";
                }
                else if (EditType == TagType.修改)
                {
                    strSql = "Select ID From tb_DoorUser where UserId='" + strUserId + "' and  IsGroup='" + GroupPeoType.人员.ToString() + "'" +
                        " and tvChildId<>'" + strAlterChildId + "'";
                }
                if (!string.IsNullOrEmpty(strSql))
                {
                    if (operatedata.blCheckHas(strSql))
                    {
                        MessageUtil.ShowTips("用户ID 重复，请重新输入");
                        return;
                    }
                }
                #endregion

                #region 密码

                if (!string.IsNullOrEmpty(strPsw))
                {
                    if (strPsw.Length > 4)
                    {
                        MessageUtil.ShowTips("请输入4位数字密码");
                        return;
                    }
                    try
                    {
                        int iPsw = Convert.ToInt32(strPsw);
                    }
                    catch
                    {
                        MessageUtil.ShowTips("请输入4位数字密码");
                        return;
                    }
                }
                if (strPowerType == UserPower.系统用户.ToString())
                {
                    iPower = 3;
                }

                #endregion
            }



            #endregion

            #endregion

            sbtnUserOk.Enabled = false;

            #region  添加权限
            //IC卡门禁授权
            string strPower = DoorPowerType.未授权.ToString();
            if (CurMJInfo.DoorUsing == DeviceUsing.启用.ToString())
            {
                if (EditType == TagType.添加)
                {
                    if (wgControl.AddCardPower(strCard))
                    {
                        strPower = DoorPowerType.有权限.ToString();
                    }
                    else
                    {
                        MessageUtil.ShowTips("下发IC门禁权限失败，请检查门禁控制");
                    }
                }
            }
            //指纹门禁授权
            if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString())
            {
                if (chbMJ1.Checked)
                {
                    clsZkDoor zkdoor = ZKControl.listZk.Find(p => p.StrIp == CurMJInfo.FingerDoorIp);
                    if (zkdoor != null)
                    {
                        bool blFinger = zkdoor.SetUserInfo(strUserId, strName, strPsw, iPower);
                        if (blFinger == false)
                        {
                            MessageUtil.ShowTips("设置用户信息失败，请检查1#指纹门禁控制器");
                            sbtnUserOk.Enabled = true;
                            return;
                        }
                    }
                }
            }
            if (CurMJInfo.UsingFinger2 == DeviceUsing.启用.ToString())
            {
                if (chbMJ2.Checked)
                {
                    clsZkDoor zkdoor = ZKControl.listZk.Find(p => p.StrIp == CurMJInfo.FingerDoorIp2);
                    if (zkdoor != null)
                    {
                        bool blFinger = zkdoor.SetUserInfo(strUserId, strName, strPsw, iPower);
                        if (blFinger == false)
                        {
                            MessageUtil.ShowTips("设置用户信息失败，请检查2#指纹门禁控制器");
                            sbtnUserOk.Enabled = true;
                            return;
                        }
                    }
                }
            }
            #endregion

            string strParent = "";
            string strChild = "";
            string strGroup = "";
            string[] strFingerDoorIp = new string[4] { null, null, null, null };
            if (EditType == TagType.添加)
            {
                TreeNode node = new TreeNode();
                if (treeView1.SelectedNode == null)
                {
                    //获取ChildId
                    strChild = operatedata.GetChildId("tb_DoorUser", "tvChildId");
                    node.Name = strChild;// node.Name 为本节点的编号 
                    strParent = "0";
                    node.Tag = strParent;
                    node.Text = strName;
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 2;
                    treeView1.Nodes.Add(node);
                }
                else
                {
                    if (treeView1.SelectedNode.ImageIndex == 2 || treeView1.SelectedNode.ImageIndex == 3)
                    {
                        MessageUtil.ShowTips("用户节点下不可添加用户");
                        sbtnUserOk.Enabled = true;
                        return;
                    }
                    else
                    {
                        strChild = operatedata.GetChildId("tb_DoorUser", "tvChildId");
                        node.Name = strChild;// node.Name 为本节点的编号 
                        strParent = treeView1.SelectedNode.Name.ToString();
                        strGroup = treeView1.SelectedNode.Text.ToString();
                        node.Tag = strParent;
                        node.Text = strName;
                        node.ImageIndex = 2;
                        node.SelectedImageIndex = 2;
                        treeView1.SelectedNode.Nodes.Add(node);   //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo
                    }
                }

                if (CurMJInfo.DoorUsing == DeviceUsing.未启用.ToString())
                {
                    strCard = "";
                    strNo = "";
                    strPower = "";
                }
                if (CurMJInfo.UsingFinger == DeviceUsing.未启用.ToString() &&
                    CurMJInfo.UsingFinger2 == DeviceUsing.未启用.ToString())
                {
                    strUserId = "";
                    strPsw = "";
                    strPowerType = "";
                }
                if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString())
                {//指纹1#门禁启用
                    if (chbMJ1.Checked)
                    {
                        strFingerDoorIp[0] = CurMJInfo.FingerDoorIp;
                    }
                }
                if (CurMJInfo.UsingFinger2 == DeviceUsing.启用.ToString())
                {//指纹2#门禁启用
                    if (chbMJ2.Checked)
                    {
                        strFingerDoorIp[1] = CurMJInfo.FingerDoorIp2;
                    }
                }

                strSql = "insert into tb_DoorUser (tvParent,tvChildId,IsGroup,GroupName,UserName,DoorNo1,DoorNo2,IcNo,CardNo,IsPower,UserId,PassWord,PowerType) values " +
                    "('" + strParent + "','" + strChild + "','" + GroupPeoType.人员.ToString() + "','" + strGroup + "','" + strName + "'" +
                    ",'" + strFingerDoorIp[0] + "','" + strFingerDoorIp[1] + "'" +
                    ",'" + strCard + "','" + strNo + "','" + strPower + "','" + strUserId + "','" + strPsw + "','" + strPowerType + "')";
                datalogic.SqlComNonQuery(strSql);
                if (CurMJInfo.UsingFinger == DeviceUsing.未启用.ToString() ||
                    CurMJInfo.UsingFinger2 == DeviceUsing.未启用.ToString())
                {
                    tbFingerUserId.Text = GetUserId();
                }
                treeView1.ExpandAll();
            }
            else if (EditType == TagType.修改)
            {
                treeView1.SelectedNode.Text = strName;
                if (CurMJInfo.DoorUsing == DeviceUsing.未启用.ToString())
                {
                    strCard = "";
                    strNo = "";
                    strPower = "";
                }
                if (CurMJInfo.UsingFinger == DeviceUsing.未启用.ToString() ||
                    CurMJInfo.UsingFinger2 == DeviceUsing.未启用.ToString())
                {
                    strUserId = "";
                    strPsw = "";
                    strPowerType = "";
                }
                //UserId,PassWord,PowerType
                strSql = "update tb_DoorUser set UserName='" + strName + "',DoorNo1='" + strFingerDoorIp[0] + "',DoorNo2='" + strFingerDoorIp[1]+  "',IcNo='" + strCard + "',CardNo='" + strNo + "',UserId='" + strUserId + "'" +
                        ",PassWord='" + strPsw + "',PowerType='" + strPowerType + "' where tvChildId='" + strAlterChildId + "' ";
                datalogic.SqlComNonQuery(strSql);
                if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString() ||
                   CurMJInfo.UsingFinger2 == DeviceUsing.启用.ToString())
                {
                    strSql = "update tb_BoxIcPower set UserName='" + strName + "' where UserId='" + strAlterChildId + "' ";
                    datalogic.SqlComNonQuery(strSql);
                }
                ShowNew();
            }
            sbtnUserOk.Enabled = true;
        }

        private void sbtnUserExit_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (EditType == TagType.初值)
                {
                    if (treeView1.SelectedNode.ImageIndex == 0 || treeView1.SelectedNode.ImageIndex == 1)
                    {
                        string str = treeView1.SelectedNode.Name.ToString();
                        dataTable.Rows.Clear();
                        QueryChildNode(str);
                        this.gridControl1.DataSource = dataTable;
                    }
                    xtraTabControl1.SelectedTabPage = xpageUserAll;
                }
            }
        }


        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
        }

        private void frmDoorUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CurMJInfo.UsingFinger == DeviceUsing.启用.ToString() || 
                CurMJInfo.UsingFinger2==DeviceUsing.未启用.ToString())
            {
                foreach(clsZkDoor zkdoor in ZKControl.listZk)
                {
                    if (zkdoor.BlConnoct)
                    {
                        zkdoor.OnlyDisConnect();
                    }
                }
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0) return;
            DataRow dr = this.gridView1.GetDataRow(hand);
            if (dr == null) return;
            if (CurMJInfo.DoorUsing == DeviceUsing.启用.ToString())
            {
                string strIsPower = dr["IsPower"].ToString();
                string strIc = dr["IcNo"].ToString();
                if (strIsPower == DoorPowerType.有权限.ToString())
                {
                    e.Appearance.ForeColor = Color.Blue;// 改变行字体颜色
                    e.Appearance.BackColor = Color.Blue;// 改变行背景颜色
                    e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
                }
                else
                {
                    if (!string.IsNullOrEmpty(strIc))
                    {
                        e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
                        e.Appearance.BackColor = Color.Red;// 改变行背景颜色
                        e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
                    }
                }
            }
        }

        //private void sbtnFingerOk_Click(object sender, EventArgs e)
        //{
        //    #region 输入判断

        //    string strName = tbFingerName.Text.Trim();
        //    string strPsw = tbFingerPsw.Text.Trim();
        //    string strPower = cbbFingerPower.Text.Trim();
        //    int iPower = 0;
        //    string strUserId = tbFingerUserId.Text.Trim();
        //    string strSql = "";


        //    #region  姓名

        //    if (string.IsNullOrEmpty(strName))
        //    {
        //        MessageUtil.ShowTips("姓名不能为空");
        //        return;
        //    }
        //    if (strName.Length > 50)
        //        strName = strName.Substring(0, 50);
        //    if (EditType == TagType.添加)
        //    {
        //        strSql = "Select ID From tb_DoorUser where UserName='" + strName + "' and  IsGroup='" + GroupPeoType.人员.ToString() + "' ";
        //    }
        //    else if (EditType == TagType.修改)
        //    {
        //        strSql = "Select ID From tb_DoorUser where UserName='" + strName + "' and  IsGroup='" + GroupPeoType.人员.ToString() + "'" +
        //            " and tvChildId<>'" + strAlterChildId + "'";
        //    }
        //    if (!string.IsNullOrEmpty(strSql))
        //    {
        //        if (operatedata.blCheckHas(strSql))
        //        {
        //            MessageUtil.ShowTips("用户名称重复，请重新输入");
        //            return;
        //        }
        //    }

        //    #endregion

        //    #region  用户ID

        //    if (string.IsNullOrEmpty(strUserId))
        //    {
        //        MessageUtil.ShowTips("用户ID 不能为空");
        //        return;
        //    }
        //    if (EditType == TagType.添加)
        //    {
        //        strSql = "Select ID From tb_DoorUser where UserId='" + strUserId + "' and  IsGroup='" + GroupPeoType.人员.ToString() + "' ";
        //    }
        //    else if (EditType == TagType.修改)
        //    {
        //        strSql = "Select ID From tb_DoorUser where UserId='" + strUserId + "' and  IsGroup='" + GroupPeoType.人员.ToString() + "'" +
        //            " and tvChildId<>'" + strAlterChildId + "'";
        //    }
        //    if (!string.IsNullOrEmpty(strSql))
        //    {
        //        if (operatedata.blCheckHas(strSql))
        //        {
        //            MessageUtil.ShowTips("用户ID 重复，请重新输入");
        //            return;
        //        }
        //    }

        //    #endregion

        //    if (!string.IsNullOrEmpty(strPsw))
        //    {
        //        if (strPsw.Length > 4)
        //        {
        //            MessageUtil.ShowTips("请输入4位数字密码");
        //            return;
        //        }
        //        try
        //        {
        //            int iPsw = Convert.ToInt32(strPsw);
        //        }
        //        catch
        //        {
        //            MessageUtil.ShowTips("请输入4位数字密码");
        //            return;
        //        }
        //    }
        //    if (strPower == UserPower.系统用户.ToString())
        //    {
        //        iPower = 3;
        //    }

        //    #endregion

        //    sbtnFingerOk.Enabled = false;

        //    bool blFinger = zkdoor.SetUserInfo(strUserId, strName, strPsw, iPower);
        //    if (blFinger == false)
        //    {
        //        MessageUtil.ShowTips("设置 用户信息 失败，请检查门禁控制器");
        //        sbtnFingerOk.Enabled = true ;
        //        return;
        //    }

        //    string strParent = "";
        //    string strChild = "";
        //    string strGroup = "";
        //    if (EditType == TagType.添加)
        //    {
        //        TreeNode node = new TreeNode();
        //        if (treeView1.SelectedNode == null)
        //        {
        //            //获取ChildId
        //            strChild = operatedata.GetChildId("tb_DoorUser", "tvChildId");
        //            node.Name = strChild;// node.Name 为本节点的编号 
        //            strParent = "0";
        //            node.Tag = strParent;
        //            node.Text = strName;
        //            node.ImageIndex = 3;
        //            node.SelectedImageIndex = 3;
        //            treeView1.Nodes.Add(node);
        //        }
        //        else
        //        {
        //            strChild = operatedata.GetChildId("tb_DoorUser", "tvChildId");
        //            node.Name = strChild;// node.Name 为本节点的编号 
        //            strParent = treeView1.SelectedNode.Name.ToString();
        //            strGroup = treeView1.SelectedNode.Text.ToString();
        //            node.Tag = strParent;
        //            node.Text = strName;
        //            node.ImageIndex = 3;
        //            node.SelectedImageIndex = 3;
        //            treeView1.SelectedNode.Nodes.Add(node);   //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo UserId PassWord PowerType
        //        }
        //        strSql = "insert into tb_DoorUser (tvParent,tvChildId,IsGroup,GroupName,UserName,IcNo,CardNo,UserId,PassWord,PowerType) values ('" + strParent + "'," +
        //              "'" + strChild + "','" + GroupPeoType.人员.ToString() + "','" + strGroup + "','" + strName + "','" + "" + "','" + "" + "'" +
        //              ",'" + strUserId + "','" + strPsw + "','" + strPower + "')";
        //        datalogic.SqlComNonQuery(strSql);
        //        treeView1.ExpandAll();
        //    }
        //    else if (EditType == TagType.修改)
        //    {
        //        strSql = "update tb_DoorUser set UserName='" + strName + "',IcNo='" + "" + "',CardNo='" + "" + "',Finger1='" + strUserId  + "'" +
        //            ",PassWord='" + strPsw  + "',PowerType='" + strPower + "' where tvChildId='" + strAlterChildId + "' ";
        //        datalogic.SqlComNonQuery(strSql);
        //        ShowNew();
        //    }
        //    sbtnFingerOk.Enabled = true;
        //}


        private void sbtnFingerExit_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void cboxIc_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxIc.Visible)
            {
                if (cboxIc.Checked)
                {
                    CurMJInfo.DoorUsing = DeviceUsing.启用.ToString();
                    //UsingIc = DeviceUsing.启用;
                    gboxIc.Enabled = true;
                }
                else
                {
                    CurMJInfo.DoorUsing = DeviceUsing.未启用.ToString();
                    //UsingIc = DeviceUsing.未启用;
                    gboxIc.Enabled = false ;
                }
                    
            }
        }

        private void cboxFinger_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxFinger.Visible) 
            {
                if (cboxFinger.Checked)
                {
                    CurMJInfo.UsingFinger = DeviceUsing.启用.ToString();
                    //UsingFinger = DeviceUsing.启用;
                    gboxFinger.Enabled = false;
                } 
                else
                {
                    CurMJInfo.UsingFinger = DeviceUsing.未启用.ToString();
                    //UsingFinger = DeviceUsing.未启用;
                    gboxFinger.Enabled = false;
                }
                 
            }
        }






    }
}
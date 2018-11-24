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

using WG3000_COMM.Core;

namespace ToolsManage.DoorManage
{
    public partial class frmPowerManage : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        DataTable dtQueryPower = new DataTable();//查询权限记录表
        DataTable dtAddPower = new DataTable();//添加权限记录表
        DataTable dtDeletePower = new DataTable();//删除权限记录表

        //clsWgInfo wgControl = new clsWgInfo();
        clsWgInfo wgControl;
        DeviceUsing usingWg = DeviceUsing.未启用; 

        public frmPowerManage()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
            this.gridView2.IndicatorWidth = 40;
            this.gridView3.IndicatorWidth = 40;
            xtraTabControl1 .ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }

        private void frmPowerManage_Load(object sender, EventArgs e)
        {
            usingWg = frmMain.UsingDoor;
            if (wgControl == null)
            {
                wgControl = new clsWgInfo();
            }
            LoadWgInfo();


            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();

            TableInit(dtQueryPower);
            TableInit(dtAddPower);
            TableInit(dtDeletePower);

            ShowDtQuery();
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
                    if (datarow["IsGroup"].ToString() == GroupPeoType.班组.ToString())
                    {
                        node.Text = datarow["GroupName"].ToString();
                        node.ImageIndex = 0;
                        node.SelectedImageIndex = 0;
                    }
                    else if (datarow["IsGroup"].ToString() == GroupPeoType.人员.ToString())
                    {
                        node.Text = datarow["UserName"].ToString();
                        string str = datarow["IsPower"].ToString();
                        if (string.IsNullOrEmpty(str) || str == DoorPowerType.未授权  .ToString())
                        {
                            node.ImageIndex = 3;
                            node.SelectedImageIndex = 3;
                        }
                        else
                        {
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 2;
                        }
                    }
                    treeView1.Nodes.Add(node);
                    AddTreeView(datarow["tvChildId"].ToString(), node);
                }
                //处理子节点
                else
                {
                    node.Name = datarow["tvChildId"].ToString();
                    node.Tag = strParent;
                    if (datarow["IsGroup"].ToString() == GroupPeoType.班组.ToString())
                    {
                        node.Text = datarow["GroupName"].ToString();
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                    }
                    else if (datarow["IsGroup"].ToString() == GroupPeoType.人员.ToString())
                    {
                        node.Text = datarow["UserName"].ToString();
                        string str = datarow["IsPower"].ToString();
                        if (string.IsNullOrEmpty(str) || str == DoorPowerType.未授权.ToString())
                        {
                            node.ImageIndex = 3;
                            node.SelectedImageIndex = 3;
                        }
                        else
                        {
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 2;
                        }
                    }
                    parentNode.Nodes.Add(node);
                    AddTreeView(datarow["tvChildId"].ToString(), node);
                }
            }

        }

        private void TableInit(DataTable dt) //dtQueryPower   GroupName UserName IcNo IsPower PowerTime   
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "GroupName";
            column.Caption = "班组名称";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UserName";
            column.Caption = "姓名";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IcNo";
            column.Caption = "IC号";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IsPower";
            column.Caption = "是否授权";
            dt.Columns.Add(column);
        }

        private void ShowDtQuery()   //dtQueryPower   GroupName UserName IcNo IsPower PowerTime  
        {
            dtQueryPower.Rows.Clear();

            string strSql = "select GroupName,UserName,IcNo,IsPower from tb_DoorUser where IsGroup='" + GroupPeoType.人员.ToString() + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                DataRow row = dtQueryPower.NewRow();
                row["GroupName"] = datarow["GroupName"].ToString();
                row["UserName"] = datarow["UserName"].ToString();
                row["IcNo"] = datarow["IcNo"].ToString();
                string str = datarow["IsPower"].ToString();
                if (string.IsNullOrEmpty(str))
                    row["IsPower"] = DoorPowerType.未授权.ToString();
                else
                    row["IsPower"] = str;
                dtQueryPower.Rows.Add(row);
            }
            this.gridControl1.DataSource = dtQueryPower;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
                {
                    if (treeView1.SelectedNode.ImageIndex == 0 || treeView1.SelectedNode.ImageIndex == 1)
                    {
                        string str = treeView1.SelectedNode.Name.ToString();
                        dtQueryPower.Rows.Clear();
                        ShowChildDtQuery(str, dtQueryPower);
                        this.gridControl1.DataSource = dtQueryPower;
                    }
                    else if (treeView1.SelectedNode.ImageIndex == 2 || treeView1.SelectedNode.ImageIndex == 3)
                    {
                        string str = treeView1.SelectedNode.Name.ToString();
                        string strSql = "select IsGroup,GroupName,UserName,tvChildId,IcNo,CardNo,IsPower from tb_DoorUser where tvChildId='" + str + "'";
                        DataTable dt = datalogic.GetDataTable(strSql);
                        if (dt.Rows.Count > 0)
                        {
                            dtQueryPower.Rows.Clear();
                            DataRow row = dtQueryPower.NewRow();
                            row["GroupName"] = dt.Rows[0]["GroupName"].ToString();
                            row["UserName"] = dt.Rows[0]["UserName"].ToString();
                            row["IcNo"] = dt.Rows[0]["IcNo"].ToString();
                            string strPower = dt.Rows[0]["IsPower"].ToString();
                            if (string.IsNullOrEmpty(strPower))
                                row["IsPower"] = DoorPowerType.未授权.ToString();
                            else
                                row["IsPower"] = strPower;
                            dtQueryPower.Rows.Add(row);
                        }
                    }
                }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
                {
                    TreeClickShowAddOrDelete(dtAddPower,false );
                    this.gridControl2.DataSource = dtAddPower;
                }
                else if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
                {
                    TreeClickShowAddOrDelete(dtDeletePower,true);
                    this.gridControl3.DataSource = dtDeletePower;
                }
            }
        }

        private void TreeClickShowAddOrDelete(DataTable datatable,bool blIsDelete)
        {
            if (treeView1.SelectedNode.ImageIndex == 0 || treeView1.SelectedNode.ImageIndex == 1)
            {
                if (MessageUtil.ShowYesNoAndTips("是否选择该节点下所有子节点？") == DialogResult.Yes)
                {
                    string str = treeView1.SelectedNode.Name.ToString();
                    ShowChildDtAddDele(str, datatable, blIsDelete);
                }
            }
            else if (treeView1.SelectedNode.ImageIndex == 2 || treeView1.SelectedNode.ImageIndex == 3)
            {
                string str = treeView1.SelectedNode.Name.ToString();
                string strSql = "select IsGroup,GroupName,UserName,tvChildId,IcNo,CardNo,IsPower from tb_DoorUser where tvChildId='" + str + "'";
                DataTable dt = datalogic.GetDataTable(strSql);
                if (dt.Rows.Count > 0)
                {
                    bool blHas = false;
                    string strPower = dt.Rows[0]["IsPower"].ToString();
                    str = dt.Rows[0]["IcNo"].ToString();
                    foreach (DataRow dr in datatable.Rows)
                    {
                        if (dr["IcNo"].ToString() == str)
                            blHas = true;
                    }
                    if (blIsDelete)
                    {
                        if (strPower != DoorPowerType.有权限.ToString())
                            blHas = true;
                    }
                    else
                    {
                        if (strPower == DoorPowerType.有权限.ToString())
                            blHas = true;
                    }
                    if (!blHas)
                    {
                        DataRow row = datatable.NewRow();
                        row["GroupName"] = dt.Rows[0]["GroupName"].ToString();
                        row["UserName"] = dt.Rows[0]["UserName"].ToString();
                        row["IcNo"] = str;
                        if (string.IsNullOrEmpty(strPower))
                            row["IsPower"] = DoorPowerType.未授权.ToString();
                        else
                            row["IsPower"] = strPower;
                        datatable.Rows.Add(row);
                    }
                }
            }
        }

        private void ShowChildDtQuery(string strChildId, DataTable datatable)   //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo IsPower PowerTime
        {
            string strSql = "select IsGroup,GroupName,UserName,tvChildId,IcNo,CardNo,IsPower from tb_DoorUser where tvParent='" + strChildId + "'";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                if (datarow["IsGroup"].ToString() == GroupPeoType.人员.ToString())
                {
                    bool blHas = false;
                    string str = datarow["IcNo"].ToString();
                    foreach (DataRow dr in datatable.Rows)
                    {
                        if (dr["IcNo"].ToString() == str)
                        {
                            blHas = true;
                        }
                    }
                    if (!blHas)
                    {
                        DataRow row = datatable.NewRow();
                        row["GroupName"] = datarow["GroupName"].ToString();
                        row["UserName"] = datarow["UserName"].ToString();
                        row["IcNo"] = datarow["IcNo"].ToString();
                        string strPower = datarow["IsPower"].ToString();
                        if (string.IsNullOrEmpty(strPower))
                            row["IsPower"] = DoorPowerType.未授权.ToString();
                        else
                            row["IsPower"] = strPower;
                        datatable.Rows.Add(row);
                    }
                }
                string strChild = datarow["tvChildId"].ToString();
                ShowChildDtQuery(strChild, datatable);
            }
        }


        private void ShowChildDtAddDele(string strChildId, DataTable datatable, bool blIsDelete)   //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo IsPower PowerTime
        {
            string strSql = "select IsGroup,GroupName,UserName,tvChildId,IcNo,CardNo,IsPower from tb_DoorUser where tvParent='" + strChildId + "'";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                if (datarow["IsGroup"].ToString() == GroupPeoType.人员.ToString())
                {
                    bool blHas = false;
                    string strPower = dt.Rows[0]["IsPower"].ToString();
                    string str = datarow["IcNo"].ToString();
                    foreach (DataRow dr in datatable.Rows)
                    {
                        if (dr["IcNo"].ToString() == str)
                        {
                            blHas = true;
                        }
                    }
                    if (blIsDelete)
                    {
                        if (strPower != DoorPowerType.有权限.ToString())
                            blHas = true;
                    }
                    else
                    {
                        if (strPower == DoorPowerType.有权限.ToString())
                            blHas = true;
                    }
                    if (!blHas)
                    {
                        DataRow row = datatable.NewRow();
                        row["GroupName"] = datarow["GroupName"].ToString();
                        row["UserName"] = datarow["UserName"].ToString();
                        row["IcNo"] = datarow["IcNo"].ToString();
                        strPower = datarow["IsPower"].ToString();
                        if (string.IsNullOrEmpty(strPower))
                            row["IsPower"] = DoorPowerType.未授权.ToString();
                        else
                            row["IsPower"] = strPower;
                        datatable.Rows.Add(row);
                    }
                }
                string strChild = datarow["tvChildId"].ToString();
                ShowChildDtAddDele(strChild, datatable, blIsDelete);
            }
        }

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            sbtnDeleOne.Enabled = false;
            sbtnClear.Enabled = false;
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }

        private void sbtnAdd_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            sbtnDeleOne.Enabled = true ;
            sbtnClear.Enabled = true;
            dtAddPower.Rows.Clear();
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            sbtnDeleOne.Enabled = true;
            sbtnClear.Enabled = true;
            dtDeletePower.Rows.Clear();
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPowerManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void sbtnNew_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            ShowDtQuery();
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

        private void sbtnAddExit_Click(object sender, EventArgs e)
        {
            sbtnDeleOne.Enabled = false ;
            sbtnClear.Enabled = false;
            dtAddPower.Rows.Clear();
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }

        private void sbtnDeleteExit_Click(object sender, EventArgs e)
        {
            sbtnDeleOne.Enabled = false;
            sbtnClear.Enabled = false;
            dtDeletePower.Rows.Clear();
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }

        private void sbtnClear_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                dtAddPower.Rows.Clear();
            }
            else
            {
                dtDeletePower.Rows.Clear();
            }
        }

        private void sbtnDeleOne_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                try
                {
                    int[] iRowSele = this.gridView2.GetSelectedRows();
                    List<string> list = new List<string>();
                    foreach (int iRow in iRowSele)
                    {
                        string str = dtAddPower.Rows[iRow]["IcNo"].ToString();
                        list.Add(str);
                    }
                    foreach (string str in list)
                    {
                        foreach (DataRow dr in dtAddPower.Rows)
                        {
                            if (dr["IcNo"].ToString() == str)
                            {
                                dtAddPower.Rows.Remove(dr);
                                break;
                            }
                        }
                    }
                    this.gridControl2.DataSource = dtAddPower;
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowTips(ex.Message);
                }
            }
            else
            {
                try
                {
                    int[] iRowSele = this.gridView3.GetSelectedRows();
                    List<string> list = new List<string>();
                    foreach (int iRow in iRowSele)
                    {
                        string str = dtDeletePower.Rows[iRow]["IcNo"].ToString();
                        list.Add(str);
                    }
                    foreach (string str in list)
                    {
                        foreach (DataRow dr in dtDeletePower.Rows)
                        {
                            if (dr["IcNo"].ToString() == str)
                            {
                                dtDeletePower.Rows.Remove(dr);
                                break;
                            }
                        }
                    }
                    this.gridControl3.DataSource = dtDeletePower;
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowTips(ex.Message);
                }
            }
        }

        private void sbtnAddOk_Click(object sender, EventArgs e)
        {
            if (usingWg == DeviceUsing.未启用)
            {
                MessageUtil.ShowError("IC门禁控制器未配置，请检查系统设置");
                return;
            }
            sbtnAddOk.Enabled = false;
            bool bl = true;
            MjRegisterCard mjrc = new MjRegisterCard();
            foreach (DataRow dr in dtAddPower.Rows)
            {
                string str = dr["IcNo"].ToString();

                bool blRet = wgControl.AddCardPower(str);
                if (blRet == false)
                {
                    bl = false;
                    break;
                }
                Thread.Sleep(20);
            }
            if (bl == true)
            {
                MessageUtil.ShowTips("添加权限成功");
                foreach (DataRow dr in dtAddPower.Rows)
                {
                    string str = dr["IcNo"].ToString();
                    string strSql = "update tb_DoorUser set IsPower='" + DoorPowerType.有权限.ToString() + "' where IcNo='" + str + "' ";
                    datalogic.SqlComNonQuery(strSql);
                    string strGroup = dr["GroupName"].ToString();
                    string strName = dr["UserName"].ToString();
                    string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    strSql = "insert into tb_RecordPower (PowerType,GroupName,UserName,OperateTime,People)" +
                           "values ('添加权限','" + strGroup + "','" + strName + "','" + strTime + "','" + frmMain.strUserName + "')";
                    datalogic.SqlComNonQuery(strSql);
                }
            }
            else
                MessageUtil.ShowTips("添加权限失败");
            dtAddPower.Rows.Clear();
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            sbtnAddOk.Enabled = true;
        }

        private void sbtnDeleteOk_Click(object sender, EventArgs e)
        {
            if (usingWg == DeviceUsing.未启用)
            {
                MessageUtil.ShowError("IC门禁控制器未配置，请检查系统设置");
                return;
            }
            sbtnDeleteOk.Enabled = false;
            bool bl = true;
            foreach (DataRow dr in dtDeletePower.Rows)
            {
                string str = dr["IcNo"].ToString();
                bool blRet = wgControl.DeleteCardPower(str);
                if (blRet==false)
                {
                    bl = false;
                    break;
                }
                Thread.Sleep(20);
            }
            if (bl == true)
            {
                foreach (DataRow dr in dtDeletePower.Rows)
                {
                    string str = dr["IcNo"].ToString();
                    string strSql = "update tb_DoorUser set IsPower='" + DoorPowerType.未授权.ToString() + "' where IcNo='" + str + "' ";
                    datalogic.SqlComNonQuery(strSql);
                    string strGroup = dr["GroupName"].ToString();
                    string strName = dr["UserName"].ToString();
                    string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    strSql = "insert into tb_RecordPower (PowerType,GroupName,UserName,OperateTime,People)" +
                           "values ('删除权限','" + strGroup + "','" + strName + "','" + strTime + "','" + frmMain.strUserName + "')";
                    datalogic.SqlComNonQuery(strSql);
                }
                MessageUtil.ShowTips("删除权限成功");
            }
                
            else
                MessageUtil.ShowTips("删除权限失败");
            dtDeletePower.Rows.Clear();
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            sbtnDeleteOk.Enabled = true ;
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0) return;
            DataRow dr = this.gridView1.GetDataRow(hand);
            if (dr == null) return;
            if (dr["IsPower"].ToString() == DoorPowerType.有权限.ToString())
            {
                e.Appearance.ForeColor = Color.Blue;// 改变行字体颜色
                e.Appearance.BackColor = Color.Blue;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
            }
            else
            {
                e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
                e.Appearance.BackColor = Color.Red;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
            }
        }




        






    }
}
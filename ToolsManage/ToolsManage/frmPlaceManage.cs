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
using ToolsManage.SystemManage;

namespace ToolsManage.ToolsManage
{
    public partial class frmPlaceManage : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        DataOperate operatedata = new DataOperate();

        DataTable dataTable = new DataTable();
        private TagType EditType = TagType.初值;
        private string strAlterChildId = "";
        //private string strNoAlter = "";

        public frmPlaceManage()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;

            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }

        private void frmCabinet_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();

            TableInit();
            ShowDataTable();
        }

        #region  子程序

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
                    }
                    else if (datarow["IsArea"].ToString() == ToolAreaType.位置.ToString())
                    {
                        node.Text = datarow["PlaceName"].ToString();
                        node.ImageIndex = 2;
                        node.SelectedImageIndex = 2;
                    }
                    else if (datarow["IsArea"].ToString() == ToolAreaType.工具柜.ToString())
                    {
                        node.Text = datarow["PlaceName"].ToString();
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 4;
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
                    else if (datarow["IsArea"].ToString() == ToolAreaType.工具.ToString())
                    {
                        node.Text = datarow["ToolID"].ToString();
                        node.ImageIndex = 3;
                        node.SelectedImageIndex = 3;
                    }
                    else if (datarow["IsArea"].ToString() == ToolAreaType.工具柜.ToString())
                    {
                        node.Text = datarow["PlaceName"].ToString();
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 4;
                    }
                    parentNode.Nodes.Add(node);
                    AddTreeView(datarow["tvChildId"].ToString(), node);
                }
            }

        }

        private void TableInit()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "AreaName";
            column.Caption = "区域名称";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PlaceName";
            column.Caption = "存放点名称";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IsArea";
            column.Caption = "存放点类型";
            dataTable.Columns.Add(column);
        }

        private void ShowDataTable()//   
        {
            dataTable.Rows.Clear();

            string strSql = "select AreaName,PlaceName,IsArea from tb_Tools where IsArea='" + ToolAreaType.位置.ToString() + "' or " +
                "IsArea='" + ToolAreaType.工具柜.ToString() + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                DataRow row = dataTable.NewRow();
                row["AreaName"] = datarow["AreaName"].ToString();
                row["PlaceName"] = datarow["PlaceName"].ToString();
                row["IsArea"] = datarow["IsArea"].ToString();
                dataTable.Rows.Add(row);
            }
            this.gridControl1.DataSource = dataTable;
        }

        //选中节点之后，选中节点的所有子节点
        private void DeleteChildNode(TreeNode currNode)
        {
            TreeNodeCollection nodes = currNode.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeNode tn in nodes)
                {
                    string str = tn.Name.ToString();
                    string strsql = "delete from tb_Tools where tvChildId='" + str + "'";
                    datalogic.SqlComNonQuery(strsql);
                    DeleteChildNode(tn);
                }
            }
            datalogic.SqlComNonQuery("delete from tb_Tools where tvChildId='" + currNode.Name.ToString() + "'");
        }

        private void QueryChildNode(string strChildId)
        {
            string strSql = "select AreaName,PlaceName,IsArea,tvChildId from tb_Tools where tvParent='" + strChildId + "'";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                if (datarow["IsArea"].ToString() == ToolAreaType.位置.ToString() || datarow["IsArea"].ToString() == ToolAreaType.工具柜.ToString())
                {
                    DataRow row = dataTable.NewRow();
                    row["AreaName"] = datarow["AreaName"].ToString();
                    row["PlaceName"] = datarow["PlaceName"].ToString();
                    row["IsArea"] = datarow["IsArea"].ToString();
                    dataTable.Rows.Add(row);
                }
                string str = datarow["tvChildId"].ToString();
                QueryChildNode(str);
            }
        }

        /// <summary>
        /// 刷新显示
        /// </summary>
        private void ShowNew()
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            treeView1.Enabled = true;
            EditType = TagType.初值;
            ShowDataTable();

            sbtnExpand.Enabled = true ;
            sbtnAddArea.Enabled = true;
            sbtnAddCabinet.Enabled = true;
            sbtnAddBox.Enabled = true;
            sbtnAlter.Enabled = true;
            sbtnDelete.Enabled = true;
        }

        private void SetBtnFalse()
        {
            sbtnExpand.Enabled = false;
            sbtnAddArea.Enabled = false;
            sbtnAddCabinet.Enabled = false;
            sbtnAddBox.Enabled = false;
            sbtnAlter.Enabled = false;
            sbtnDelete.Enabled = false;
        }

        #endregion

        private void sbtnAddArea_Click(object sender, EventArgs e)
        {
            treeView1.Enabled = false;
            if (treeView1.SelectedNode == null)
            {
                if (MessageUtil.ShowYesNoAndTips("是否添加顶级区域名称？") == DialogResult.Yes)
                {
                    xtraTabControl1.SelectedTabPage = xtraTabPage1;
                    EditType = TagType.添加;
                    gcArea.Text = "添加区域名称";
                    tbAreaName.Text = "";
                }
                else
                {
                    treeView1.Enabled = true;
                    return;
                }
            }
            else if (treeView1.SelectedNode.ImageIndex == 0 || treeView1.SelectedNode.ImageIndex == 1)
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
                EditType = TagType.添加;
                gcArea.Text = "添加区域名称";
                tbAreaName.Text = "";
            }
            else
            {
                if (treeView1.SelectedNode.ImageIndex == 2)
                    MessageUtil.ShowTips("不可在存放点下添加区域");
                if (treeView1.SelectedNode.ImageIndex == 3)
                    MessageUtil.ShowTips("不可在工具节点下添加区域");
                if (treeView1.SelectedNode.ImageIndex == 4)
                    MessageUtil.ShowTips("不可在工具柜节点下添加区域");
                treeView1.Enabled = true;
                return;
            }
            SetBtnFalse();
        }

        private void sbtnAreaOk_Click(object sender, EventArgs e)
        {
            #region 输入 判断
            string strAreaName = tbAreaName.Text.Trim();
            if (string.IsNullOrEmpty(strAreaName))
            {
                MessageUtil.ShowTips("区域名称不可为空");
                return;
            }
            if (strAreaName.Length > 50)
            {
                strAreaName = strAreaName.Substring(0, 50);
            }

            string strSame = "";
            if (EditType == TagType.添加)
            {
                strSame = "Select ID From tb_Tools where AreaName='" + strAreaName + "' and IsArea='" + ToolAreaType.区域.ToString() + "'";
            }
            else if (EditType == TagType.修改)
            {
                strSame = "Select ID From tb_Tools where AreaName='" + strAreaName + "' and IsArea='" + ToolAreaType.区域.ToString() + "' and tvChildId<>'" + strAlterChildId + "' ";
            }
            if (!string.IsNullOrEmpty(strSame))
            {
                if (operatedata.blCheckHas(strSame))
                {
                    MessageUtil.ShowTips("区域名称重复，请重新输入");
                    return;
                }
            }

            #endregion

            string strParent = "";
            string strChild = "";
            if (EditType == TagType.添加)
            {
                TreeNode node = new TreeNode();
                if (treeView1.SelectedNode == null)
                {
                    //获取ChildId
                    strChild = operatedata.GetChildId("tb_Tools", "tvChildId"); 
                    node.Name = strChild;// node.Name 为本节点的编号 
                    strParent = "0";
                    node.Tag = strParent;
                    node.Text = strAreaName;
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    treeView1.Nodes.Add(node);
                }
                else
                {
                    //获取ChildId
                    strChild = operatedata.GetChildId("tb_Tools", "tvChildId"); 
                    node.Name = strChild;// node.Name 为本节点的编号 
                    strParent = treeView1.SelectedNode.Name.ToString();
                    node.Tag = strParent;
                    node.Text = strAreaName;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                    treeView1.SelectedNode.Nodes.Add(node);
                }
                string strSql = "insert into tb_Tools (tvParent,tvChildId,IsArea,AreaName) values ('" + strParent + "'," +
                 "'" + strChild + "','" + ToolAreaType .区域 .ToString () + "','" + strAreaName + "')";
                datalogic.SqlComNonQuery(strSql);
                treeView1.ExpandAll();
            }
            else if (EditType == TagType.修改)
            {
                treeView1.SelectedNode.Text = strAreaName;
                string strSql = "update tb_Tools set AreaName='" + strAreaName + "' where tvChildId='" + strAlterChildId + "' ";
                datalogic.SqlComNonQuery(strSql);
                if (treeView1 .SelectedNode .FirstNode!=null )
                {
                    if (treeView1.SelectedNode.FirstNode.ImageIndex == 2 || treeView1.SelectedNode.FirstNode.ImageIndex == 4)
                    {
                        strSql  = "update tb_Tools set AreaName='" + strAreaName + "' where tvParent='" + strAlterChildId + "' ";
                        datalogic.SqlComNonQuery(strSql);
                    }
                }
                ShowNew();
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
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
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            EditType = TagType.初值;
            ShowDataTable();
            treeView1.Enabled = true;
        }

        private void sbtnAreaExit_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void sbtnAddCabinet_Click(object sender, EventArgs e)
        {
            treeView1.Enabled = false;
            if (treeView1.SelectedNode == null)
            {
                if (treeView1 .Nodes .Count ==0)
                    MessageUtil.ShowTips("请添加区域");
                else
                    MessageUtil.ShowTips("请选择存放点的区域");
                treeView1.Enabled = true ;
                return;
            }
            else if (treeView1.SelectedNode.ImageIndex == 0 || treeView1.SelectedNode.ImageIndex == 1)
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
                sbtnCabinetOk.Enabled = true;
                tbPlaceName.Enabled = true;
                tbArea.Text = treeView1.SelectedNode.Text.ToString();
                EditType = TagType.添加;
                gcCabinet.Text = "添加存放点";
            }
            else
            {
                if (treeView1.SelectedNode.ImageIndex == 2)
                    MessageUtil.ShowTips("不可在存放点下添加存放点");
                if (treeView1.SelectedNode.ImageIndex == 3)
                    MessageUtil.ShowTips("不可在工具节点下添加存放点");
                if (treeView1.SelectedNode.ImageIndex == 4)
                    MessageUtil.ShowTips("不可在工具柜节点下添加存放点");
                treeView1.Enabled = true;
                return;
            }
            SetBtnFalse();
        }

        private void sbtnCabinetOk_Click(object sender, EventArgs e)
        {
            #region 输入判断
            string strName = tbPlaceName.Text.Trim();
            if (string.IsNullOrEmpty(strName))
            {
                MessageUtil.ShowTips("货存放点名称不能为空");
                return;
            }
            if (strName.Length > 50)
            {
                strName = strName.Substring(0, 50);
            }

            string strSame = "";
            if (EditType == TagType.添加)
            {
                strSame = "Select ID From tb_Tools where PlaceName='" + strName + "' and IsArea='" + ToolAreaType.位置.ToString() + "' ";
            }
            else if (EditType == TagType.修改)
            {
                strSame = "Select ID From tb_Tools where PlaceName='" + strName + "' and IsArea='" + ToolAreaType.位置.ToString() + "' and tvChildId<>'" + strAlterChildId + "' ";
            }
            if (!string.IsNullOrEmpty(strSame))
            {
                if (operatedata.blCheckHas(strSame))
                {
                    MessageUtil.ShowTips("存放点名称重复，请重新输入");
                    return;
                }
            }

            #endregion

            string strParent = "";
            string strChild = "";
            string strAreaName = "";
            if (EditType == TagType.添加)
            {
                TreeNode node = new TreeNode();
                strChild = operatedata.GetChildId("tb_Tools", "tvChildId"); 
                node.Name = strChild;// node.Name 为本节点的编号 
                strParent = treeView1.SelectedNode.Name.ToString();
                strAreaName = treeView1.SelectedNode.Text.ToString();
                node.Tag = strParent;
                node.Text = strName;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                treeView1.SelectedNode.Nodes.Add(node);
                string strSql = "insert into tb_Tools (tvParent,tvChildId,IsArea,AreaName,PlaceName) values ('" + strParent + "'," +
                "'" + strChild + "','" + ToolAreaType.位置.ToString() + "','" + strAreaName + "','" + strName + "')";
                datalogic.SqlComNonQuery(strSql);
                treeView1.ExpandAll();
            }
            else if (EditType == TagType.修改)
            {
                treeView1.SelectedNode.Text = strName;
                string strSql = "update tb_Tools set PlaceName='" + strName + "' where tvChildId='" + strAlterChildId + "' ";
                datalogic.SqlComNonQuery(strSql);

                if (treeView1.SelectedNode.FirstNode != null)
                {
                    if (treeView1.SelectedNode.FirstNode.ImageIndex == 3)
                    {
                        strSql = "update tb_Tools set StoragePlace='" + strName + "' where tvParent='" + strAlterChildId + "' ";
                        datalogic.SqlComNonQuery(strSql);
                    }
                }
                ShowNew();
            }
          
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            sbtnDelete.Enabled = false;
            if (treeView1.SelectedNode == null)
            {
                MessageUtil.ShowTips("请选择需要删除的内容");
            }
            else
            {
                int iImage = treeView1.SelectedNode.ImageIndex ;
                string strName = treeView1.SelectedNode.Text;
                bool blDelect = false;

                if (iImage == 0 || iImage == 1)//区域
                {
                    if (MessageUtil.ShowYesNoAndTips("将删除“"+strName +"”区域"+"及该区域内的所有工具，删除后不可恢复请谨慎操作，确定删除？") == DialogResult.Yes)
                    {
                        blDelect = true;
                    }
                }
                else if (iImage == 2)//存放点
                {
                    if (MessageUtil.ShowYesNoAndTips("将删除“" + strName + "”存放点" + "及该存放点下的所有工具，删除后不可恢复请谨慎操作，确定删除？") == DialogResult.Yes)
                    {
                        blDelect = true;
                    }
                }
                else if (iImage == 3)//工具
                {
                    if (MessageUtil.ShowYesNoAndTips("将删除“" + strName + "”工器具" + "，删除后不可恢复请谨慎操作，确定删除？") == DialogResult.Yes)
                    {
                        blDelect = true;
                    }
                }
                else if (iImage == 4)//工具柜
                {
                    if (MessageUtil.ShowYesNoAndTips("将删除“" + strName + "”工具柜" + "及该工具柜内的所有工具,删除后不可恢复请谨慎操作，确定删除？") == DialogResult.Yes)
                    {
                        blDelect = true;
                    }
                }

                if (blDelect)
                {
                    frmExitUser.userPower = UserPower.系统用户;
                    frmExitUser frm = new frmExitUser();
                    frm.ShowDialog(this);
                    frm.Dispose();
                    if (frmExitUser.blOk)
                    {
                        if (iImage == 4)
                        {
                            string str = treeView1.SelectedNode.Name.ToString();
                            string strsql = "delete from tb_BoxIcPower where BoxChildId='" + str + "'";
                            datalogic.SqlComNonQuery(strsql);
                        }
                        if (treeView1.SelectedNode.GetNodeCount(true) > 0)
                        {
                            DeleteChildNode(treeView1.SelectedNode);
                            treeView1.SelectedNode.Remove();
                        }
                        else
                        {
                            string str = treeView1.SelectedNode.Name.ToString();
                            string strsql = "delete from tb_Tools where tvChildId='" + str + "'";
                            datalogic.SqlComNonQuery(strsql);
                            treeView1.SelectedNode.Remove();
                        }
                        ShowNew();


                    }
                }
            }
            sbtnDelete.Enabled = true;
        }

        private void sbtnNew_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();

            EditType = TagType.初值;
            ShowDataTable();
            treeView1.Enabled = true;

        }

        private void sbtnCabinetExit_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void sbtnAlter_Click(object sender, EventArgs e)
        {
            treeView1.Enabled = false;
            if (treeView1.SelectedNode == null)
            {
                MessageUtil.ShowTips("请选择需要修改的内容");
                treeView1.Enabled = true ;
                return;
            }
            else if (treeView1.SelectedNode.ImageIndex == 3)
            {
                MessageUtil.ShowTips("工器具不可在此处修改，如需修改请在工器具管理界面中修改");
                treeView1.Enabled = true;
                return;
            }
            else 
            {
                EditType = TagType.修改;
                strAlterChildId = treeView1.SelectedNode.Name.ToString();
                if (treeView1.SelectedNode.ImageIndex == 2)
                {
                    #region  修改货架信息

                    gcCabinet.Text = "修改存放点信息";
                    xtraTabControl1.SelectedTabPage = xtraTabPage2;
                    string strSql = "select PlaceName from tb_Tools where tvChildId='" + strAlterChildId + "' ";
                    object ob = datalogic.SqlComScalar(strSql);
                    if (ob != null)
                    {
                        tbPlaceName.Text = ob.ToString();
                    }
                    tbArea.Text = treeView1.SelectedNode.Parent.Text.ToString();

                    #endregion
                }
                if (treeView1.SelectedNode.ImageIndex == 4)
                {
                    bool blJudge = frmMain.PowerJudge(UserPower.厂家);
                    if (!blJudge)
                    {
                        treeView1.Enabled = true;
                        return;
                    }
                        
                    #region  修改工具柜信息

                    groupBox.Text = "修改工具柜信息";
                    xtraTabControl1.SelectedTabPage = xtraTabPage4;
                    string strSql = "select PlaceName,HasDoor,DoorIp,DoorSn,BoxHasRfid,BoxRfidIp,BoxRfidPort,BoxRfidMain,BoxRfidAnt1,BoxRfidAnt2," +
                        "BoxRfidAnt3,BoxRfidAnt4,wgPort from tb_Tools where tvChildId='" + strAlterChildId + "' ";
                    DataTable dt = datalogic.GetDataTable(strSql);
                    if (dt.Rows.Count > 0)
                    {
                        tbBoxName.Text = dt.Rows[0]["PlaceName"].ToString();
                        tbDoorIP.Text = dt.Rows[0]["DoorIp"].ToString();
                        tbDoorSn.Text = dt.Rows[0]["DoorSn"].ToString();
                        tbDooPort.Text = dt.Rows[0]["wgPort"].ToString();

                        string strHas = dt.Rows[0]["HasDoor"].ToString();
                        if (strHas == DeviceUsing.启用.ToString())
                        {
                            rbtnDoorHas.Checked = true;
                            rbtnDoorNo.Checked = false;
                        }
                        else
                        {
                            rbtnDoorHas.Checked = false;
                            rbtnDoorNo.Checked = true;
                        }
                    }
                    tbBoxArea.Text = treeView1.SelectedNode.Parent.Text.ToString();

                    #endregion
                }
                else if (treeView1.SelectedNode.ImageIndex == 1 || treeView1.SelectedNode.ImageIndex == 0)
                {
                    #region  修改区域名称

                    gcArea.Text = "修改区域名称";
                    xtraTabControl1.SelectedTabPage = xtraTabPage1;
                    tbAreaName.Text = treeView1.SelectedNode.Text.ToString();

                    #endregion
                }
            }
            SetBtnFalse();
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
                    xtraTabControl1.SelectedTabPage = xtraTabPage3;
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

        private void sbtnAddBox_Click(object sender, EventArgs e)
        {
            bool blJudge = frmMain.PowerJudge(UserPower.厂家);
            if (!blJudge)
                return;

            treeView1.Enabled = false;
            if (treeView1.SelectedNode == null)
            {
                if (treeView1.Nodes.Count == 0)
                    MessageUtil.ShowTips("请添加区域");
                else
                    MessageUtil.ShowTips("请选择存放点的区域");
                treeView1.Enabled = true;
                return;
            }
            else if (treeView1.SelectedNode.ImageIndex == 0 || treeView1.SelectedNode.ImageIndex == 1)
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage4;
                tbBoxArea.Text = treeView1.SelectedNode.Text.ToString();
                EditType = TagType.添加;
                groupBox.Text = "添加工具柜";
            }
            else
            {
                if (treeView1.SelectedNode.ImageIndex == 2)
                    MessageUtil.ShowTips("不可在存放点下添加工具柜");
                if (treeView1.SelectedNode.ImageIndex == 3)
                    MessageUtil.ShowTips("不可在工具节点下添加工具柜");
                if (treeView1.SelectedNode.ImageIndex == 4)
                    MessageUtil.ShowTips("不可在工具柜节点下添加工具");
                treeView1.Enabled = true;
                return;
            }
            SetBtnFalse();
        }

        private void sbtnSearch_Click(object sender, EventArgs e)
        {
            frmDoorSearch frm = new frmDoorSearch();
            frm.ShowDialog(this);
            frm.Dispose();
            if (frmDoorSearch.blSelect)
            {
                tbDoorIP.Text = frmDoorSearch.strIp;
                tbDoorSn.Text = frmDoorSearch.strSn;  
            }
        }

        private void sbtnBoxOk_Click(object sender, EventArgs e)
        {
            #region 输入判断

            string strName = tbBoxName.Text.Trim();
            if (string .IsNullOrEmpty (strName ))
            {
                MessageUtil.ShowTips("工具柜名称不能为空");
                return;
            }

            string strSame = "";
            if (EditType == TagType.添加)
            {
                strSame = "Select ID From tb_Tools where PlaceName='" + strName + "' and IsArea='" + ToolAreaType.工具柜.ToString() + "' ";
            }
            else if (EditType == TagType.修改)
            {
                strSame = "Select ID From tb_Tools where PlaceName='" + strName + "' and IsArea='" + ToolAreaType.工具柜.ToString() + "' and tvChildId<>'" + strAlterChildId + "' ";
            }
            if (!string.IsNullOrEmpty(strSame))
            {
                if (operatedata.blCheckHas(strSame))
                {
                    MessageUtil.ShowTips("存放点名称重复，请重新输入");
                    return;
                }
            }

            if (strName.Length > 50)
            {
                strName = strName.Substring(0, 50);
            }

            #region  门禁

            DeviceUsing DoorUsing = DeviceUsing.未启用;
            string strDoorIp = tbDoorIP.Text.Trim();
            string strDoorSn = tbDoorSn.Text.Trim();
            string strWgPort = tbDooPort.Text.Trim();
            if (rbtnDoorHas.Checked)
            {
                DoorUsing = DeviceUsing.启用;
                if (string.IsNullOrEmpty(strDoorIp))
                {
                    MessageUtil.ShowTips("工具柜门禁IP不能为空");
                    return;
                }
                if (string.IsNullOrEmpty(strDoorSn))
                {
                    MessageUtil.ShowTips("工具柜门禁SN不能为空");
                    return;
                }
                if (string.IsNullOrEmpty(strWgPort ))
                {
                    MessageUtil.ShowTips("工具柜门禁 端口号 不能为空");
                    return;
                }
            }
            else
            {
                DoorUsing = DeviceUsing.未启用;
            }

            #endregion

            #region

            //#region  RFID

            //DeviceUsing RfidUsing = DeviceUsing.未启用;
            //DeviceUsing Ant1Using = DeviceUsing.未启用;
            //DeviceUsing Ant2Using = DeviceUsing.未启用;
            //DeviceUsing Ant3Using = DeviceUsing.未启用;
            //DeviceUsing Ant4Using = DeviceUsing.未启用;
            //BoxRfidMain boxRfidMain = BoxRfidMain.未配置;

            //string strRfifIp = tbRfidIp.Text.Trim();
            //string strRfidPort = tbRfidPort.Text.Trim();


            //if (rbtnMain.Checked)
            //{
            //    boxRfidMain = BoxRfidMain.主机;
            //    if (cboxAnt3.Checked || cboxAnt4.Checked)
            //    {
            //        MessageUtil.ShowTips("RFID读写器作为主机模式时，应选择天线1或天线2");
            //        return;
            //    }
            //}
            //else
            //{
            //    boxRfidMain = BoxRfidMain.从机;
            //    if (cboxAnt1.Checked || cboxAnt2.Checked)
            //    {
            //        MessageUtil.ShowTips("RFID读写器作为从机模式时，应选择天线3或天线4");
            //        return;
            //    }
            //}


            //if (rbtnRfidHas.Checked)
            //{
            //    RfidUsing = DeviceUsing.启用;
            //    if (string.IsNullOrEmpty(strRfifIp))
            //    {
            //        MessageUtil.ShowTips("RFID读写器IP不能为空");
            //        return;
            //    }
            //    if (string.IsNullOrEmpty(strRfidPort))
            //    {
            //        MessageUtil.ShowTips("RFID读写器端口号不能为空");
            //        return;
            //    }
            //    if (!cboxAnt1.Checked && !cboxAnt2.Checked && !cboxAnt3.Checked && !cboxAnt4.Checked)
            //    {
            //        MessageUtil.ShowTips("请选择RFID读写器天线");
            //        return;
            //    }
            //    int iAntCount = 0;
            //    if (cboxAnt1.Checked)
            //    {
            //        iAntCount++;
            //        Ant1Using = DeviceUsing.启用;
            //    }
            //    if (cboxAnt2.Checked)
            //    {
            //        iAntCount++;
            //        Ant2Using = DeviceUsing.启用;
            //    }
            //    if (cboxAnt3.Checked)
            //    {
            //        iAntCount++;
            //        Ant3Using = DeviceUsing.启用;
            //    }
            //    if (cboxAnt4.Checked)
            //    {
            //        iAntCount++;
            //        Ant4Using = DeviceUsing.启用;
            //    }
            //    if (iAntCount > 2)
            //    {
            //        MessageUtil.ShowTips("RFID读写器天线数量大于2，请重新选择");
            //        return;
            //    }
            //    // BoxHasRfid,BoxRfidIp,BoxRfidPort
            //    string strSql = "";
            //    if (EditType == TagType.添加)
            //    {
            //        strSql = "select PlaceName,BoxRfidAnt1,BoxRfidAnt2,BoxRfidAnt3,BoxRfidAnt4 from tb_Tools where IsArea='" + ToolAreaType.工具柜.ToString() + "' and " +
            //        "(BoxRfidIp='" + strRfifIp + "' or BoxRfidPort='" + strRfidPort + "')";
            //    }
            //    else
            //    {
            //        strSql = "select PlaceName,BoxRfidAnt1,BoxRfidAnt2,BoxRfidAnt3,BoxRfidAnt4 from tb_Tools where IsArea='" + ToolAreaType.工具柜.ToString() + "' and " +
            //         "(BoxRfidIp='" + strRfifIp + "' or BoxRfidPort='" + strRfidPort + "') and tvChildId<>'" + strAlterChildId + "'";
            //    }
            //    DataTable dt = datalogic.GetDataTable(strSql);
            //    if (dt.Rows.Count > 0)
            //    {
            //        string name = dt.Rows[0]["PlaceName"].ToString();
            //        if (dt.Rows.Count == 1)
            //        {
            //            string str = dt.Rows[0]["BoxRfidAnt1"].ToString();
            //            if (Ant1Using == DeviceUsing.启用)
            //            {
            //                if (str == DeviceUsing.启用.ToString())
            //                {
            //                    MessageUtil.ShowTips(name + " 工具柜已设置天线1，请重新设置");
            //                    return;
            //                }
            //            }
            //            str = dt.Rows[0]["BoxRfidAnt2"].ToString();
            //            if (Ant2Using == DeviceUsing.启用)
            //            {
            //                if (str == DeviceUsing.启用.ToString())
            //                {
            //                    MessageUtil.ShowTips(name + " 工具柜已设置天线2，请重新设置");
            //                    return;
            //                }
            //            }
            //            str = dt.Rows[0]["BoxRfidAnt3"].ToString();
            //            if (Ant3Using == DeviceUsing.启用)
            //            {
            //                if (str == DeviceUsing.启用.ToString())
            //                {
            //                    MessageUtil.ShowTips(name + " 工具柜已设置天线3，请重新设置");
            //                    return;
            //                }
            //            }
            //            str = dt.Rows[0]["BoxRfidAnt4"].ToString();
            //            if (Ant4Using == DeviceUsing.启用)
            //            {
            //                if (str == DeviceUsing.启用.ToString())
            //                {
            //                    MessageUtil.ShowTips(name + " 工具柜已设置天线4，请重新设置");
            //                    return;
            //                }
            //            }
            //        }
            //        if (dt.Rows.Count > 1)
            //        {
            //            MessageUtil.ShowTips(name + " 工具柜已设置该IP或端口号，请重新设置");
            //            return;
            //        }
            //    }

            //}
            //else
            //{
            //    RfidUsing = DeviceUsing.未启用;
            //}

            //#endregion

            #endregion

            #endregion

            string strParent = "";
            string strChild = "";
            string strAreaName = "";
            if (EditType == TagType.添加)
            {
                TreeNode node = new TreeNode();
                if (treeView1.SelectedNode == null)
                {
                    MessageUtil.ShowTips("请选择工具柜的区域");
                    return;
                }
                else
                {
                    if (treeView1.SelectedNode.ImageIndex == 2)
                    {
                        MessageUtil.ShowTips("存放点下不可添加工具柜");
                        return;
                    }
                    else if (treeView1.SelectedNode.ImageIndex == 3)
                    {
                        MessageUtil.ShowTips("工具节点下不可添加工具柜");
                        return;
                    }
                    else if (treeView1.SelectedNode.ImageIndex == 4)
                    {
                        MessageUtil.ShowTips("工具柜节点下不可添加工具柜");
                        return;
                    }
                    else
                    {
                        strChild = operatedata.GetChildId("tb_Tools", "tvChildId"); 
                        node.Name = strChild;// node.Name 为本节点的编号 
                        strParent = treeView1.SelectedNode.Name.ToString();
                        strAreaName = treeView1.SelectedNode.Text.ToString();
                        node.Tag = strParent;
                        node.Text = strName;
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 4;
                        treeView1.SelectedNode.Nodes.Add(node);//HasDoor,DoorIp,DoorSn,BoxHasRfid,BoxRfidIp,BoxRfidPort,BoxRfidAnt1,BoxRfidAnt2,BoxRfidAnt3,BoxRfidAnt4 BoxRfidMain
                        string strSql = "insert into tb_Tools (tvParent,tvChildId,IsArea,AreaName,PlaceName,HasDoor,DoorIp,DoorSn,wgPort) values " +
                            "('" + strParent + "','" + strChild + "','" + ToolAreaType.工具柜.ToString() + "','" + strAreaName + "','" + strName + "'"+
                            ",'" + DoorUsing.ToString() + "','" + strDoorIp + "','" + strDoorSn + "','" + strWgPort + "')";
                        datalogic.SqlComNonQuery(strSql);
                        treeView1.ExpandAll();
                    }
                }
            }
            else if (EditType == TagType.修改)
            {
                treeView1.SelectedNode.Text = strName;//,,,,,BoxRfidAnt2,BoxRfidAnt3,BoxRfidAnt4
                string strSql = "update tb_Tools set PlaceName='" + strName + "',HasDoor='" + DoorUsing .ToString () + "',DoorIp='" + strDoorIp  + "',"+
                    "DoorSn='" + strDoorSn + "',wgPort='" + strWgPort + "' where tvChildId='" + strAlterChildId + "' ";
                datalogic.SqlComNonQuery(strSql);

                if (treeView1.SelectedNode.FirstNode != null)
                {
                    if (treeView1.SelectedNode.FirstNode.ImageIndex == 4)
                    {
                        strSql = "update tb_Tools set StoragePlace='" + strName + "' where tvParent='" + strAlterChildId + "' ";
                        datalogic.SqlComNonQuery(strSql);
                    }
                }
                ShowNew();

                strSql = "update tb_BoxIcPower set BoxName='" + strName + "' where BoxChildId='" + strAlterChildId + "' ";
                datalogic.SqlComNonQuery(strSql);

            }
           
        }

        private void sbtnBoxExit_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void rbtnDoorHas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDoorHas.Checked)
            {
                tbDoorIP.Enabled = true;
                tbDooPort.Enabled = true;
                tbDoorSn.Enabled = true;
                sbtnSearch.Enabled = true;
            }
        }

        private void rbtnDoorNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDoorNo.Checked)
            {
                tbDoorIP.Enabled = false;
                tbDooPort.Enabled = false;
                tbDoorSn.Enabled = false;
                sbtnSearch.Enabled = false;
            }
        }

















     
    }
}
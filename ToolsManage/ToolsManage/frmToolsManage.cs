using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Diagnostics;

using WG3000_COMM.Core;

using ToolsManage.BaseClass;

namespace ToolsManage.ToolsManage
{
    public partial class frmToolsManage : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        //OperateData operatedata = new OperateData();

        DataTable dataTable = new DataTable();
        DataTable dtOut = new DataTable();

        wgMjController wgMjController = new wgMjController();

        public frmToolsManage()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
            wgMjController.PORT = 60000;
        }

        private void frmToolsManage_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            cbbToolInRoom.Text = "全部";

            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();

            TableInit();
            dtOutInit();
            BrTableShowInit();
            ShowDataTable();
        }

        private void BrTableShowInit()// ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,InPeople,InTime
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolID";
            Col1.Caption = "工具ID";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolType";
            Col1.Caption = "工具种类";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolName";
            Col1.Caption = "工具名称";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "RFIDCoding";
            Col1.Caption = "RFID编号";
            Col1.Visible = true ;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "StoragePlace";
            Col1.Caption = "存放位置";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IsInStore";
            Col1.Caption = "工具状态";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);
        }

        private void TableInit()//ToolType,ToolName,ToolID,RFIDCoding,StoragePlace
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolID";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolType";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolName";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "RFIDCoding";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "StoragePlace";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IsInStore";
            dataTable.Columns.Add(column);
        }

        /// <summary>
        /// 导出表
        /// </summary>
        private void dtOutInit()
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            dtOut.Columns.Add(column); 

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolID";
            dtOut.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolType";
            dtOut.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolName";
            dtOut.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "StoragePlace";
            dtOut.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IsInStore";
            dtOut.Columns.Add(column);
        }

        private void ShowDataTable()//   
        {
            string strSql = "select ID,ToolType,ToolName,ToolID,StoragePlace,IsInStore,RFIDCoding from tb_Tools where IsArea='" + ToolAreaType.工具.ToString() + "' ";
            dataTable=datalogic.GetDataTable(strSql);
            this.gridControl1.DataSource = dataTable;
        }

        private void AddTreeView(string strParent, TreeNode parentNode)
        {
            try
            {
                string strSql = "select tvParent,tvChildId,IsArea,AreaName,PlaceName,ToolID,IsInStore from tb_Tools where tvParent='" + strParent + "'";
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
                            node.ImageIndex = 6;
                            node.SelectedImageIndex = 6;
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
                            parentNode.Nodes.Add(node);
                        }
                        else if (datarow["IsArea"].ToString() == ToolAreaType.位置.ToString())
                        {
                            node.Text = datarow["PlaceName"].ToString();
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 2;
                            parentNode.Nodes.Add(node);
                        }
                        else if (datarow["IsArea"].ToString() == ToolAreaType.工具柜.ToString())
                        {
                            node.Text = datarow["PlaceName"].ToString();
                            node.ImageIndex = 6;
                            node.SelectedImageIndex = 6;
                            parentNode.Nodes.Add(node);
                        }
                        //else if (datarow["IsArea"].ToString() == "工具")
                        //{
                        //    node.Text = datarow["ToolID"].ToString();
                        //    node.ImageIndex = 3;
                        //    node.SelectedImageIndex = 3;
                        //}
                        //parentNode.Nodes.Add(node);
                        AddTreeView(datarow["tvChildId"].ToString(), node);
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.ImageIndex != 3)
                {
                    string str = treeView1.SelectedNode.Name.ToString();
                    dataTable.Rows.Clear();
                    QueryChildNode(str);
                    this.gridControl1.DataSource = dataTable;
                }
                if (treeView1.SelectedNode.ImageIndex == 6)
                {
                    sbtnOpenBox.Enabled = true;
                }
                else
                {
                    sbtnOpenBox.Enabled = false;
                }
            }
        }

        private void QueryChildNode(string strChildId)//  ToolType,ToolName,ToolID,StoragePlace
        {
            string strCondition = "";
            if (cbbToolInRoom.Text == ToolsState.在库.ToString())
                strCondition = ToolsState.在库.ToString();
            else if (cbbToolInRoom.Text == ToolsState.借出.ToString())
                strCondition = ToolsState.借出.ToString();
            else if (cbbToolInRoom.Text == ToolsState.外借超时.ToString())
                strCondition = ToolsState.外借超时.ToString();
            string strSql = "select ID,tvChildId,ToolType,ToolName,ToolID,StoragePlace,IsArea,IsInStore,RFIDCoding from tb_Tools where tvParent='" + strChildId + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                if (datarow["IsArea"].ToString() == "工具" )
                {
                    if (string.IsNullOrEmpty(strCondition))
                    {
                        DataRow row = dataTable.NewRow();
                        row["ID"] = datarow["ID"].ToString();
                        row["ToolType"] = datarow["ToolType"].ToString();
                        row["ToolName"] = datarow["ToolName"].ToString();
                        row["ToolID"] = datarow["ToolID"].ToString();
                        row["StoragePlace"] = datarow["StoragePlace"].ToString();
                        row["IsInStore"] = datarow["IsInStore"].ToString();
                        row["RFIDCoding"] = datarow["RFIDCoding"].ToString();
                        dataTable.Rows.Add(row);
                    }
                    else
                    {
                        if (datarow["IsInStore"].ToString() == strCondition)
                        {
                            DataRow row = dataTable.NewRow();
                            row["ID"] = datarow["ID"].ToString();
                            row["ToolType"] = datarow["ToolType"].ToString();
                            row["ToolName"] = datarow["ToolName"].ToString();
                            row["ToolID"] = datarow["ToolID"].ToString();
                            row["StoragePlace"] = datarow["StoragePlace"].ToString();
                            row["IsInStore"] = datarow["IsInStore"].ToString();
                            dataTable.Rows.Add(row);
                        }
                    }
      
                }
                string str = datarow["tvChildId"].ToString();
                QueryChildNode(str);
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                if (treeView2.Nodes.Count > 0)
                    treeView2.Nodes.Clear();

                AddTreeViewToolsType("0", (TreeNode)null);
                treeView2.ExpandAll();
                sbtnOpenBox.Enabled = false;
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                if (treeView1.Nodes.Count > 0)
                    treeView1.Nodes.Clear();
                AddTreeView("0", (TreeNode)null);
                treeView1.ExpandAll();
                ShowDataTable();
            }
        }

        private void AddTreeViewToolsType(string strParent, TreeNode parentNode)
        {
            try
            {
                string strSql = "select tvParent,tvChildId,ToolName,ToolType from tb_TypeAndName where tvParent='" + strParent + "' ";
                DataTable dt = datalogic.GetDataTable(strSql);

                foreach (DataRow datarow in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    //处理根节点
                    if (parentNode == null)
                    {
                        node.Name = datarow["tvChildId"].ToString();// node.Name 为本节点的编号
                        node.Tag = strParent;                       // node.Tag 为本节点父节点的编号
                        node.Text = datarow["ToolType"].ToString();
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 4;
                        treeView2.Nodes.Add(node);
                        AddTreeViewToolsType(datarow["tvChildId"].ToString(), node);
                    }
                    //处理子节点
                    else
                    {
                        node.Name = datarow["tvChildId"].ToString();
                        node.Tag = strParent;
                        string str = datarow["ToolName"].ToString();
                        node.Text = str;
                        node.ImageIndex = 5;
                        node.SelectedImageIndex = 5;

                        //strSql = "select tvParent,tvChildId,ToolID,ToolName from tb_Tools where ToolName='" + str + "'";
                        //DataTable dtToos = datalogic.GetDataTable(strSql);
                        //foreach (DataRow drTools in dtToos.Rows)
                        //{
                        //    TreeNode tnToos = new TreeNode();
                        //    tnToos.Name = drTools["tvChildId"].ToString();
                        //    //tnToos.Tag = strParent;
                        //    tnToos.Text = drTools["ToolID"].ToString();
                        //    tnToos.ImageIndex = 3;
                        //    tnToos.SelectedImageIndex = 3;
                        //    node.Nodes.Add(tnToos);
                        //}

                        parentNode.Nodes.Add(node);
                        AddTreeViewToolsType(datarow["tvChildId"].ToString(), node);
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }


        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            #region  treeView2_AfterSelect

            if (treeView2.SelectedNode != null)
            {
                if (treeView2.SelectedNode.ImageIndex != 3)
                {
                    string strCondition = "";
                    if (cbbToolInRoom.Text == ToolsState.在库.ToString())
                        strCondition = ToolsState.在库.ToString();
                    else if (cbbToolInRoom.Text == ToolsState.借出.ToString())
                        strCondition = ToolsState.借出.ToString();
                    else if (cbbToolInRoom.Text == ToolsState.外借超时.ToString())
                        strCondition = ToolsState.外借超时.ToString();
                    dataTable.Rows.Clear();
                    if (treeView2.SelectedNode.ImageIndex == 4)//种类
                    {
                        string strChildId = treeView2.SelectedNode.Name.ToString();
                        string strSql = "select ToolName from tb_TypeAndName where tvParent='" + strChildId + "' ";
                        DataTable dt = datalogic.GetDataTable(strSql);
                        foreach (DataRow datarow in dt.Rows)
                        {
                            string strName = datarow["ToolName"].ToString();
                            strSql = "select ID,ToolType,ToolName,ToolID,StoragePlace,IsArea,IsInStore,RFIDCoding from tb_Tools where ToolName='" + strName + "' ";
                            DataTable dtType = datalogic.GetDataTable(strSql);
                            foreach (DataRow dr in dtType.Rows)
                            {
                                if (dr["IsArea"].ToString() == "工具")
                                {
                                    if (string.IsNullOrEmpty(strCondition))
                                    {
                                        DataRow row = dataTable.NewRow();
                                        row["ID"] = dr["ID"].ToString();
                                        row["ToolType"] = dr["ToolType"].ToString();
                                        row["ToolName"] = dr["ToolName"].ToString();
                                        row["ToolID"] = dr["ToolID"].ToString();
                                        row["StoragePlace"] = dr["StoragePlace"].ToString();
                                        row["IsInStore"] = dr["IsInStore"].ToString();
                                        row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                                        dataTable.Rows.Add(row);
                                    }
                                    else
                                    {
                                        if (dr["IsInStore"].ToString() == strCondition)
                                        {
                                            DataRow row = dataTable.NewRow();
                                            row["ID"] = dr["ID"].ToString();
                                            row["ToolType"] = dr["ToolType"].ToString();
                                            row["ToolName"] = dr["ToolName"].ToString();
                                            row["ToolID"] = dr["ToolID"].ToString();
                                            row["StoragePlace"] = dr["StoragePlace"].ToString();
                                            row["IsInStore"] = dr["IsInStore"].ToString();
                                            row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                                            dataTable.Rows.Add(row);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (treeView2.SelectedNode.ImageIndex == 5)//名称
                    {
                        string str = treeView2.SelectedNode.Text.ToString();
                        string strSql = "select ID,ToolType,ToolName,ToolID,StoragePlace,IsArea,IsInStore,RFIDCoding from tb_Tools where ToolName='" + str + "' ";
                        DataTable dt = datalogic.GetDataTable(strSql);
                        foreach (DataRow datarow in dt.Rows)
                        {
                            if (datarow["IsArea"].ToString() == "工具")
                            {
                                if (string.IsNullOrEmpty(strCondition))
                                {
                                    DataRow row = dataTable.NewRow();
                                    row["ID"] = datarow["ID"].ToString();
                                    row["ToolType"] = datarow["ToolType"].ToString();
                                    row["ToolName"] = datarow["ToolName"].ToString();
                                    row["ToolID"] = datarow["ToolID"].ToString();
                                    row["StoragePlace"] = datarow["StoragePlace"].ToString();
                                    row["IsInStore"] = datarow["IsInStore"].ToString();
                                    row["RFIDCoding"] = datarow["RFIDCoding"].ToString();
                                    dataTable.Rows.Add(row);
                                }
                                else
                                {
                                    if (datarow["IsInStore"].ToString() == strCondition)
                                    {
                                        DataRow row = dataTable.NewRow();
                                        row["ID"] = datarow["ID"].ToString();
                                        row["ToolType"] = datarow["ToolType"].ToString();
                                        row["ToolName"] = datarow["ToolName"].ToString();
                                        row["ToolID"] = datarow["ToolID"].ToString();
                                        row["StoragePlace"] = datarow["StoragePlace"].ToString();
                                        row["IsInStore"] = datarow["IsInStore"].ToString();
                                        row["RFIDCoding"] = datarow["RFIDCoding"].ToString();
                                        dataTable.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                    //QueryChildNode(str);
                    this.gridControl1.DataSource = dataTable;
                }
            }

            #endregion
        }

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            string strCondition = "";
            if (cbbToolInRoom.Text == ToolsState.在库.ToString())
                strCondition = ToolsState.在库.ToString();
            else if (cbbToolInRoom.Text == ToolsState.借出.ToString())
                strCondition = ToolsState.借出.ToString();
            else if (cbbToolInRoom.Text == ToolsState.外借超时.ToString())
                strCondition = ToolsState.外借超时.ToString();
            dataTable.Rows.Clear();

            string strSql = "select ID,ToolType,ToolName,ToolID,StoragePlace,IsArea,IsInStore,RFIDCoding from tb_Tools ";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["IsArea"].ToString() == "工具")
                {
                    if (string.IsNullOrEmpty(strCondition))
                    {
                        DataRow row = dataTable.NewRow();
                        row["ID"] = dr["ID"].ToString();
                        row["ToolType"] = dr["ToolType"].ToString();
                        row["ToolName"] = dr["ToolName"].ToString();
                        row["ToolID"] = dr["ToolID"].ToString();
                        row["StoragePlace"] = dr["StoragePlace"].ToString();
                        row["IsInStore"] = dr["IsInStore"].ToString();
                        row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                        dataTable.Rows.Add(row);
                    }
                    else
                    {
                        if (dr["IsInStore"].ToString() == strCondition)
                        {
                            DataRow row = dataTable.NewRow();
                            row["ID"] = dr["ID"].ToString();
                            row["ToolType"] = dr["ToolType"].ToString();
                            row["ToolName"] = dr["ToolName"].ToString();
                            row["ToolID"] = dr["ToolID"].ToString();
                            row["StoragePlace"] = dr["StoragePlace"].ToString();
                            row["IsInStore"] = dr["IsInStore"].ToString();
                            row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            this.gridControl1.DataSource = dataTable;

            #region

            //if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            //{
            //    if (treeView1.SelectedNode != null)
            //    {
            //        if (treeView1.SelectedNode.ImageIndex != 3)
            //        {
            //            string str = treeView1.SelectedNode.Name.ToString();
            //            dataTable.Rows.Clear();
            //            QueryChildNode(str);
            //            this.gridControl1.DataSource = dataTable;
            //        }
            //    }
            //}
            //else if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            //{
            //    #region  treeView2_AfterSelect
            //    if (treeView2.SelectedNode != null)
            //    {
            //        if (treeView2.SelectedNode.ImageIndex != 3)
            //        {
            //            string strCondition = "";
            //            if (cbbToolInRoom.Text == "在库")
            //                strCondition = "在库";
            //            else if (cbbToolInRoom.Text == "借出")
            //                strCondition = "借出";
            //            dataTable.Rows.Clear();
            //            if (treeView2.SelectedNode.ImageIndex == 4)//种类
            //            {
            //                string strChildId = treeView2.SelectedNode.Name.ToString();
            //                string strSql = "select ToolName from tb_TypeAndName where tvParent='" + strChildId + "' ";
            //                DataTable dt = datalogic.GetDataTable(strSql);
            //                foreach (DataRow datarow in dt.Rows)
            //                {
            //                    string strName = datarow["ToolName"].ToString();
            //                    strSql = "select ID,ToolType,ToolName,ToolID,StoragePlace,IsArea,IsInStore from tb_Tools where ToolName='" + strName + "' ";
            //                    DataTable dtType = datalogic.GetDataTable(strSql);
            //                    foreach (DataRow dr in dtType.Rows)
            //                    {
            //                        if (dr["IsArea"].ToString() == "工具")
            //                        {
            //                            if (string.IsNullOrEmpty(strCondition))
            //                            {
            //                                DataRow row = dataTable.NewRow();
            //                                row["ID"] = datarow["ID"].ToString();
            //                                row["ToolType"] = datarow["ToolType"].ToString();
            //                                row["ToolName"] = datarow["ToolName"].ToString();
            //                                row["ToolID"] = datarow["ToolID"].ToString();
            //                                row["StoragePlace"] = datarow["StoragePlace"].ToString();
            //                                row["IsInStore"] = datarow["IsInStore"].ToString();
            //                                dataTable.Rows.Add(row);
            //                            }
            //                            else
            //                            {
            //                                if (datarow["IsInStore"].ToString() == strCondition)
            //                                {
            //                                    DataRow row = dataTable.NewRow();
            //                                    row["ID"] = datarow["ID"].ToString();
            //                                    row["ToolType"] = datarow["ToolType"].ToString();
            //                                    row["ToolName"] = datarow["ToolName"].ToString();
            //                                    row["ToolID"] = datarow["ToolID"].ToString();
            //                                    row["StoragePlace"] = datarow["StoragePlace"].ToString();
            //                                    row["IsInStore"] = datarow["IsInStore"].ToString();
            //                                    dataTable.Rows.Add(row);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            else if (treeView2.SelectedNode.ImageIndex == 5)//名称
            //            {
            //                string str = treeView2.SelectedNode.Text.ToString();
            //                string strSql = "select ID,ToolType,ToolName,ToolID,StoragePlace,IsArea,IsInStore from tb_Tools where ToolName='" + str + "' ";
            //                DataTable dt = datalogic.GetDataTable(strSql);
            //                foreach (DataRow datarow in dt.Rows)
            //                {
            //                    if (datarow["IsArea"].ToString() == "工具")
            //                    {
            //                        if (string.IsNullOrEmpty(strCondition))
            //                        {
            //                            DataRow row = dataTable.NewRow();
            //                            row["ID"] = datarow["ID"].ToString();
            //                            row["ToolType"] = datarow["ToolType"].ToString();
            //                            row["ToolName"] = datarow["ToolName"].ToString();
            //                            row["ToolID"] = datarow["ToolID"].ToString();
            //                            row["StoragePlace"] = datarow["StoragePlace"].ToString();
            //                            row["IsInStore"] = datarow["IsInStore"].ToString();
            //                            dataTable.Rows.Add(row);
            //                        }
            //                        else
            //                        {
            //                            if (datarow["IsInStore"].ToString() == strCondition)
            //                            {
            //                                DataRow row = dataTable.NewRow();
            //                                row["ID"] = datarow["ID"].ToString();
            //                                row["ToolType"] = datarow["ToolType"].ToString();
            //                                row["ToolName"] = datarow["ToolName"].ToString();
            //                                row["ToolID"] = datarow["ToolID"].ToString();
            //                                row["StoragePlace"] = datarow["StoragePlace"].ToString();
            //                                row["IsInStore"] = datarow["IsInStore"].ToString();
            //                                dataTable.Rows.Add(row);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            //QueryChildNode(str);
            //            this.gridControl1.DataSource = dataTable;
            //        }
            //    }
            //    #endregion
            //}

            #endregion



        }

        private void sbtnExpand_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                if (treeView1.Nodes.Count > 0)
                {
                    if (treeView1.Nodes[0].IsExpanded == true)
                        treeView1.CollapseAll();
                    else
                        treeView1.ExpandAll();
                }

            }
            else if(xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                if (treeView2.Nodes.Count > 0)
                {
                    if (treeView2.Nodes[0].IsExpanded == true)
                        treeView2.CollapseAll();
                    else
                        treeView2.ExpandAll();
                }
            }
        }

        private void sbtnScrap_Click(object sender, EventArgs e)
        {
            bool blJudge = frmMain.PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            sbtnScrap.Enabled = false;
            int[] rows = gridView1.GetSelectedRows(); //选中多行   
            if (rows.Length > 0)
            {
                if (MessageUtil.ShowYesNoAndTips("确定报废选中工具？") == DialogResult.Yes)
                {
                    frmScrap frm = new frmScrap();
                    DialogResult ret = frm.ShowDialog(this );
                    if (ret == DialogResult.OK)
                    {
                        string strScrapInfo = frmScrap.strScrapInfo;
                        string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        for (int i = 0; i < rows.Length; i++)
                        {
                            string strID = gridView1.GetRowCellDisplayText(rows[i], "ID").ToString();
                            string strSql = "delete from tb_Tools where ID='" + strID + "' ";
                            datalogic.SqlComNonQuery(strSql);
                            string strToolType = gridView1.GetRowCellDisplayText(rows[i], "ToolType").ToString();
                            string strToolName = gridView1.GetRowCellDisplayText(rows[i], "ToolName").ToString();
                            string strToolId = gridView1.GetRowCellDisplayText(rows[i], "ToolID").ToString();
                            string strPlace = gridView1.GetRowCellDisplayText(rows[i], "StoragePlace").ToString();


                            strSql = "insert into tb_RecordScrap (ToolType,ToolName,ToolID,StoragePlace,ScrapInfo,ScrapPeople,ScrapTime)" +
                        "values ('" + strToolType + "','" + strToolName + "','" + strToolId + "','" + strPlace + "',"+
                        "'" + strScrapInfo + "','" + frmMain .strUserName + "','" + strTime + "')";
                            datalogic.SqlComNonQuery(strSql);
                        }
                    }
                    if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
                    {
                        if (treeView2.Nodes.Count > 0)
                            treeView2.Nodes.Clear();

                        AddTreeViewToolsType("0", (TreeNode)null);
                        treeView2.ExpandAll();
                    }
                    else if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
                    {
                        if (treeView1.Nodes.Count > 0)
                            treeView1.Nodes.Clear();
                        AddTreeView("0", (TreeNode)null);
                        treeView1.ExpandAll();
                        ShowDataTable();
                    }
                    ShowDataTable();

                }
            }
            else
            {
                MessageUtil.ShowTips("请选择需报废的工具");
            }
            sbtnScrap.Enabled = true;
        }

        private void sbtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string savePath = FileDialogHelper.SaveExcel();
                if (!string.IsNullOrEmpty(savePath))
                {
                    string outError = "";

                    dtOut.Rows.Clear();

                    DataRow outRow = dtOut.NewRow();//存放位置,工具状态
                    outRow["ID"] = "序号";
                    outRow["ToolType"] = "工具种类";
                    outRow["ToolName"] = "工具名称";
                    outRow["ToolID"] = "工具ID";
                    outRow["StoragePlace"] = "存放位置";
                    outRow["IsInStore"] = "工具状态";
                    dtOut.Rows.Add(outRow);

               


                    int nRow = 0;
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        nRow++;
                        DataRow outRows = dtOut.NewRow();
                        outRows["ID"] = nRow.ToString ();
                        outRows["ToolType"] = drow["ToolType"].ToString();
                        outRows["ToolName"] = drow["ToolName"].ToString();
                        outRows["ToolID"] = drow["ToolID"].ToString();
                        outRows["StoragePlace"] = drow["StoragePlace"].ToString();
                        outRows["IsInStore"] = drow["IsInStore"].ToString();
                        dtOut.Rows.Add(outRows);
                    }

                    AsposeExcelTools.DataTableToExcel(dtOut, savePath, out outError);

                    if (!string.IsNullOrEmpty(outError))
                    {
                        MessageBox.Show(outError);
                    }
                    else
                    {
                        Process.Start(savePath);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageUtil.ShowTips(ee .ToString ());
            }
        }

        private void sbtnAlter_Click(object sender, EventArgs e)
        {
            bool blJudge = frmMain.PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            int[] rows = gridView1.GetSelectedRows(); //选中多行   
            if (rows.Length == 1)
            {
                if (infoOfSystem.usingRfid == DeviceUsing.启用)
                {
                    frmInRoom frm = new frmInRoom();
                    frm.Tag = TagType.修改.ToString();
                    frm.sId = gridView1.GetRowCellDisplayText(rows[0], "ID").ToString();
                    frm.sToolType = gridView1.GetRowCellDisplayText(rows[0], "ToolType").ToString();
                    frm.sToolName = gridView1.GetRowCellDisplayText(rows[0], "ToolName").ToString();
                    frm.sToolId = gridView1.GetRowCellDisplayText(rows[0], "ToolID").ToString();
                    frm.sPlace = gridView1.GetRowCellDisplayText(rows[0], "StoragePlace").ToString();
                    frm.sRFID = gridView1.GetRowCellDisplayText(rows[0], "RFIDCoding").ToString();
                    frm.ShowDialog(this);
                    frm.Dispose();
                }
                else
                {
                    frmInRoomPower frm = new frmInRoomPower(); 
                    frm.Tag = TagType.修改.ToString();
                    frm.sId = gridView1.GetRowCellDisplayText(rows[0], "ID").ToString();
                    frm.sToolType = gridView1.GetRowCellDisplayText(rows[0], "ToolType").ToString();
                    frm.sToolName = gridView1.GetRowCellDisplayText(rows[0], "ToolName").ToString();
                    frm.sToolId = gridView1.GetRowCellDisplayText(rows[0], "ToolID").ToString();
                    frm.sPlace = gridView1.GetRowCellDisplayText(rows[0], "StoragePlace").ToString();
                    frm.sRFID = gridView1.GetRowCellDisplayText(rows[0], "RFIDCoding").ToString();
                    frm.ShowDialog(this);
                    frm.Dispose();
                }
              

                treeView1.Nodes.Clear();
                AddTreeView("0", (TreeNode)null);
                treeView1.ExpandAll();

                ShowDataTable();
            }
            else
            {
                if (rows.Length == 0)
                {
                    MessageUtil.ShowTips("请选择工具");
                }
                else
                {
                    MessageUtil.ShowTips("请选择一个工具进行修改");
                }
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0) return;
            DataRow dr = this.gridView1.GetDataRow(hand);
            if (dr == null) return;



            if (dr["IsInStore"].ToString() == ToolsState.在库.ToString())
            {
                e.Appearance.ForeColor = Color.Blue;// 改变行字体颜色
                e.Appearance.BackColor = Color.Blue;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
            }
            else if (dr["IsInStore"].ToString() == ToolsState.借出 .ToString())
            {
                e.Appearance.ForeColor = Color.Orange;// 改变行字体颜色
                e.Appearance.BackColor = Color.Orange;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Orange;// 添加渐变颜色
            }
            else if (dr["IsInStore"].ToString() == ToolsState.外借超时.ToString())
            {
                e.Appearance.ForeColor = Color.Red ;// 改变行字体颜色
                e.Appearance.BackColor = Color.Red;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Red;// 添加渐变颜色
            }
        }

        private void sbtnOpenBox_Click(object sender, EventArgs e)
        {
            sbtnOpenBox.Enabled = false;
            if (treeView1.SelectedNode != null)
            {
                string strChildId = treeView1.SelectedNode.Name.ToString();
                string strName = treeView1.SelectedNode.Text .ToString();
                string strSql = "select IsArea,HasDoor,DoorIp,DoorSn from tb_Tools where tvChildId='" + strChildId + "'";
                DataTable dt = datalogic.GetDataTable(strSql);
                if (dt.Rows.Count > 0)
                {
                    string str = dt.Rows[0]["IsArea"].ToString();
                    if (str != ToolAreaType.工具柜.ToString())
                    {
                        MessageUtil.ShowError("请选择工具柜，开柜门");
                        sbtnOpenBox.Enabled = true;
                        return;
                    }
                    str = dt.Rows[0]["HasDoor"].ToString();
                    if (str != DeviceUsing .启用 .ToString ())
                    {
                        MessageUtil.ShowError(strName +" 工具柜未启用门禁功能");
                        sbtnOpenBox.Enabled = true;
                        return;
                    }
                    string strIp = dt.Rows[0]["DoorIp"].ToString();
                    string strSn = dt.Rows[0]["DoorSn"].ToString();
                    if (string.IsNullOrEmpty(strIp) || string.IsNullOrEmpty(strSn))
                    {
                        MessageUtil.ShowError(strName +" 工具柜门禁控制器IP或SN为空，请重新设置");
                        sbtnOpenBox.Enabled = true;
                        return;
                    }
                    else
                    {
                        int iSn = Convert.ToInt32(strSn);
                        wgMjController.ControllerSN = iSn;
                        wgMjController.IP = strIp;
                        if (wgMjController.RemoteOpenDoorIP(1) > 0)
                        {
                            MessageUtil.ShowTips(strName +" 开柜门成功！");
                        }
                        else
                        {
                            MessageUtil.ShowError (strName +" 开柜门失败，请检查网络或控制器");
                        }
                        sbtnOpenBox.Enabled = true;
                    }


                }
            }
        }





    }
}
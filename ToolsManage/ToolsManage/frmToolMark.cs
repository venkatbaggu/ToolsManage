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

using ToolsManage.BaseClass;
using ToolsManage.BaseClass.SeralClass;

namespace ToolsManage.ToolsManage
{
    public partial class frmToolMark : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        clsSerialVoice serial = new clsSerialVoice();

        public static bool blMark = false;

        /// <summary>
        /// 所有的工器具 包含所有工器具
        /// </summary>
        DataTable tableAll = new DataTable();
        /// <summary>
        /// 条件选择后 显示的工器具
        /// </summary>
        DataTable tableSelectShow = new DataTable();

        public frmToolMark()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
        }

        private void frmToolMark_Load(object sender, EventArgs e)
        {
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            tableAllInit();
            tableSelectShowInit();
            tableShowInit();
            ShowDataTable();
        }

        #region  子程序

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
                        treeView1.Nodes.Add(node);
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

        private void tableShowInit()// ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,InPeople,InTime
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
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolName";
            Col1.Caption = "工具名称";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "RFIDCoding";
            Col1.Caption = "RFID编号";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "StoragePlace";
            Col1.Caption = "工具存放位置";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IsInStore";
            Col1.Caption = "工具状态";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "stateMark";
            Col1.Caption = "盘库结果";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);
        }

        private void tableAllInit()//ToolType,ToolName,ToolID,RFIDCoding,StoragePlace
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            tableAll.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolID";
            tableAll.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolType";
            tableAll.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolName";
            tableAll.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "RFIDCoding";
            tableAll.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "StoragePlace";
            tableAll.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IsInStore";
            tableAll.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "stateMark";
            tableAll.Columns.Add(column);
        }

        private void tableSelectShowInit()//ToolType,ToolName,ToolID,RFIDCoding,StoragePlace
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            tableSelectShow.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolID";
            tableSelectShow.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolType";
            tableSelectShow.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolName";
            tableSelectShow.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "RFIDCoding";
            tableSelectShow.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "StoragePlace";
            tableSelectShow.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IsInStore";
            tableSelectShow.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "stateMark";
            tableSelectShow.Columns.Add(column);
        }

        private void ShowDataTable()//   
        {
            string strSql = "select ID,ToolType,ToolName,ToolID,StoragePlace,IsInStore,RFIDCoding from tb_Tools where IsArea='" + ToolAreaType.工具.ToString() + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);
            tableAll.Rows.Clear();
            foreach (DataRow dr in dt.Rows)//dtTools ToolID  RfidCoding ToolBR TimeBR ToolType ToolName
            {
                DataRow row = tableAll.NewRow();
                row["ID"] = dr["ID"].ToString();
                row["ToolType"] = dr["ToolType"].ToString();
                row["ToolName"] = dr["ToolName"].ToString();
                row["ToolID"] = dr["ToolID"].ToString();
                row["StoragePlace"] = dr["StoragePlace"].ToString();
                row["IsInStore"] = dr["IsInStore"].ToString();
                row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                row["stateMark"] = "";
                tableAll.Rows.Add(row);
            }
            this.gridControl1.DataSource = tableAll;
        }

        #endregion

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
                    tableSelectShow.Rows.Clear();
                    if (treeView1.SelectedNode.ImageIndex == 4)//种类
                    {
                        foreach (DataRow dr in tableAll.Rows)//dtTools ToolID  RfidCoding ToolBR TimeBR ToolType ToolName
                        {
                            string strType = treeView1.SelectedNode.Text;
                            if (dr["ToolType"].ToString() == strType)
                            {
                                if (cbbMark.Text == "全部")
                                {
                                    DataRow row = tableSelectShow.NewRow();
                                    row["ID"] = dr["ID"].ToString();
                                    row["ToolType"] = dr["ToolType"].ToString();
                                    row["ToolName"] = dr["ToolName"].ToString();
                                    row["ToolID"] = dr["ToolID"].ToString();
                                    row["StoragePlace"] = dr["StoragePlace"].ToString();
                                    row["IsInStore"] = dr["IsInStore"].ToString();
                                    row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                                    row["stateMark"] = dr["stateMark"].ToString();
                                    tableSelectShow.Rows.Add(row);
                                }
                                else if (cbbMark.Text == "盘库错误")//
                                {
                                    if (dr["stateMark"].ToString() == "盘库错误")
                                    {
                                        DataRow row = tableSelectShow.NewRow();
                                        row["ID"] = dr["ID"].ToString();
                                        row["ToolType"] = dr["ToolType"].ToString();
                                        row["ToolName"] = dr["ToolName"].ToString();
                                        row["ToolID"] = dr["ToolID"].ToString();
                                        row["StoragePlace"] = dr["StoragePlace"].ToString();
                                        row["IsInStore"] = dr["IsInStore"].ToString();
                                        row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                                        row["stateMark"] = dr["stateMark"].ToString();
                                        tableSelectShow.Rows.Add(row);
                                    }
                                }
                                else if (cbbMark.Text == "盘库正确")//
                                {
                                    if (dr["stateMark"].ToString() != "盘库错误")
                                    {
                                        DataRow row = tableSelectShow.NewRow();
                                        row["ID"] = dr["ID"].ToString();
                                        row["ToolType"] = dr["ToolType"].ToString();
                                        row["ToolName"] = dr["ToolName"].ToString();
                                        row["ToolID"] = dr["ToolID"].ToString();
                                        row["StoragePlace"] = dr["StoragePlace"].ToString();
                                        row["IsInStore"] = dr["IsInStore"].ToString();
                                        row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                                        row["stateMark"] = dr["stateMark"].ToString();
                                        tableSelectShow.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                    else if (treeView1.SelectedNode.ImageIndex == 5)//名称
                    {
                        foreach (DataRow dr in tableAll.Rows)//dtTools ToolID  RfidCoding ToolBR TimeBR ToolType ToolName
                        {
                            string strType = treeView1.SelectedNode.Text;
                            if (dr["ToolName"].ToString() == strType)
                            {
                                if (cbbMark.Text == "全部")
                                {
                                    DataRow row = tableSelectShow.NewRow();
                                    row["ID"] = dr["ID"].ToString();
                                    row["ToolType"] = dr["ToolType"].ToString();
                                    row["ToolName"] = dr["ToolName"].ToString();
                                    row["ToolID"] = dr["ToolID"].ToString();
                                    row["StoragePlace"] = dr["StoragePlace"].ToString();
                                    row["IsInStore"] = dr["IsInStore"].ToString();
                                    row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                                    row["stateMark"] = dr["stateMark"].ToString();
                                    tableSelectShow.Rows.Add(row);
                                }
                                else if (cbbMark.Text == "盘库错误")//
                                {
                                    if (dr["stateMark"].ToString() == "盘库错误")
                                    {
                                        DataRow row = tableSelectShow.NewRow();
                                        row["ID"] = dr["ID"].ToString();
                                        row["ToolType"] = dr["ToolType"].ToString();
                                        row["ToolName"] = dr["ToolName"].ToString();
                                        row["ToolID"] = dr["ToolID"].ToString();
                                        row["StoragePlace"] = dr["StoragePlace"].ToString();
                                        row["IsInStore"] = dr["IsInStore"].ToString();
                                        row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                                        row["stateMark"] = dr["stateMark"].ToString();
                                        tableSelectShow.Rows.Add(row);
                                    }
                                }
                                else if (cbbMark.Text == "盘库正确")//
                                {
                                    if (dr["stateMark"].ToString() != "盘库错误")
                                    {
                                        DataRow row = tableSelectShow.NewRow();
                                        row["ID"] = dr["ID"].ToString();
                                        row["ToolType"] = dr["ToolType"].ToString();
                                        row["ToolName"] = dr["ToolName"].ToString();
                                        row["ToolID"] = dr["ToolID"].ToString();
                                        row["StoragePlace"] = dr["StoragePlace"].ToString();
                                        row["IsInStore"] = dr["IsInStore"].ToString();
                                        row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                                        row["stateMark"] = dr["stateMark"].ToString();
                                        tableSelectShow.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                    this.gridControl1.DataSource = tableSelectShow;
                }
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

        private void sbtnMark_Click(object sender, EventArgs e)
        {

            bool blRet = serial.OnOffMark(OnOffMark .开启 );
            if (blRet)
            {
                blMark = true;
                lock (MainControl.listMark)
                {
                    MainControl.listMark.Clear();
                }
                frmProgressBar frm = new frmProgressBar();
                frm.ShowDialog(this);
                frm.Dispose();

                blRet = serial.OnOffMark(OnOffMark.关闭 );
                if (blRet)
                {
                    blMark = false;
                    int iCount = tableAll.Rows.Count;
                    for (int iIndex = 0; iIndex < iCount; iIndex++)
                    {
                        string strCord = tableAll.Rows[iIndex]["RFIDCoding"].ToString();
                        if (tableAll.Rows[iIndex]["IsInStore"].ToString() == ToolsState.在库.ToString())
                        {
                            if (MainControl.listMark.Contains(strCord))
                            {
                                tableAll.Rows[iIndex]["stateMark"] = ToolsState.在库.ToString();
                            }
                            else
                            {
                                tableAll.Rows[iIndex]["stateMark"] = ToolsState.盘库错误.ToString();
                            }
                        }
                        else if (tableAll.Rows[iIndex]["IsInStore"].ToString() == ToolsState.借出.ToString() || tableAll.Rows[iIndex]["IsInStore"].ToString() == ToolsState.外借超时.ToString())
                        {
                            if (!MainControl.listMark.Contains(strCord))
                            {
                                if (tableAll.Rows[iIndex]["IsInStore"].ToString() == ToolsState.借出.ToString())
                                    tableAll.Rows[iIndex]["stateMark"] = ToolsState.借出.ToString();
                                else if (tableAll.Rows[iIndex]["IsInStore"].ToString() == ToolsState.外借超时.ToString())
                                    tableAll.Rows[iIndex]["stateMark"] = ToolsState.外借超时.ToString();
                            }
                            else
                            {
                                tableAll.Rows[iIndex]["stateMark"] = ToolsState.盘库错误.ToString();
                            }
                        }
                    }
                    this.gridControl1.DataSource = tableAll;
                    cbbMark.Enabled = true;
                    sbtnQuery.Enabled = true;
                }
                else
                {
                    MessageUtil.ShowTips("关闭激活器失败，请检查继电器控制器");
                }
            }
            else
            {
                MessageUtil.ShowTips("开启激活器失败，请检查继电器控制器");
            }

        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            if (cbbMark.Text == "全部")
            {
                this.gridControl1.DataSource = tableAll;
            }
            else if (cbbMark.Text == "盘库正确")
            {
                tableSelectShow.Rows.Clear();
                foreach (DataRow dr in tableAll.Rows)//dtTools ToolID  RfidCoding ToolBR TimeBR ToolType ToolName
                {
                    if (dr["stateMark"].ToString() != "盘库错误")
                    {
                        DataRow row = tableSelectShow.NewRow();
                        row["ID"] = dr["ID"].ToString();
                        row["ToolType"] = dr["ToolType"].ToString();
                        row["ToolName"] = dr["ToolName"].ToString();
                        row["ToolID"] = dr["ToolID"].ToString();
                        row["StoragePlace"] = dr["StoragePlace"].ToString();
                        row["IsInStore"] = dr["IsInStore"].ToString();
                        row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                        row["stateMark"] = dr["stateMark"].ToString();
                        tableSelectShow.Rows.Add(row);
                    }
                }
                this.gridControl1.DataSource = tableSelectShow;
            }
            else if (cbbMark.Text == "盘库错误")
            {
                tableSelectShow.Rows.Clear();
                foreach (DataRow dr in tableAll.Rows)//dtTools ToolID  RfidCoding ToolBR TimeBR ToolType ToolName
                {
                    if (dr["stateMark"].ToString() == "盘库错误")
                    {
                        DataRow row = tableSelectShow.NewRow();
                        row["ID"] = dr["ID"].ToString();
                        row["ToolType"] = dr["ToolType"].ToString();
                        row["ToolName"] = dr["ToolName"].ToString();
                        row["ToolID"] = dr["ToolID"].ToString();
                        row["StoragePlace"] = dr["StoragePlace"].ToString();
                        row["IsInStore"] = dr["IsInStore"].ToString();
                        row["RFIDCoding"] = dr["RFIDCoding"].ToString();
                        row["stateMark"] = dr["stateMark"].ToString();
                        tableSelectShow.Rows.Add(row);
                    }
                }
                this.gridControl1.DataSource = tableSelectShow;
            }
        }

        private void sbtnExport_Click(object sender, EventArgs e)
        {

        }





    }

    ///// <summary>
    ///// 继电器开关
    ///// </summary>
    //public enum OnOffRelay : byte
    //{
    //    开启 = 0xff,
    //    关闭 = 0x00
    //}

    ///// <summary>
    ///// 设备对应 继电器的哪一个
    ///// </summary>
    //public enum DeviceRelayNo : byte
    //{
    //    激活器 = 0x00,
    //    警灯 = 0x01
    //}

    ///// <summary>
    ///// 继电器 状态
    ///// </summary>
    //public enum RelayState
    //{
    //    闭合,
    //    断开
    //}


}
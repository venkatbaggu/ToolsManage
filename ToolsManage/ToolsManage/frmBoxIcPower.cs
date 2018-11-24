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
    public partial class frmBoxIcPower : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        DataTable dataTable = new DataTable();

        public frmBoxIcPower()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
        }

        private void frmBoxIcPower_Load(object sender, EventArgs e)
        {
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();

            TableInit();
            TableShowInit();

            string strSql = "select ID,BoxName,UserName,IcNo,CardNo from tb_BoxIcPower ";
            this.gridControl1.DataSource = datalogic.GetDataTable(strSql);
        }



        #region   子程序

        private void TableInit()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");// ID BoxParentId BoxChildId UserId  BoxName  UserName CardNo
            column.ColumnName = "ID";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "BoxName";//工具柜名称
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UserName";//用户名
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IcNo";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CardNo";//卡编号
            dataTable.Columns.Add(column);

        }

        private void TableShowInit()
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();// // ID BoxId UserId  BoxName  UserName CardNo
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "BoxName";
            Col1.Caption = "工具柜名称";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "UserName";
            Col1.Caption = "用户名称";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IcNo";
            Col1.Caption = "IC卡号";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "CardNo";
            Col1.Caption = "IC卡编号";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);
        }

        private void ShowDataTable()//   
        {
            dataTable.Rows.Clear();

            string strSql = "select ID,BoxName,UserName,CardNo,IcNo from tb_BoxIcPower  ";//   ID,BoxParentId,BoxChildId,UserId,BoxName,UserName,CardNo
            //DataTable dt = datalogic.GetDataTable(strSql);
            //foreach (DataRow datarow in dt.Rows)
            //{
            //    DataRow row = dataTable.NewRow();
            //    row["PlaceName"] = datarow["PlaceName"].ToString();
            //    row["DoorPsw1"] = datarow["DoorPsw1"].ToString();
            //    row["DoorPsw2"] = datarow["DoorPsw2"].ToString();
            //    row["DoorPsw3"] = datarow["DoorPsw3"].ToString();
            //    row["DoorPsw4"] = datarow["DoorPsw4"].ToString();
            //    dataTable.Rows.Add(row);
            //}
            this.gridControl1.DataSource = datalogic.GetDataTable(strSql);
        }

        /// <summary>
        /// 获得数据库信息放入TreeView中 (递归)   
        /// </summary>
        /// <param name="strParent"></param>
        /// <param name="parentNode"></param>
        private void AddTreeView(string strParent, TreeNode parentNode)
        {
            //  tvParent tvChildId  IsArea  AreaName
            string strSql = "select tvParent,tvChildId,IsArea,AreaName,PlaceName from tb_Tools where tvParent='" + strParent + "' ";
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



        #endregion

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.ImageIndex == 0)
                {
                    string str = treeView1.SelectedNode.Name.ToString();
                    dataTable.Rows.Clear();

                    string strSql = "select ID,BoxName,UserName,CardNo,IcNo from tb_BoxIcPower where BoxParentId='" + str + "' ";
                    //DataTable dt = datalogic.GetDataTable(strSql);
                    //foreach (DataRow datarow in dt.Rows)
                    //{
                    //    DataRow row = dataTable.NewRow();
                    //    row["ToolName"] = datarow["ToolName"].ToString();
                    //    row["ToolsNameCoding"] = datarow["ToolsNameCoding"].ToString();
                    //    row["ToolsCycle"] = datarow["ToolsCycle"].ToString();
                    //    dataTable.Rows.Add(row);
                    //}
                    this.gridControl1.DataSource = datalogic.GetDataTable(strSql);
                }
                else if (treeView1.SelectedNode.ImageIndex == 4)//   ID,BoxParentId,BoxChildId,UserId,BoxName,UserName,CardNo
                {
                    string str = treeView1.SelectedNode.Name.ToString();
                    dataTable.Rows.Clear();

                    string strSql = "select ID,BoxName,UserName,CardNo,IcNo from tb_BoxIcPower where BoxChildId='" + str + "' ";
                    this.gridControl1.DataSource = datalogic.GetDataTable(strSql);
                }
            }
        }

        private void sbtnNew_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            ShowDataTable();
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

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtnPowerManage_Click(object sender, EventArgs e)
        {
            frmBoxPowerManage frm = new frmBoxPowerManage(); 
            frm.ShowDialog(this);
            frm.Dispose();
            string strSql = "select ID,BoxName,UserName,CardNo,IcNo from tb_BoxIcPower ";
            this.gridControl1.DataSource = datalogic.GetDataTable(strSql);
        }











    }
}
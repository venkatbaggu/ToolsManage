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

namespace ToolsManage.TestManage
{
    public partial class frmTestIn : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        DataTable dtToolTest = new DataTable();
        private bool blFirst = false;

        public frmTestIn()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
        }

        private void frmTestIn_Load(object sender, EventArgs e)
        {
            cbbPeopleIn.Text = frmMain.strUserName;
            TableShowInit();
            TableInit();

            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();
        }

        private void TableInit()//ToolType,ToolName,ToolID,RFIDCoding,StoragePlace
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            column.Caption = "ID";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolID";
            column.Caption = "工具ID";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolType";
            column.Caption = "工具种类";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolName";
            column.Caption = "工具名称";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();// ID,ToolType,ToolName,ToolID,ToolTestTime,ToolTestResult,ToolTestPeople,ToolTestRemark
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolTestTime";
            column.Caption = "上次试验时间";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");// 上次试验时间  试验结果  备注信息 录入人
            column.ColumnName = "ToolTestResult";
            column.Caption = "试验结果";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();// ID,ToolType,ToolName,ToolID,ToolTestTime,ToolTestResult,ToolTestPeople,ToolTestRemark
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolTestPeople";
            column.Caption = "录入人";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolTestRemark";
            column.Caption = "备注信息";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "StoragePlace";
            column.Caption = "工具存放位置";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "NextTestTime";
            column.Caption = "下次试验时间";
            dtToolTest.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TestState";
            column.Caption = "试验状态";
            dtToolTest.Columns.Add(column);// 

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TestCycle";
            column.Caption = "试验周期";
            dtToolTest.Columns.Add(column);// TestCycle
        }

        private void TableShowInit()
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

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();//ID,ToolType,ToolName,ToolID,ToolTestTime,ToolTestResult,ToolTestPeople,ToolTestRemark
            Col1.FieldName = "StoragePlace";
            Col1.Caption = "工具存放位置";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();// 上次试验时间  试验结果  备注信息 录入人
            Col1.FieldName = "ToolTestTime";
            Col1.Caption = "上次试验时间";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolTestResult";
            Col1.Caption = "试验结果";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolTestPeople";
            Col1.Caption = "录入人";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolTestRemark";
            Col1.Caption = "备注信息";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "NextTestTime";
            Col1.Caption = "下次试验时间";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "TestState";
            Col1.Caption = "试验状态";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "TestCycle";
            Col1.Caption = "试验周期";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

        }

        private void AddTreeViewToolsType(string strParent, TreeNode parentNode)
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
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
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
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;

                    strSql = "select tvParent,tvChildId,ToolID,ToolName from tb_Tools where ToolName='" + str + "' and TestCycle <> '' ";
                    DataTable dtToos = datalogic.GetDataTable(strSql);
                    foreach (DataRow drTools in dtToos.Rows)
                    {
                        TreeNode tnToos = new TreeNode();
                        tnToos.Name = drTools["tvChildId"].ToString();
                        //tnToos.Tag = strParent;
                        tnToos.Text = drTools["ToolID"].ToString();
                        tnToos.ImageIndex = 2;
                        tnToos.SelectedImageIndex = 2;
                        node.Nodes.Add(tnToos);
                    }

                    parentNode.Nodes.Add(node);
                    AddTreeViewToolsType(datarow["tvChildId"].ToString(), node);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode==null )
                return ;
            if (!blFirst)
            {
                blFirst = true;
                return;
            }
            if (treeView1.SelectedNode.ImageIndex == 0 || treeView1.SelectedNode.ImageIndex == 1)
            {
                if (MessageUtil.ShowYesNoAndTips("是否选择该节点下所有子节点？") == DialogResult.Yes)
                {
                    if (treeView1.SelectedNode.ImageIndex == 0)//种类
                    {
                        string strChildId = treeView1.SelectedNode.Name.ToString();
                        string strSql = "select ToolName from tb_TypeAndName where tvParent='" + strChildId + "' ";
                        DataTable dtNames = datalogic.GetDataTable(strSql);
                        foreach (DataRow drNames in dtNames.Rows)
                        {
                            string strName = drNames["ToolName"].ToString();// ID,ToolType,ToolName,ToolID,ToolTestTime,ToolTestResult,ToolTestPeople,ToolTestRemark

                            ToolsNameShow(strName);
                        }
                    }
                    else if (treeView1.SelectedNode.ImageIndex == 1)//名称
                    {
                        string strName = treeView1.SelectedNode.Text.ToString();
                        ToolsNameShow(strName);
                    }
                }
            }
            else if (treeView1.SelectedNode.ImageIndex == 2)// ID,ToolType,ToolName,ToolID,ToolTestTime,ToolTestResult,ToolTestPeople,ToolTestRemark,NextTestTime,TestState
            {
                string strToolID = treeView1.SelectedNode.Text.ToString();

                bool blHas = false;
                foreach (DataRow dr in dtToolTest.Rows)
                {
                    if (dr["ToolID"].ToString() == strToolID)
                    {
                        blHas = true;
                        break;
                    }
                }
                if (!blHas)
                {
                    string strSql = "select ID,ToolType,ToolName,ToolID,StoragePlace,ToolTestTime,ToolTestResult,ToolTestPeople,ToolTestRemark,NextTestTime,TestState,TestCycle" +
                        " from tb_Tools where ToolID='" + strToolID + "' and TestCycle <> ''";
                    DataTable dt = datalogic.GetDataTable(strSql);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dtToolTest.NewRow();
                        row["ID"] = dt.Rows[0]["ID"].ToString();
                        row["ToolType"] = dt.Rows[0]["ToolType"].ToString();
                        row["ToolName"] = dt.Rows[0]["ToolName"].ToString();
                        row["ToolID"] = dt.Rows[0]["ToolID"].ToString();
                        row["ToolTestTime"] = dt.Rows[0]["ToolTestTime"].ToString();
                        row["ToolTestResult"] = dt.Rows[0]["ToolTestResult"].ToString();
                        row["ToolTestPeople"] = dt.Rows[0]["ToolTestPeople"].ToString();
                        row["ToolTestRemark"] = dt.Rows[0]["ToolTestRemark"].ToString();
                        row["StoragePlace"] = dt.Rows[0]["StoragePlace"].ToString();
                        row["NextTestTime"] = dt.Rows[0]["NextTestTime"].ToString();
                        row["TestState"] = dt.Rows[0]["TestState"].ToString();
                        row["TestCycle"] = dt.Rows[0]["TestCycle"].ToString();
                        dtToolTest.Rows.Add(row);
                    }
                }
            }
            this.gridControl1.DataSource = dtToolTest;
        }

        private void ToolsNameShow(string strName)
        {
            string strSql = "select ID,ToolType,ToolName,ToolID,StoragePlace,ToolTestTime,ToolTestResult,ToolTestPeople,ToolTestRemark,NextTestTime,TestState,TestCycle" +
                " from tb_Tools where ToolName='" + strName + "' and IsArea='" + ToolAreaType.工具.ToString() + "' and TestCycle <> ''";
            DataTable dtTools = datalogic.GetDataTable(strSql);
            foreach (DataRow drTools in dtTools.Rows)
            {
                string strID = drTools["ID"].ToString();
                bool blHas = false;
                foreach (DataRow drShow in dtToolTest.Rows)
                {
                    if (drShow["ID"].ToString() == strID)
                    {
                        blHas = true;
                        break;
                    }
                }
                if (!blHas)
                {
                    DataRow row = dtToolTest.NewRow();
                    row["ID"] = drTools["ID"].ToString();
                    row["ToolType"] = drTools["ToolType"].ToString();
                    row["ToolName"] = drTools["ToolName"].ToString();
                    row["ToolID"] = drTools["ToolID"].ToString();
                    row["ToolTestTime"] = drTools["ToolTestTime"].ToString();
                    row["ToolTestResult"] = drTools["ToolTestResult"].ToString();
                    row["ToolTestPeople"] = drTools["ToolTestPeople"].ToString();
                    row["ToolTestRemark"] = drTools["ToolTestRemark"].ToString();
                    row["StoragePlace"] = drTools["StoragePlace"].ToString();
                    row["NextTestTime"] = drTools["NextTestTime"].ToString();
                    row["TestState"] = drTools["TestState"].ToString();
                    row["TestCycle"] = drTools["TestCycle"].ToString();
                    dtToolTest.Rows.Add(row);
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

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows(); //选中多行   
            if (rows.Length > 0)
            {
                if (MessageUtil.ShowYesNoAndTips("确定删除表中所选工具录入试验记录？") == DialogResult.Yes)
                {

                    int[] iRowSele = this.gridView1.GetSelectedRows();
                    List<string> list = new List<string>();
                    foreach (int iRow in iRowSele)
                    {
                        string str = dtToolTest.Rows[iRow]["ID"].ToString();
                        list.Add(str);
                        //string str = gridView1.GetRowCellDisplayText(rows[iRow], "ID").ToString();
                        //list.Add(str);
                    }
                    foreach (string str in list)
                    {
                        foreach (DataRow dr in dtToolTest.Rows)
                        {
                            if (dr["ID"].ToString() == str)
                            {
                                dtToolTest.Rows.Remove(dr);
                                break;
                            }
                        }
                    }
                    this.gridControl1.DataSource = dtToolTest;
                }
            }
            else
            {
                MessageUtil.ShowTips("无选中的工具");
            }
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            if (dtToolTest.Rows.Count > 0)
            {
                string strTime = dtpTest.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string strResult = cbbTestResult.Text;
                string strPeople = cbbPeopleIn.Text;
                string strRemark = tbRemark.Text;
                if (strRemark.Length > 50)
                    strRemark.Substring(0, 50);

                foreach (DataRow dr in dtToolTest.Rows)
                {
                    string strID = dr["ID"].ToString();
                    string strToolID = dr["ToolID"].ToString();
                    string strType = dr["ToolType"].ToString();
                    string strName = dr["ToolName"].ToString();
              
                    if (strResult == "不合格")
                    {
                        string strPlace = dr["StoragePlace"].ToString();
                        string strSql = "delete from tb_Tools where ID='" + strID + "' ";
                        datalogic.SqlComNonQuery(strSql);
                        strSql = "insert into tb_RecordScrap (ToolType,ToolName,ToolID,StoragePlace,ScrapInfo,ScrapPeople,ScrapTime)" +
                       "values ('" + strType + "','" + strName + "','" + strToolID + "','" + strPlace + "'," +
                       "'试验不合格','" + frmMain .strUserName + "','" + strTime + "')";
                        datalogic.SqlComNonQuery(strSql);
                    }
                    else
                    {
                        string strCycle = dr["TestCycle"].ToString();
                        string strNextTime = "";
                         if (!string.IsNullOrEmpty(strCycle))
                         {
                             int iCycle = Convert.ToInt32(strCycle);
                             DateTime detimeNext = dtpTest.Value.AddMonths(iCycle);
                             strNextTime = detimeNext.ToString("yyyy-MM-dd");
                         }

                         // ID,ToolType,ToolName,ToolID,ToolTestTime,ToolTestResult,ToolTestPeople,ToolTestRemark StoragePlace NextTestTime,TestState
                        string strSql = "update tb_Tools set ToolTestTime='" + strTime + "',ToolTestResult='" + strResult + "',ToolTestPeople='" + strPeople + "',"+
                            "ToolTestRemark='" + strRemark + "',NextTestTime='" + strNextTime + "',TestState='在试验周期内' where ID='" + strID + "' ";
                        datalogic.SqlComNonQuery(strSql);

                        strSql = "insert into tb_RecordTest (ToolType,ToolName,ToolID,ToolTestTime,ToolTestResult,ToolTestPeople,ToolTestRemark) values " +
                            "('" + strType + "','" + strName + "','" + strToolID + "','" + strTime + "','" + strResult + "','" + strPeople + "','" + strRemark + "')";
                        datalogic.SqlComNonQuery(strSql);
                    }
                }
                MessageUtil.ShowTips("以上工具试验录入成功！");
                dtToolTest.Rows.Clear();
                this.gridControl1.DataSource = dtToolTest;
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
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

    }
}
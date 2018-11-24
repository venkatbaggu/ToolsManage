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
    public partial class frmToolName : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        DataTable dataTable = new DataTable();
        DataOperate operatedata = new DataOperate();

        private TagType EditType = TagType.初值;
        private string strAlterChildId = "";
        private string strNoAlterType = "";//种类 没有修改时的值
        private string strNoAltName = "";//名称 没有修改时的值
        private string strNoCycle = "";//周期 没有修改时的值

        public frmToolName()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }

        private void frmToolName_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();

            TableInit();
            ShowDataTable();
        }

        #region 子程序

        private void ShowNew()
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            treeView1.Enabled = true;
            EditType = TagType.初值;
            treeView1.Nodes.Clear();
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
            ShowDataTable();

            sbtnExpand.Enabled = true ;
            sbtnAddType.Enabled = true;
            sbtnAddName.Enabled = true;
            sbtnAlter.Enabled = true;
            sbtnDelete.Enabled = true;
        }

        private void AddTreeView(string strParent, TreeNode parentNode)
        {
            string strSql = "select tvParent,tvChildId,ToolType,ToolName from tb_TypeAndName where tvParent='" + strParent + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);
            if (dt != null && dt.Rows.Count > 0)
            {
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
                        AddTreeView(datarow["tvChildId"].ToString(), node);
                    }
                    //处理子节点
                    else
                    {
                        node.Name = datarow["tvChildId"].ToString();
                        node.Tag = strParent;
                        node.Text = datarow["ToolName"].ToString();
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                        parentNode.Nodes.Add(node);
                        AddTreeView(datarow["tvChildId"].ToString(), node);
                    }
                }
            }
        }

        private void SetBtnFalse()
        {
            sbtnExpand.Enabled = false;
            sbtnAddType.Enabled = false;
            sbtnAddName.Enabled = false;
            sbtnAlter.Enabled = false;
            sbtnDelete.Enabled = false;
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
                    string strsql = "delete from tb_TypeAndName where tvChildId='" + str + "'";
                    datalogic.SqlComNonQuery(strsql);
                    str = tn.Text.ToString();
                    strsql = "delete from tb_Tools where ToolName='" + str + "'";
                    datalogic.SqlComNonQuery(strsql);
                    DeleteChildNode(tn);
                }
            }
            datalogic.SqlComNonQuery("delete from tb_TypeAndName where tvChildId='" + currNode.Name.ToString() + "'");
        }

        private void ShowDataTable()
        {
            dataTable.Rows.Clear();

            string strSql = "select ToolName,ToolsNameCoding,ToolsCycle from tb_TypeAndName where tvParent<>'0' ";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                DataRow row = dataTable.NewRow();
                row["ToolName"] = datarow["ToolName"].ToString();
                row["ToolsNameCoding"] = datarow["ToolsNameCoding"].ToString();
                row["ToolsCycle"] = datarow["ToolsCycle"].ToString();
                dataTable.Rows.Add(row);
            }
            this.gridControl1.DataSource = dataTable;
        }

        private void TableInit()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolName";
            column.Caption = "工具名称";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolsNameCoding";
            column.Caption = "工具名称编码";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolsCycle";
            column.Caption = "工具试验周期";
            dataTable.Columns.Add(column);
        }

        #endregion

        private void sbtnExpand_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
            {
                if (treeView1.Nodes[0].IsExpanded == true)
                    treeView1.CollapseAll();
                else
                    treeView1.ExpandAll();
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
                treeView1.Enabled = true;
                EditType = TagType.初值;
                ShowDataTable();
            }
        }

        private void sbtnAddType_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
            EditType = TagType.添加;
            gcType.Text = "添加工具种类";
            treeView1.Enabled = false;
            SetBtnFalse();
        }

        private void sbtnTypeOk_Click(object sender, EventArgs e)
        {
            #region 输入 判断
            string strType = tbType.Text.Trim();
            if (string.IsNullOrEmpty(strType))
            {
                MessageUtil.ShowTips("工具种类不可为空");
                return;
            }
            if (strType.Length > 50)
            {
                strType = strType.Substring(0, 50);
            }

            string strSame = "";
            if (EditType == TagType.添加)
            {
                strSame = "Select ID From tb_TypeAndName where ToolType='" + strType + "' ";
            }
            else if (EditType == TagType.修改)
            {
                strSame = "Select ID From tb_TypeAndName where ToolType='" + strType + "' and tvChildId<>'" + strAlterChildId + "' ";
            }
            if (!string.IsNullOrEmpty(strSame))
            {
                if (operatedata.blCheckHas(strSame))
                {
                    MessageUtil.ShowTips("工具种类重复，请重新输入");
                    return;
                }
            }

            #endregion

            string strParent = "";
            string strChild = "";
            if (EditType == TagType.添加)
            {
                TreeNode node = new TreeNode();
                //获取ChildId
                strChild = operatedata.GetChildId("tb_TypeAndName", "tvChildId"); 
                node.Name = strChild;// node.Name 为本节点的编号 
                strParent = "0";
                node.Tag = strParent;
                node.Text = strType;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                treeView1.Nodes.Add(node);
                string strSql = "insert into tb_TypeAndName (tvParent,tvChildId,ToolType) values ('" + strParent + "'," +
                 "'" + strChild + "','" + strType + "')";
                datalogic.SqlComNonQuery(strSql);
                treeView1.ExpandAll();
            }
            else if (EditType == TagType.修改)
            {
                treeView1.SelectedNode.Text = strType;
                string strSql = "update tb_TypeAndName set ToolType='" + strType + "' where tvChildId='" + strAlterChildId + "' ";
                datalogic.SqlComNonQuery(strSql);
                strSql = "update tb_Tools set ToolType='" + strType + "' where ToolType='" + strNoAlterType + "' ";
                datalogic.SqlComNonQuery(strSql);
                ShowNew();
            }
        }

        private void sbtnNameOk_Click(object sender, EventArgs e)
        {
            #region 输入判断
            string strCycle = tbCycle.Text.Trim();
            string strName = tbName.Text.Trim();
            string strCoding = tbNameCoding.Text.Trim();
            if (!string.IsNullOrEmpty(strCycle))
            {
                try
                {
                    int i = Convert.ToInt32(strCycle);
                }
                catch
                {
                    MessageUtil.ShowTips("工具试验周期格式错误，请重新输入");
                    return;
                }
            }
            if (string.IsNullOrEmpty(strName))
            {
                MessageUtil.ShowTips("工具名称不能为空");
                return;
            }
            if (string.IsNullOrEmpty(strCoding))
            {
                MessageUtil.ShowTips("工具名称编码不能为空");
                return;
            }


            string strSame = "";
            if (EditType == TagType.添加)
            {
                strSame = "Select ID From tb_TypeAndName where ToolName='" + strName + "' ";
            }
            else if (EditType == TagType.修改)
            {
                strSame = "Select ID From tb_TypeAndName where ToolName='" + strName + "' and tvParent<>'0' and tvChildId<>'" + strAlterChildId + "' ";
            }
            if (!string.IsNullOrEmpty(strSame))
            {
                if (operatedata.blCheckHas(strSame))
                {
                    MessageUtil.ShowTips("工具名称重复，请重新输入");
                    return;
                }
            }

            strSame = "";
            if (EditType == TagType.添加)
            {
                strSame = "Select ID From tb_TypeAndName where ToolsNameCoding='" + strCoding + "' ";
            }
            else if (EditType == TagType.修改)
            {
                strSame = "Select ID From tb_TypeAndName where ToolsNameCoding='" + strCoding + "' and tvParent<>'0' and tvChildId<>'" + strAlterChildId + "' ";
            }
            if (!string.IsNullOrEmpty(strSame))
            {
                if (operatedata.blCheckHas(strSame))
                {
                    MessageUtil.ShowTips("工具名称编码重复，请重新输入");
                    return;
                }
            }

            if (strName.Length > 50)
            {
                strName = strName.Substring(0, 50);
            }
            if (strCoding.Length > 50)
            {
                strCoding = strCoding.Substring(0, 50);
            }
            #endregion

            string strParent = "";
            string strChild = "";
            if (EditType == TagType.添加)
            {
                TreeNode node = new TreeNode();
                //获取ChildId
                strChild = operatedata.GetChildId("tb_TypeAndName", "tvChildId"); 
                node.Name = strChild;// node.Name 为本节点的编号 
                strParent = treeView1.SelectedNode.Name.ToString();
                node.Tag = strParent;
                node.Text = strName;
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                treeView1.SelectedNode.Nodes.Add(node);
                string strSql = "insert into tb_TypeAndName (tvParent,tvChildId,ToolName,ToolsNameCoding,ToolsCycle) values ('" + strParent + "'," +
                   "'" + strChild + "','" + strName + "','" + strCoding + "','" + strCycle + "')";
                datalogic.SqlComNonQuery(strSql);
                treeView1.ExpandAll();
                strSql = "select top 1 ToolsNameCoding from tb_TypeAndName where tvParent<>'0' order by id desc ";// select top 1 * from table order by id desc
               object ob = datalogic.SqlComScalar(strSql);
                int i = 0;
                if (ob != null)
                {
                    i = Convert.ToInt32(ob.ToString());
                }
                i++;
                string str = i.ToString();
                int iLength = 4 - str.Length;
                str = clsCommon.FormatString(iLength) + str;
                tbNameCoding.Text = str;
            }
            else  if (EditType ==TagType .修改 )
            {
                treeView1.SelectedNode.Text = strName;
                string strSql = "update tb_TypeAndName set ToolName='" + strName + "', ToolsNameCoding='" + strCoding + "', ToolsCycle='" + strCycle + "'" +
                           " where tvChildId='" + strAlterChildId + "' ";
                datalogic.SqlComNonQuery(strSql);

                strSql = "update tb_Tools set ToolName='" + strName + "',TestCycle='" + strNoCycle + "' where ToolName='" + strNoAltName + "' ";
                datalogic.SqlComNonQuery(strSql);
                ShowNew();
            }
        }

        private void sbtnNew_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void sbtnAlter_Click(object sender, EventArgs e)
        {
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
                if (treeView1.SelectedNode.ImageIndex == 1)
                {
                    gcName.Text = "修改工具名称";
                    xtraTabControl1.SelectedTabPage = xtraTabPage3;
                    tbNameType.Text = treeView1.SelectedNode.Parent.Text;
                    string strSql = "select ToolName,ToolsNameCoding,ToolsCycle from tb_TypeAndName where tvChildId='" + strAlterChildId + "' ";
                    DataTable dt = datalogic.GetDataTable(strSql);
                    if (dt != null)
                    {
                        //strNoAltName = dt.Rows[0]["ToolName"].ToString();
                        //strNoAltCode = dt.Rows[0]["ToolsNameCoding"].ToString();
                        //strNoCycle =dt.Rows[0]["ToolsCycle"].ToString();
                        //tbName.Text = strNoAltName;
                        //tbNameCoding.Text = strNoAltCode;
                        //tbCycle.Text = strNoCycle;

                        strNoAltName = dt.Rows[0]["ToolName"].ToString();
                        //strNoAltCode = dt.Rows[0]["ToolsNameCoding"].ToString();
                        strNoCycle = dt.Rows[0]["ToolsCycle"].ToString();
                        tbName.Text = dt.Rows[0]["ToolName"].ToString();
                        tbNameCoding.Text = dt.Rows[0]["ToolsNameCoding"].ToString();
                        tbCycle.Text = dt.Rows[0]["ToolsCycle"].ToString();
                    }
                }
                else
                {
                    gcType.Text = "修改工具种类";
                    xtraTabControl1.SelectedTabPage = xtraTabPage2;
                    strNoAlterType = treeView1.SelectedNode.Text.ToString();
                    tbType.Text = strNoAlterType;
                }
            }
            SetBtnFalse();
        }

        private void sbtnAddName_Click(object sender, EventArgs e)
        {
            treeView1.Enabled = false;
            if (treeView1.Nodes.Count == 0)
            {
                MessageUtil.ShowTips("请先添加工具种类");
                treeView1.Enabled = true ;
                return;
            }
            else if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.ImageIndex == 0)
                {
                    xtraTabControl1.SelectedTabPage = xtraTabPage3;
                    tbNameType.Text = treeView1.SelectedNode.Text;
                    EditType = TagType.添加;

                    gcName.Text = "添加工具名称";
                    //tbName.Text = "";
                    //tbNameCoding.Text = "";
                    //tbName.Enabled = true;
                    //tbNameCoding.Enabled = true;
                    //tbCycle.Enabled = true;
                    //sbtnNameOk.Enabled = true;

                    string strSql = "select top 1 ToolsNameCoding from tb_TypeAndName where tvParent<>'0' order by id desc ";// select top 1 * from table order by id desc
                    object ob = datalogic.SqlComScalar(strSql);
                    int i = 0;
                    if (ob != null)
                    {
                        i = Convert.ToInt32(ob.ToString());
                    }
                    i++;
                    string str = i.ToString();
                    int iLength = 4 - str.Length;
                    str = clsCommon.FormatString(iLength) + str;
                    tbNameCoding.Text = str;
                }
                else if (treeView1.SelectedNode.ImageIndex == 1)
                {
                    MessageUtil.ShowTips("不可在工具名称下添加工具名称");
                    treeView1.Enabled = true;
                    return;
                }
            }
            else
            {
                MessageUtil.ShowTips("请选择在工具种类下添加工具名称");
                treeView1.Enabled = true;
                return;
            }
            SetBtnFalse();
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            sbtnDelete.Enabled = false;
            if (treeView1.SelectedNode == null)
            {
                MessageUtil.ShowTips("请选择需要删除的内容");
                sbtnDelete.Enabled = true;
                return;
            }
            else
            {
                if (treeView1.SelectedNode.GetNodeCount(true) > 0)
                {
                    if (MessageUtil.ShowYesNoAndTips("将删除该节点及所属该节点下的所有工具，删除后不可恢复，请谨慎操作，确定删除？") == DialogResult.Yes)
                    {
                        frmExitUser.userPower = UserPower.系统用户;
                        frmExitUser frm = new frmExitUser();
                        frm.ShowDialog(this);
                        frm.Dispose();
                        if (frmExitUser.blOk)
                        {
                            DeleteChildNode(treeView1.SelectedNode);
                            treeView1.SelectedNode.Remove();
                        }
                        else
                        {
                            sbtnDelete.Enabled = true;
                            return;
                        }
                    }
                    else
                    {
                        sbtnDelete.Enabled = true;
                        return;
                    }
                }
                else
                {
                    string str = treeView1.SelectedNode.Text.ToString();
                    string strSql = "select ID from tb_Tools where ToolName='" + str + "' ";
                    DataTable dt = datalogic.GetDataTable(strSql);
                    if (dt.Rows.Count > 0)
                    {
                        if (MessageUtil.ShowYesNoAndTips("将删除该节点及所属该节点下的所有工具，删除后不可恢复，请谨慎操作，确定删除？") == DialogResult.Yes)
                        {
                            frmExitUser.userPower = UserPower.系统用户;
                            frmExitUser frm = new frmExitUser();
                            frm.ShowDialog(this);
                            frm.Dispose();
                            if (frmExitUser.blOk)
                            {
                                str = treeView1.SelectedNode.Name.ToString();
                                string strsql = "delete from tb_TypeAndName where tvChildId='" + str + "'";
                                datalogic.SqlComNonQuery(strsql);
                                str = treeView1.SelectedNode.Text.ToString();
                                treeView1.SelectedNode.Remove();
                                strsql = "delete from tb_Tools where ToolName='" + str + "'";
                                datalogic.SqlComNonQuery(strsql);
                            }
                            else
                            {
                                sbtnDelete.Enabled = true;
                                return;
                            }
                        }
                        else
                        {
                            sbtnDelete.Enabled = true;
                            return;
                        }
                    }
                    else
                    {
                        if (MessageUtil.ShowYesNoAndTips("确定删除该选中节点？") == DialogResult.Yes)
                        {
                            str = treeView1.SelectedNode.Name.ToString();
                            string strsql = "delete from tb_TypeAndName where tvChildId='" + str + "'";
                            datalogic.SqlComNonQuery(strsql);
                            str = treeView1.SelectedNode.Text.ToString();
                            treeView1.SelectedNode.Remove();
                        }
                        else
                        {
                            sbtnDelete.Enabled = true;
                            return;
                        }
                    }

                   
                }
                ShowNew();
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtnTypeExit_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void sbtnNameExit_Click(object sender, EventArgs e)
        {
            ShowNew();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (EditType == TagType.初值)
                {
                    if (treeView1.SelectedNode.ImageIndex == 0)
                    {
                        string str = treeView1.SelectedNode.Name.ToString();
                        dataTable.Rows.Clear();

                        string strSql = "select ToolName,ToolName,ToolsNameCoding,ToolsCycle from tb_TypeAndName where tvParent='" + str + "' ";
                        DataTable dt = datalogic.GetDataTable(strSql);
                        foreach (DataRow datarow in dt.Rows)
                        {
                            DataRow row = dataTable.NewRow();
                            row["ToolName"] = datarow["ToolName"].ToString();
                            row["ToolsNameCoding"] = datarow["ToolsNameCoding"].ToString();
                            row["ToolsCycle"] = datarow["ToolsCycle"].ToString();
                            dataTable.Rows.Add(row);
                        }
                        this.gridControl1.DataSource = dataTable;
                    }
                    xtraTabControl1.SelectedTabPage = xtraTabPage1;
                }
            }

            #region

            //#region

            //if (EditType == TagType.修改)
            //{
            //    strAlterChildId = treeView1.SelectedNode.Name.ToString();
            //    if (treeView1.SelectedNode.ImageIndex == 1)
            //    {
            //        gcName.Text = "修改工具名称";
            //        xtraTabControl1.SelectedTabPage = xtraTabPage3;
            //        tbNameType.Text = treeView1.SelectedNode.Parent.Text;
            //        tbName.Enabled = true;
            //        tbNameCoding.Enabled = true;
            //        tbCycle.Enabled = true;
            //        sbtnNameOk.Enabled = true;

            //        string strSql = "select ToolName,ToolsNameCoding,ToolsCycle from tb_TypeAndName where tvChildId='" + strAlterChildId + "' ";
            //        DataTable dt = datalogic.GetDataTable(strSql);
            //        if (dt.Rows.Count > 0)
            //        {
            //            strNoAltName = dt.Rows[0]["ToolName"].ToString();
            //            tbNameCoding.Text = dt.Rows[0]["ToolsNameCoding"].ToString();
            //            strNoCycle = dt.Rows[0]["ToolsCycle"].ToString();
            //            tbName.Text = strNoAltName;
            //            //tbNameCoding.Text = strNoAltCode;
            //            tbCycle.Text = strNoCycle;
            //        }
            //    }
            //    else
            //    {
            //        gcType.Text = "修改工具种类";
            //        xtraTabControl1.SelectedTabPage = xtraTabPage2;
            //        tbType.Enabled = true;
            //        sbtnTypeOk.Enabled = true;
            //        strNoAlterType = treeView1.SelectedNode.Text.ToString();
            //        tbType.Text = strNoAlterType;
            //    }
            //}
            //else if (EditType == TagType.初值)
            //{
            //    if (treeView1.SelectedNode.ImageIndex == 0)
            //    {
            //        string str = treeView1.SelectedNode.Name.ToString();
            //        dataTable.Rows.Clear();

            //        string strSql = "select ToolName,ToolName,ToolsNameCoding,ToolsCycle from tb_TypeAndName where tvParent='" + str + "' ";
            //        DataTable dt = datalogic.GetDataTable(strSql);
            //        foreach (DataRow datarow in dt.Rows)
            //        {
            //            DataRow row = dataTable.NewRow();
            //            row["ToolName"] = datarow["ToolName"].ToString();
            //            row["ToolsNameCoding"] = datarow["ToolsNameCoding"].ToString();
            //            row["ToolsCycle"] = datarow["ToolsCycle"].ToString();
            //            dataTable.Rows.Add(row);
            //        }
            //        this.gridControl1.DataSource = dataTable;
            //    }
            //    xtraTabControl1.SelectedTabPage = xtraTabPage1;
            //}
            //else if (EditType == TagType.添加)
            //{
            //    if (treeView1.SelectedNode.ImageIndex == 0)
            //    {
            //        tbNameType.Text = treeView1.SelectedNode.Text;
            //    }
            //}

            //#endregion

            #endregion

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
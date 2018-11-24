using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

//using NSB4000.ClassCommon;

namespace NSB4000
{
    public partial class frmSystemSet : DevExpress.XtraEditors.XtraForm
    {
        //DataLogic datalogic = new DataLogic();
        DataTable dt;
        

        public frmSystemSet()
        {
            InitializeComponent();
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TableInit()
        {
            dt = new DataTable("dtArea");
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Describe";
            //column.Caption = "描述";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Content";
            dt.Columns.Add(column);
        }

        private void sbtnAddArea_Click(object sender, EventArgs e)
        {
            //TableInit();
            //DataRow row;
            //int iAreaNo = treeView1.Nodes.Count+1;
            //string strAreaName = iAreaNo.ToString() + "#区域";

            //row = dt.NewRow();
            //row["Describe"] = "区域名";
            //row["Content"] = strAreaName;
            //dt.Rows.Add(row);

            //this.gridControl1.DataSource = dt;

            //TreeNode node = new TreeNode();
            //node.Tag = iAreaNo;
            //node.Text = strAreaName;
            //node.ImageIndex = 0;
            //treeView1.Nodes.Add(node);

            //treeView1.SelectedNode = node;

            //string strSql = strSql = "insert into tb_Probe (IsArea,TreeParent,AreaName) values ('" + "Area" + "','" + iAreaNo.ToString() + "','" + strAreaName + "')";
            //datalogic.SqlComNonQuery(strSql);
        }

        private void sbtnAddSf6_Click(object sender, EventArgs e)
        {
            //if (this.treeView1.SelectedNode!=null)
            //{
            //    if (this.treeView1.SelectedNode.ImageIndex == 0)
            //    {
            //        string strAreaId = treeView1.SelectedNode.Tag.ToString();
            //        //float fAlarm = 1.4f;
            //        string strAlarm = "1.4";
            //        DataRow row;

            //        TableInit();

            //        int iProbeNo = 1;
            //        for (int i = 0; i < treeView1.Nodes.Count; i++)
            //        {
            //            iProbeNo += treeView1.Nodes[i].Nodes.Count;
            //        }

            //        string strProbeName = iProbeNo.ToString();

            //        row = dt.NewRow();
            //        row["Describe"] = "变送器名称";
            //        row["Content"] = strProbeName;
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "变送器地址";
            //        row["Content"] = strProbeName;
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "所属区域";
            //        row["Content"] = this.treeView1.SelectedNode.Text;
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "是否启用";
            //        row["Content"] = "True";
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "报警阀值（V）";
            //        row["Content"] = strAlarm;
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "通信测试";
            //        row["Content"] = "";
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "SF6含量";
            //        row["Content"] = "";
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "氧气含量";
            //        row["Content"] = "";
            //        dt.Rows.Add(row);

            //        this.gridControl1.DataSource = dt;

            //        TreeNode node = new TreeNode();
            //        node.Tag = strProbeName;
            //        node.Text = strProbeName;
            //        node.ImageIndex = 1;

            //        this.treeView1.SelectedNode.Nodes.Add(node);
            //        this.treeView1.ExpandAll();

            //        string strSql = strSql = "insert into tb_Probe (AreaId,ProbeName,ProbeAddr,ProbeType,IsUse,AlarmValue) values "+
            //            "('" + strAreaId + "','" + strProbeName + "','" + strProbeName + "','" + "SF6" + "','" + "True" + "','" + strAlarm + "')";
            //        datalogic.SqlComNonQuery(strSql);

            //    }
            //    else
            //    {
            //        MessageUtil.ShowTips("请选择添加变送器的区域");
            //    }
            //}
            //else
            //{
            //    MessageUtil.ShowTips("请选择添加变送器的区域");
            //}
        }

        private void frmSystemSet_Load(object sender, EventArgs e)
        {
            //string strSql = "select * from tb_Area";
            //DataTable dt = datalogic.GetDataTable(strSql);

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    TreeNode node = new TreeNode();
            //    node.Tag = dt.Rows[i]["ID"].ToString();
            //    node.Text = dt.Rows[i]["AreaName"].ToString(); ;
            //    node.ImageIndex = 0;
            //    treeView1.Nodes.Add(node);
            //}

            //strSql = "select * from tb_Probe";
            //dt = datalogic.GetDataTable(strSql);
            //for (int i = 0; i < treeView1.Nodes.Count; i++)
            //{
            //    for (int j = 0; j < dt.Rows.Count; j++)
            //    {
            //        if (dt.Rows[j]["AreaId"].ToString() == treeView1.Nodes[i].Tag .ToString())
            //        {
            //            TreeNode node = new TreeNode();
            //            node.Tag = dt.Rows[j]["ID"].ToString();
            //            node.Text = dt.Rows[j]["ProbeName"].ToString();
            //            node.ImageIndex = 1;

            //            this.treeView1.Nodes[i].Nodes.Add(node);
            //        }
            //    }
            //}
            //this.treeView1.ExpandAll();
        }

        private void sbtnExpand_Click(object sender, EventArgs e)
        {
            bool blExpand = false;
            for (int i = 0; i < treeView1.Nodes.Count;i++ )
            {
                if (treeView1.Nodes[i].IsExpanded)
                    blExpand = true;
            }
            if (blExpand)
            {
                this.treeView1.CollapseAll();
            }
            else
            {
                this.treeView1.ExpandAll();
            }
        }

        private void sbtnAddWsd_Click(object sender, EventArgs e)
        {
            //if (this.treeView1.SelectedNode != null)
            //{
            //    if (this.treeView1.SelectedNode.ImageIndex == 0)
            //    {
            //        string strAreaId = treeView1.SelectedNode.Tag.ToString();
            //        DataRow row;

            //        TableInit();

            //        int iProbeNo = 1;
            //        for (int i = 0; i < treeView1.Nodes.Count; i++)
            //        {
            //            iProbeNo += treeView1.Nodes[i].Nodes.Count;
            //        }

            //        string strProbeName = iProbeNo.ToString();

            //        row = dt.NewRow();
            //        row["Describe"] = "变送器名称";
            //        row["Content"] = strProbeName;
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "变送器地址";
            //        row["Content"] = strProbeName;
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "所属区域";
            //        row["Content"] = this.treeView1.SelectedNode.Text;
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "是否启用";
            //        row["Content"] = "True";
            //        dt.Rows.Add(row);

            //        row = dt.NewRow();
            //        row["Describe"] = "通信测试";
            //        row["Content"] = "";
            //        dt.Rows.Add(row);

            //        //row = dt.NewRow();
            //        //row["Describe"] = "温度（℃）";
            //        //row["Content"] = "";
            //        //dt.Rows.Add(row);

            //        //row = dt.NewRow();
            //        //row["Describe"] = "湿度（%RH）";
            //        //row["Content"] = "";
            //        //dt.Rows.Add(row);

            //        this.gridControl1.DataSource = dt;

            //        TreeNode node = new TreeNode();
            //        node.Tag = strProbeName;
            //        node.Text = strProbeName;
            //        node.ImageIndex = 2;

            //        this.treeView1.SelectedNode.Nodes.Add(node);
            //        this.treeView1.ExpandAll();

            //        string strSql = strSql = "insert into tb_Probe (AreaId,ProbeName,ProbeAddr,ProbeType,IsUse) values " +
            //            "('" + strAreaId + "','" + strProbeName + "','" + strProbeName + "','" + "SF6" + "','" + "True" + "')";
            //        datalogic.SqlComNonQuery(strSql);

            //    }
            //    else
            //    {
            //        MessageUtil.ShowTips("请选择添加变送器的区域");
            //    }
            //}
            //else
            //{
            //    MessageUtil.ShowTips("请选择添加变送器的区域");
            //}
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            //if (treeView1.SelectedNode != null)
            //{
            //    string strSelectId = treeView1.SelectedNode.Tag.ToString();
            //    if (treeView1.SelectedImageIndex == 1)
            //    {
            //        if (treeView1.SelectedNode.Nodes.Count > 0)
            //        {
            //            DialogResult dr=  MessageUtil.ShowYesNoAndTips("确认删除该区域，及该区域下的变送器？");
            //            if (dr == DialogResult.OK)
            //            {
            //            //    string strsql = "delete from tb_Area where ID='" +  + "'";// (ID,AreaName
            //            //datalogic.SqlComNonQuery(strsql);
            //            }
            //        }
            //        else
            //        { 
                    
            //        }
            //    }
            //    else
            //    { 
                
            //    }
            //}
        }




    }
}
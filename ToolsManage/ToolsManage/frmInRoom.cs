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

using ReaderB;//RFID

namespace ToolsManage.ToolsManage
{
    public partial class frmInRoom : DevExpress.XtraEditors.XtraForm
    {
        private DataOperate operatedata = new DataOperate();
        private DataLogic datalogic = new DataLogic();

        private string strTreeSeleParent = "";
        public string sId = "";
        public string sToolType = "";
        public string sToolName = "";
        public string sToolId = "";
        public string sPlace = "";
        public string sRFID = "";

        #region RFID

        private int Port = 5;   //串口号
        private byte ComAdr = 0xff; //当前操作的ComAdr
        private byte Baud = 5; //波特率 5=57600
        private int frmHandle = 0;//返回与读写器连接端口对应的句柄
        private int openresult;  // 连接RFID返回值
        private int errorcode;//写EPC时 的返回状态数据

        #endregion


        public frmInRoom()
        {
            InitializeComponent();
        }

        private void frmInRoom_Load(object sender, EventArgs e)
        {
            #region  连接RFID 读写器

            try
            {
                StaticClassReaderB.CloseComPort();//先关闭RFID读写器
                openresult = StaticClassReaderB.OpenComPort(Port, ref ComAdr, Baud, ref frmHandle);
                if (openresult != 0)
                {
                    MessageUtil.ShowError("RFID读写器连接错误，请检查RFID读写器");
                }
            }
            catch
            {
                MessageUtil.ShowError("RFID读写器连接错误，请检查RFID读写器");
            }

            #endregion

            if (frmMain.blDebug)
            {
                tbRFID.Enabled = true;
                sbtnRfidCopy.Visible = true;
            }
            else
            {
                tbRFID.Enabled = false;
                sbtnRfidCopy.Visible = false;
            }

            if (this.Tag != null)
            {
                if (this.Tag.ToString() == TagType.修改.ToString())
                {
                    this.Text = "修改工具信息";
                    cbbToolType.Text = sToolType;
                    cbbToolName.Text = sToolName;
                    tbToolID.Text = sToolId;
                    tbPlace.Text = sPlace;
                    tbRFID.Text = sRFID;

                    cbbToolType.Enabled = false;
                    cbbToolName.Enabled = false;
                }
                else if (this.Tag.ToString() == TagType.添加.ToString())
                {
                    this.Text = "工具入库管理";

                    #region  绑定 工具种类

                    //string strSql = "select ToolName from tb_TypeAndName ";
                    //DataTable dt = datalogic.GetDataTable(strSql);
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    cbbToolName.Items.Add(dr["ToolName"].ToString());
                    //}
                    //cbbToolName.SelectedIndex = 0;

                    string strSql = "select ToolType from tb_TypeAndName where tvParent='0'";
                    DataTable dt = datalogic.GetDataTable(strSql);
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbbToolType.Items.Add(dr["ToolType"].ToString());
                    }
                    if (cbbToolType.Items.Count > 0)
                        cbbToolType.SelectedIndex = 0;

                    #endregion
                }
            }

            cbbOverPeople.Text = frmMain.strUserName;
            AddTreeView("0", (TreeNode)null);
            treeView1.ExpandAll();
        }

        #region  子程序

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

        #endregion

        private void cbbToolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Tag.ToString() == TagType.添加.ToString())
            {
                #region  自动生成工具ID

                string strName = cbbToolName.Text;
                string strSql = "select top 1 ToolID from tb_Tools where IsArea='" + ToolAreaType.工具.ToString() + "' "+
                    "and ToolName='" + strName + "' order by id desc ";// select top 1 * from table order by id desc
                object ob = datalogic.SqlComScalar(strSql);

                int iNo = 0;
                if (ob != null)
                {
                    int iLength = strName.Length;
                    string strToolId = ob.ToString();
                    if (strToolId.Length >= iLength)
                    {
                        string strNameOfId = strToolId.Substring(0, iLength);//strToolId中的工具名称 绝缘手套007 绝缘手套
                        if (strName == strNameOfId)
                        {
                            if ((strToolId.Length - iLength) == 3)//数字部分的长度
                            {
                                string strNo = strToolId.Substring(iLength, 3);
                                try
                                {
                                    iNo = Convert.ToInt32(strNo);
                                }
                                catch (Exception ex)
                                {
                                    //iNo = 0;
                                    if (frmMain.blDebug)
                                        MessageUtil.ShowTips(ex.Message);
                                }
                            }
                        }
                    }
                }

                iNo++;
                string striNo = iNo.ToString();
                int iLen = 3 - striNo.Length;
                striNo = clsCommon.FormatString(iLen) + striNo;
                tbToolIdAdd.Text = striNo;
                tbToolID.Text = strName + striNo;

                #endregion

                #region  自动生成 RFID

                if (cboxRfid.Checked)
                {
                    //工具名称编码 tbRfidName
                    strSql = "select ToolsNameCoding from tb_TypeAndName where ToolName='" + cbbToolName.Text + "' ";
                    ob = datalogic.SqlComScalar(strSql);
                    if (ob != null)
                    {
                        string strServerAddrRfid = infoOfSystem.strServerAddr;
                        if (strServerAddrRfid.Length == 1)
                        {
                            strServerAddrRfid = "000" + strServerAddrRfid;
                        }
                        else if (strServerAddrRfid.Length == 2)
                        {
                            strServerAddrRfid = "00" + strServerAddrRfid;
                        }
                        if (strServerAddrRfid.Length > 4)
                        {
                            strServerAddrRfid = strServerAddrRfid.Substring(0, 4);
                        }
                        tbRfidName.Text = strServerAddrRfid + ob.ToString();
                    }

                    //RFID 编码
                    strSql = "select top 1 RFIDCoding from tb_Tools where IsArea='" + ToolAreaType.工具.ToString() + "' " +
                        "and ToolName='" + strName + "' order by id desc ";// select top 1 * from table order by id desc
                    ob = datalogic.SqlComScalar(strSql);
                    iNo = 0;
                    if (ob != null)
                    {
                        string strLastCoding = ob.ToString();
                        if (strLastCoding.Length == 12)
                        {
                            string strLastNo = strLastCoding.Substring(8, 4);
                            iNo = Convert.ToInt32(strLastNo);
                        }
                    }

                    iNo++;
                    striNo = iNo.ToString();
                    iLen = 4 - striNo.Length;
                    striNo = clsCommon.FormatString(iLen) + striNo;
                    tbRfidNo.Text = striNo;
                    tbRFID.Text = tbRfidName.Text + tbRfidNo.Text;
                }
                
            

                #endregion

                strSql = "select ToolsCycle from tb_TypeAndName where ToolName='" + cbbToolName.Text.Trim() + "' ";
                ob = datalogic.SqlComScalar(strSql);
                if (ob != null)
                {
                    tbCycle.Text = ob.ToString();
                }
            }
        }

        private void cbbToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Tag.ToString() == TagType.添加.ToString())
            {
                cbbToolName.Items.Clear();
                string strSql = "select tvChildId from tb_TypeAndName where ToolType='" + cbbToolType.Text.Trim() + "' ";
                object ob = datalogic.SqlComScalar(strSql);
                if (ob != null)
                {
                    strSql = "select ToolName from tb_TypeAndName where tvParent='" + ob.ToString() + "'";
                    DataTable dt = datalogic.GetDataTable(strSql);
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbbToolName.Items.Add(dr["ToolName"].ToString());
                    }
                    if (cbbToolName.Items.Count > 0)
                        cbbToolName.SelectedIndex = 0;
                }
            }
       }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            sbtnOk.Enabled = false;

            #region  输入 判断

            string strToolId = tbToolID.Text.Trim();//strToolId
            string strRFID = tbRFID.Text.Trim();
            string strToolType = cbbToolType.Text.Trim();
            string strToolName = cbbToolName.Text.Trim();
            string strPlace = tbPlace.Text.Trim();
            string strPeople = cbbOverPeople.Text.Trim();
            string strTime = dtpTime.Text;

            if (string.IsNullOrEmpty(strToolId))
            {
                MessageUtil.ShowTips("工具ID不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strPlace))
            {
                MessageUtil.ShowTips("工具存放位置不可为空");
                return;
            }
            if (this.Tag != null)
            {
                string strSql = "";
                if (this.Tag.ToString() == TagType.修改.ToString())
                {
                    strSql = "Select ID From tb_Tools where ToolID='" + strToolId + "' and ID<>'" + sId + "'";
                }
                else if (this.Tag.ToString() == TagType.添加.ToString())
                {
                    strSql = "Select ID From tb_Tools where ToolID='" + strToolId + "' ";
                }
                if (!string.IsNullOrEmpty(strSql))
                {
                    if (operatedata.blCheckHas(strSql))
                    {
                        MessageUtil.ShowTips("工具ID重复，请重新输入");
                        return;
                    }
                }

            }

            if (cboxRfid.Checked)
            {
                if (string.IsNullOrEmpty(strRFID))
                {
                    MessageUtil.ShowTips("RFID编号不可为空");
                    return;
                }
                else
                {
                    if (strRFID.Length != 12)
                    {
                        MessageUtil.ShowTips("RFID编号格式错误，请重新输入");
                        return;
                    }
                    else
                    {

                        if (this.Tag != null)
                        {
                            string strSql = "";
                            if (this.Tag.ToString() == TagType.修改.ToString())
                            {
                                strSql = "Select ID From tb_Tools where RFIDCoding='" + strRFID + "' and ID<>'" + sId + "'";
                            }
                            else if (this.Tag.ToString() == TagType.添加.ToString())
                            {
                                strSql = "Select ID From tb_Tools where RFIDCoding='" + strRFID + "' ";
                            }
                            if (!string.IsNullOrEmpty(strSql))
                            {
                                if (operatedata.blCheckHas(strSql))
                                {
                                    MessageUtil.ShowTips("RFID编号重复，请重新输入");
                                    return;
                                }
                            }
                        }
                    }
                }
            }


            #endregion

            #region 入库记录 及 写RFID

            if (this.Tag != null)
            {
                if (this.Tag.ToString() == TagType.修改.ToString())
                {
                    string strSql = "update tb_Tools set tvParent='" + strTreeSeleParent + "',ToolType='" + strToolType + "',ToolName='" + strToolName + "'" +
                        ",ToolID='" + strToolId + "',RFIDCoding='" + strRFID + "',StoragePlace='" + strPlace + "' ,TestCycle='" + tbCycle.Text + "'" +
                        "where ID='" + sId + "' ";
                    datalogic.SqlComNonQuery(strSql);
                    this.Close();
                }
                else if (this.Tag.ToString() == TagType.添加.ToString())
                {
                    try
                    {
                        int WriteEPCResult = 1;
                        string strWriteEPC = tbRFID.Text;
                        if (cboxRfid.Checked)
                        {
                            //strWriteEPC = MainControl.strServerAddr + strWriteEPC;
                            byte WriteEPClen = Convert.ToByte(strWriteEPC.Length / 4);//WriteEPC的字节长度   ////byte WriteEPClen = Convert.ToByte(strWriteEPC.Length / 4);//WriteEPC的字节长度   //20151109 8616
                            byte[] WriteEPC = new byte[WriteEPClen];//访问密码数组形式
                            //WriteEPC = HexStringToByteArray(strWriteEPC);
                            WriteEPC = SerialPortUtil.HexStrToByte(strWriteEPC);
                            byte[] PassWord = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 }; //byte[] PassWord = new byte[] { 0x00, 0x00, 0x00, 0x00 }; //  20151109 8616 
                            for (int i = 0; i < 50; i++)//最多写50次
                            {
                                WriteEPCResult = StaticClassReaderB.WriteEPC_G2(ref ComAdr, PassWord, WriteEPC, WriteEPClen, ref errorcode, frmHandle);                            //fCmdRet = StaticClassReaderB.WriteEPC_G2(ref fComAdr, fPassWord, EPC, ENum, ref ferrorcode, frmcomportindex);
                                if (WriteEPCResult == 0)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            WriteEPCResult = 0;
                            strWriteEPC = "";
                        }
                    
                        if (WriteEPCResult == 0)//写RFID成功
                        {
                            #region 更新 显示树

                            TreeNode node = new TreeNode();
                            //获取ChildId
                            string strChild = operatedata.GetChildId("tb_Tools", "tvChildId");
                            node.Name = strChild;// node.Name 为本节点的编号 
                            string strParent = treeView1.SelectedNode.Name.ToString();
                            node.Tag = strParent;
                            node.Text = strToolId;
                            node.ImageIndex = 3;
                            node.SelectedImageIndex = 3;
                            treeView1.SelectedNode.Nodes.Add(node);
                            //treeView1.CollapseAll();

                            #endregion


                            string strTimeBorrRet = DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss");
                            string strSql = "insert into tb_Tools (tvParent,tvChildId,IsArea,ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,InPeople,InTime,IsInStore,"+
                                            "TestCycle,TestState,BorrowReturnTime)" +
                                            "values ('" + strParent + "','" + strChild + "','" + ToolAreaType .工具 .ToString ()+ "','" + strToolType + "','" + strToolName + "'," +
                                            "'" + strToolId + "','" + strWriteEPC + "','" + strPlace + "','" + strPeople + "','" + strTime + "',"+
                                            "'" + ToolsState.在库 .ToString ()+ "','" + tbCycle.Text + "','未录入试验记录','" + strTime + "')";
                            datalogic.SqlComNonQuery(strSql);

                            strSql = "insert into tb_RecordInRoom (ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,InPeople,InTime) values ('" + strToolType + "'," +
                                "'" + strToolName + "','" + strToolId + "','" + strWriteEPC + "','" + strPlace + "','" + strPeople + "','" + strTime + "')";
                            datalogic.SqlComNonQuery(strSql);

                            #region  自动生成工具ID

                            string strName = cbbToolName.Text;
                            strSql = "select top 1 ToolID from tb_Tools where IsArea='" + ToolAreaType.工具.ToString() + "' "+
                                "and ToolName='" + strName + "' order by id desc ";// select top 1 * from table order by id desc
                          object  ob = datalogic.SqlComScalar(strSql);

                            int iNo = 0;
                            if (ob != null)
                            {
                                int iLength = strName.Length;
                                strToolId = ob.ToString();
                                string strNameOfId = strToolId.Substring(0, iLength);//strToolId中的工具名称 绝缘手套007 绝缘手套
                                if (strName == strNameOfId)
                                {
                                    if ((strToolId.Length - iLength) == 3)//数字部分的长度
                                    {
                                        string strNo = strToolId.Substring(iLength, 3);
                                        try
                                        {
                                            iNo = Convert.ToInt32(strNo);
                                        }
                                        catch (Exception ex)
                                        {
                                            //iNo = 0;
                                            if (frmMain.blDebug)
                                                MessageUtil.ShowTips(ex.Message);
                                        }
                                    }
                                }
                            }

                            iNo++;
                            string striNo = iNo.ToString();
                            int iLen = 3 - striNo.Length;
                            striNo = clsCommon.FormatString(iLen) + striNo;
                            tbToolIdAdd.Text = striNo;
                            tbToolID.Text = strName + striNo;

                            #endregion

                            #region  自动生成 RFID

                            if (cboxRfid.Checked)
                            {
                                //工具名称编码 tbRfidName
                                strSql = "select ToolsNameCoding from tb_TypeAndName where ToolName='" + cbbToolName.Text + "' ";
                                ob = datalogic.SqlComScalar(strSql);
                                if (ob != null)
                                {
                                    string strServerAddrRfid = infoOfSystem.strServerAddr;
                                    if (strServerAddrRfid.Length == 1)
                                    {
                                        strServerAddrRfid = "000" + strServerAddrRfid;
                                    }
                                    else if (strServerAddrRfid.Length == 2)
                                    {
                                        strServerAddrRfid = "00" + strServerAddrRfid;
                                    }
                                    if (strServerAddrRfid.Length > 4)
                                    {
                                        strServerAddrRfid = strServerAddrRfid.Substring(0, 4);
                                    }
                                    tbRfidName.Text = strServerAddrRfid + ob.ToString();
                                }

                                //RFID 编码
                                strSql = "select top 1 RFIDCoding from tb_Tools where IsArea='" + ToolAreaType.工具.ToString() + "' and " +
                                "ToolName='" + strName + "' order by id desc ";// select top 1 * from table order by id desc
                                ob = datalogic.SqlComScalar(strSql);
                                iNo = 0;
                                if (ob != null)
                                {
                                    string strLastCoding = ob.ToString();
                                    if (strLastCoding.Length == 12)
                                    {
                                        string strLastNo = strLastCoding.Substring(8, 4);
                                        iNo = Convert.ToInt32(strLastNo);
                                    }
                                }

                                iNo++;
                                striNo = iNo.ToString();
                                iLen = 4 - striNo.Length;
                                striNo = clsCommon.FormatString(iLen) + striNo;
                                tbRfidNo.Text = striNo;
                                tbRFID.Text = tbRfidName.Text + tbRfidNo.Text;
                            }

                          

                            #endregion
                        }
                        else
                        {
                            MessageUtil.ShowError("工具入库失败，请检查RFID标签是否正确放置或重新录");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (frmMain .blDebug )
                            MessageUtil.ShowError(ex .ToString ());
                        //MessageUtil.ShowError("工具入库失败，请检查RFID标签是否正确放置、RFID读卡器是否正确连接或重新录入");
                    }
                }
            }

            #endregion

            //sbtnOk.Enabled = true;

            treeView1.SelectedNode = null;
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                StaticClassReaderB.CloseSpecComPort(Port);
            }
            catch
            {
                MessageUtil.ShowError("RFID读写器错误，请检查RFID读写器");
            }
            this.Close();
        }

        //private byte[] HexStringToByteArray(string s)//字符串转16进制数组
        //{
        //    s = s.Replace(" ", "");
        //    byte[] buffer = new byte[s.Length / 2];
        //    for (int i = 0; i < s.Length; i += 2)
        //    {
        //        buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
        //    }
        //    return buffer;
        //}


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.ImageIndex == 2 || treeView1.SelectedNode.ImageIndex == 4)
            {
                strTreeSeleParent = treeView1.SelectedNode.Name;
                tbPlace.Text = treeView1.SelectedNode.Text.ToString();
                sbtnOk.Enabled = true;
            }
            else
            {
                sbtnOk.Enabled = false;
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

        private void cboxRfid_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxRfid.Checked == false)
            {
                tbRFID.Text = "";
            }
        }

        private void sbtnRfidCopy_Click(object sender, EventArgs e)
        {
            sbtnRfidCopy.Enabled = false;

            #region  输入 判断

            string strRFID = tbRFID.Text.Trim();
            if (string.IsNullOrEmpty(strRFID))
            {
                MessageUtil.ShowTips("RFID编号不可为空");
                sbtnRfidCopy.Enabled = true;
                return;
            }
            else
            {
                if (strRFID.Length != 12)
                {
                    MessageUtil.ShowTips("RFID编号格式错误，请重新输入");
                    sbtnRfidCopy.Enabled = true;
                    return;
                }
            }
            #endregion

            try
            {
                string strWriteEPC = tbRFID.Text;
                //strWriteEPC = MainControl.strServerAddr + strWriteEPC;
                int WriteEPCResult = 1;
                byte WriteEPClen = Convert.ToByte(strWriteEPC.Length / 4);//WriteEPC的字节长度   ////byte WriteEPClen = Convert.ToByte(strWriteEPC.Length / 4);//WriteEPC的字节长度   //20151109 8616
                byte[] WriteEPC = new byte[WriteEPClen];//访问密码数组形式
                //WriteEPC = HexStringToByteArray(strWriteEPC);
                WriteEPC = SerialPortUtil.HexStrToByte(strWriteEPC);
                byte[] PassWord = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 }; //byte[] PassWord = new byte[] { 0x00, 0x00, 0x00, 0x00 }; //  20151109 8616 
                for (int i = 0; i < 50; i++)//最多写50次
                {
                    WriteEPCResult = StaticClassReaderB.WriteEPC_G2(ref ComAdr, PassWord, WriteEPC, WriteEPClen, ref errorcode, frmHandle);                            //fCmdRet = StaticClassReaderB.WriteEPC_G2(ref fComAdr, fPassWord, EPC, ENum, ref ferrorcode, frmcomportindex);
                    if (WriteEPCResult == 0)
                    {
                        i = 50;
                    }
                }
                if (WriteEPCResult == 0)//写RFID成功
                {
                    MessageUtil.ShowTips("复制成功");
                }
                else
                {
                    MessageUtil.ShowError("复制失败，请检查RFID标签是否正确放置，请重新复制");
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowError(ex.ToString());
            }

            sbtnRfidCopy.Enabled = true;
        }
    }
}
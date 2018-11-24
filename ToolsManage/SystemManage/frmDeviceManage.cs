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
using ToolsManage.BaseClass.OtherClass;

namespace ToolsManage.SystemManage
{
    public partial class frmDeviceManage : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        AppConfig config = new AppConfig();

        private string strId = "";

        public frmDeviceManage()
        {
            InitializeComponent();
        }

        private void frmDeviceManage_Load(object sender, EventArgs e)// tb_SysDevice ID,DoorSN,DoorIP//IcDoorOfRfid1 FingerDoorOfRfid
        {
            #region xml

         
            string strXml = config.AppConfigGet("IsUserPowReader");
            if (strXml == DeviceUsing.启用.ToString())
            {
                cboxPowReaderUser.Checked = true;
                gboxPowRead.Enabled = true;
                gboxIn.Enabled = true;
                gboxOut.Enabled = true;
                gBoxMarkId.Enabled = true;
            }
            else
            {
                cboxPowReaderUser.Checked = false;
                gboxPowRead.Enabled = false;
                gboxIn.Enabled = false;
                gboxOut.Enabled = false;
                gBoxMarkId.Enabled = false;
            }

            tbPowReadIp.Text = config.AppConfigGet("PowReaderIP");
            tbPowReadToIp.Text = config.AppConfigGet("PowReaderToIP");
            tbPowReadPort.Text = config.AppConfigGet("PowReaderToPort");
            tbOutId1.Text = config.AppConfigGet("OutId1");
            tbOutId2.Text = config.AppConfigGet("OutId2");
            tbOutId3.Text = config.AppConfigGet("OutId3");
            tbOutId4.Text = config.AppConfigGet("OutId4");
            tbInId1.Text = config.AppConfigGet("InId1");
            tbInId2.Text = config.AppConfigGet("InId2");
            tbInId3.Text = config.AppConfigGet("InId3");
            tbInId4.Text = config.AppConfigGet("InId4");
            tbMarkTime.Text = config.AppConfigGet("MarkTmie");

            ShowMarkId();

            #endregion

            string strSql = "select ID,DoorUsing,DoorSN,DoorIP,DoorCount,OverDayBorr,BorrRetSpan,ServerAddr" +
                ",RfidUsing1,RfidUsing2,RfidIp1,RfidIp2,RfidPort1,RfidPort2,DoorName1,DoorName2,ServerUsing,EnvirUsing,"+
                "RfidBoxUsing,DoorType,FingerDoorIp,RfidBoxTime,FingerPort,wgPort,WgDoorName3,WgDoorName4,UsingFinger"+
                ",FingerDoorName,IcDoorOfRfid1,IcDoorOfRfid2,IcDoorOfRfid3,IcDoorOfRfid4,FingerDoorOfRfid,Rfid1No,Rfid2No,BorrOver" +
                ",ErrInfo,UsingFinger2,FingerDoorIp2,FingerPort2,FingerDoorName2,FingerDoorOfRfid2 from tb_SysDevice ";
            DataTable dt = datalogic.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                strId = dt.Rows[0]["ID"].ToString();

                #region  门禁

                #region IC门禁

                string str = dt.Rows[0]["DoorUsing"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxIc.Checked = true;
                else
                    cboxIc.Checked = false;
                //门关联RFID
                str = dt.Rows[0]["IcDoorOfRfid1"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxOfRfid1.Checked = true; 
                else
                    cboxOfRfid1.Checked = false;
                str = dt.Rows[0]["IcDoorOfRfid2"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxOfRfid2.Checked = true;
                else
                    cboxOfRfid2.Checked = false;
                str = dt.Rows[0]["IcDoorOfRfid3"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxOfRfid3.Checked = true;
                else
                    cboxOfRfid3.Checked = false;
                str = dt.Rows[0]["IcDoorOfRfid4"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxOfRfid4.Checked = true;
                else
                    cboxOfRfid4.Checked = false;

                tbDoorSn.Text = dt.Rows[0]["DoorSN"].ToString();
                tbDoorIP.Text = dt.Rows[0]["DoorIP"].ToString();
                tbPortWg.Text = dt.Rows[0]["wgPort"].ToString();
                tbDoorName1.Text = dt.Rows[0]["DoorName1"].ToString();
                tbDoorName2.Text = dt.Rows[0]["DoorName2"].ToString();
                tbDoorName3.Text = dt.Rows[0]["WgDoorName3"].ToString();
                tbDoorName4.Text = dt.Rows[0]["WgDoorName4"].ToString();

                str = dt.Rows[0]["DoorName1"].ToString();
                if (string.IsNullOrEmpty(str))
                    cboxName1.Checked = false;
                else
                    cboxName1.Checked = true;
                str = dt.Rows[0]["DoorName2"].ToString();
                if (string.IsNullOrEmpty(str))
                    cboxName2.Checked = false;
                else
                    cboxName2.Checked = true;
                str = dt.Rows[0]["WgDoorName3"].ToString();
                if (string.IsNullOrEmpty(str))
                    cboxName3.Checked = false;
                else
                    cboxName3.Checked = true;
                str = dt.Rows[0]["WgDoorName4"].ToString();
                if (string.IsNullOrEmpty(str))
                    cboxName4.Checked = false;
                else
                    cboxName4.Checked = true;

                #endregion

                #region  指纹门禁

                //1#指纹门禁
                str = dt.Rows[0]["UsingFinger"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxFinger.Checked = true;
                else
                    cboxFinger.Checked = false;
                str = dt.Rows[0]["FingerDoorOfRfid"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxOfRfidFinger.Checked = true;
                else
                    cboxOfRfidFinger.Checked = false;
                tbIpFinger.Text = dt.Rows[0]["FingerDoorIp"].ToString();
                tbPortFinger.Text = dt.Rows[0]["FingerPort"].ToString();
                tbDoorNameFinger.Text = dt.Rows[0]["FingerDoorName"].ToString();

                //2#指纹门禁
                str = dt.Rows[0]["UsingFinger2"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxFinger2.Checked = true;
                else
                    cboxFinger2.Checked = false;
                str = dt.Rows[0]["FingerDoorOfRfid2"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxOfRfidFinger2.Checked = true;
                else
                    cboxOfRfidFinger2.Checked = false;
                tbIpFinger2.Text = dt.Rows[0]["FingerDoorIp2"].ToString();
                tbPortFinger2.Text= dt.Rows[0]["FingerPort2"].ToString();
                tbDoorNameFinger2.Text = dt.Rows[0]["FingerDoorName2"].ToString();

                #endregion

                #endregion

                #region RFID 大门

                //RFID1
                str = dt.Rows[0]["RfidUsing1"].ToString();
                if (str == DeviceUsing.启用.ToString())
                {
                    cboxRfid1.Checked = true;
                    gboxRfid1.Enabled = true;
                }
                else
                {
                    cboxRfid1.Checked = false;
                    gboxRfid1.Enabled = false;
                }
                //RFID2
                str = dt.Rows[0]["RfidUsing2"].ToString();
                if (str == DeviceUsing.启用.ToString())
                {
                    cboxRfid2.Checked = true;
                    gboxRfid2.Enabled = true;
                }
                else
                {
                    cboxRfid2.Checked = false;
                    gboxRfid2.Enabled = false;
                }
                 
                tbRfidIp1.Text = dt.Rows[0]["RfidIp1"].ToString();
                tbRfidIp2.Text = dt.Rows[0]["RfidIp2"].ToString();
                tbRfidPort1.Text = dt.Rows[0]["RfidPort1"].ToString();
                tbRfidPort2.Text = dt.Rows[0]["RfidPort2"].ToString();
                #endregion

                #region 环境管理

                str = dt.Rows[0]["EnvirUsing"].ToString();
                if (str == DeviceUsing.启用.ToString())
                {
                    cboxEnvir.Checked = true;
                    sbtnEnvirTest.Enabled = true;
                }
                else
                {
                    cboxEnvir.Checked = false;
                    sbtnEnvirTest.Enabled = false;
                }
                #endregion

                #region 数据上传

                str = dt.Rows[0]["ServerUsing"].ToString();
                if (str == DeviceUsing.启用.ToString())
                {
                    cboxUp.Checked = true;
                    tbAddr.Enabled = true;
                }
                else
                {
                    cboxUp.Checked = false ;
                    tbAddr.Enabled = false;
                }
                tbAddr.Text = dt.Rows[0]["ServerAddr"].ToString();

                #endregion

                #region  RFID工具柜

                //str = dt.Rows[0]["RfidBoxUsing"].ToString();
                //if (str == DeviceUsing.启用.ToString())
                //{
                //    cboxRfidBox.Checked = true;
                //    tbRfidBoxTime.Text = dt.Rows[0]["RfidBoxTime"].ToString();
                //}
                //else
                //{
                //    cboxRfidBox.Checked = false;
                //}

                #endregion

                #region   20170808后添加  是否顺序读取RFID天线 及 外借超时提醒

                str = dt.Rows[0]["Rfid1No"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxRfid1No.Checked = true;
                else
                    cboxRfid1No.Checked = false;
                str = dt.Rows[0]["Rfid2No"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxRfid2No.Checked = true;
                else
                    cboxRfid2No.Checked = false;

                str = dt.Rows[0]["BorrOver"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxBorrOver.Checked = true;
                else
                    cboxBorrOver.Checked = false;

                str = dt.Rows[0]["ErrInfo"].ToString();
                if (str == DeviceUsing.启用.ToString())
                    cboxError.Checked = true; 
                else
                    cboxError.Checked = false;

                #endregion

                //tbOverBorr.Text = dt.Rows[0]["OverDayBorr"].ToString();
                //tbBorrRetSpan.Text = dt.Rows[0]["BorrRetSpan"].ToString();
        
            }
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            #region 门禁

            #region IC门禁

            DeviceUsing usingIc = DeviceUsing.未启用;
            string  ofRfidDoor1 = DeviceUsing.未启用.ToString ();
            string ofRfidDoor2 = DeviceUsing.未启用.ToString();
            string ofRfidDoor3 = DeviceUsing.未启用.ToString();
            string ofRfidDoor4 = DeviceUsing.未启用.ToString();
            if (cboxOfRfid1.Checked)
                ofRfidDoor1 = DeviceUsing.启用.ToString();
            if (cboxOfRfid2.Checked)
                ofRfidDoor2 = DeviceUsing.启用.ToString();
            if (cboxOfRfid3.Checked)
                ofRfidDoor3 = DeviceUsing.启用.ToString();
            if (cboxOfRfid4.Checked)
                ofRfidDoor4 = DeviceUsing.启用.ToString();

            string strIcSn = tbDoorSn.Text.Trim();
            string strIcIp = tbDoorIP.Text.Trim();
            string strIcPort = tbPortWg.Text.Trim();
            string strNameDoor1 = "";
            string strNameDoor2 = "";
            string strNameDoor3 = "";
            string strNameDoor4 = "";
            if (cboxName1.Checked )
                strNameDoor1 = tbDoorName1.Text.Trim();
            if (cboxName2.Checked)
                strNameDoor2 = tbDoorName2.Text.Trim();
            if (cboxName3.Checked)
                strNameDoor3 = tbDoorName3.Text.Trim();
            if (cboxName4.Checked)
                strNameDoor4 = tbDoorName4.Text.Trim();

            if (cboxIc.Checked)
            {
                usingIc = DeviceUsing.启用;

                if (string.IsNullOrEmpty(strIcSn))
                {
                    MessageUtil.ShowTips("IC门禁 SN不可为空");
                    return;
                }
                if (string.IsNullOrEmpty(strIcIp))
                {
                    MessageUtil.ShowTips("IC门禁 IP不可为空");
                    return;
                }
                if (string.IsNullOrEmpty(strIcPort))
                {
                    MessageUtil.ShowTips("IC门禁 端口号 不可为空");
                    return;
                }
                if (cboxName1.Checked)
                {
                    if (string.IsNullOrEmpty(strNameDoor1))
                    {
                        MessageUtil.ShowTips("IC门禁 1#门名称不可为空");
                        return;
                    }
                }
                if (cboxName2.Checked)
                {
                    if (string.IsNullOrEmpty(strNameDoor2))
                    {
                        MessageUtil.ShowTips("IC门禁 2#门名称不可为空");
                        return;
                    }
                }
                if (cboxName3.Checked)
                {
                    if (string.IsNullOrEmpty(strNameDoor3))
                    {
                        MessageUtil.ShowTips("IC门禁 3#门名称不可为空");
                        return;
                    }
                }
                if (cboxName4.Checked)
                {
                    if (string.IsNullOrEmpty(strNameDoor4))
                    {
                        MessageUtil.ShowTips("IC门禁 4#门名称不可为空");
                        return;
                    }
                }
            }

            #endregion

            #region  指纹 门禁

            DeviceUsing usingFinger = DeviceUsing.未启用;
            

            string ofRfidDoorFinger = DeviceUsing.未启用.ToString();
            if (cboxOfRfidFinger.Checked)
                ofRfidDoorFinger = DeviceUsing.启用.ToString();
            string strIpFinger = tbIpFinger.Text.Trim();
            string strPortFinger = tbPortFinger.Text.Trim();
            string strNameFinger = tbDoorNameFinger.Text.Trim();
            if (cboxFinger.Checked)
            {
                usingFinger = DeviceUsing.启用;
                if (string.IsNullOrEmpty(strIpFinger))
                {
                    MessageUtil.ShowTips("请输入1#指纹门禁IP地址");
                    return;
                }
                if (string.IsNullOrEmpty(strPortFinger))
                {
                    MessageUtil.ShowTips("请输入1#指纹门禁端口号");
                    return;
                }
                if (string.IsNullOrEmpty(strNameFinger))
                {
                    MessageUtil.ShowTips("请输入1#指纹门禁门名称");
                    return;
                }
            }
            DeviceUsing usingFinger2 = DeviceUsing.未启用;
            string ofRfidDoorFinger2 = DeviceUsing.未启用.ToString();
            if (cboxOfRfidFinger2.Checked)
                ofRfidDoorFinger2 = DeviceUsing.启用.ToString();
            string strIpFinger2 = tbIpFinger2.Text.Trim();
            string strProtFinger2 = tbPortFinger2.Text.Trim();
            string strNameFinger2 = tbDoorNameFinger2.Text.Trim();
            if (cboxFinger2.Checked)
            {
                usingFinger2 = DeviceUsing.启用;
                if(String.IsNullOrEmpty(strIpFinger2))
                {
                    MessageUtil.ShowTips("请输入2#指纹门禁IP地址");
                    return;
                }
                if(String.IsNullOrEmpty(strProtFinger2))
                {
                    MessageUtil.ShowTips("请输入2#指纹门禁端口号");
                    return;
                }
                if(String.IsNullOrEmpty(strNameFinger2))
                {
                    MessageUtil.ShowTips("请输入2#指纹门禁门名称");
                    return;
                }
            }

            #endregion

            #endregion

            #region  Rfid

            string strRfidIp1 = tbRfidIp1.Text.Trim();
            string strRfidIp2 = tbRfidIp2.Text.Trim();
            string strRfidPort1 = tbRfidPort1.Text.Trim();
            string strRfidPort2 = tbRfidPort2.Text.Trim();
            string strRfidUsing1 = "";
            string strRfidUsing2 = "";
            

            if (cboxRfid1.Checked)
            {
                if (string.IsNullOrEmpty(strRfidIp1))
                {
                    MessageUtil.ShowTips("RFID读写器1IP不可为空");
                    return;
                }
                if (string.IsNullOrEmpty(strRfidPort1 ))
                {
                    MessageUtil.ShowTips("RFID读写器1端口号不可为空");
                    return;
                }
                strRfidUsing1 = DeviceUsing.启用.ToString();
            }
            else
            {
                strRfidUsing1 = DeviceUsing.未启用.ToString();
            }
            if (cboxRfid2.Checked)
            {
                if (string.IsNullOrEmpty(strRfidIp2))
                {
                    MessageUtil.ShowTips("RFID读写器2IP不可为空");
                    return;
                }
                if (string.IsNullOrEmpty(strRfidPort2))
                {
                    MessageUtil.ShowTips("RFID读写器2端口号不可为空");
                    return;
                }
                strRfidUsing2 = DeviceUsing.启用.ToString();
            }
            else
            {
                strRfidUsing2 = DeviceUsing.未启用.ToString();
            }

            if (cboxRfid1.Checked && cboxRfid2.Checked)
            {
                if (strRfidIp1 == strRfidIp2)
                {
                    MessageUtil.ShowTips("RFID读写器IP不可相同");
                    return;
                }
                if (strRfidPort1 == strRfidPort2)
                {
                    MessageUtil.ShowTips("RFID读写器端口号不可相同");
                    return;
                }
            }

            #endregion

            #region 服务上传

            string strUsingUp = "";
            string strAddr = tbAddr.Text.Trim();
            if (cboxUp.Checked)
            {
                strUsingUp = DeviceUsing.启用.ToString();
                if (string.IsNullOrEmpty(strAddr))
                {
                    MessageUtil.ShowTips("本机服务地址不可为空");
                    return;
                }
            }
            else
            {
                strUsingUp = DeviceUsing.未启用.ToString();
            }

            #endregion

            #region  环境管理设置

            string strUsingEnvir = "";
            if (cboxEnvir.Checked) 
            {
                frmMain.UsingEnvir = DeviceUsing.启用;
                strUsingEnvir = DeviceUsing.启用.ToString();
            }
            else
            {
                frmMain.UsingEnvir = DeviceUsing.未启用;
                strUsingEnvir = DeviceUsing.未启用.ToString();
            }

            #endregion

            #region RFID 工具柜

            #endregion

            #region  其他

            string strRfid1No = DeviceUsing.未启用.ToString();
            string strRfid2No = DeviceUsing.未启用.ToString();
            string strBorrOver = DeviceUsing.未启用.ToString();
            if (cboxRfid1No.Checked)
                strRfid1No = DeviceUsing.启用.ToString();
            if (cboxRfid2No.Checked)
                strRfid2No = DeviceUsing.启用.ToString();
            if (cboxBorrOver.Checked)
                strBorrOver = DeviceUsing.启用.ToString();
            string strErrInfo = DeviceUsing.未启用.ToString();
            if (cboxError .Checked )
                strErrInfo = DeviceUsing.启用.ToString();


            #endregion

            #region xml

            if (cboxPowReaderUser.Checked)
            {
                config.AppConfigSet("IsUserPowReader", DeviceUsing.启用.ToString());
                config.AppConfigSet("PowReaderIP", DeviceUsing.启用.ToString());
                if (string.IsNullOrEmpty(tbPowReadIp.Text))
                {
                    MessageUtil.ShowTips("半有源读写器 IP 不可为空");
                    return;
                }
                if (string.IsNullOrEmpty(tbPowReadToIp.Text))
                {
                    MessageUtil.ShowTips("半有源读写器 目标IP 不可为空");
                    return;
                }
                if (string.IsNullOrEmpty(tbPowReadToIp.Text))
                {
                    MessageUtil.ShowTips("半有源读写器 目标端口 不可为空");
                    return;
                }
                if (string.IsNullOrEmpty(tbOutId1.Text) && string.IsNullOrEmpty(tbOutId2.Text) && string.IsNullOrEmpty(tbOutId3.Text) && string.IsNullOrEmpty(tbOutId4.Text))
                {
                    MessageUtil.ShowTips("请填写室外 激活器ID");
                    return;
                }
                if (string.IsNullOrEmpty(tbInId1.Text) && string.IsNullOrEmpty(tbInId2.Text) && string.IsNullOrEmpty(tbInId3.Text) && string.IsNullOrEmpty(tbInId4.Text))
                {
                    MessageUtil.ShowTips("请填写室内 激活器ID");
                    return;
                }
                if (string.IsNullOrEmpty(tbMarkTime.Text)) 
                {
                    MessageUtil.ShowTips("盘库时长 不可为空");
                    return;
                }
            }
               
            else
                config.AppConfigSet("IsUserPowReader", DeviceUsing.未启用.ToString());

            config.AppConfigSet("PowReaderIP", tbPowReadIp.Text);
            config.AppConfigSet("PowReaderToIP", tbPowReadToIp.Text);
            config.AppConfigSet("PowReaderToPort", tbPowReadPort.Text);
            config.AppConfigSet("OutId1", tbOutId1.Text);
            config.AppConfigSet("OutId2", tbOutId2.Text);
            config.AppConfigSet("OutId3", tbOutId3.Text);
            config.AppConfigSet("OutId4", tbOutId4.Text);
            config.AppConfigSet("InId1", tbInId1.Text);
            config.AppConfigSet("InId2", tbInId2.Text);
            config.AppConfigSet("InId3", tbInId3.Text);
            config.AppConfigSet("InId4", tbInId4.Text);

            config.AppConfigSet("MarkTmie", tbMarkTime.Text);

            #endregion

            string strSql = "select ID from tb_SysDevice ";
            DataTable dt = datalogic.GetDataTable(strSql);//wgPort,WgDoorName3,WgDoorName4,UsingFinger,FingerDoorName	
            if (dt.Rows.Count > 0)
            {//IcDoorOfRfid1,IcDoorOfRfid2,IcDoorOfRfid3,IcDoorOfRfid4,FingerDoorOfRfid  Rfid1No,Rfid2No,BorrOver
                strSql = "update tb_SysDevice set DoorUsing='" + usingIc .ToString () + "',DoorSN='" + strIcSn  + "',DoorIP='" + strIcIp  + "'," +
                          "DoorCount='" + "" + "',RfidUsing1='" + strRfidUsing1 + "',RfidUsing2='" + strRfidUsing2 + "',RfidIp1='" + strRfidIp1 + "'," +
                          "RfidIp2='" + strRfidIp2 + "',RfidPort1='" + strRfidPort1 + "',RfidPort2='" + strRfidPort2 + "',ServerAddr='" + strAddr + "'," +
                          "DoorName1='" + strNameDoor1 + "',DoorName2='" + strNameDoor2 + "',ServerUsing='" + strUsingUp + "',EnvirUsing='" + strUsingEnvir + "'" +
                          ",RfidBoxUsing='" + "" + "',DoorType='" + "" + "',FingerDoorIp='" + strIpFinger + "',RfidBoxTime='" + "" + "',"+
                          "FingerPort='" + strPortFinger + "',wgPort='" + strIcPort + "',WgDoorName3='" + strNameDoor3 + "',WgDoorName4='" + strNameDoor4  + "'"+
                          ",UsingFinger='" + usingFinger.ToString() + "',FingerDoorName='" + strNameFinger + "',IcDoorOfRfid1='" + ofRfidDoor1 + "'" +
                          ",IcDoorOfRfid2='" + ofRfidDoor2 + "',IcDoorOfRfid3='" + ofRfidDoor3 + "',IcDoorOfRfid4='" + ofRfidDoor4 + "'" +
                          ",FingerDoorOfRfid='" + ofRfidDoorFinger + "',Rfid1No='" + strRfid1No + "',Rfid2No='" + strRfid2No + "'" +
                          ",BorrOver='" + strBorrOver + "',ErrInfo='" + strErrInfo + "',UsingFinger2='" +usingFinger2 + "',FingerDoorIp2='"+ strIpFinger2+ "',FingerPort2='" + 
                          strProtFinger2 + "',FingerDoorName2='" + strNameFinger2+ "',FingerDoorOfRfid2='"+ ofRfidDoorFinger2 +  "' where ID='" + strId + "' ";
            }
            else
            {
                strSql = "insert into tb_SysDevice (DoorUsing,DoorSN,DoorIP,DoorCount,RfidUsing1,RfidUsing2,RfidIp1,RfidIp2,RfidPort1,RfidPort2," +
                        "DoorName1,DoorName2,ServerUsing,EnvirUsing,RfidBoxUsing,DoorType,FingerDoorIp,RfidBoxTime,FingerPort,wgPort,WgDoorName3"+
                        ",WgDoorName4,UsingFinger,FingerDoorName,IcDoorOfRfid1,IcDoorOfRfid2,IcDoorOfRfid3,IcDoorOfRfid4,FingerDoorOfRfid,Rfid1No"+
                        ",Rfid2No,BorrOver,ErrInfo,UsingFinger2,FingerDoorIp2,FingerPort2,FingerDoorName2,FingerDoorOfRfid2	)" +
                        "values ('" + usingIc.ToString() + "','" + strIcSn + "','" + strIcIp + "','" + "" + "','" + strRfidUsing1 + "'" +
                        ",'" + strRfidUsing2 + "','" + strRfidIp1 + "','" + strRfidIp2 + "','" + strRfidPort1 + "','" + strRfidPort2 + "','" + strNameDoor1 + "'" +
                        ",'" + strNameDoor2 + "','" + strUsingUp + "','" + strUsingEnvir + "','" + "" + "','" + "" + "','" + strIpFinger + "','" + "" + "',"+
                        "'" + strPortFinger + "','" + strIcPort + "','" + strNameDoor3 + "','" + strNameDoor4 + "','" + usingFinger.ToString() + "'"+
                        ",'" + strNameFinger + "' ,'" + ofRfidDoor1 + "','" + ofRfidDoor2 + "','" + ofRfidDoor3 + "','" + ofRfidDoor4 + "','" + ofRfidDoorFinger + "'"+
                        ",'" + strRfid1No + "','" + strRfid2No + "','" + strBorrOver + "','" + strErrInfo + "','"+ usingFinger2.ToString() + "','"+
                        strIpFinger2 + "','"+strProtFinger2 + "','"+ strNameFinger2 + "','"+ ofRfidDoorFinger2+ "')";
            }
            int iRet = datalogic.SqlComNonQuery(strSql);
            if (iRet == 0)
            {
                MessageUtil.ShowError("设置失败");
            }
            else
            {
                MessageUtil.ShowTips("设置成功");
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtnSearch_Click(object sender, EventArgs e)
        {
            frmDoorSearch frm = new frmDoorSearch();
            frm.ShowDialog(this);
            frm.Dispose();
            if (frmDoorSearch.blSelect)
            {
                tbDoorIP.Text  = frmDoorSearch.strIp;
                tbDoorSn.Text  = frmDoorSearch.strSn;
            }
        }



        private void cboxRfid1_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxRfid1.Checked)
            {
                gboxRfid1.Enabled = true;
                cboxPowReaderUser.Checked = false;
            }
            else
            {
                gboxRfid1.Enabled = false;
            }
        }

        private void cboxRfid2_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxRfid2.Checked)
            {
                gboxRfid2.Enabled = true;
                cboxPowReaderUser.Checked = false;
            }
            else
            {
                gboxRfid2.Enabled = false;
            }
        }





        private void cboxEnvir_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxEnvir.Checked)
            {
                sbtnEnvirTest.Enabled = true;
            }
            else
            {
                sbtnEnvirTest.Enabled = false;
            }
        }

        private void cboxUp_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxUp.Checked)
            {
                tbAddr.Enabled = true;
            }
            else
            {
                tbAddr.Enabled = false ;
            }
        }

        private void sbtnEnvirTest_Click(object sender, EventArgs e)
        {

        }

        private void cboxName1_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxName1.Checked)
                tbDoorName1.Enabled = true;
            else
                tbDoorName1.Enabled = false;
        }

        private void cboxName2_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxName2.Checked)
                tbDoorName2.Enabled = true;
            else
                tbDoorName2.Enabled = false;
        }

        private void cboxName3_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxName3.Checked)
                tbDoorName3.Enabled = true;
            else
                tbDoorName3.Enabled = false;
        }

        private void cboxName4_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxName4.Checked)
                tbDoorName4.Enabled = true;
            else
                tbDoorName4.Enabled = false;
        }

        private void cboxIc_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxIc.Checked)
            {
                gboxWg.Enabled = true;
                gboxDoorName.Enabled = true;
            }
            else
            {
                gboxWg.Enabled = false;
                gboxDoorName.Enabled = false;
            }
        }

        private void cboxFinger_CheckedChanged(object sender, EventArgs e)
        {
            //if (cboxFinger.Checked)
            //    gboxFinger.Enabled = true;
            //else
            //    gboxFinger.Enabled = false;
            if (cboxFinger.Checked)
            {
                if (cboxFinger2.Checked)
                {
                    tpIFace1.Parent = null;
                    tpIFace2.Parent = null;
                    this.tcIFace.Controls.AddRange(new System.Windows.Forms.Control[]
                    {

                    this.tpIFace1,
                    this.tpIFace2,

                    });
                }
                else
                {
                    tpIFace2.Parent = null;
                    tpIFace1.Parent = tcIFace;
                }
            }
            else
            {
                if (cboxFinger2.Checked)
                {
                    tpIFace1.Parent = null;
                    tpIFace2.Parent = tcIFace;
                }
                else
                {
                    tpIFace2.Parent = null;
                    tpIFace1.Parent = null;
                }
            }
        }

        private void cboxFinger2_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxFinger2.Checked)
            {
                if (cboxFinger.Checked)
                {
                    tpIFace1.Parent = null;
                    tpIFace2.Parent = null;
                    this.tcIFace.Controls.AddRange(new System.Windows.Forms.Control[]
                    {

                    this.tpIFace1,
                    this.tpIFace2,

                    });
                }
                else
                {
                    tpIFace2.Parent = tcIFace;
                    tpIFace1.Parent = null;
                }
            }
            else
            {
                if (cboxFinger.Checked)
                {
                    tpIFace1.Parent = tcIFace;
                    tpIFace2.Parent = null;
                }
                else
                {
                    tpIFace2.Parent = null;
                    tpIFace1.Parent = null;
                }
            }
        }

        private void sbtnMarkAdd_Click(object sender, EventArgs e)
        {
            string str = tbMarkId.Text;
            foreach (ListViewItem lvi in listId.Items)  //选中项遍历
            {
                if (str == lvi.SubItems[1].Text)
                {
                    MessageBox.Show("已经存在");
                    return;
                }
            }

            string strXml = config.AppConfigGet("MarkToolId");
            if (!string.IsNullOrEmpty(strXml))
                strXml += "+";
            strXml += str;
            config.AppConfigSet("MarkToolId", strXml);
            ShowMarkId();
        }

        /// <summary>
        /// 显示xml文件中的 盘库激活器Id
        /// </summary>
        private void ShowMarkId()
        {
            string strXml = config.AppConfigGet("MarkToolId");
            string[] strs = strXml.Split('+');
            listId.Items.Clear();
            foreach (string item in strs)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    ListViewItem aListItem = new ListViewItem();
                    aListItem = listId.Items.Add((listId.Items.Count + 1).ToString());
                    aListItem.SubItems.Add(item);
                }
            }
            if (listId.Items.Count > 0)
                this.listId.Items[this.listId.Items.Count - 1].EnsureVisible();
        }

        private void sbtnMarkDele_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listId.Items)  //选中项遍历
            {
                if (tbMarkId.Text == lvi.SubItems[1].Text)
                    listId.Items.Remove(lvi);   //按项移除
            }
      
            string strXml = "";
            foreach (ListViewItem lvi in listId.Items)
            {
                strXml += lvi.SubItems[1].Text;
                strXml += "+";
            }
            config.AppConfigSet("MarkToolId", strXml);
            ShowMarkId();
        }

        private void sbtnMarkClear_Click(object sender, EventArgs e)
        {
            config.AppConfigSet("MarkToolId", "");
            ShowMarkId();
        }

        private void cboxPowReaderUser_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxPowReaderUser.Checked)
            {
                cboxPowReaderUser.Checked = true;
                gboxPowRead.Enabled = true;
                gboxIn.Enabled = true;
                gboxOut.Enabled = true;
                gBoxMarkId.Enabled = true;

                cboxRfid1.Checked = false;
            }
            else
            {
                cboxPowReaderUser.Checked = false;
                gboxPowRead.Enabled = false;
                gboxIn.Enabled = false;
                gboxOut.Enabled = false;
                gBoxMarkId.Enabled = false;
            }
        }

        private void sbtnOtherSet_Click(object sender, EventArgs e)
        {
            frmOtherSet frm = new frmOtherSet();
            frm.ShowDialog(this);
            frm.Dispose();
        }        
    }
}
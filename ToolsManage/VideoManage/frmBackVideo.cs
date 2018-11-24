using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.IO;

using ToolsManage.BaseClass;

namespace ToolsManage.VideoManage
{
    public partial class frmBackVideo : DevExpress.XtraEditors.XtraForm
    {
        System.Threading.Timer TimerReadFilePos;

        private PlayState m_CurPlayState = PlayState.PlayStop;//当前播放方式
        private IntPtr m_ptrRealHandle;
        private PlayCtrl.PFILEREFDONE m_FileRefDone = null;
        private int m_lPort = 0;
        private string FilePath = @"D:\HikVideo\";

        Dictionary<string, string> dicVideoInfo = new Dictionary<string, string>();

        public frmBackVideo()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;//  获取或设置一个值，该值指示是否捕获对错误线程的调用，这些调用在调试应用程序时访问控件的 System.Windows.Forms.Control.Handle
            TimerStart();
        }

        private void frmBackVideo_Load(object sender, EventArgs e)
        {
            try
            {
                //cmbStationName.Items.AddRange(StationName);
                //cmbStationName.SelectedIndex = 0;
                //cmbLXMode.Items.AddRange(VideoMode);
                //cmbLXMode.SelectedIndex = 0;
                SetQueryTime();
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        private void SetQueryTime()
        {
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtpEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }

        private void TimerStart()
        {
            if (TimerReadFilePos == null)
            {
                TimerReadFilePos = new System.Threading.Timer(new System.Threading.TimerCallback(TimerCall), null, 0, 1000);
            }
            else
            {
                TimerReadFilePos.Change(0, 1000);
            }
        }

        private void TimerCall(object obj)
        {
            if (m_CurPlayState != PlayState.PlayStop && m_CurPlayState != PlayState.None)
            {
                GetFilePlayPos();
            }
        }

        private void GetFilePlayPos()
        {
            float pos = PlayCtrl.PlayM4_GetPlayPos(m_lPort); 
            this.pgbPlayPos.Value = Convert.ToInt32(pos * 100);
            if (pos == 1)
            {//播放结束
                StopVideo();
            }
        }

        private void StopVideo()
        {
            if (m_CurPlayState != PlayState.PlayStop && m_CurPlayState != PlayState.None)
            {
                if (PlayCtrl.PlayM4_Stop(m_lPort))
                {
                    if (PlayCtrl.PlayM4_CloseFile(m_lPort))
                    {
                        if (PlayCtrl.PlayM4_FreePort(m_lPort))
                        {
                            m_CurPlayState = PlayState.PlayStop;
                            SetCtrlEnable();
                        }
                    }
                    else
                    {
                        uint error = PlayCtrl.PlayM4_GetLastError(m_lPort);
                    }
                }
                else
                {
                    uint error = PlayCtrl.PlayM4_GetLastError(m_lPort);
                }
            }
        }

        private void SetCtrlEnable()
        {
            switch (m_CurPlayState)
            {
                case PlayState.PlayNormal:
                    sbtnStop.Enabled = true;
                    sbtnPause.Enabled = true;
                    sbtnNormal.Enabled = false;
                    sbtnSpeed.Enabled = true;
                    sbtnSlow.Enabled = true;
                    break;
                case PlayState.PlayStop:
                    sbtnStop.Enabled = false;
                    sbtnPause.Enabled = false;
                    sbtnNormal.Enabled = false;
                    sbtnSpeed.Enabled = false;
                    sbtnSlow.Enabled = false;
                    break;
                case PlayState.PlayPause:
                    sbtnStop.Enabled = true;
                    sbtnPause.Enabled = true;
                    sbtnNormal.Enabled = false;
                    sbtnSpeed.Enabled = false;
                    sbtnSlow.Enabled = false;
                    break;
                case PlayState.PlayRestore:
                    sbtnStop.Enabled = true;
                    sbtnPause.Enabled = true;
                    sbtnNormal.Enabled = true;
                    sbtnSpeed.Enabled = true;
                    sbtnSlow.Enabled = true;
                    break;
                case PlayState.PlayFast:
                    sbtnStop.Enabled = true;
                    sbtnPause.Enabled = true;
                    sbtnNormal.Enabled = true;
                    sbtnSpeed.Enabled = false;
                    sbtnSlow.Enabled = true;
                    break;
                case PlayState.PlaySlow:
                    sbtnStop.Enabled = true;
                    sbtnPause.Enabled = true;
                    sbtnNormal.Enabled = true;
                    sbtnSpeed.Enabled = true;
                    sbtnSlow.Enabled = false;
                    break;
            }
        }

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            string filepaht = FilePath + "\\";
            if (!Directory.Exists(filepaht))
            {
                MessageBox.Show("存放路径不存在！");
                return;
            }
            else
            {
                string starttime = dtpStart.Value.ToString("yyyyMMddHHmmss");
                string endtime = dtpEnd.Value.ToString("yyyyMMddHHmmss");
                GetSelFileName(filepaht, starttime, endtime);
                if (dicVideoInfo != null && dicVideoInfo.Count > 0)
                {
                    BindListView();
                }
                else
                {
                    MessageBox.Show("没有找到视频文件！");
                }
            }
        }

        private void BindListView()
        {
            try
            {
                if (dicVideoInfo != null && dicVideoInfo.Count > 0)
                {
                    lvVideoInfo.Items.Clear();
                    foreach (string key in dicVideoInfo.Keys)
                    {
                        ListViewItem lvi = new ListViewItem(key);
                        lvi.Tag = dicVideoInfo[key];
                        lvVideoInfo.Items.Add(lvi);
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        /// <summary>
        /// 获取播放文件
        /// </summary>
        /// <param name="filepath"></param>
        private void GetSelFileName(string filepath, string starttime, string endtime)
        {
            try
            {
                dicVideoInfo.Clear();
                DirectoryInfo theFolder = new DirectoryInfo(filepath);// DirectoryInfo   公开用于创建、移动和枚举目录和子目录的实例方法。
                DirectoryInfo[] dirInfo = theFolder.GetDirectories();
                //遍历文件夹
                long curfile = 0;
                long start = 0;
                long end = 0;
                foreach (DirectoryInfo NextFolder in dirInfo)
                {//文件夹只有年月日 没有时分秒
                    string folderName = NextFolder.Name;
                    if (PageValidate.IsNumber(folderName))
                    {
                        curfile = long.Parse(folderName);
                    }
                    else
                    {
                        continue;
                    }
                    start = long.Parse(starttime.Substring(0, 8));
                    end = long.Parse(endtime.Substring(0, 8));
                    if (curfile >= start && curfile <= end)
                    {
                        FileInfo[] fileInfo = NextFolder.GetFiles();
                        foreach (FileInfo NextFile in fileInfo)  //遍历文件
                        {//文件名有年月日 时分秒
                            string filename = NextFile.Name;
                            string[] splitfilename = filename.Split('.');
                            if (splitfilename.Length == 2)
                            {
                                if (PageValidate.IsNumber(splitfilename[0]))
                                {
                                    curfile = long.Parse(splitfilename[0]);
                                    start = long.Parse(starttime);
                                    end = long.Parse(endtime);
                                    if (curfile >= start && curfile <= end)
                                    {
                                        if (!dicVideoInfo.ContainsKey(NextFile.Name))
                                        {
                                            dicVideoInfo.Add(NextFile.Name, filepath + NextFolder.Name + "\\" + NextFile.Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        string curPlayFile = "";
        private void lvVideoInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvVideoInfo.SelectedItems.Count > 0)
            {
                ListViewItem lvi = new ListViewItem();
                lvi = lvVideoInfo.SelectedItems[0];
                //获取选中的值
                curPlayFile = lvi.Tag.ToString();
                StartPlayVideo(curPlayFile);
            }
        }

        private void StartPlayVideo(string filename)
        {
            if (filename != "")
            {
                StopVideo();
                if (PlayCtrl.PlayM4_GetPort(ref m_lPort))
                {
                    m_ptrRealHandle = picPlayBack.Handle;
                    m_FileRefDone = new PlayCtrl.PFILEREFDONE(PlayCallBack);
                    if (PlayCtrl.PlayM4_SetFileRefCallBack(m_lPort, m_FileRefDone, 0))
                    { }
                    if (PlayCtrl.PlayM4_OpenFile(m_lPort, filename))
                    {
                        if (PlayCtrl.PlayM4_Play(m_lPort, m_ptrRealHandle))
                        {
                            m_CurPlayState = PlayState.PlayNormal;
                            SetCtrlEnable();
                        }
                        else
                        {
                            uint error = PlayCtrl.PlayM4_GetLastError(m_lPort);
                        }
                    }
                }
                else
                {
                    uint error = PlayCtrl.PlayM4_GetLastError(m_lPort);
                }
            }
        }

        private void PlayCallBack(uint nPort, uint nUser)
        {
        }

        private void sbtnStop_Click(object sender, EventArgs e)
        {
            StopVideo();
        }

        private void sbtnNormal_Click(object sender, EventArgs e)
        {
            if (PlayCtrl.PlayM4_Play(m_lPort, m_ptrRealHandle))
            {
                m_CurPlayState = PlayState.PlayNormal;
                SetCtrlEnable();
            }
        }

        private void sbtnSpeed_Click(object sender, EventArgs e)
        {
            if (PlayCtrl.PlayM4_Play(m_lPort, m_ptrRealHandle))
            {//先恢复正常播放
                if (PlayCtrl.PlayM4_Fast(m_lPort))
                {
                    m_CurPlayState = PlayState.PlayFast;
                    SetCtrlEnable();
                }
            }
        }

        private void sbtnSlow_Click(object sender, EventArgs e)
        {
            if (PlayCtrl.PlayM4_Play(m_lPort, m_ptrRealHandle))
            {//先恢复正常播放
                if (PlayCtrl.PlayM4_Slow(m_lPort))
                {
                    m_CurPlayState = PlayState.PlaySlow;
                    SetCtrlEnable();
                }
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            if (m_CurPlayState != PlayState.PlayStop && m_CurPlayState != PlayState.None)
            {
                StopVideo();
            }
            this.Close();
        }

        private void sbtnPause_Click(object sender, EventArgs e)
        {
            if (sbtnPause.Text == "暂停")
            {
                sbtnPause.Text = "恢复";
                //this.btnPause.Image = global::HikHelp.Properties.Resources.恢复;
                if (PlayCtrl.PlayM4_Pause(m_lPort, 1))
                {
                    m_CurPlayState = PlayState.PlayPause;
                    SetCtrlEnable();
                }
            }
            else if (sbtnPause.Text == "恢复")
            {
                sbtnPause.Text = "暂停";
                //this.btnPause.Image = global::HikHelp.Properties.Resources.暂停;
                if (PlayCtrl.PlayM4_Pause(m_lPort, 0))
                {
                    m_CurPlayState = PlayState.PlayRestore;
                    SetCtrlEnable();
                }
            }
        }






    }

    public enum PlayState
    {
        PlayNormal,
        PlayFast,
        PlaySlow,
        PlayStop,
        PlayPause,
        PlayRestore,
        None,
    }

}
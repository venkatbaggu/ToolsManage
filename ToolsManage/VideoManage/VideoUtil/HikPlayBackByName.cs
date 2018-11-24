using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HikVideo;
using System.Runtime.InteropServices;
using ToolsManage.VideoManage;

namespace DAMSystem.UCControl.VideoUtil
{
    public partial class UCHikVideo : UCBaseVideo
    {
        private Int32 m_lFindHandle = -1;
        private Int32 m_lPlayHandle = -1;
        private Int32 m_lDownHandle = -1;
        private string str;
        private string str1;
        private string str2;
        private string str3;
        private string sPlayBackFileName = null;
        private Int32 i = 0;
        private Int32 m_lTree = 0;

        //private bool m_bPause = false;
        private bool m_bReverse = false;

        private long iSelIndex = 0;
        private uint dwAChanTotalNum = 0;
        private uint dwDChanTotalNum = 0;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo;
        public CHCNetSDK.NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40;
        public CHCNetSDK.NET_DVR_GET_STREAM_UNION m_unionGetStream;
        public CHCNetSDK.NET_DVR_IPCHANINFO m_struChanInfo;

        public override void PlayBackByName()
        {
            cursecond = 0;
            if (tcVideo.SelectedTab != tpPlayBackByName)
                tcVideo.SelectedTab = tpPlayBackByName;
            DateTime timeCur = DateTime.Now;
            dateTimeStart.Value = new System.DateTime(timeCur.Year, timeCur.Month, timeCur.Day, 0, 0, 0);
            dateTimeEnd.Value = new System.DateTime(timeCur.Year, timeCur.Month, timeCur.Day, 23, 59, 59);
            if (CurVideo != null)
            {
                ucVideoPlayBackByName.CameraType = CurVideo.CameraType;
                //StopPlayBack();
                m_iChanIndex = Convert.ToInt32(CurVideo.Tag);              
                //CloseAllVideo(false);
            }
        }

        #region 按文件名视频回放
        private void btnSearch_Click(object sender, EventArgs e)
        {
            listViewFile.Items.Clear();//清空文件列表 

            CHCNetSDK.NET_DVR_FILECOND_V40 struFileCond_V40 = new CHCNetSDK.NET_DVR_FILECOND_V40();

            //struFileCond_V40.lChannel = iChannelNum[(int)iSelIndex]; //通道号 Channel number
            struFileCond_V40.lChannel = CurVideo.ChannelNo;
            struFileCond_V40.dwFileType = 0xff; //0xff-全部，0-定时录像，1-移动侦测，2-报警触发，...
            struFileCond_V40.dwIsLocked = 0xff; //0-未锁定文件，1-锁定文件，0xff表示所有文件（包括锁定和未锁定）

            //设置录像查找的开始时间 Set the starting time to search video files
            struFileCond_V40.struStartTime.dwYear = dateTimeStart.Value.Year;
            struFileCond_V40.struStartTime.dwMonth = dateTimeStart.Value.Month;
            struFileCond_V40.struStartTime.dwDay = dateTimeStart.Value.Day;
            struFileCond_V40.struStartTime.dwHour = dateTimeStart.Value.Hour;
            struFileCond_V40.struStartTime.dwMinute = dateTimeStart.Value.Minute;
            struFileCond_V40.struStartTime.dwSecond = dateTimeStart.Value.Second;

            //设置录像查找的结束时间 Set the stopping time to search video files
            struFileCond_V40.struStopTime.dwYear = dateTimeEnd.Value.Year;
            struFileCond_V40.struStopTime.dwMonth = dateTimeEnd.Value.Month;
            struFileCond_V40.struStopTime.dwDay = dateTimeEnd.Value.Day;
            struFileCond_V40.struStopTime.dwHour = dateTimeEnd.Value.Hour;
            struFileCond_V40.struStopTime.dwMinute = dateTimeEnd.Value.Minute;
            struFileCond_V40.struStopTime.dwSecond = dateTimeEnd.Value.Second;

            //开始录像文件查找 Start to search video files 
            m_lFindHandle = CHCNetSDK.NET_DVR_FindFile_V40(m_lUserID, ref struFileCond_V40);

            if (m_lFindHandle < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_FindFile_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                MessageBox.Show(str);
                return;
            }
            else
            {
                CHCNetSDK.NET_DVR_FINDDATA_V30 struFileData = new CHCNetSDK.NET_DVR_FINDDATA_V30(); ;
                while (true)
                {
                    //逐个获取查找到的文件信息 Get file information one by one.
                    int result = CHCNetSDK.NET_DVR_FindNextFile_V30(m_lFindHandle, ref struFileData);

                    if (result == CHCNetSDK.NET_DVR_ISFINDING)  //正在查找请等待 Searching, please wait
                    {
                        continue;
                    }
                    else if (result == CHCNetSDK.NET_DVR_FILE_SUCCESS) //获取文件信息成功 Get the file information successfully
                    {
                        str1 = struFileData.sFileName;

                        str2 = Convert.ToString(struFileData.struStartTime.dwYear) + "-" +
                            Convert.ToString(struFileData.struStartTime.dwMonth) + "-" +
                            Convert.ToString(struFileData.struStartTime.dwDay) + " " +
                            Convert.ToString(struFileData.struStartTime.dwHour) + ":" +
                            Convert.ToString(struFileData.struStartTime.dwMinute) + ":" +
                            Convert.ToString(struFileData.struStartTime.dwSecond);

                        str3 = Convert.ToString(struFileData.struStopTime.dwYear) + "-" +
                            Convert.ToString(struFileData.struStopTime.dwMonth) + "-" +
                            Convert.ToString(struFileData.struStopTime.dwDay) + " " +
                            Convert.ToString(struFileData.struStopTime.dwHour) + ":" +
                            Convert.ToString(struFileData.struStopTime.dwMinute) + ":" +
                            Convert.ToString(struFileData.struStopTime.dwSecond);

                        listViewFile.Items.Add(new ListViewItem(new string[] { str1, str2, str3 }));//将查找的录像文件添加到列表中

                    }
                    else if (result == CHCNetSDK.NET_DVR_FILE_NOFIND || result == CHCNetSDK.NET_DVR_NOMOREFILE)
                    {
                        break; //未查找到文件或者查找结束，退出   No file found or no more file found, search is finished 
                    }
                    else
                    {
                        break;
                    }
                }

            }
        }

        private void listViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewFile.SelectedIndices.Count > 0)
            {
                sPlayBackFileName = listViewFile.FocusedItem.SubItems[0].Text;
            }
        }

        private void StartPlayBackByName()
        {
            if (sPlayBackFileName == null)
            {
                MessageBox.Show("Please select one file firstly!");//先选择回放的文件
                return;
            }

            if (m_lPlayHandle >= 0)
            {
                //如果已经正在回放，先停止回放
                if (!CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopPlayBack failed, error code= " + iLastErr; //停止回放失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }

                m_bReverse = false;
                btnReverse.Text = "Reverse";
                labelReverse.Text = "切换为倒放";

                m_bPause = false;
                btnPause.Text = "||";
                labelPause.Text = "暂停";

                m_lPlayHandle = -1;
                PlaybackprogressBar.Value = 0;
            }

            //按文件名回放
            m_lPlayHandle = CHCNetSDK.NET_DVR_PlayBackByName(m_lUserID, sPlayBackFileName, ucVideoPlayBackByName.Handle);
            if (m_lPlayHandle < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PlayBackByName failed, error code= " + iLastErr;
                MessageBox.Show(str);
                return;
            }

            uint iOutValue = 0;
            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PLAYSTART failed, error code= " + iLastErr; //回放控制失败，输出错误号
                MessageBox.Show(str);
                return;
            }
            timerPlayback.Interval = 1000;
            timerPlayback.Enabled = true;
            btnStopPlay.Enabled = true;
        }

        private void timerPlayback_Tick(object sender, EventArgs e)
        {
            PlaybackprogressBar.Maximum = 100;
            PlaybackprogressBar.Minimum = 0;

            uint iOutValue = 0;
            int iPos = 0;

            IntPtr lpOutBuffer = Marshal.AllocHGlobal(4);

            //获取回放进度
            CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYGETPOS, IntPtr.Zero, 0, lpOutBuffer, ref iOutValue);

            iPos = (int)Marshal.PtrToStructure(lpOutBuffer, typeof(int));

            if ((iPos > PlaybackprogressBar.Minimum) && (iPos < PlaybackprogressBar.Maximum))
            {
                PlaybackprogressBar.Value = iPos;
            }

            if (iPos == 100)  //回放结束
            {
                PlaybackprogressBar.Value = iPos;
                if (!CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopPlayBack failed, error code= " + iLastErr; //回放控制失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                m_lPlayHandle = -1;
                timerPlayback.Stop();
            }

            if (iPos == 200) //网络异常，回放失败
            {
                MessageBox.Show("The playback is abnormal for the abnormal network!");
                timerPlayback.Stop();
            }
            Marshal.FreeHGlobal(lpOutBuffer);
        }

        private void btnStartPlay_Click(object sender, EventArgs e)
        {
            if (sPlayBackFileName == null)
            {
                MessageBox.Show("Please select one file firstly!");//先选择回放的文件
                return;
            }

            if (m_lPlayHandle >= 0)
            {
                //如果已经正在回放，先停止回放
                if (!CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopPlayBack failed, error code= " + iLastErr; //停止回放失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }

                m_bReverse = false;
                btnReverse.Text = "Reverse";
                labelReverse.Text = "切换为倒放";

                m_bPause = false;
                btnPause.Text = "||";
                labelPause.Text = "暂停";

                m_lPlayHandle = -1;
                PlaybackprogressBar.Value = 0;
            }

            //按文件名回放
            m_lPlayHandle = CHCNetSDK.NET_DVR_PlayBackByName(m_lUserID, sPlayBackFileName, ucVideoPlayBackByName.PicHandle);
            if (m_lPlayHandle < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PlayBackByName failed, error code= " + iLastErr;
                MessageBox.Show(str);
                return;
            }

            uint iOutValue = 0;
            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PLAYSTART failed, error code= " + iLastErr; //回放控制失败，输出错误号
                MessageBox.Show(str);
                return;
            }
            timerPlayback.Interval = 1000;
            timerPlayback.Enabled = true;
            btnStopPlay.Enabled = true;
        }

        private void btnStopPlay_Click(object sender, EventArgs e)
        {
            if (m_lPlayHandle < 0)
            {
                return;
            }

            //停止回放
            if (!CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_StopPlayBack failed, error code= " + iLastErr;
                MessageBox.Show(str);
                return;
            }

            PlaybackprogressBar.Value = 0;
            timerPlayback.Stop();

            m_bReverse = false;
            btnReverse.Text = "Reverse";
            labelReverse.Text = "切换为倒放";

            m_bPause = false;
            btnPause.Text = "||";
            labelPause.Text = "暂停";

            m_lPlayHandle = -1;
            ucVideoPlayBackByName.Invalidate();//刷新窗口    
            btnStopPlay.Enabled = false;
        }

        private void listViewFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewFile.SelectedItems.Count > 0)
            {
                sPlayBackFileName = listViewFile.FocusedItem.SubItems[0].Text;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            uint iOutValue = 0;

            if (!m_bPause)
            {
                if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYPAUSE, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_PLAYPAUSE failed, error code= " + iLastErr; //回放控制失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                m_bPause = true;
                btnPause.Text = ">";
                labelPause.Text = "播放";
            }
            else
            {
                if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYRESTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_PLAYRESTART failed, error code= " + iLastErr; //回放控制失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                m_bPause = false;
                btnPause.Text = "||";
                labelPause.Text = "暂停";
            }
        }

        private void btnSlow_Click(object sender, EventArgs e)
        {
            uint iOutValue = 0;

            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSLOW, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PLAYSLOW failed, error code= " + iLastErr; //回放控制失败，输出错误号
                MessageBox.Show(str);
                return;
            }
        }

        private void btnFast_Click(object sender, EventArgs e)
        {
            uint iOutValue = 0;

            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYFAST, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PLAYFAST failed, error code= " + iLastErr; //回放控制失败，输出错误号
                MessageBox.Show(str);
                return;
            }
        }

        private void btnFrame_Click(object sender, EventArgs e)
        {
            uint iOutValue = 0;

            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYFRAME, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PLAYFRAME failed, error code= " + iLastErr; //回放控制失败，输出错误号
                MessageBox.Show(str);
                return;
            }
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            uint iOutValue = 0;

            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYNORMAL, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PLAYNORMAL failed, error code= " + iLastErr; //回放控制失败，输出错误号
                MessageBox.Show(str);
                return;
            }
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            uint iOutValue = 0;
            if (!m_bReverse)
            {
                if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAY_REVERSE, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_PLAY_REVERSE failed, error code= " + iLastErr; //回放控制失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                m_bReverse = true;
                btnReverse.Text = "正序播放";
                labelReverse.Text = "切换为正放";
            }
            else
            {
                if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAY_FORWARD, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_PLAY_FORWARD failed, error code= " + iLastErr; //回放控制失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                m_bReverse = false;
                btnReverse.Text = "倒序播放";
                labelReverse.Text = "切换为倒放";
            }
        }
        #endregion
    }
}

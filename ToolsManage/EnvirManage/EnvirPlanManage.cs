using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolsManage.Domain;

namespace ToolsManage.EnvirManage
{
    /// <summary>
    /// 环境计划管理
    /// </summary>
    public class EnvirPlanManage
    {
        public static EventWaitHandle hThdSend = new EventWaitHandle(true, EventResetMode.ManualReset);
        private  ConcurrentDictionary<int, Dictionary<int,TaskPlan>> dicDeviceplan = null;
        public EnvirPlanManage()
        {
      
        }

        public void Start()
        {
            CheckTask();
        }

        private void GetDevicePlan()
        {
            try
            {
                IList<TbDeviceplan> list = TbDeviceplan.FindAll("IsValid=1", "ID ASC", "", 0, 0);
                if (list != null && list.Count > 0)
                {
                    if (dicDeviceplan == null)
                        dicDeviceplan = new ConcurrentDictionary<int, Dictionary<int, TaskPlan>>();
                    list = list.FindAll(p => p.DeviceName == "新风");
                    if (list != null && list.Count > 0)
                    {
                        foreach (TbDeviceplan tdp in list)
                        {
                            int starthour = Convert.ToInt16(tdp.StartTime.Split(':')[0]);
                            int startmin = Convert.ToInt16(tdp.StartTime.Split(':')[1]);
                            int stophour = Convert.ToInt16(tdp.StopTime.Split(':')[0]);
                            int stopmin = Convert.ToInt16(tdp.StopTime.Split(':')[1]);
                            if (!dicDeviceplan.ContainsKey(tdp.DeviceType))
                            {
                                dicDeviceplan.TryAdd(tdp.DeviceType, new Dictionary<int, EnvirManage.TaskPlan>());
                            }                      
                            if (!dicDeviceplan[tdp.DeviceType].ContainsKey(tdp.PlanNo))
                            {
                                dicDeviceplan[tdp.DeviceType].Add(tdp.PlanNo, null);
                                
                                TaskPlan tp = new TaskPlan()
                                {
                                    DeviceID = tdp.DeviceType,
                                    DeviceName = tdp.DeviceName,
                                    TaskStartHour = starthour,
                                    TaskStartMin = startmin,
                                    TaskStopHour = stophour,
                                    TaskStopMin = stopmin,
                                    IsStart = false,
                                    LastExecuteTime = DateTime.Now,
                                };
                                dicDeviceplan[tdp.DeviceType][tdp.PlanNo] = tp;
                            }
                            else
                            {
                                if (tdp.IsValid == false)
                                {
                                    dicDeviceplan[tdp.DeviceType].Remove(tdp.PlanNo);
                                }
                                else
                                {
                                    dicDeviceplan[tdp.DeviceType][tdp.PlanNo].TaskStartHour = starthour;
                                    dicDeviceplan[tdp.DeviceType][tdp.PlanNo].TaskStartMin = startmin;
                                    dicDeviceplan[tdp.DeviceType][tdp.PlanNo].TaskStopHour = stophour;
                                    dicDeviceplan[tdp.DeviceType][tdp.PlanNo].TaskStopMin = stopmin;
                                }
                            }                           
                        }
                    }
                }
            }
            catch(Exception ex)
            { }
        }

        private void CheckTask()
        {
            Task task = new Task(() => CheckTaskMethod());
            task.Start();
        }

        private void CheckTaskMethod()
        {
            while (true)
            {
                try
                {

                    GetDevicePlan();
                    if (dicDeviceplan != null && dicDeviceplan.Count > 0)
                    {
                        int XFType = 1;
                        if (dicDeviceplan.ContainsKey(XFType))
                        {//新风系统
                            if (dicDeviceplan[XFType] != null && dicDeviceplan[XFType].Count > 0)
                            {
                                foreach (int key in dicDeviceplan[XFType].Keys)
                                {
                                    DateTime now = DateTime.Now;
                                    int hour2 = now.Hour;
                                    int minute3 = now.Minute;
                                    if (hour2 == dicDeviceplan[XFType][key].TaskStartHour)
                                    {
                                        if (minute3 == dicDeviceplan[XFType][key].TaskStartMin)
                                        {//启动开启任务
                                            frmMain.envirControl.ExecuteTask(XFType, BaseClass.OnOffRelay.开启);
                                        }
                                    }
                                    if (hour2 == dicDeviceplan[XFType][key].TaskStopHour)
                                    {
                                        if (minute3 == dicDeviceplan[XFType][key].TaskStopMin)
                                        {//启动停止任务
                                            frmMain.envirControl.ExecuteTask(XFType, BaseClass.OnOffRelay.关闭);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //跳过这一分钟
                    Thread.Sleep(1000 * (60 - DateTime.Now.Second));
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }

    public class TaskPlan
    {
        private int m_DeviceID;

        private string m_DeviceName;

        private int m_TaskStartHour;

        private int m_TaskStartMin;

        private int m_TaskStopHour;

        private int m_TaskStopMin;

        private bool m_IsStart;

        private DateTime m_LastExecuteTime;

        public int DeviceID
        {
            get
            {
                return m_DeviceID;
            }

            set
            {
                m_DeviceID = value;
            }
        }

        public string DeviceName
        {
            get
            {
                return m_DeviceName;
            }

            set
            {
                m_DeviceName = value;
            }
        }

        public bool IsStart
        {
            get
            {
                return m_IsStart;
            }

            set
            {
                m_IsStart = value;
            }
        }

        public DateTime LastExecuteTime
        {
            get
            {
                return m_LastExecuteTime;
            }

            set
            {
                m_LastExecuteTime = value;
            }
        }

        public int TaskStartHour
        {
            get
            {
                return m_TaskStartHour;
            }

            set
            {
                m_TaskStartHour = value;
            }
        }

        public int TaskStartMin
        {
            get
            {
                return m_TaskStartMin;
            }

            set
            {
                m_TaskStartMin = value;
            }
        }

        public int TaskStopHour
        {
            get
            {
                return m_TaskStopHour;
            }

            set
            {
                m_TaskStopHour = value;
            }
        }

        public int TaskStopMin
        {
            get
            {
                return m_TaskStopMin;
            }

            set
            {
                m_TaskStopMin = value;
            }
        }
    }
}

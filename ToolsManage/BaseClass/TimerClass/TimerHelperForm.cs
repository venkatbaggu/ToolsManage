using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    public class TimerHelperForm
    {
        public System.Windows.Forms.Timer timer;
        private int timerInterval;
        private TimerState timerState;

        /// <summary>
        /// 定时器执行操作的函数原型
        /// </summary>
        public delegate void TimerExecution();

        /// <summary>
        /// 定时器执行时调用的操作
        /// </summary>
        public event TimerExecution Execute;

        /// <summary>
        /// 创建一个指定时间间隔的定时器，并在指定的延迟后开始启动。（默认间隔为100毫秒）
        /// </summary>
        public TimerHelperForm()
        {
            timerInterval = 100;
            timerState = TimerState.Stopped;
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new System.EventHandler(Tick);
        }

        /// <summary>
        /// 创建一个指定时间间隔的定时器
        /// </summary>
        /// <param name="interval">定时器执行操作的间隔时间（毫秒）</param>
        /// <param name="start">是否启动</param>
        public TimerHelperForm(int interval, bool start)
        {
            timer = new System.Windows.Forms.Timer();
            timerInterval = interval;
            timer.Interval = timerInterval;
            timer.Tick += new System.EventHandler(Tick);

            if (start)
            {
                timer.Enabled = true;
                timerState = TimerState.Running;
            }
            else
            {
                timer.Enabled = false;
                timerState = TimerState.Stopped;
            }
        }

        public void  Dispose()
        {
            timer.Dispose();
        }

        /// <summary>
        /// 立即启动定时器
        /// </summary>
        public void Start()
        {
            timerState = TimerState.Running;
            timer.Interval = timerInterval;
            timer.Enabled = true;
        }

        /// <summary>
        /// 停止定时器
        /// 注意：运行中的线程不会被停止
        /// </summary>
        public void Stop()
        {
            timerState = TimerState.Stopped;
            timer.Enabled = false;
        }

        private void Tick(object sender, EventArgs e)
        {
            if (timerState == TimerState.Running && Execute != null)
            {
                lock (this)
                {
                    Execute();
                }
            }
        }

        /// <summary>
        /// 定时器的状态
        /// </summary>
        public TimerState State
        {
            get
            {
                return timerState;
            }
        }

    }
}

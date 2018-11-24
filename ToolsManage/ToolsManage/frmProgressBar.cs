using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ToolsManage.BaseClass.OtherClass;
using ToolsManage.BaseClass;

namespace ToolsManage.ToolsManage
{
    public partial class frmProgressBar : DevExpress.XtraEditors.XtraForm
    {
        AppConfig config = new AppConfig();

        private int iTime = 5;

        public frmProgressBar()
        {
            InitializeComponent();
        }

        private void frmProgressBar_Load(object sender, EventArgs e)
        {
            try
            {
                string strMarkTime = config.AppConfigGet("MarkTmie");
                if (!string.IsNullOrEmpty(strMarkTime))
                {
                    iTime = Convert.ToInt32(strMarkTime);
                }

                //设置一个最小值
                progressBarControl1.Properties.Minimum = 0;
                //设置一个最大值
                progressBarControl1.Properties.Maximum = iTime;
                //设置步长，即每次增加的数
                progressBarControl1.Properties.Step = 1;
                //设置进度条的样式
                progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                progressBarControl1.Position = 0;
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 


            //for (int i = 0; i < progressBarControl1.Properties.Maximum; i++)
            //{
            //    //处理当前消息队列中的所有windows消息
            //    Application.DoEvents();
            //    //当前线程挂起指定的时间,这个是为了演示
            //    System.Threading.Thread.Sleep(12);
            //    //执行步长
            //    progressBarControl1.PerformStep();
            //}
        }

        int iCount = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBarControl1.PerformStep();
            iCount++;
            if (iCount > iTime)
            {
                this.Close();
            }
        }


    }
}
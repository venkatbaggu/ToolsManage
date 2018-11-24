using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;
using System.Diagnostics;

using ToolsManage.BaseClass;

namespace ToolsManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

            //DevExpress.UserSkins.BonusSkins.Register();
            //DevExpress.Skins.SkinManager.EnableFormSkins();
            //UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmLogin());//frmMain  frmLogin


            Process instance = StartupHelper.RunningInstance();
            if (instance == null)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.Skins.SkinManager.EnableFormSkins();
                UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SplashScreenManager.ShowForm(typeof (frmStart ));
                 
                Application.Run(new frmMain());//frmMain  frmLogin


                

                //gc.MainDialog = new MainForm();
                //gc.MainDialog.StartPosition = FormStartPosition.CenterScreen;

                //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //Application.Run(gc.MainDialog);

            }
            else
            {
                //如果已经存在进程，那么提示信息，并切换到存在的界面    
                StartupHelper.HandleRunningInstance(instance, "系统已经在运行！");
            } 



        }
    }
}

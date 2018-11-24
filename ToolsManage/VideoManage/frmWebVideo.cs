using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Threading;

using ToolsManage.BaseClass;
using ToolsManage.BaseClass.OtherClass;

namespace ToolsManage.VideoManage
{
    public partial class frmWebVideo : DevExpress.XtraEditors.XtraForm
    {
        System.Threading.Tasks.Task task = null;
        public frmWebVideo()
        {
            InitializeComponent();
            try
            {
                AppConfig config = new AppConfig();
                string strIp = config.AppConfigGet("VideoIP");
                strIp = "http://" + strIp;
                webBrowser1.Navigate(strIp);
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
        }

        private void PreviewVideo()
        {
            task = new System.Threading.Tasks.Task(() =>
            {
                //ChangeStatus(TaskStatus.进行中);
                //Executer.Do();
                OpenWeb(task);  //第一包
            });
            task.Start();            
        }

        private void OpenWeb(System.Threading.Tasks.Task task)
        {
            try
            {
                AppConfig config = new AppConfig();
                string strIp = config.AppConfigGet("VideoIP");
                strIp = "http://" + strIp;
                webBrowser1.Navigate(strIp);
                //while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                //{
                //    //等待  
                //    Thread.Sleep(100);          

                //}
                Thread.Sleep(2000);
                webBrowser1.Document.GetElementById("username").InnerText = "admin";
                webBrowser1.Document.GetElementById("password").Focus();
                webBrowser1.Document.GetElementById("password").InnerText = "admin12345";
                webBrowser1.Document.GetElementById("username").Focus();
                //task.ContinueWith((a) => {

                //    //HtmlElementCollection htmlele = webBrowser1.Document.GetElementsByTagName("button");
                //    //htmlele[0].Focus();
                //});     
                task.ContinueWith((a) => { Thread.Sleep(2000); spanClick("ng-binding", "label"); });

                task.ContinueWith((a) => { Thread.Sleep(3000); spanClick("icon-playall", "i"); });
            }
            catch(Exception ex)
            { }
        }

        private bool spanClick(string className)
        {
            bool findBtn = false;
            try
            {
                
                HtmlElementCollection htmlele = webBrowser1.Document.GetElementsByTagName("span");
                foreach (HtmlElement item in htmlele)
                {
                    if (item.OuterHtml.IndexOf(className) != -1)
                    {
                        item.InvokeMember("click");
                        findBtn = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
            return findBtn;
        }

        private void initTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                initTimer.Enabled = false;
                webBrowser1.Document.GetElementById("username").InnerText = "admin";
                //webBrowser1.Document.GetElementById("password").InnerText = "admin12345";
                //webBrowser1.Document.GetElementById("username").SetAttribute("value", "admin");
                //webBrowser1.Document.GetElementById("password").SetAttribute("value", "admin12345");
                //webBrowser1.Document.GetElementById("username").Focus();
                webBrowser1.Document.GetElementById("password").Focus();
                webBrowser1.Document.GetElementById("password").InnerText = "admin12345";
                webBrowser1.Document.GetElementById("username").Focus();
                //HtmlElementCollection htmlele = webBrowser1.Document.GetElementsByTagName("button");
                //htmlele[0].Focus();
                bool find = spanClick("ng-binding", "label");
                if (find)
                {
                    timer2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                spanClick("icon-playall", "i");
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
        }

        private bool spanClick(string className, string tagName)
        {
            bool findBtn = false;
            try
            {
                HtmlElementCollection htmlele = webBrowser1.Document.GetElementsByTagName(tagName);
                foreach (HtmlElement item in htmlele)
                {
                    if (item.OuterHtml.IndexOf(className) != -1)
                    {
                        item.InvokeMember("click");
                        findBtn = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
            return findBtn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool find = spanClick("btn", "button");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url != webBrowser1.Document.Url) return;
            // 当 e.Url == webBrowser1.Document.Url 我们要的网页加载完毕
            // 加载完毕
            // ...
        }

        private void frmWebVideo_Load(object sender, EventArgs e)
        {
            //PreviewVideo();
        }
    }
}
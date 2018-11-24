using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    /// <summary>
    /// 更新主界面 书界面
    /// </summary>
    public class clsNewTreeView
    {
        private string strToolId;
        private string strContent;
        private bool blShow;



        public clsNewTreeView(string toolId, string content,bool show)
        {
            strToolId = toolId;
            strContent = content;
            blShow = show;
        }

        public bool BlShow
        {
            get { return blShow; }
            set { blShow = value; }
        }

        public string StrContent
        {
            get { return strContent; }
            set { strContent = value; }
        }

        public string StrToolId
        {
            get { return strToolId; }
            set { strToolId = value; }
        }

    }
}

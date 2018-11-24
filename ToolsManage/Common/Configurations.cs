using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsManage.Common
{
    public class Configurations
    {
        public static bool Is485XF => GetValue<bool>("Is485XF");

        public static bool IsStartPlanTask => GetValue<bool>("IsStartPlanTask");

        public static string VideoIP=> GetValue<string>("VideoIP");

        public static int VideoPort => GetValue<int>("VideoPort");

        public static string VideoUserName => GetValue<string>("VideoUserName");
        public static string VideoPSW => GetValue<string>("VideoPSW"); 

        public static int CameraCount => GetValue<int>("CameraCount"); 

        public static bool IsDisDebug => GetValue<bool>("IsDisDebug");

        private static T GetValue<T>(string key)
        {
            try
            {
                return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}

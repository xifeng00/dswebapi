using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config
{
    //公共配置静态变量
    public class config
    {
        public static log4net.ILog configLog = null;
        public static void outlog(string className,string message)
        {
            configLog.Info(className + ":" + message);
        }
    }
}

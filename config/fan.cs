using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fan
{
    public class fan
    {
        public static string toStr(object obj)
        {
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }
        public static  bool toBool(int i)
        {
            if (i == 0) 
            { 
                return false; 
            }
            else
            {
                return true;
            }
        }
    }
}

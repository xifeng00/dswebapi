using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fan
{
    public class Fan
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
        public static int toInt(object num)
        {
            int toint = 0;
            try
            {
                toint = Convert.ToInt32(num);
            }
            catch
            {
                toint = 0;
            }
            return toint;
        }
    }
}

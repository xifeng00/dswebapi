using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace json
{
    public class ujson
    {
        public static string ToStr(object _j)
        {
            return JsonConvert.SerializeObject(_j);
        }
        public static T toClass<T>(string jsonstr)
        {
            try
            {
                return (T)JsonConvert.DeserializeObject(jsonstr);
            }
            catch(Exception ee)
            {
                config.config.outlog("ujson.toClass",ee.Message);
                return default;
            }
        }

    }
}

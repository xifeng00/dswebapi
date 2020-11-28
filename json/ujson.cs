using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace json
{
    public class ujson
    {
        private static string logclassname = "ujson.";
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
        public static string toStr<T>(List<T> list_t)
        {

            JsonSerializer serializer = new JsonSerializer();
            StringWriter sw = new StringWriter();
            serializer.Serialize(new JsonTextWriter(sw), list_t);
           return sw.GetStringBuilder().ToString();
        }
        public static List<T> toList<T>(string jsonstr)
        {
            List<T> lslsist = null;
            try
            {
                lslsist= JsonConvert.DeserializeObject<List<T>>(jsonstr);
            }
            catch(Exception ee)
            {
                config.config.outlog(logclassname + "toList<T>", ee.Message);
            }
            return lslsist;
          
        }

    }
}

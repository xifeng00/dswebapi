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
        public static object toObject(string jsonstr)
        {
            return JsonConvert.DeserializeObject( jsonstr);
        }

    }
}

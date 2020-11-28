using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dswebapi.Models;

namespace dswebapi.db.cache
{
    public class DbCache
    {
        //缓存容器
        private static Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();
        /// <summary>
        /// 添加缓存
        /// </summary>
        public static void CacheAddOne(object value)
        {
            string key = ((DataBase)value).id;
            if (!CacheDictionary.ContainsKey(key))
            {
                CacheDictionary.Add(key, value);
            }
        }
        /// <summary>
        /// 一次添加多个对象 如List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void CacheAdds<T>(List<T> tlist)
        {
           
                foreach (object obj in tlist)
                {
                    CacheAddOne(obj);
                }
           
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        public static T CacheGet<T>(string key)
        {
            if (key == null) { return default; }
            if (CacheDictionary.ContainsKey(key))
                return (T)CacheDictionary[key];
            else
                return default;
        }
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool CacheExsits(object obj)
        {
            return CacheDictionary.ContainsKey(((DataBase)obj).id);
        }
        public static bool CacheDelete(object obj)
        {
           return CacheDictionary.Remove(((DataBase)obj).id);
        }
        public static void CacheUpdate(object obj)
        {
             CacheDictionary[((DataBase)obj).id] = obj;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace dswebapi.Util
{
    public class SessionUtil
    {
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetSession(HttpContext content, string key, string value)
        {
            content.Session.SetString(key, value);
        }
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        public string GetSession(HttpContext context, string key, string defaultValue = "")
        {
            string value = context.Session.GetString(key);
            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }
            return value;
        }
    }
}

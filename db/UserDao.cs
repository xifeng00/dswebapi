using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.Encrypt;
using dswebapi.Models;
using dswebapi.Util;

namespace dswebapi.db
{
    public class UserDao
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(UserDao));
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public User login(string account, string pwd)
        {
            string jmpwd = EncryptProvider.Md5(pwd);
            string sql = "select * from \"user\" where account='" + account + "' and pwd='"+ jmpwd + "'";
            List<User> userls= dbdao.DbSql<User>(sql);
            if (userls.Count > 0)
            {
                log.Info("用户登录：" + account + " 时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:sss"));
                CacheHelperNetCore.CacheInsertFromMinutes("cache", userls[0].id.ToString(), 10);
                User user = (User)userls[0];
                return user;
            }
            else
                return null;
        }

        /// <summary>
        /// 验证用户账户 不允许重复
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool accountVerification(string account)
        {
            string sql = "select * from \"user\" where account='" + account + "'";
            List<User> userls = dbdao.DbSql<User>(sql);
            if (userls.Count > 0)
            {
                return false;
            }
            else
                return true;
        }
    }
}

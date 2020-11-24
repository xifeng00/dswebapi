using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.Encrypt;
using dswebapi.Models;

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
        public bool login(string name, string pwd)
        {
            string jmpwd = EncryptProvider.Md5(pwd);
            string sql = "select * from user where name='"+name+"' and pwd='"+pwd+"'";
            List<User> userls= dbdao.DbSql<User>(sql);
            if (userls.Count > 0)
            {
                log.Info("用户登录：" + name + " 时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:sss"));
                return true;
            }
            else
                return false;
        }
    }
}

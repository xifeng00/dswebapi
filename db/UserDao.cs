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
                return true;
            else
                return false;
        }
    }
}

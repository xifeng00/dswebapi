using dswebapi.db;
using dswebapi.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NETCore.Encrypt;

namespace dswebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(UserController));

        public UserController()
        {
            this.userDao = new UserDao();
        }

        private UserDao userDao;
        [HttpGet]
        public bool login(string account, string pwd)
        {
            return userDao.login(account, pwd);
        }

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<User> users = db.dbdao.GetList<User>();
                StringBuilder sb = new StringBuilder();
                foreach (Models.User a in users)
                {
                    sb.Append(JsonConvert.SerializeObject(a));
                    sb.Append("\r\n");
                }
                //log.Info($"testController-GetArea:{sb.ToString()}");
                log.Info(sb);

                return sb.ToString();
            }
            catch (Exception ee)
            {
                log.Error(ee.Message);
                return ee.Message;
            }
        }
        [HttpPost]
        public bool save([FromBody] User user)
        {
            User temp = db.dbdao.GetById<User>(user.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(user);
            }
            else
            {
                user.pwd= EncryptProvider.Md5(user.pwd);//密码加密过程
                return dbdao.DbInsert(user);
            }
        }
    }
}

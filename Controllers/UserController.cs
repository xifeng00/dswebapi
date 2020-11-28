using dswebapi.db;
using dswebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NETCore.Encrypt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace dswebapi.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> login(string account, string pwd)
        {
            if (account == "" || pwd == ""||account==null || pwd==null)
            {
                return Content("没有登录") ;
            }
            User user = userDao.login(account, pwd);
            if (user!=null)
            {
                var claims = new List<Claim>(){
                        new Claim(ClaimTypes.Name,user.id)
                };
                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(2),//有效时间20分钟
                    IsPersistent = false,
                    AllowRefresh = false
                });
                JsonResult result = new JsonResult(user);
                return result;
            }
            return Content("用户名密码错误"); 
        }                               

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            object cc = HttpContext.User.Identity.Name;
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
        /// <summary>
        /// 验证用户账户 不允许重复
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool accountVerification(string account)
        {
            return userDao.accountVerification(account);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            return dbdao.GetList<User>();
        }
    }
}

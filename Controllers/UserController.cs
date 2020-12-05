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
using Microsoft.Extensions.Logging;

namespace dswebapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private string userid = "";
        private User curUser = null;
        public UserController(ILogger<UserController> logger,
            ILoggerFactory loggerFactory)
        {
            this.userDao = new UserDao();
            _logger = logger;
            this._loggerFactory = loggerFactory;
        }

        private UserDao userDao;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> login(string account, string pwd)
        {
            if (account == "" || pwd == ""||account==null || pwd==null)
            {
                return Content("nologin") ;
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
                    ExpiresUtc = DateTime.Now.AddMinutes(30),//有效时间20分钟
                    IsPersistent = false,
                    AllowRefresh = false
                });
                JsonResult result = new JsonResult(user);
                _logger.LogInformation(user.account+"登录");
                return result;
            }
            return Content("no login"); 
        }                               

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string GetUserAll()
        {
            object cc = HttpContext.User.Identity.Name;
            try
            {

                List<User> users = db.dbdao.DbSql<User>("select * from public.user order by num");
                string result_str = json.ujson.toStr<User>(users);
                _logger.LogInformation(result_str);
                return result_str;

            }
            catch (Exception ee)
            {
                _logger.LogError(ee.Message);
                return ee.Message;
            }
        }
        [HttpGet]
        public string InsertUser()
        {
            try
            {
                try
                {
                    List<User> roles1 = db.dbdao.DbSql<User>("select * from public.user where name='' and num=9999");
                    if (roles1.Count > 0)
                    {
                        return "重复新增";
                    }
                    this.userid = (string)HttpContext.User.Identity.Name;
                    this.curUser = db.dbdao.GetById<User>(userid);
                    User r = new User();
                    r.id = Guid.NewGuid().ToString();
                    r.account = "121-1211-1211";
                    r.name = "";
                    r.num = 9999;
                    r.createtime = DateTime.Now;
                    r.updatetime = DateTime.Now;
                    r.createuserid = this.userid;
                    r.tel = "";
                    r.pwd = "DC5C7986DAEF50C1E02AB09B442EE34F";
                    r.roleids = "";
                    r.rolenames = "";
                    r.organid = "";
                    r.organname = "";
                    r.managerorganids = "";
                    r.managerorgannames = "";

                    if (db.dbdao.DbInsert<User>(r))
                    {
                        return "ok";
                    }
                    else
                    {
                        return "failed";
                    }
                    _logger.LogDebug(curUser.account + "插入角色数据" + r.id);
                }
                catch (Exception ee)
                {
                    _logger.LogDebug(ee.Message);
                    return ee.Message;
                }

            }
            catch (Exception ee)
            {
                _logger.LogError(ee.Message);
                return ee.Message;
            }
        }
        [HttpPost]
        public string DeleteUser([FromBody] User user)
        {
            try
            {
                //Address lsadd1 = db.dbdao.GetById<Address>("ss");
                this.userid = (string)HttpContext.User.Identity.Name;
                this.curUser = db.dbdao.GetById<User>(userid);

                if (db.dbdao.DbDelete<User>(user))
                {
                    _logger.LogDebug(this.curUser.account + " 删除用户数据出错" + json.ujson.ToStr(user));
                    return user.id + "ok";
                }
                else
                {
                    return "failed";
                }

            }
            catch (Exception ee)
            {
                _logger.LogError(ee.Message);
                return ee.Message;
            }
            //return device.id;
        }

        [HttpPost]
        public string SaveUser([FromBody] User user)
        {

            this.userid = (string)HttpContext.User.Identity.Name;
            this.curUser = db.dbdao.GetById<User>(userid);
            if (dbdao.DbUpdate(user))
            {
                _logger.LogDebug(this.curUser.account + " 保存用户数据" + json.ujson.ToStr(user));
                return "ok";
            }
            else
            {
                _logger.LogDebug(this.curUser.account + " 保存用户数据出错" + json.ujson.ToStr(user));
                return "failed";
            }
            //return device.id;
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

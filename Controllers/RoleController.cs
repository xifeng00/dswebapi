using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dswebapi.db;
using System.Text;
using Newtonsoft.Json;
using dswebapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace dswebapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private string userid = "";
         private User curUser =null;
        public RoleController(ILogger<RoleController> logger,
            ILoggerFactory loggerFactory)
        {
            _logger = logger;
            this._loggerFactory = loggerFactory;
        }
        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string GetRole()
        {
            try
            {

                List<Role> roles = db.dbdao.DbSql<Role>("select * from public.role order by num");
                string result_str = json.ujson.toStr<Role>(roles);
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
        public string InsertRole()
        {
            try
            {
                try
                {
                    List<Role> roles1 = db.dbdao.DbSql<Role>("select * from public.role where name='' and num=999");
                    if(roles1.Count>0)
                    {
                        return "重复新增";
                    }
                    this.userid = (string)HttpContext.User.Identity.Name;
                    this.curUser = db.dbdao.GetById<User>(userid);
                    Role r = new  Role();
                    r.id = Guid.NewGuid().ToString();
                    r.interfaceids = "";
                    r.interfacenames = "";
                    r.name = "";
                    r.num = 999;
                    r.createtime = DateTime.Now;
                    r.updatetime = DateTime.Now;
                    r.createuserid = this.userid;
                   
                    if (db.dbdao.DbInsert<Role>(r))
                    {
                        return "ok";
                    }
                    else
                    {
                        return "failed";
                    }
                    _logger.LogDebug(curUser.account + "插入用户数据" + r.id);
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
        public string DeleteRole([FromBody] Role role)
        {
            try
            {
                //Address lsadd1 = db.dbdao.GetById<Address>("ss");
                this.userid = (string)HttpContext.User.Identity.Name;
                this.curUser = db.dbdao.GetById<User>(userid);

                if (db.dbdao.DbDelete<Role>(role))
                {
                    _logger.LogDebug(this.curUser.account + " 删除角色数据出错" + json.ujson.ToStr(role));
                    return role.id + "ok";
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
        public string SaveRole([FromBody] Role role)
        {

            this.userid = (string)HttpContext.User.Identity.Name;
            this.curUser = db.dbdao.GetById<User>(userid);
            if (dbdao.DbUpdate(role))
            {
                _logger.LogDebug(this.curUser.account + " 保存角色数据" + json.ujson.ToStr(role));
                return "ok";
            }
            else
            {
                _logger.LogDebug(this.curUser.account + " 保存角色数据出错" + json.ujson.ToStr(role));
                return "failed";
            }
            //return device.id;
        }

    }
}


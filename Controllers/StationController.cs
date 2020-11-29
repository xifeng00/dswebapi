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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

namespace dswebapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StationController : ControllerBase
    {
        private readonly ILogger<StationController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private string userid = "";
        private User curUser = null;
        public StationController(ILogger<StationController> logger,
            ILoggerFactory loggerFactory)
        { 
            _logger = logger;
            this._loggerFactory = loggerFactory;
            
        }
        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                this.userid = (string)HttpContext.User.Identity.Name;
                this.curUser = db.dbdao.GetById<User>(userid);
                List<Station> stations = db.dbdao.GetList<Station>();
                string result_str = json.ujson.toStr<Station>(stations);             
                _logger.LogInformation(result_str);
                return result_str;
            }
            catch (Exception ee)
            {
                _logger.LogDebug(ee.Message);
                return ee.Message;
            }
        }
        [HttpGet]
        public string Insert()
        {
            try
            {
                this.userid = (string)HttpContext.User.Identity.Name;
                this.curUser = db.dbdao.GetById<User>(userid);
                Station s = new Station();
                s.addressid = "00000-00000-00000-00000-00001";
                s.addressname = "modeladdress";
                s.id = Guid.NewGuid().ToString();
                s.fzr = "负责人";
                s.creattime = DateTime.UtcNow;
                s.updatetime = DateTime.UtcNow;
                s.tel = "00000000000";
                s.remark = "news";
                s.name = "system new ";
                s.creatuserid = this.userid;
                if (db.dbdao.DbInsert<Station>(s))
                {
                    return "ok";
                }
                else
                {
                    return "failed";
                }
                _logger.LogDebug(curUser.account + "插入场站数据" + s.id);
            }
            catch (Exception ee)
            {
                _logger.LogDebug(ee.Message);
                return ee.Message;
            }
        }
        [HttpPost]
        public string Save([FromBody] Station station)
        {
            this.userid = (string)HttpContext.User.Identity.Name;
            this.curUser = db.dbdao.GetById<User>(userid);
            if (dbdao.DbUpdate(station))
            {
                _logger.LogDebug(this.curUser.account + " 保存场站数据" + json.ujson.ToStr(station));
                return "ok";
            }
            else
            {
                _logger.LogDebug(this.curUser.account + " 保存场站数据出错" + json.ujson.ToStr(station));
                return "failed";
            }
        }
        /// <summary>
        /// 删除场站
        /// </summary>
        /// <param name="lsadd1"></param>
        /// <returns></returns>
        [HttpPost]
        public string Delete([FromBody] Station lsastation)
        {
            try
            {
                //Address lsadd1 = db.dbdao.GetById<Address>("ss");
                this.userid = (string)HttpContext.User.Identity.Name;
                this.curUser = db.dbdao.GetById<User>(userid);

                if (db.dbdao.DbDelete<Station>(lsastation))
                {
                    _logger.LogDebug(this.curUser.account + " 删除场站数据出错" + json.ujson.ToStr(lsastation));
                    return lsastation.id + "ok";
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
        }

        [HttpGet]
        public string GetAreaAll()
        {
            try
            {
                List<Area> areas = db.dbdao.DbSql<Area>("SELECT * from  area ORDER BY id,parent_id");
                return json.ujson.toStr<Area>(areas);
            }
            catch (Exception ee)
            {
                _logger.LogDebug(ee.Message);
                return ee.Message;
            }
        }
       
       
    }
}

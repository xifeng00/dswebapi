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
    /// <summary>
    /// 组织信息维护
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrganController : ControllerBase
    {
        private readonly ILogger<OrganController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private string userid = "";
        private User curUser = null;
        public OrganController(ILogger<OrganController> logger,
            ILoggerFactory loggerFactory)
        { 
            _logger = logger;
            this._loggerFactory = loggerFactory;
            
        }
        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string GetOrgan()
        {
            try
            {
                this.userid = (string)HttpContext.User.Identity.Name;
                this.curUser = db.dbdao.GetById<User>(userid);
                List<Organ> organs = db.dbdao.DbSql<Organ>("select * from public.organ  order by num");
                string result_str = json.ujson.toStr<Organ>(organs);             
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
        public string InsertOrgan(string parentid)
        {
            try
            {
                this.userid = (string)HttpContext.User.Identity.Name;
                this.curUser = db.dbdao.GetById<User>(userid);
                Organ or = new Organ();
                or.id = Guid.NewGuid().ToString();
                or.parentid = parentid;
                or.addressid = "00000 - 00000 - 00000 - 00000 - 00001";
                or.addressname = "默认";
                or.areaid = "100000";
                or.areaname = "中国";
                or.createtime = DateTime.Now;
                or.createuserid = this.userid;
                or.updatetime = DateTime.Now;
                or.name = "请更改";
                or.spellname = "";
                or.state = 1;
                or.otherinfo = "请输入其它信息";
              
                if (db.dbdao.DbInsert<Organ>(or))
                {
                    return "ok";
                }
                else
                {
                    return "failed";
                }
                _logger.LogDebug(curUser.account + "插入组织数据" + or.id);
            }
            catch (Exception ee)
            {
                _logger.LogDebug(ee.Message);
                return ee.Message;
            }
        }
        [HttpPost]
        public string SaveOrgan([FromBody] Organ organ)
        {
            this.userid = (string)HttpContext.User.Identity.Name;
            this.curUser = db.dbdao.GetById<User>(userid);
            if (dbdao.DbUpdate(organ))
            {
                _logger.LogDebug(this.curUser.account + " 保存组织数据" + json.ujson.ToStr(organ));
                return "ok";
            }
            else
            {
                _logger.LogDebug(this.curUser.account + " 保存组织数据出错" + json.ujson.ToStr(organ));
                return "failed";
            }
        }
        /// <summary>
        /// 删除场站
        /// </summary>
        /// <param name="lsadd1"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteOrganForIdLis([FromBody] List<IdKey> idlist)
        {
            this.curUser = db.dbdao.GetById<User>((string)HttpContext.User.Identity.Name);
            string delsql = @"delete from public.organ where id in ";         
            string tj1 = "";
            foreach (IdKey ik in idlist)
            {
                if (tj1 != "")
                {
                    tj1 += ",";
                }
                tj1 += "'" + ik.id + "'";

            }
            delsql = delsql + "("+tj1+")";

            if (dbdao.AdoExecuteSql(delsql) > 0)
            {
                _logger.LogDebug(curUser.account + "删除组织" + delsql.ToString());
                return "ok";
            }
            else
            {
                _logger.LogDebug(curUser.account + "删除组织失败" + delsql.ToString());
                return "failed";
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

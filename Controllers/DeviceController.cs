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
using System.Data;

namespace dswebapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private User curUser = null;
        public DeviceController(ILogger<DeviceController> logger,
            ILoggerFactory loggerFactory)
        {
            _logger = logger;
            this._loggerFactory = loggerFactory;

        }
        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get(int online,string sn1)
        {
            try
            {
                string sql = "";
                if(sn1!=""&& sn1!=null)
                {
                    sql = "select * from device where device_sn like '%" + sn1 + "%'";
                }
                else
                {  if (online == -1)
                    {
                        sql = "select * from device";
                    }
                    else
                    {
                        sql = "select * from device where device_online='"+ fan.Fan.toBool(online)+"'";
                    }
                }
                List<Device> devices = db.dbdao.DbSql<Device>(sql);
                if (devices != null)//加载关联类
                {
                    foreach (Device D in devices)
                    {
                        D.Load();
                    }
                }
                return json.ujson.toStr<Device>(devices);
            }
            catch (Exception ee)
            {
                _logger.LogDebug(ee.Message);
                return ee.Message;
            }
        }
        /// <summary>
        /// 返回所有的地锁列表
        /// </summary>
        /// <returns></returns>
        public string GetAll()
        {
            try
            {

                List<Device> devices = db.dbdao.GetList<Device>() ;
                return json.ujson.toStr<Device>(devices);
            }
            catch (Exception ee)
            {
                _logger.LogDebug(ee.Message);
                return ee.Message;
            }
        }
        /// <summary>
        /// 新增地锁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string InsertDevice()
        {
            try
            {
                
                this.curUser = db.dbdao.GetById<User>((string)HttpContext.User.Identity.Name);
                int maxnum = 0;
                DataTable lsdt = dbdao.AdoSql("SELECT max(TO_NUMBER(device_num,'999999')) as maxnum from device ");
                if (lsdt != null && lsdt.Rows.Count > 0)
                {
                    maxnum = fan.Fan.toInt(lsdt.Rows[0]["maxnum"]);
                }
                string userid = (string)HttpContext.User.Identity.Name;
                Device insert_d = new Device();
                insert_d.id= Guid.NewGuid().ToString();
                insert_d.device_num = (maxnum+1).ToString("000000");
                insert_d.name = "地锁";
                insert_d.createtime = DateTime.Now;
                insert_d.updatetime = DateTime.Now;
                insert_d.stationid = "0000001-000002-0003-0004-123456789abc";
                insert_d.stationname = "默认";
                insert_d.createuserid = userid;
                //insert_d.deviceConfig = "";
                //insert_d.deviceDebugs = "";
                insert_d.device_config = "";
                insert_d.device_debug = "";
                insert_d.device_mac = "";
                insert_d.device_state = false;
                insert_d.device_type = "SY";
                insert_d.device_usernum = "0001";
                insert_d.device_ver = "";
                insert_d.device_sn = insert_d.device_type + insert_d.device_num;
                insert_d.name = "LJD-DS-"+insert_d.device_sn;
                //配置项
                insert_d.pautoopentime = 180;
                insert_d.pbreathed = 60;
                insert_d.pclosetime = 8;
                insert_d.pdaytime = 7200;
                insert_d.pdelytime = 1000;
                insert_d.perrspace = 600;
                insert_d.pisautoopen = 1;
                insert_d.pljcs = 30;
                insert_d.popentime = 10;
                insert_d.ppw = "no";
                insert_d.psgtime = 60;
                insert_d.psrvip = "61.158.167.166";
                insert_d.psrvport = 40661;
                insert_d.pssid = "no";
                insert_d.pupdatedns = "www.laijiadiancn.com";
                insert_d.pupdatefile = "/assets/sy/updatelist.ini";
                insert_d.pupdateport = 80;
                insert_d.device_config = insert_d.SetConfigToStr();


                if (db.dbdao.DbInsert<Device>(insert_d))
                {
                    _logger.LogDebug(this.curUser.account + "新增地锁" + insert_d.device_sn);
                    return insert_d.id + "ok";
                }
                return "fail";

            }
            catch (Exception ee)
            {
                _logger.LogError(ee.Message);
                return ee.Message;
            }
        }
        [HttpPost]
        public string SaveDevice([FromBody]Device device)
        {
            this.curUser = db.dbdao.GetById<User>((string)HttpContext.User.Identity.Name);
            if (dbdao.DbUpdate<Device>(device))
            {
                _logger.LogDebug(curUser.account + "更新成功" + json.ujson.ToStr(device));
                return "ok";
            }
            else
            {
                _logger.LogDebug(curUser.account + "更新失败" + json.ujson.ToStr(device));
                return "failed";
            }
            //return device.id;
        }
        [HttpPost]
        public string DeleteDevice([FromBody] List<IdKey> idlist)
        {
           
            this.curUser = db.dbdao.GetById<User>((string)HttpContext.User.Identity.Name);
            List<string> sqllist = new List<string>();
            

            string insertsql = @"insert into device_del(id,name ,device_sn ,device_num ,device_mac ,device_online ,device_state ,stationid ,device_ver ,device_type ,
                                 device_usernum , createtime ,updatetime ,createuserid ,stationname,device_config ,pssid  ,ppw  ,perrspace,pbreathed,pdelytime,popentime,pclosetime ,pautoopentime ,
                                 pisautoopen,pljcs  ,psrvip ,psrvport,pdaytime,psgtime,pupdatedns,pupdatefile,pupdateport,device_debug)
                                 SELECT id,name ,device_sn ,device_num ,device_mac ,device_online ,device_state ,stationid ,device_ver ,device_type ,
                                 device_usernum , createtime ,updatetime ,createuserid ,stationname,device_config ,pssid  ,ppw  ,perrspace,pbreathed,pdelytime,popentime,pclosetime ,pautoopentime ,
                                 pisautoopen,pljcs  ,psrvip ,psrvport,pdaytime,psgtime,pupdatedns,pupdatefile,pupdateport,device_debug
                                 from device ";
                                           

            string sqldele = "delete from device where id in (select id from device_del)";
            string tj1 = "";
            foreach(IdKey ik in idlist)
            {
                if (tj1 != "")
                {
                    tj1 += ",";
                }
                tj1+="'" + ik.id + "'";

            }
            string sqlwhere = " where id in(" + tj1 + ")";
            insertsql = insertsql + sqlwhere;
            sqllist.Add(insertsql);
            sqllist.Add(sqldele);
            if (dbdao.AdoExecuteSql(sqllist) > 0)
            {
                _logger.LogDebug(curUser.account + "删除地锁" + sqllist.ToString());
                return "ok";
            }
            else
            {
                _logger.LogDebug(curUser.account + "删除失败" + sqllist.ToString());
                return "failed";
            }

        }

    } 
}

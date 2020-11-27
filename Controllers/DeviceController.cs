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

namespace dswebapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(DeviceController));

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
                        sql = "select * from device where device_online='"+ fan.fan.toBool(online)+"'";
                    }
                }
                List<Device> devices = db.dbdao.DbSql<Device>(sql);
                return json.ujson.toStr<Device>(devices);
            }
            catch (Exception ee)
            {
                log.Error(ee.Message);
                return ee.Message;
            }
        }
        /// <summary>
        /// 返回所有的地锁列表
        /// </summary>
        /// <returns></returns>
        public string GetALL()
        {
            try
            {

                List<Device> devices = db.dbdao.GetList<Device>() ;
                return json.ujson.toStr<Device>(devices);
            }
            catch (Exception ee)
            {
                log.Error(ee.Message);
                return ee.Message;
            }
        }
        [HttpPost]
        public bool Save([FromBody]Device device)
        {
            Device temp= db.dbdao.GetById<Device>(device.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(device);
            }
            else
            {
                return dbdao.DbInsert(device);  
            }
            //return device.id;
        }

    } 
}

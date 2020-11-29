using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using dswebapi.db;
using dswebapi.Models;
using Microsoft.AspNetCore.Authorization;

namespace dswebapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DeviceDebugController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(DeviceDebugController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<DeviceDebug> devicedebugs = db.dbdao.GetList<DeviceDebug>();
                StringBuilder sb = new StringBuilder();
                foreach ( Models.DeviceDebug a in devicedebugs)
                {
                    sb.Append(JsonConvert.SerializeObject(a));
                    //sb.Append("\r\n");
                }
                //log.Info($"testController-GetArea:{sb.ToString()}");
                log.Info(sb);

                string cc = (Guid.NewGuid()).ToString();
                sb.Append(cc);
                return sb.ToString();
            }
            catch (Exception ee)
            {
                log.Error(ee.Message);
                return ee.Message;
            }
        }

        [HttpPost]
        public bool Save([FromBody] DeviceDebug devicedebug)
        {
            DeviceDebug temp = db.dbdao.GetById<DeviceDebug>(devicedebug.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(devicedebug);
            }
            else
            {
                return dbdao.DbInsert(devicedebug);
            }
            //return device.id;
        }

    }
}

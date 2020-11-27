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
    public class DeviceTimeController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(DeviceTimeController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<DeviceTime> devicetimes = db.dbdao.GetList<DeviceTime>();
                StringBuilder sb = new StringBuilder();
                foreach (Models.DeviceTime a in devicetimes)
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
        public bool Save([FromBody] DeviceTime devicetime)
        {
            DeviceTime temp = db.dbdao.GetById<DeviceTime>(devicetime.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(devicetime);
            }
            else
            {
                return dbdao.DbInsert(devicetime);
            }
            //return device.id;
        }

    }
}

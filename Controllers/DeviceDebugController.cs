using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using dswebapi.db;
using dswebapi.Models;

namespace dswebapi.Controllers
{
    public class DeviceDebugController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(DeviceDebugController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<DeviceConfig> deviceconfigs = db.dbdao.GetList<DeviceConfig>();
                StringBuilder sb = new StringBuilder();
                foreach ( Models.DeviceConfig a in deviceconfigs)
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
        public bool Save([FromBody] DeviceConfig deviceconfig)
        {
            DeviceConfig temp = db.dbdao.GetById<DeviceConfig>(deviceconfig.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(deviceconfig);
            }
            else
            {
                return dbdao.DbInsert(deviceconfig);
            }
            //return device.id;
        }

    }
}

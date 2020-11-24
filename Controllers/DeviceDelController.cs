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
    public class DeviceDelController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(DeviceDelController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<DeviceDel> devicedels = db.dbdao.GetList<DeviceDel>();
                StringBuilder sb = new StringBuilder();
                foreach (Models.DeviceDel a in devicedels)
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
        public bool Save([FromBody] DeviceDel devicedel)
        {
            DeviceDel temp = db.dbdao.GetById<DeviceDel>(devicedel.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(devicedel);
            }
            else
            {
                return dbdao.DbInsert(devicedel);
            }
            //return device.id;
        }

    }
}

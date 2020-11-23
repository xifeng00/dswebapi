using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using db;
using System.Text;
using Newtonsoft.Json;

namespace dswebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(UserController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<Device> devices = db.dbdao.GetList<Device>();
                StringBuilder sb = new StringBuilder();
                foreach (db.Device a in devices)
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
        public bool Save([FromBody]Device device)
        {
            Device temp= db.dbdao.GetById<Device>(device.id.ToString());
            if (temp != null)
            {
                return dbdao.DbInsert(device);
            }
            else
            {
                return dbdao.DbUpdate(device);
            }
            //return device.id;
        }

    } 
}

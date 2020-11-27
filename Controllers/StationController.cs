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
    public class StationController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(UserController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<Station> stations = db.dbdao.GetList<Station>();
                StringBuilder sb = new StringBuilder();
                foreach (Models.Station a in stations)
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
        public bool Save([FromBody] Station station)
        {
            Station temp = db.dbdao.GetById<Station>(station.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(station);
            }
            else
            {
                return dbdao.DbInsert(station);
            }
            //return device.id;
        }

    }
}

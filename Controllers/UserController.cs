using db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dswebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(UserController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<User> users = db.dbdao.GetList<User>();
                StringBuilder sb = new StringBuilder();
                foreach (db.User a in users)
                {
                    sb.Append(JsonConvert.SerializeObject(a));
                    sb.Append("\r\n");
                }
                //log.Info($"testController-GetArea:{sb.ToString()}");
                log.Info(sb);

                return sb.ToString();
            }
            catch (Exception ee)
            {
                log.Error(ee.Message);
                return ee.Message;
            }
        } 
    }
}

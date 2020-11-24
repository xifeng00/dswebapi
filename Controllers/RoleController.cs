using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dswebapi.db;
using System.Text;
using Newtonsoft.Json;
using dswebapi.Models;

namespace dswebapi.Controllers
{
    public class RoleController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(RoleController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<Role> roles = db.dbdao.GetList<Role>();
                StringBuilder sb = new StringBuilder();
                foreach (Models.Role a in roles)
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
        public bool Save([FromBody] Role role)
        {
            Role temp = db.dbdao.GetById<Role>(role.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(role);
            }
            else
            {
                return dbdao.DbInsert(role);
            }
            //return device.id;
        }

    }
}


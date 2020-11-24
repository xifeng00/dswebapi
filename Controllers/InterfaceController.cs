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
    public class InterfaceController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(InterfaceController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<Interface> interfaces = db.dbdao.GetList<Interface>();
                StringBuilder sb = new StringBuilder();
                foreach (Models.Interface a in interfaces)
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
        public bool Save([FromBody] Interface iinterface)
        {
            Interface temp = db.dbdao.GetById<Interface>(iinterface.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(iinterface);
            }
            else
            {
                return dbdao.DbInsert(iinterface);
            }
            //return device.id;
        }

    }
}


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
    public class AddressController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(AddressController));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<Address> addresses = db.dbdao.GetList<Address>();
                StringBuilder sb = new StringBuilder();
                foreach (Models.Address a in addresses)
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

        //新增修改
        [HttpPost]
        public bool Save([FromBody] Address address)
        {
            Address temp = db.dbdao.GetById<Address>(address.id.ToString());
            if (temp != null)
            {
                return dbdao.DbUpdate(address);
            }
            else
            {
                return dbdao.DbInsert(address);
            }
            //return device.id;
        }
    }
}

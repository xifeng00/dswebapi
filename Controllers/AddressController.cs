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
    public class AddressController : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(AddressController));
        /// <summary>
        /// 返回反有地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetAddressAll()
        {
            try
            {
                List<Address> addresses = db.dbdao.DbSql<Address>("SELECT * from  address ORDER BY name");
                return json.ujson.toStr<Address>(addresses);
            }
            catch (Exception ee)
            {
                log.Error(ee.Message);
                return ee.Message;
            }
        }
        /// <summary>
        /// 新增一条地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string InsertAddress()
        {
            try
            {
                Address ins_address = new Address();
                ins_address.id = Guid.NewGuid().ToString();
                ins_address.name = "新建";
                ins_address.creattime = DateTime.Now;
                ins_address.areaid = "100000";
                ins_address.areaname = "中国";
                ins_address.creatuserid = "444";
                if (db.dbdao.DbInsert<Address>(ins_address))
                {
                    return ins_address.id+"ok";
                }
                return "fail";

            }
            catch (Exception ee)
            {
                log.Error(ee.Message);
                return ee.Message;
            }
        }
        [HttpPost]
        public string Delete([FromBody] Address lsadd1 )
        {
            try
            {
                //Address lsadd1 = db.dbdao.GetById<Address>("ss");

                if (db.dbdao.DbDelete<Address>(lsadd1))
                {
                    return lsadd1.id + "ok";
                }
                else
                {
                    return "failed";
                }

            }
            catch (Exception ee)
            {
                log.Info(ee.Message);
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

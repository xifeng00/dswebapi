
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
    public class test : ControllerBase
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(test));

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string Get()
        {
            try
            {
                List<Area> areas = db.dbdao.GetList<Area>();
                //List<Area> areas = dbdao.GetById("530823");
                StringBuilder sb = new StringBuilder();
                foreach (db.Area a in areas)
                {
                    sb.Append(JsonConvert.SerializeObject(a));
                    sb.Append("\r\n");
                }
                //log.Info($"testController-GetArea:{sb.ToString()}");
                log.Info(sb);
                log.Info("改动一下");
                log.Info("改动一下11111111111");

                return sb.ToString();
            }
            catch(Exception ee)
            {
                log.Error(ee.Message);
                return ee.Message;
            }
        }
        
        public string insert()
        {
            db.Test t = new Test();
            t.id = Guid.NewGuid().ToString();
            t.name = "test";
            db.dbdao.DbInsert(t);
            return JsonConvert.SerializeObject(t);

        }
        public string testcache()
        {
            try
            {
               Area a= dbdao.GetById<Area>("110105");
                return json.ujson.ToStr(a);
               
               
            }
            catch(Exception ee)
            {
                log.Error(ee.Message);
                return ee.Message;
            }
        }
        public string sql()
        {
            try
            {
                StringBuilder esb = new StringBuilder();
                string sql = "select * from area order by id";
                List<Area> areas = db.dbdao.DbSql<Area>(sql);
                foreach (Area a in areas)
                {

                    if (!dbdao.DbUpdate<Area>(a))
                    {
                        esb.Append(json.ujson.ToStr(a));
                        esb.Append("\r\n");
                    }


                }
                return "error：\r\n" + esb.ToString();
            }
            catch(Exception ee)
            {
                return ee.Message;
            }

        }
      

    }
}

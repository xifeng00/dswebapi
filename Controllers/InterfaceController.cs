using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using dswebapi.db;
using dswebapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace dswebapi.Controllers
{
   
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InterfaceController : ControllerBase
    {
        private readonly ILogger<InterfaceController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private string userid = "";
        private User curUser = null;
        public InterfaceController(ILogger<InterfaceController> logger,
            ILoggerFactory loggerFactory)
        {
            _logger = logger;
            this._loggerFactory = loggerFactory;
        }
        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public string GetInterface()
        {
            try
            {
                List<Interface> faces = db.dbdao.DbSql<Interface>("select * from public.interface order by num");
                string result_str = json.ujson.toStr<Interface>(faces);
                _logger.LogInformation(result_str);
                return result_str;

            }
            catch (Exception ee)
            {
                _logger.LogError(ee.Message);
                return ee.Message;
            }
        }

      

    }
}


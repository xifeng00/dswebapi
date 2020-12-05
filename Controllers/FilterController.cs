using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dswebapi.Util;
using dswebapi.db;
using dswebapi.Models;

namespace dswebapi.Controllers
{
    public class FilterController : ActionFilterAttribute
    {
        DeviceDao deviceDao = new DeviceDao();
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cc = context.HttpContext.Request.Path;
            string[] bb = cc.ToString().Split('/');
            string homebase=bb[2].ToString();
            string actionstr = bb[3].ToString();
            if (homebase.ToLower().Equals("instruct") && !actionstr.ToLower().Equals("stop"))///判断访问模块为设备交互模块
            {
                string devicesn = context.HttpContext.Request.Query["sn"].ToString();
                List<string> devicesnls=(List<string>)CacheHelperNetCore.CacheValue("device");//从内存中获取设备列表缓存
                if (devicesnls == null || !devicesnls.Contains(devicesn)) {//当请求的编码不在缓存中
                    Device device= deviceDao.getBySn(devicesn);
                    if (device.id != null)
                    {
                        if (devicesnls == null)
                            devicesnls = new List<string>();
                        devicesnls.Add(devicesn);
                        CacheHelperNetCore.CacheInsert("device", devicesnls);
                    }  
                    else
                    {
                        JsonResult result = new JsonResult("invalid sn!");//非法设备
                        context.Result = result; 
                    }
                }
            }
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dswebapi.Controllers
{
    public class FilterController : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cc = context.HttpContext.Request.Path;
            string[] bb = cc.ToString().Split('/');
            string homebase=bb[2].ToString();
            if (homebase.ToLower().Equals("instruct"))///判断访问模块为设备交互模块
            {
                //在缓存中创建指令集合
            }
            
            ////拦截全局里是否带了token
            //if (string.IsNullOrEmpty(context.HttpContext.Request.Query["token"]))
            //{

            //}
        }


    }
}

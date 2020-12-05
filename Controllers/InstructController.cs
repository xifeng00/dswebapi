using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dswebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InstructController : ControllerBase
    {

        public Task<IActionResult> heart()
        {

            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNETPT.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace ASPNETPT.Controllers.Api
{
    [Route("api/btc")]
    public class BtcController : Controller

    {
        private IBtcRepo _context;

        public BtcController(IBtcRepo context)
        {
            _context = context;
        }
        [HttpGet("api/btc")]
        public IActionResult Get()
        {

            return Ok(_context.GetBtCprops());
        }

        [HttpPost("api/btc")]
        public IActionResult Post([FromBody]BtCprop data)
        {
            return Ok(true);
        }
    }
}

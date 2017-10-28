using ASPNETPT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETPT.Controllers.Api
{
    [Route("api/btc")]
    public class BtcController : Controller

    {
        private readonly IBtcRepo _context;

        public BtcController(IBtcRepo context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_context.GetBtCprops());
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] BtCprop data)
        {
            if (ModelState.IsValid)
                return Created($"api/btc/{data.Name}", data);
            return BadRequest("Bad data");
        }
    }
}
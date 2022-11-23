using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TestServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Schedule : ControllerBase
    {
        MyContext context;
        public Schedule(MyContext context)
        {
            this.context = context;
        }
        [Authorize]
        [HttpGet("Week")]
        public IActionResult GetWeek()
        {
            string Id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }
    }
}

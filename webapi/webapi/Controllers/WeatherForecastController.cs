using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }




        [HttpGet("NeedAdmin")]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult NeedAdmin()
        {
            return Ok(new { Message = "你是管理員" });
        }


        [HttpGet("NeedVip")]
        [Authorize(Policy = "Vip")]
        public IActionResult NeedVIP()
        {
            return Ok(new { Message = "你是VIP." });
        }


    }
}

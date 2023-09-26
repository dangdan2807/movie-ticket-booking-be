using Microsoft.AspNetCore.Mvc;

namespace MovieTicketBookingBe.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;

        public UserController(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            _logger.Information("Get all users");
            return Ok("");
        }
    }
}

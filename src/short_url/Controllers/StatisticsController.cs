using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingBe.Models.Response;
using MovieTicketBookingBe.Services;
using System.Net;
using System.Security.Claims;
using ShortUrlBachEnd.Services;
using ShortUrlBachEnd.Models.DTO;

namespace ShortUrlBachEnd.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/statistics")]
    [ApiVersion("1.0")]
    public class StatisticsController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IRoleService _roleService;
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(Serilog.ILogger logger, IRoleService roleService, IStatisticsService statisticsService)
        {
            _logger = logger;
            _roleService = roleService;
            _statisticsService = statisticsService;
        }

        [HttpGet("base")]
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> StatisticsBase()
        {
            try
            {
                List<string> roles = User.FindAll(ClaimTypes.Role).Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value).ToList();
                var role = "";
                var priority = -1;
                roles.Select(async item => await _roleService.GetRoleByCode(item)).Select(item =>
                {
                    if (item.Result == null)
                    {
                        throw new Exception("Role not found");
                    }
                    if (priority == -1 || item.Result.priority < priority)
                    {
                        priority = item.Result.priority;
                        role = item.Result.roleCode;
                    }
                    return item;
                }).ToList();

                var result = await _statisticsService.StatisticsBase();

                return Ok(new SuccessResponse(
                    HttpStatusCode.OK,
                    "Get statistics base successfully",
                    result
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(
                    HttpStatusCode.BadRequest,
                    ex.Message
                ));
            }
        }
    }
}

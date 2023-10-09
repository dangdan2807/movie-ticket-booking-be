using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingBe.src.Models.Response;
using MovieTicketBookingBe.src.Services;
using MovieTicketBookingBe.src.ViewModels;
using System.Net;
using System.Security.Claims;

namespace MovieTicketBookingBe.src.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/short-url")]
    [ApiVersion("1.0")]
    public class ShortUrlController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IShortUrlService _shortUrlService;

        public ShortUrlController(Serilog.ILogger logger, IShortUrlService shortUrlService)
        {
            _logger = logger;
            _shortUrlService = shortUrlService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateShortUrl(CreateShortUrlVM createShortUrlVM)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = int.Parse(userId);

                var shortUrl = await _shortUrlService.CreateShortUrl(userIdInt, createShortUrlVM);
                return Ok(new SuccessResponse(
                    HttpStatusCode.OK,
                    "Create short url successfully",
                    shortUrl
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

        [HttpGet]
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> GetShortUrlsForAdmin([FromQuery]PaginationVM paginationVM, string? keyword = "", bool? status = true)
        {
            try
            {
                var shortUrls = await _shortUrlService.GetShortUrlsForAdmin(paginationVM, keyword, status);
                return Ok(new SuccessResponse(
                    HttpStatusCode.OK,
                    "Get short urls successfully",
                    shortUrls.shortUrls,
                    shortUrls.pagination
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

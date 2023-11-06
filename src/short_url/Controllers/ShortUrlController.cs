using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.Models.Response;
using MovieTicketBookingBe.Services;
using MovieTicketBookingBe.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;

namespace MovieTicketBookingBe.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/short-url")]
    [ApiVersion("1.0")]
    public class ShortUrlController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IShortUrlService _shortUrlService;
        private readonly IRoleService _roleService;

        public ShortUrlController(Serilog.ILogger logger, IShortUrlService shortUrlService, IRoleService roleService)
        {
            _logger = logger;
            _shortUrlService = shortUrlService;
            _roleService = roleService;
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
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(
                    HttpStatusCode.BadRequest,
                    ex.Message
                ));
            }
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN, MOD, VIP, MEMBER")]
        public async Task<IActionResult> GetShortUrls([FromQuery] PaginationVM paginationVM, string? keyword = "",
            DateTime? startDate = null, DateTime? endDate = null, bool? status = true)
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

                var shortUrls = new GetShortUrlsDTO();
                if (role.Equals("ADMIN") || role.Equals("MOD"))
                {
                    shortUrls = await _shortUrlService.GetShortUrlsForAdmin(paginationVM, keyword, startDate, endDate, status);
                }
                else
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                    int userIdInt = int.Parse(userId);

                    shortUrls = await _shortUrlService.GetShortUrlsForUserId(userIdInt, paginationVM, keyword, startDate, endDate, status);
                }

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

        [HttpGet("hash-id/{hashId}")]
        [Authorize(Roles = "ADMIN, MOD, VIP, MEMBER")]
        public async Task<IActionResult> GetShortUrlByHashIdAndUserId(string hashId)
        {
            try
            {
                // get role
                List<string> roles = User.FindAll(ClaimTypes.Role).Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value).ToList();

                // get user id
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = int.Parse(userId);

                var shortUrlDTO = await _shortUrlService.GetShortUrlByHashIdAndUserId(hashId, userIdInt, roles);

                return Ok(new SuccessResponse(
                    HttpStatusCode.OK,
                    "Get short url successfully",
                    shortUrlDTO
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

        [HttpGet("short-link/{shortLink}")]
        public async Task<IActionResult> GetShortUrlByShortLink(string shortLink)
        {
            try
            {
                var shortUrlDTO = await _shortUrlService.GetShortUrlByShortLink(shortLink);

                return Ok(new SuccessResponse(
                    HttpStatusCode.OK,
                    "Get short url successfully",
                    shortUrlDTO
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

        [HttpPut("short-link/{shortLink}")]
        [Authorize(Roles = "ADMIN, MOD, VIP, MEMBER")]
        public async Task<IActionResult> UpdateShortUrlByShortLink(string shortLink, UpdateShortUrlVM updateShortUrlVM)
        {
            try
            {
                List<string> roles = User.FindAll(ClaimTypes.Role).Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value).ToList();
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = int.Parse(userId);

                if (string.IsNullOrEmpty(shortLink))
                {
                    throw new Exception("short url is required");
                }
                var updateShortUrlDTO = await _shortUrlService.UpdateShortUrlByShortLink(shortLink, userIdInt, roles, updateShortUrlVM);

                return Ok(new SuccessResponse(
                    HttpStatusCode.OK,
                    "Update short url successfully",
                    updateShortUrlDTO
                ));
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(
                    HttpStatusCode.BadRequest,
                    ex.Message
                ));
            }
        }

        [HttpPut("short-link/{shortLink}/click")]
        public async Task<IActionResult> UpdateClickCountByShortLink(string shortLink)
        {
            try
            {
                if (string.IsNullOrEmpty(shortLink))
                {
                    throw new Exception("short url is required");
                }
                var updateShortUrlDTO = await _shortUrlService.UpdateClickCountByShortLink(shortLink);

                return Ok(new SuccessResponse(
                    HttpStatusCode.OK,
                    "Update short url successfully",
                    updateShortUrlDTO
                ));
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(
                    HttpStatusCode.BadRequest,
                    ex.Message
                ));
            }
        }

        [HttpDelete("short-link/{shortLink}")]
        [Authorize(Roles = "ADMIN, MOD, VIP, MEMBER")]
        public async Task<IActionResult> DeleteShortUrlByShortLink(string shortLink)
        {
            try
            {
                List<string> roles = User.FindAll(ClaimTypes.Role).Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value).ToList();
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = int.Parse(userId);

                if (string.IsNullOrEmpty(shortLink))
                {
                    throw new Exception("short url is required");
                }
                var deleteShortUrlDTO = await _shortUrlService.DeleteShortUrlByShortLink(shortLink, userIdInt, roles);

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status403Forbidden);
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

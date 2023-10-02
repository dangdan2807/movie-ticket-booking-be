using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingBe.Models.Response;
using MovieTicketBookingBe.Services;
using MovieTicketBookingBe.ViewModels;
using System.Net;
using System.Security.Claims;

namespace MovieTicketBookingBe.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IUserService _userService;

        public UserController(Serilog.ILogger logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> GetUsers(int currentPage = 1, int pageSize = 10, string sort = "ASC")
        {
            try
            {
                var usersDTO = await _userService.GetUsers(currentPage, pageSize, sort);

                var successResponse = new SuccessResponse(
                    HttpStatusCode.OK,
                    "Get users successfully",
                    usersDTO.users,
                    usersDTO.pagination
                );
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message, null));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var userDTO = await _userService.GetUserById(id);
                if (userDTO == null)
                {
                    throw new Exception("User not found");
                }

                var successResponse = new SuccessResponse(
                    HttpStatusCode.OK,
                    "Get user successfully",
                    userDTO
                );
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> UpdateUserById(int id, UpdateUserVM updateUserVM)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = Int32.Parse(userId);

                var user = await _userService.UpdateUserById(id, userIdInt, updateUserVM);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var successResponse = new SuccessResponse(
                    HttpStatusCode.OK,
                    "Update user successfully",
                    user
                );
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = Int32.Parse(userId);

                var userDTO = await _userService.GetUserById(userIdInt);
                if (userDTO == null)
                {
                    throw new Exception("User not found");
                }

                var successResponse = new SuccessResponse(
                    HttpStatusCode.OK,
                    "Get user profile successfully",
                    userDTO
                );
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UpdateProfileVM updateProfileVM)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = Int32.Parse(userId);

                var userDTO = await _userService.UpdateProfile(userIdInt, updateProfileVM);
                if (userDTO == null)
                {
                    throw new Exception("User not found");
                }

                var successResponse = new SuccessResponse(
                    HttpStatusCode.OK,
                    "Update user info successfully",
                    userDTO
                );
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPut("profile/password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordVM updatePasswordVM)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = Int32.Parse(userId);

                var userDTO = await _userService.UpdatePassword(userIdInt, updatePasswordVM);
                if (userDTO == null)
                {
                    throw new Exception("User not found");
                }

                var successResponse = new SuccessResponse(
                    HttpStatusCode.OK,
                    "Update user password successfully"
                );
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}

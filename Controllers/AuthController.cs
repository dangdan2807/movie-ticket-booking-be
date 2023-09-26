using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.Response;
using MovieTicketBookingBe.Services;
using MovieTicketBookingBe.ViewModels;
using System.Net;
using System.Text.RegularExpressions;

namespace MovieTicketBookingBe.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IUserService _userService;

        public AuthController(Serilog.ILogger logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            try
            {
                if (string.IsNullOrEmpty(registerVM?.fullname?.Trim()))
                {
                    throw new Exception("Fullname is required");
                }
                else if (registerVM?.fullname?.Trim().Length > 50 || registerVM?.fullname?.Trim().Length < 3)
                {
                    throw new Exception("Fullname must be between 3 - 50 characters");
                }

                Regex regex = new Regex(@"^0[3|5|7|8|9]\d{8}$");
                if (string.IsNullOrEmpty(registerVM?.phone?.Trim()))
                {
                    throw new Exception("Phone is required");
                }
                else if (!regex.IsMatch(registerVM?.phone?.Trim()))
                {
                    throw new Exception("Invalid phone number");
                }

                if (string.IsNullOrEmpty(registerVM?.password?.Trim()))
                {
                    throw new Exception("Password is required");
                }
                else if (registerVM?.password?.Trim().Length > 255 || registerVM?.password?.Trim().Length < 6)
                {
                    throw new Exception("Password must be between 6 - 255 characters");
                }

                var createUser = await _userService.CreateUser(new User
                {
                    FullName = registerVM.fullname,
                    Phone = registerVM.phone,
                    Address = registerVM.address + "",
                    Password = registerVM.password,

                });

                return Ok(new SuccessResponse
                (
                    HttpStatusCode.Created,
                    "Register successfully",
                    createUser
                ));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse
                (
                    HttpStatusCode.BadRequest,
                    ex.Message,
                    null
                ));
            }
        }
    }
}

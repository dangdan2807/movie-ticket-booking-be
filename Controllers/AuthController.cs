using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ITokenService _tokenService;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public AuthController(
            Serilog.ILogger logger, 
            IUserService userService,
            ITokenService tokenService, 
            IAuthenticationSchemeProvider authenticationSchemeProvider
        )
        {
            _logger = logger;
            _userService = userService;
            _tokenService = tokenService;
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var loginDTO = await _userService.Login(loginViewModel);
                return Ok(new SuccessResponse
                (
                    HttpStatusCode.OK,
                    "Login successfully",
                    loginDTO
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                (
                    HttpStatusCode.BadRequest,
                    ex.Message
                ));
            }
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
                    ex.Message
                ));
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(accessToken))
                {
                    return BadRequest(new ErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Access token is required"
                    ));
                }
                await _tokenService.RevokeToken(accessToken);

                return Ok(new SuccessResponse(
                    HttpStatusCode.OK,
                    "Logout successfully"
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
    }
}

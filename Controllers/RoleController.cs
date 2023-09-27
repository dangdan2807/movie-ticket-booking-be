using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.Response;
using MovieTicketBookingBe.Services;
using MovieTicketBookingBe.ViewModels;
using System.Net;

namespace MovieTicketBookingBe.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/roles")]
    [ApiVersion("1.0")]
    public class RoleController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IRoleService _roleService;

        public RoleController(Serilog.ILogger logger, IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var rolesDTO = await _roleService.GetRoles();

                var successResponse = new SuccessResponse(
                    HttpStatusCode.OK, 
                    "Get roles successfully", 
                    rolesDTO.roles,
                    rolesDTO.pagination
                );
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message, null));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleVM createRoleVM)
        {
            try
            {
                if (createRoleVM == null)
                {
                    throw new Exception("Role is null");
                }

                if (string.IsNullOrEmpty(createRoleVM?.roleName?.Trim()))
                {
                    throw new Exception("Role name is required");
                }
                else if (createRoleVM?.roleName?.Trim().Length > 100 || createRoleVM?.roleName?.Trim().Length < 3)
                {
                    throw new Exception("Role name must be between 3 - 100 characters");
                }

                if (string.IsNullOrEmpty(createRoleVM?.roleCode?.Trim()))
                {
                    throw new Exception("Role code is required");
                }
                else if (createRoleVM?.roleCode?.Trim().Length > 50 || createRoleVM?.roleCode?.Trim().Length < 3)
                {
                    throw new Exception("Role code must be between 3 - 50 characters");
                }

                var role = await _roleService.CreateRole(new Role
                {
                    RoleName = createRoleVM.roleName,
                    RoleCode = createRoleVM.roleCode,
                    Status = createRoleVM.status == true ? true : false,
                });
                var successResponse = new SuccessResponse(HttpStatusCode.Created, "Create role successfully", role);

                return CreatedAtAction(nameof(GetRoleById), new { id = role.roleId }, successResponse);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetRoleById(id);
                if (role == null)
                {
                    throw new Exception("Role not found");
                }
                var successResponse = new SuccessResponse(HttpStatusCode.OK, "Get role successfully", role);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message, null));
            }
        }
    }
}

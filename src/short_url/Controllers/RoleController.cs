using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.Response;
using MovieTicketBookingBe.Services;
using MovieTicketBookingBe.ViewModels;
using System.Net;
using System.Security.Claims;

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
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> GetRoles([FromRoute] PaginationVM paginationVM, string? keyword, bool? status = true)
        {
            try
            {
                var rolesDTO = await _roleService.GetRoles(paginationVM, keyword, status);

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
        [Authorize(Roles = "ADMIN, MOD")]
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = int.Parse(userId);

                var role = await _roleService.CreateRole(userIdInt, new Role
                {
                    RoleName = createRoleVM.roleName,
                    RoleCode = createRoleVM.roleCode,
                    Description = createRoleVM.description,
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
        [Authorize(Roles = "ADMIN, MOD, VIP, MEMBER")]
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

        [HttpGet("code/{code}")]
        [Authorize(Roles = "ADMIN, MOD, VIP, MEMBER")]
        public async Task<IActionResult> GetRoleByCode(string code)
        {
            try
            {
                var role = await _roleService.GetRoleByCode(code);
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
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> UpdateRoleById(int id, UpdateRoleVM updateRoleVM)
        {
            try
            {

                if (updateRoleVM == null)
                {
                    throw new Exception("Role is null");
                }
                if (id <= 0)
                {
                    throw new Exception("Id is invalid");
                }

                if (string.IsNullOrEmpty(updateRoleVM?.roleName?.Trim()))
                {
                    throw new Exception("Role name is required");
                }
                else if (updateRoleVM?.roleName?.Trim().Length > 100 || updateRoleVM?.roleName?.Trim().Length < 3)
                {
                    throw new Exception("Role name must be between 3 - 100 characters");
                }

                if (string.IsNullOrEmpty(updateRoleVM?.roleCode?.Trim()))
                {
                    throw new Exception("Role code is required");
                }
                else if (updateRoleVM?.roleCode?.Trim().Length > 50 || updateRoleVM?.roleCode?.Trim().Length < 3)
                {
                    throw new Exception("Role code must be between 3 - 50 characters");
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = int.Parse(userId);

                var roleDTO = await _roleService.UpdateRoleById(id, userIdInt, new Role
                {
                    RoleName = updateRoleVM.roleName,
                    RoleCode = updateRoleVM.roleCode,
                    Description = updateRoleVM.description,
                    Status = updateRoleVM.status == true ? true : false,
                });

                var successResponse = new SuccessResponse(HttpStatusCode.OK, "Update role successfully", roleDTO);

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> DeleteRoleById(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) + "";
                int userIdInt = int.Parse(userId);

                await _roleService.DeleteRoleById(id, userIdInt);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}

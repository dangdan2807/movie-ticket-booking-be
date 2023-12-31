﻿using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.Repositories;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly Serilog.ILogger _logger;
        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository, Serilog.ILogger logger)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<RoleDTO> CreateRole(int userId, Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role is null");
            }
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var existingRole = await _roleRepository.GetRoleByCode(role.RoleCode);
            if (existingRole != null)
            {
                throw new Exception("Role code is already exists");
            }
            var newRole = await _roleRepository.CreateRole(userId, role);
            return new RoleDTO
            {
                roleId = newRole.RoleId,
                roleName = newRole.RoleName,
                roleCode = newRole.RoleCode,
                priority = role.Priority,
                status = newRole.Status,
                createAt = newRole.CreateAt,
            };
        }

        public async Task<RoleDTO> GetRoleById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }
            var role = await _roleRepository.GetRoleById(id);
            return new RoleDTO
            {
                roleId = role.RoleId,
                roleName = role.RoleName,
                roleCode = role.RoleCode,
                priority = role.Priority,
                status = role.Status,
                createAt = role.CreateAt,
            };
        }

        public async Task<RoleDTO> GetRoleByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new Exception("code is empty or null");
            }
            var role = await _roleRepository.GetRoleByCode(code);
            return new RoleDTO
            {
                roleId = role.RoleId,
                roleName = role.RoleName,
                roleCode = role.RoleCode,
                priority = role.Priority,
                status = role.Status,
                createAt = role.CreateAt,
            };
        }

        public async Task<GetRolesDTO> GetRoles(PaginationVM paginationVM, string? keyword, bool? status = true)
        {
            if (paginationVM.currentPage <= 0 || paginationVM.pageSize <= 0)
            {
                throw new Exception("Page is invalid");
            }
            if (paginationVM.sort.Equals("ASC") && paginationVM.sort.Equals("DESC"))
            {
                throw new Exception("Sort is invalid");
            }
            return await _roleRepository.GetRoles(paginationVM, keyword, status);
        }

        public async Task<RoleDTO> UpdateRoleById(int id, int userId, Role role)
        {
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }
            if (role == null)
            {
                throw new Exception("Role is null");
            }

            if (string.IsNullOrEmpty(role.RoleName.Trim()))
            {
                throw new Exception("Role name is required");
            }
            else if (role.RoleName.Trim().Length > 100 || role.RoleName.Trim().Length < 3)
            {
                throw new Exception("Role name must be between 3 - 100 characters");
            }

            if (string.IsNullOrEmpty(role.RoleCode.Trim()))
            {
                throw new Exception("Role code is required");
            }
            else if (role.RoleCode.Trim().Length > 50 || role.RoleCode.Trim().Length < 3)
            {
                throw new Exception("Role code must be between 3 - 50 characters");
            }

            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var updateRole = await _roleRepository.UpdateRoleById(id, userId, role);

            return new RoleDTO
            {
                roleId = updateRole.RoleId,
                roleName = updateRole.RoleName,
                roleCode = updateRole.RoleCode,
                priority = updateRole.Priority,
                status = updateRole.Status,
                createAt = updateRole.CreateAt,
            };
        }

        public async Task<RoleDTO> DeleteRoleById(int id, int userId)
        {
            if (id <= 0)
            {
                throw new Exception("Id is invalid");
            }

            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var updateRole = await _roleRepository.DeleteRoleById(id, userId);
            return new RoleDTO
            {
                roleId = updateRole.RoleId,
                roleName = updateRole.RoleName,
                roleCode = updateRole.RoleCode,
                priority = updateRole.Priority,
                status = updateRole.Status,
                createAt = updateRole.CreateAt,
            };
        }
    }
}

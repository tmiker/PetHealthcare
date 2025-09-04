using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminUserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<(bool IsSuccess, ICollection<UserInfoDTO>? UserInfoDTOs, string? ErrorMessage)> GetAllUsersAsync()
        {
            ICollection<UserInfoDTO> userInfoDTOs = new List<UserInfoDTO>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);

                //IEnumerable<ApplicationUser> admins = await _userManager.GetUsersInRoleAsync("Admin");
                //bool isAdmin = admins.Contains(user);

                bool isLocked = await _userManager.IsLockedOutAsync(user);
                UserInfoDTO userDTO = new UserInfoDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsLocked = isLocked
                };
                foreach (var role in roles) userDTO.Roles.Add(role);
                userDTO.IsAdmin = roles.Contains("Admin");
                userInfoDTOs.Add(userDTO);
            }

            if (userInfoDTOs.Count > 0) return (true, userInfoDTOs, null);
            else return (false, null, "Error retrieving users.");
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> LockAsync(string id)
        {
            if (id == null) return (false, "Invalid user id.");
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return (false, "Invalid user id.");
            IdentityResult result = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(1000));
            if (result.Succeeded)
            {
                if (await _userManager.IsLockedOutAsync(user)) return (true, null);
                else return (false, "Error locking user.");
            }
            else return (false, "Error locking user.");
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UnLockAsync(string id)
        {
            if (id == null) return (false, "Invalid user id.");
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return (false, "Invalid user id.");
            IdentityResult result = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now);
            if (result.Succeeded)
            {
                if (!await _userManager.IsLockedOutAsync(user)) return (true, null);
                else return (false, "Error unlocking user.");
            }
            else return (false, "Error unlocking user.");
        }

        public async Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserAdminRole(string id)
        {
            if (id == null) return (false, null, "Invalid user id.");
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return (false, null, "Invalid user id.");
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                await _userManager.UpdateAsync(user);
                return (true, "Admin role removed.", null);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                await _userManager.UpdateAsync(user);
                return (true, "Admin role assigned.", null);
            }

        }
        public async Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserManagerRole(string id)
        {
            if (id == null) return (false, null, "Invalid user id.");
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return (false, null, "Invalid user id.");
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Manager"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Manager");
                await _userManager.UpdateAsync(user);
                return (true, "Manager role removed.", null);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Manager");
                await _userManager.UpdateAsync(user);
                return (true, "Manager role assigned.", null);
            }
        }

        public async Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserEmployeeRole(string id)
        {
            if (id == null) return (false, null, "Invalid user id.");
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return (false, null, "Invalid user id.");
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Employee"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Employee");
                await _userManager.UpdateAsync(user);
                return (true, "Employee role removed.", null);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                await _userManager.UpdateAsync(user);
                return (true, "Employee role assigned.", null);
            }
        }

        public async Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserCustomerRole(string id)
        {
            if (id == null) return (false, null, "Invalid user id.");
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return (false, null, "Invalid user id.");
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Customer"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Customer");
                await _userManager.UpdateAsync(user);
                return (true, "Customer role removed.", null);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                await _userManager.UpdateAsync(user);
                return (true, "Customer role assigned.", null);
            }
        }

        public async Task<(bool IsLocked, string? ErrorMessage)> UserIsLockedAsync(string id)
        {
            if (id == null) return (false, "Invalid user id.");
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return (false, "Invalid user id.");
            bool isLocked = await _userManager.IsLockedOutAsync(user);
            if (isLocked) return (true, null);
            else return (false, "User is unlocked.");
        }

        public async Task<(bool IsUnlocked, string? ErrorMessage)> UserIsUnlockedAsync(string id)
        {
            if (id == null) return (false, "Invalid user id.");
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) return (false, "Invalid user id.");
            bool islocked = await _userManager.IsLockedOutAsync(user);
            if (!islocked) return (true, null);
            else return (false, "User is locked.");
        }
    }
}

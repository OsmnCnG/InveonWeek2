using InveonWeek2.Dto;
using InveonWeek2.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InveonWeek2.Repository
{
    public class RoleRepository(RoleManager<AppRole> roleManager) : IRoleRepository
    {
        public async Task<bool> RoleCreate(string roleName)
        {
            var IsRoleExist = await roleManager.RoleExistsAsync(roleName);

            if (IsRoleExist) {
                return false;
            }

            AppRole role = new AppRole()
            {
                Name = roleName,
            };

            await roleManager.CreateAsync(role);

            return true;

        }


        public async Task<List<AppRole>> GetAllRoles()
        {

            var roles = await roleManager.Roles.ToListAsync();

            return roles;

        }

        public async Task<Result> RoleDelete(Guid id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                return new Result
                {
                    Success = false,
                };
            }

            if (role.Name == "Admin")
            {
                return new Result
                {
                    Success = false,
                    Message = "Bu rol silinemez."
                };
            }

            var result = await roleManager.DeleteAsync(role);

            if (result == null || !result.Succeeded)
            {
                return new Result
                {
                    Success = false,
                };
            }

            return new Result
            {
                Success = true,
                Message = "Rol başarıyla silindi."
            };
        }

    }
}

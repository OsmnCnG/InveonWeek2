using InveonWeek2.Dto;
using InveonWeek2.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InveonWeek2.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> userRole;

        public UserRepository(UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager)
        {
            userManager = _userManager;
            userRole = _roleManager;
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<AppUser> FirstOrDefaultAsync(Expression<Func<AppUser,bool>>? filter = null)
        {
            if (filter == null)
            {
                return await userManager.Users.FirstOrDefaultAsync();
            }


            return await userManager.Users.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> Any(Expression<Func<AppUser, bool>> filter)
        {
            return await userManager.Users.AnyAsync(filter);
        }

        public async Task<bool> CreateAsync(AppUser newUser, string password)
        {

            var IsExecute = await userManager.CreateAsync(newUser, password);

            if (!IsExecute.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(string id)
        {

            var dUser = await userManager.FindByIdAsync(id);

            var IsExecute = await userManager.DeleteAsync(dUser);

            if (!IsExecute.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(AppUser user)
        {
            var updateUser = await userManager.FindByIdAsync(user.Id.ToString());

            if (updateUser == null)
            {
                return false;
            }

            updateUser.PhoneNumber = user.PhoneNumber;
            updateUser.UserName = user.UserName;
            updateUser.Email = user.Email;

            await userManager.UpdateAsync(updateUser);

            return true;
        }


        public async Task<bool> AddRole(string roleName, Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());


            var result = await userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded) {
                return false;
            }

            return true;
        }


        public async Task<bool> RemoveRole(string roleName, Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());


            var result = await userManager.RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}

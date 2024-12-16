using InveonWeek2.Repository;
using InveonWeek2.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace InveonWeek2.Service
{
    #region Interface
    public interface IUserService
    {
        Task<List<AppUser>> GetAllUsers();

        Task<AppUser> Select(Expression<Func<AppUser, bool>>? filter = null);

        Task<bool> Any(Expression<Func<AppUser, bool>> filter);

        Task<bool> CreateUser(AppUser appUser, string password);

        Task<bool> Delete(string id);

        Task<bool> UpdateUser(AppUser user);

        Task<bool> AddRole(string roleName, Guid id);

        Task<bool> RemoveRole(string roleName, Guid id);

    }
    #endregion

    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<List<AppUser>> GetAllUsers()
        {
            return await userRepository.GetAllAsync();
        }


        public async Task<AppUser> Select(Expression<Func<AppUser, bool>>? filter = null)
        {
            return await userRepository.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> Any(Expression<Func<AppUser, bool>> filter)
        {
            return await userRepository.Any(filter);
        }


        public async Task<bool> CreateUser(AppUser appUser, string password)
        {
            return await userRepository.CreateAsync(appUser, password);
        }

        public async Task<bool> Delete(string id)
        {
            return await userRepository.Delete(id);
        }

        public async Task<bool> UpdateUser(AppUser user)
        {
            return await userRepository.UpdateAsync(user); ;
        }

        public async Task<bool> AddRole(string roleName, Guid id)
        {
            return await userRepository.AddRole(roleName, id);
        }

        public async Task<bool> RemoveRole(string roleName, Guid id)
        {
            return await userRepository.RemoveRole(roleName, id);
        }


    }
}

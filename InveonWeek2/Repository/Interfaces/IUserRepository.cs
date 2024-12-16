using System.Linq.Expressions;

namespace InveonWeek2.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByIdAsync(string id);

        Task<List<AppUser>> GetAllAsync();

        Task<AppUser> FirstOrDefaultAsync(Expression<Func<AppUser, bool>>? filter = null);

        Task<bool> Any(Expression<Func<AppUser, bool>> filter);

        Task<bool> CreateAsync(AppUser newUser, string password);

        Task<bool> Delete(string id);

        Task<bool> UpdateAsync(AppUser user);

        Task<bool> AddRole(string roleName, Guid id);

        Task<bool> RemoveRole(string roleName, Guid id);


    }
}

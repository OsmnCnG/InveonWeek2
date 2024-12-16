using InveonWeek2.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InveonWeek2.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<bool> RoleCreate(string roleName);

        Task<List<AppRole>> GetAllRoles();

        Task<Result> RoleDelete(Guid id);


    }
}

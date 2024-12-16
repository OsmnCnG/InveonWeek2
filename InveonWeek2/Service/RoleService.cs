using InveonWeek2.Dto;
using InveonWeek2.Repository;
using InveonWeek2.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InveonWeek2.Service
{
    public interface IRoleService
    {
        Task<bool> CreateRole(string roleName);
        Task<Result> DeleteRole(Guid id);
        Task<List<AppRole>> GetAllRoles();
    }

    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        public async Task<bool> CreateRole(string roleName)
        {
            return await roleRepository.RoleCreate(roleName);
        }

        public async Task<Result> DeleteRole(Guid id)
        {
            return await roleRepository.RoleDelete(id);
        }

        public async Task<List<AppRole>> GetAllRoles()
        {
            return await roleRepository.GetAllRoles();
        }
    }
}

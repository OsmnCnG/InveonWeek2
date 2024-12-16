using InveonWeek2.Repository;
using InveonWeek2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InveonWeek2.Controllers
{

    [Authorize("Admin")]
    public class RoleController(IUserService userService, IRoleService roleService) : Controller
    {

        public async Task<IActionResult> Roles()
        {
            return View(await roleService.GetAllRoles());
        }


        [HttpGet]
        public async Task<IActionResult> AssignRole(Guid id)
        {
            var user = await userService.Select(us => us.Id == id);
            
            return View("AssignRole",user);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string roleName, Guid id)
        {

            var user = await userService.Select(i => i.Id == id);

            await userService.AddRole(roleName, user.Id);

            return RedirectToAction("GetAllUsers", "User");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            var user = await userService.Select(i => i.Id == id);

            return View("RemoveRole", user);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveRole(string roleName, Guid id)
        {
            var user = await userService.Select(i => i.Id == id);

            await userService.RemoveRole(roleName, id);

            return RedirectToAction("GetAllUsers", "User");
        }

        public IActionResult RoleCreate()
        {
            return View("RoleCreate");
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(string roleName)
        {
            var result = await roleService.CreateRole(roleName);

            var roles = await roleService.GetAllRoles();

            if (!result)
            {
                TempData["ErrorMessage"] = "Bu rol zaten mevcut";
                return RedirectToAction("Roles", roles);
            }

            //return View("Roles", roles);
            return RedirectToAction("Roles", roles);

        }


        public async Task<IActionResult> RoleDelete(Guid id)
        {

            var result = await roleService.DeleteRole(id);

            if (!result.Success) {

                TempData["ErrorMessage"] = result.Message;

                return View("Roles", await roleService.GetAllRoles());
            }

            TempData["SuccessMessage"] = result.Message;


            return View("Roles", await roleService.GetAllRoles());

        }



    }
}

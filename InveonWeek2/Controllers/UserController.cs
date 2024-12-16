using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InveonWeek2.Repository;
using Microsoft.AspNetCore.Authorization;
using InveonWeek2.Service;
using InveonWeek2.Dto;

namespace InveonWeek2.Controllers
{
    [Authorize("Admin")]
    public class UserController(IUserService userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllUsers()
        {
            //test
            return View("Index", await userService.GetAllUsers());


            //var UserList = await userManager.Users.ToListAsync();

            //return View("Index",UserList);
        }


        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var dUser = await userManager.Users
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var dUser = await userService
                .Select(m => m.Id == id);


            if (dUser == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı bulunamadı!";
                return RedirectToAction("GetAllUsers", "User");
            }

            //var result = await userManager.DeleteAsync(dUser);


            var result = await userService.Delete(dUser.Id.ToString());

            if (!result)
            {
                TempData["ErrorMessage"] = "Kullanıcı silinemedi!";
                return RedirectToAction("GetAllUsers", "User");
            }

            //if (!result.Succeeded)
            //{
            //    TempData["ErrorMessage"] = "Kullanıcı silinemedi!";
            //    return RedirectToAction("GetAllUsers", "User");
            //}

            TempData["SuccessMessage"] = "Kullanıcı başarıyla silindi!";

            return RedirectToAction("GetAllUsers", "User");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(Guid id)
        {

            //var user = await userManager.Users.FirstOrDefaultAsync(x=>x.Id == id);


            return View(await userService.Select(u => u.Id == id));

            //return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(AppUser user)
        {
            //if (ModelState.IsValid)
            //{
            //    var existingUser = await userService.Select(x=>x.Id == user.Id);

            //    existingUser.PhoneNumber = user.PhoneNumber;
            //    existingUser.UserName = user.UserName;
            //    existingUser.Email = user.Email;

            //    var updatedUser = await userManager.UpdateAsync(existingUser);

            //    return RedirectToAction("GetAllUsers", "User");
            //}

            var result = await userService.UpdateUser(user);

            if (!result) { 
                TempData["ErrorMessage"] = "Kullanıcı güncellenirken bir sorun oluştu!";
            }

            TempData["SuccessMessage"] = "Kullanıcı güncellendi!";


            return RedirectToAction("GetAllUsers", "User");

        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View("CreateUser");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {

            if (await userService.Any(e => e.Email == userDto.Email))
            {
                TempData["ErrorMessage"] = "Bu email adresi kullanılıyor!";
                return RedirectToAction("GetAllUsers");
            }

            var newUser = new AppUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                PasswordHash = userDto.Password
            };

            var identityResult = await userService.CreateUser(newUser, userDto.Password);


            if (!identityResult)
            {
                TempData["ErrorMessage"] = "Hesap oluşturulamadı!";

                return RedirectToAction("GetAllUsers");
            }
            TempData["SuccessMessage"] = "Hesabınız başarıyla oluşturuldu!";

            return RedirectToAction("GetAllUsers");

        }
    }
}

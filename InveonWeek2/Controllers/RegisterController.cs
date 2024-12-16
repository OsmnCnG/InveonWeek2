using InveonWeek2.Dto;
using InveonWeek2.Repository;
using InveonWeek2.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Cryptography;
using System.Text;

namespace InveonWeek2.Controllers
{
    public class RegisterController(IUserService userService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Register(UserDto userDto)
        {

            if (await userService.Any(e => e.Email == userDto.Email))
            {
                TempData["ErrorMessage"] = "Bu email adresi kullanılıyor!";
                return RedirectToAction("Index");
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
                return RedirectToAction("Register");
            }
            TempData["SuccessMessage"] = "Hesabınız başarıyla oluşturuldu!";

            return RedirectToAction("Index");
        }
    }
}

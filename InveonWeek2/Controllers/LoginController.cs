using InveonWeek2.Dto;
using InveonWeek2.Repository;
using InveonWeek2.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InveonWeek2.Controllers
{
    public class LoginController(RoleManager<AppRole> roleManager ,SignInManager<AppUser> signInManager, IUserService userService)  : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userService.Select(i=>i.Email == loginDto.Email);

            if (user == null)
            {
                return View("Index");
            }

            var checkUserPassword = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!checkUserPassword.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email veya şifreniz yanlış");
                return View("Index");
            }

            await signInManager.SignInAsync(user, new AuthenticationProperties() { IsPersistent = true });

            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Login");
        }

    }
}

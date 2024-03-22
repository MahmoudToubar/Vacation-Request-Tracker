using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vacation_Request_Tracker.Models;

namespace Vacation_Request_Tracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser>userManager, SignInManager<IdentityUser>signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerModel.Username,
                Email = registerModel.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerModel.Password);

            if (identityResult.Succeeded)
            {
                // assign this user the "User" role
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                if (roleIdentityResult.Succeeded)
                {

                    return RedirectToAction("Login");
                }
            }


            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var signInResult = await signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

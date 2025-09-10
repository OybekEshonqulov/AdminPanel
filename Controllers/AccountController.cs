using AdminPanel1.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AdminPanel1.Models; // agar User bu yerda bo‘lsa
using System.Threading.Tasks;

namespace AdminPanel1.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // 1. Email bo'yicha userni topamiz
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email yoki parol noto‘g‘ri");
                return View(model);
            }

            // 2. UserName bilan tizimga kiramiz
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if(await _userManager.IsInRoleAsync(user, "Admin"))
                
                    return RedirectToAction("Index", "Admin");
                
                else if (await _userManager.IsInRoleAsync(user, "User"))
                
                    return RedirectToAction("Index", "User");
                
            }

            ModelState.AddModelError("", "Email yoki parol noto‘g‘ri");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}


using Next2OnlineMedicalApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Next2OnlineMedicalApp.Data;
using Next2OnlineMedicalApp.Models;

namespace Next2OnlineMedicalApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

		// GET: Account/Login
		public IActionResult Login()
		{
			return View();
		}

		// POST: Account/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

				if (result.Succeeded)
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user != null)
					{
						var roles = await _userManager.GetRolesAsync(user);

						if (roles.Contains("Admin"))
						{
							return RedirectToAction("Index", "CPanel", new { area = "Admin" });
						}
						else if (roles.Contains("Doctor"))
						{
							return RedirectToAction("Index", "CPanel", new { area = "Doctor" });
						}
						else if (roles.Contains("Patient"))
						{
							return RedirectToAction("Index", "CPanel", new { area = "PatientArea" });
						}
						else
						{
							return RedirectToAction("Index", "Home");
						}
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				}
			}

			return View(model);
		}
		public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


            
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Next2OnlineMedicalApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CPanel : Controller
    {
		private readonly SignInManager<IdentityUser> _signInManager;

		public CPanel(SignInManager<IdentityUser> signInManager)
		{
			_signInManager = signInManager;
		}
		public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home", new { area = "" }); 
		}
	}
}

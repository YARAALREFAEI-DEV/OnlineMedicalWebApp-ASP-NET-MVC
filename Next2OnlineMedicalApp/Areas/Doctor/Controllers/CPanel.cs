using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Next2OnlineMedicalApp.Data;

namespace Next2OnlineMedicalApp.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class CPanel : Controller
    {




        private readonly AppDbContext _context;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public CPanel(AppDbContext appDbContext,  SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = appDbContext;
            _signInManager = signInManager;
            _userManager = userManager; 
        }
        public IActionResult Index(string doctortName)
        {
            ViewBag.DoctortName = doctortName;
            return View();
        }
        // GET: /Profile/Index
        //public async Task<IActionResult> Profile()
        //{
        //    var userId = _userManager.GetUserId(User);
        //    if (userId == null)
        //    {
        //        return NotFound();
        //    }

        //    var identityUser = await _userManager.FindByIdAsync(userId);
        //    if (identityUser == null)
        //    {
        //        return NotFound();
        //    }

        //    // Map IdentityUser to Doctor model
        //    var doctor = new Next2OnlineMedicalApp.Models.Doctor
        //    {
        //        FirstName = identityUser.UserName!,
        //        Email = identityUser.Email!,
        //        // Add other properties as necessary
        //    };

        //    return View(doctor);
        //}
    


    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" }); 
        }
    }
    }

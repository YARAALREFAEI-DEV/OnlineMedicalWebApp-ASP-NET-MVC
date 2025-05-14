using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Next2OnlineMedicalApp.Data;
using Next2OnlineMedicalApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Next2OnlineMedicalApp.Areas.PatientArea.Controllers
{
    [Area("PatientArea")]
    public class PatientsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PatientsController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PatientArea/Patients
        public async Task<IActionResult> Index()
        {
            var userEmail = _userManager.GetUserName(User);

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Email == userEmail);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: PatientArea/Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: PatientArea/Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: PatientArea/Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,UserId,Password,Email,FirstName,LastName,PhoneNumber,DateOfBirth,Address,MedicalHistory")] Next2OnlineMedicalApp.Models.Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }
    }
}

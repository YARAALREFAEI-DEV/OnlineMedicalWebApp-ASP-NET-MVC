using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Next2OnlineMedicalApp.Data;
using Next2OnlineMedicalApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Next2OnlineMedicalApp.Areas.PatientArea.Controllers
{
    [Area("PatientArea")]
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentsController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PatientArea/Appointments
        public async Task<IActionResult> Index()
        {
            var userEmail = _userManager.GetUserName(User);

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Email == userEmail);

            if (patient == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Where(a => a.PatientId == patient.PatientId)
                .ToListAsync();

            return View(appointments ?? new List<Appointment>());
        }


        // GET: PatientArea/Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: PatientArea/Appointments/Create
        public async Task<IActionResult> Create()
        {
            var doctors = await _context.Doctors.ToListAsync();

            if (doctors == null || !doctors.Any())
            {
                return NotFound("No doctors found.");
            }

            ViewBag.DoctorList = new SelectList(doctors, "DoctorId", "FullNameWithSpecialization");

            return View();
        }




        // POST: PatientArea/Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,DoctorId,AppointmentDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Status = Appointment.AppointmentStatus.Confirmed;
                var userEmail = _userManager.GetUserName(User);

                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Email == userEmail);

                if (patient == null)
                {
                    return NotFound();
                }

                var existingAppointment = await _context.Appointments
                    .FirstOrDefaultAsync(a => a.PatientId == patient.PatientId);

                if (existingAppointment != null)
                {
                    ModelState.AddModelError("", "You already have an appointment.");
                    return View(appointment);
                }

                appointment.PatientId = patient.PatientId;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = appointment.AppointmentId });
            }
            return View(appointment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch appointment including related doctor data
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }


        // POST: PatientArea/Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,PatientId,DoctorId,AppointmentDate,Status")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = appointment.AppointmentId });
            }
            return View(appointment);
        }

        // GET: PatientArea/Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            var userEmail = _userManager.GetUserName(User);
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Email == userEmail);

            if (patient == null || appointment.PatientId != patient.PatientId)
            {
                return Unauthorized();
            }

            return View(appointment);
        }

        // POST: PatientArea/Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}

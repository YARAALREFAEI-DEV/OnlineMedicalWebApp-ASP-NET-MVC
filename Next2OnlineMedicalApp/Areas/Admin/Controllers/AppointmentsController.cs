using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Next2OnlineMedicalApp.Data;
using Next2OnlineMedicalApp.Models;

namespace Next2OnlineMedicalApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Appointments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Appointments.Include(a => a.Doctor);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Admin/Appointments/Create
        public IActionResult Create()
        {

            ViewBag.Statuses = Enum.GetValues(typeof(Next2OnlineMedicalApp.Models.Appointment.AppointmentStatus))
                         .Cast<Next2OnlineMedicalApp.Models.Appointment.AppointmentStatus>()
                         .Select(s => new SelectListItem
                         {
                             Value = s.ToString(),
                             Text = s.ToString()
                         }).ToList();
            var doctors = _context.Doctors
                .Select(d => new SelectListItem
                {
                    Value = d.DoctorId.ToString(),
                    Text = d.FullNameWithSpecialization
                }).ToList();

            ViewBag.DoctorId = new SelectList(doctors, "Value", "Text");
            return View();
        }




        // POST: Admin/Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,PatientId,DoctorId,AppointmentDate,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Address", appointment.DoctorId);
            return View(appointment);
        }

        // GET: Admin/Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Statuses = Enum.GetValues(typeof(Next2OnlineMedicalApp.Models.Appointment.AppointmentStatus))
                        .Cast<Next2OnlineMedicalApp.Models.Appointment.AppointmentStatus>()
                        .Select(s => new SelectListItem
                        {
                            Value = s.ToString(),
                            Text = s.ToString()
                        }).ToList();
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var doctors = _context.Doctors
                .Select(d => new SelectListItem
                {
                    Value = d.DoctorId.ToString(),
                    Text = d.FullNameWithSpecialization
                }).ToList();

            ViewBag.DoctorId = new SelectList(doctors, "Value", "Text", appointment.DoctorId.ToString());
            return View(appointment);
        }


        // POST: Admin/Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Address", appointment.DoctorId);
            return View(appointment);
        }

        // GET: Admin/Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Admin/Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}

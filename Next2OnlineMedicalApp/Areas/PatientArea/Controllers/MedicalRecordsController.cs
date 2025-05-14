using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Next2OnlineMedicalApp.Data;
using Next2OnlineMedicalApp.Models;

namespace Next2OnlineMedicalApp.Areas.PatientArea.Controllers
{
    [Area("PatientArea")]
    public class MedicalRecordsController : Controller
    {
        private readonly AppDbContext _context;

        public MedicalRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PatientArea/MedicalRecords
        public async Task<IActionResult> Index()
        {
            return View(await _context.MedicalRecords.ToListAsync());
        }
        // GET: PatientArea/MedicalRecords/Details/5
        public async Task<IActionResult> Details(int? id, int doctorId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = await _context.MedicalRecords
                .Where(m => m.RecordId == id && m.DoctorId == doctorId)
                .FirstOrDefaultAsync();

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }



        private bool MedicalRecordExists(int id)
        {
            return _context.MedicalRecords.Any(e => e.RecordId == id);
        }
    }
}

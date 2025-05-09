﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HR_Tool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HR_Tool.Controllers
{
    public class SummaryController : Controller
    {
        private readonly HRDBContext _context;

        public SummaryController(HRDBContext context)
        {
            _context = context;
        }

        // GET: Summary/Index
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            var competencies = await _context.Competency.ToListAsync();
            var proficiencies = await _context.Proficiencies.ToListAsync();

            var jobModels = await _context.JobModel
                .Include(j => j.Competency)
                .Include(j => j.Proficiencies)
                .ToListAsync();

            ViewBag.CompetencyList = new SelectList(competencies, "CompetencyID", "CompetencyName");
            ViewBag.ProficiencyList = new SelectList(proficiencies, "ProficiencyID", "ProficiencyLevel");

            ViewBag.JobModels = jobModels;

            return View(new SummaryViewModel());
        }
    }
}
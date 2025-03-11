using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR_Tool.Models;

namespace HR_Tool.Controllers
{
    public class JobModelController : Controller
    {
        private readonly HRDBContext _context;

        public JobModelController(HRDBContext context)
        {
            _context = context;
        }

        // GET: JobModel
        public async Task<IActionResult> Index()
        {
            var hRDBContext = _context.JobModel.Include(j => j.Categories).Include(j => j.Competency).Include(j => j.Proficiencies);
            return View(await hRDBContext.ToListAsync());
        }

        // GET: JobModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobModel = await _context.JobModel
                .Include(j => j.Categories)
                .Include(j => j.Competency)
                .Include(j => j.Proficiencies)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobModel == null)
            {
                return NotFound();
            }

            return View(jobModel);
        }

        // GET: JobModel/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryName");
            ViewData["CompetencyID"] = new SelectList(_context.Set<Competency>(), "CompetencyID", "CompetencyName");
            ViewData["ProficiencyID"] = new SelectList(_context.Set<Proficiency>(), "ProficiencyID", "ProficiencyLevel");
            return View();
        }

        // POST: JobModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,Archived,Active,CompetencyID,ProficiencyID,CategoryID")] JobModel jobModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryName", jobModel.CategoryID);
            ViewData["CompetencyID"] = new SelectList(_context.Set<Competency>(), "CompetencyID", "CompetencyName", jobModel.CompetencyID);
            ViewData["ProficiencyID"] = new SelectList(_context.Set<Proficiency>(), "ProficiencyID", "ProficiencyLevel", jobModel.ProficiencyID);
            return View(jobModel);
        }

        // GET: JobModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobModel = await _context.JobModel.FindAsync(id);
            if (jobModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryName", jobModel.CategoryID);
            ViewData["CompetencyID"] = new SelectList(_context.Set<Competency>(), "CompetencyID", "CompetencyName", jobModel.CompetencyID);
            ViewData["ProficiencyID"] = new SelectList(_context.Set<Proficiency>(), "ProficiencyID", "ProficiencyLevel", jobModel.ProficiencyID);
            return View(jobModel);
        }

        // POST: JobModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,Archived,Active,CompetencyID,ProficiencyID,CategoryID")] JobModel jobModel)
        {
            if (id != jobModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobModelExists(jobModel.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryName", jobModel.CategoryID);
            ViewData["CompetencyID"] = new SelectList(_context.Set<Competency>(), "CompetencyID", "CompetencyName", jobModel.CompetencyID);
            ViewData["ProficiencyID"] = new SelectList(_context.Set<Proficiency>(), "ProficiencyID", "ProficiencyLevel", jobModel.ProficiencyID);
            return View(jobModel);
        }

        // GET: JobModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobModel = await _context.JobModel
                .Include(j => j.Categories)
                .Include(j => j.Competency)
                .Include(j => j.Proficiencies)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobModel == null)
            {
                return NotFound();
            }

            return View(jobModel);
        }

        // POST: JobModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobModel = await _context.JobModel.FindAsync(id);
            if (jobModel != null)
            {
                _context.JobModel.Remove(jobModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobModelExists(int id)
        {
            return _context.JobModel.Any(e => e.ID == id);
        }
    }
}

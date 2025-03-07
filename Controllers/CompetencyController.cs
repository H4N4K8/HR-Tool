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
    public class CompetencyController : Controller
    {
        private readonly HRDBContext _context;

        public CompetencyController(HRDBContext context)
        {
            _context = context;
        }

        // GET: Competency
        public async Task<IActionResult> Index()
        {
            return View(await _context.Competency.ToListAsync());
        }

        // GET: Competency/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competency = await _context.Competency
                .FirstOrDefaultAsync(m => m.CompetencyID == id);
            if (competency == null)
            {
                return NotFound();
            }

            return View(competency);
        }

        // GET: Competency/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Competency/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetencyID,CompetencyName")] Competency competency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(competency);
        }

        // GET: Competency/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competency = await _context.Competency.FindAsync(id);
            if (competency == null)
            {
                return NotFound();
            }
            return View(competency);
        }

        // POST: Competency/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CompetencyID,CompetencyName")] Competency competency)
        {
            if (id != competency.CompetencyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetencyExists(competency.CompetencyID))
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
            return View(competency);
        }

        // GET: Competency/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competency = await _context.Competency
                .FirstOrDefaultAsync(m => m.CompetencyID == id);
            if (competency == null)
            {
                return NotFound();
            }

            return View(competency);
        }

        // POST: Competency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var competency = await _context.Competency.FindAsync(id);
            if (competency != null)
            {
                _context.Competency.Remove(competency);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetencyExists(string id)
        {
            return _context.Competency.Any(e => e.CompetencyID == id);
        }
    }
}

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
    public class ProficiencyController : Controller
    {
        private readonly HRDBContext _context;

        public ProficiencyController(HRDBContext context)
        {
            _context = context;
        }

        // GET: Proficiency
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proficiencies.ToListAsync());
        }

        // GET: Proficiency/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proficiency = await _context.Proficiencies
                .FirstOrDefaultAsync(m => m.ProficiencyID == id);
            if (proficiency == null)
            {
                return NotFound();
            }

            return View(proficiency);
        }

        // GET: Proficiency/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proficiency/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProficiencyID,ProficiencyLevel")] Proficiency proficiency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proficiency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proficiency);
        }

        // GET: Proficiency/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proficiency = await _context.Proficiencies.FindAsync(id);
            if (proficiency == null)
            {
                return NotFound();
            }
            return View(proficiency);
        }

        // POST: Proficiency/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProficiencyID,ProficiencyLevel")] Proficiency proficiency)
        {
            if (id != proficiency.ProficiencyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proficiency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProficiencyExists(proficiency.ProficiencyID))
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
            return View(proficiency);
        }

        // GET: Proficiency/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proficiency = await _context.Proficiencies
                .FirstOrDefaultAsync(m => m.ProficiencyID == id);
            if (proficiency == null)
            {
                return NotFound();
            }

            return View(proficiency);
        }

        // POST: Proficiency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var proficiency = await _context.Proficiencies.FindAsync(id);
            if (proficiency != null)
            {
                _context.Proficiencies.Remove(proficiency);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProficiencyExists(string id)
        {
            return _context.Proficiencies.Any(e => e.ProficiencyID == id);
        }
    }
}

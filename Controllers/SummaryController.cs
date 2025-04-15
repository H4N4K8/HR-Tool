using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HR_Tool.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index(SummaryViewModel summaryModel)
        {
            var competencies = await _context.Competency.ToListAsync();
            var proficiencies = await _context.Proficiencies.ToListAsync();

            if (competencies == null || proficiencies == null)
            {
                // Handle the case where the lists are null or empty
                return View("Error"); // Or return a default view with a message.
            }

            // Populate ViewBag with the list data
            ViewBag.CompetencyID = new SelectList(competencies, "CompetencyID", "CompetencyName");
            ViewBag.ProficiencyID = new SelectList(proficiencies, "ProficiencyID", "ProficiencyLevel");

            var data = new SummaryViewModel
            {
                competency = competencies,
                proficiency = proficiencies,
                Description = summaryModel?.Description,
                Active = summaryModel?.Active ?? true, // Defaulting to true if null
                Archived = summaryModel?.Archived ?? false // Defaulting to false if null
            };

            return View(data);
        }

        [HttpPost]
        public IActionResult Preview(string SelectedItemsJson, string? Description, bool Active, bool Archived)
        {
            var selectedItems = new List<SelectedItem>();

            // Deserialize the SelectedItemsJson to List<SelectedItem>
            if (!string.IsNullOrEmpty(SelectedItemsJson))
            {
                selectedItems = System.Text.Json.JsonSerializer.Deserialize<List<SelectedItem>>(SelectedItemsJson);
            }

            // Prepare the model with selected items and other properties
            var model = new SummaryViewModel
            {
                SelectedItems = selectedItems, // Ensure this is populated with items
                Description = Description,
                Active = Active,
                Archived = Archived
            };

            return View(model);
        }
    }
}
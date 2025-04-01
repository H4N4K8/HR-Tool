using HR_Tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SummaryController : Controller
{
    private readonly HRDBContext _context;

    public SummaryController(HRDBContext context)
    {
        _context = context;
    }

    // GET: Summary/Index
    public async Task<IActionResult> IndexAsync()
    {
        // Fetching data from the database
        var competencies = await _context.Competency.ToListAsync();
        var proficiencies = await _context.Proficiencies.ToListAsync();

        var viewModel = new SummaryViewModel.IndexViewModel
        {
            CompetencyList = competencies,
            ProficiencyList = proficiencies,
            // Retrieve the list of selected items from TempData
            SelectedCompetencyProficiencyList = TempData["SelectedCompetencyProficiencyList"] as List<string> ?? new List<string>()
        };

        return View(viewModel);
    }

    // POST: Summary/Index - Add selected competency and proficiency to the list
    [HttpPost]
    public async Task<IActionResult> IndexAsync(SummaryViewModel.IndexViewModel model)
    {
        // Fetch selected competency and proficiency from the database using string IDs
        var selectedCompetency = await _context.Competency
            .FirstOrDefaultAsync(c => c.CompetencyID == model.SelectedCompetencyId);
        var selectedProficiency = await _context.Proficiencies
            .FirstOrDefaultAsync(p => p.ProficiencyID == model.SelectedProficiencyId);

        if (selectedCompetency != null && selectedProficiency != null)
        {
            // Get the existing list from TempData, if any
            var selectedList = TempData["SelectedCompetencyProficiencyList"] as List<string> ?? new List<string>();

            // Add the new competency-proficiency pair to the list
            var newItem = $"{selectedCompetency.CompetencyName} - {selectedProficiency.ProficiencyLevel}";
            selectedList.Add(newItem);

            // Store the updated list back into TempData
            TempData["SelectedCompetencyProficiencyList"] = selectedList;
        }

        // Repopulate dropdown lists for the form
        model.CompetencyList = await _context.Competency.ToListAsync();
        model.ProficiencyList = await _context.Proficiencies.ToListAsync();

        // Pass the updated list to the view model
        model.SelectedCompetencyProficiencyList = TempData["SelectedCompetencyProficiencyList"] as List<string> ?? new List<string>();

        return View(model);
    }

    // POST: Summary/Index - Delete an item from the list
    [HttpPost]
    public IActionResult DeleteItem(string itemToDelete)
    {
        // Get the current list from TempData
        var selectedList = TempData["SelectedCompetencyProficiencyList"] as List<string> ?? new List<string>();

        // Remove the item if it exists
        if (selectedList.Contains(itemToDelete))
        {
            selectedList.Remove(itemToDelete);
        }

        // Store the updated list back into TempData
        TempData["SelectedCompetencyProficiencyList"] = selectedList;

        return RedirectToAction("Index");
    }
}
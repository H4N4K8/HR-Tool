using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR_Tool.Models
{
    public class SummaryViewModel
    {
        public string? CompetencyID { get; set; }
        public string? ProficiencyID { get; set; }
        public string? CompetencyName { get; set; }
        public string? ProficiencyLevel { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool Active { get; set; }
        public bool Archived { get; set; }

        // For transferring multiple selected items
        public List<SummaryViewModel> SelectedSummaries { get; set; } = new();
    }
}

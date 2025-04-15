using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR_Tool.Models
{
    public class SummaryViewModel
    {
        public List<Competency> competency { get; set; } = new List<Competency>();
        public List<Proficiency> proficiency { get; set; } = new List<Proficiency>();
        public string? Description { get; set; }
        public bool Archived { get; set; } = false;
        public bool Active { get; set; } = true;

        // New: List of selected competency/proficiency pairs
        public List<SelectedItem> SelectedItems { get; set; } = new();
    }

    public class SelectedItem
    {
        public string? competency { get; set; }
        public string? proficiency { get; set; }
    }
}

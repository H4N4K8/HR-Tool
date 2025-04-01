namespace HR_Tool.Models
{
    public class SummaryViewModel
    {
        public class IndexViewModel
        {
            // These will be used for the dropdown selections
            public List<Competency> CompetencyList { get; set; }
            public List<Proficiency> ProficiencyList { get; set; }

            // These represent the current selections from the form
            public string? SelectedCompetencyId { get; set; }
            public string? SelectedProficiencyId { get; set; }

            // These are the selected values to be displayed on the page
            public string? SelectedCompetencyName { get; set; }
            public string? SelectedProficiencyLevel { get; set; }
            public List<string> SelectedCompetencyProficiencyList { get; set; } = new List<string>();
        }
    }
}

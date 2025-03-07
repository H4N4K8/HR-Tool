using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace HR_Tool.Models
{
    public class JobModel
    {
        public int ID { get; set; }
        public string? Description { get; set; }
        public bool Archived { get; set; }
        public bool Active { get; set; }

        // pulling in model FKs
        [ValidateNever]
        public Competency Competency { get; set; } = null!;
        [Required(ErrorMessage ="Please enter a Competency")]
        public string CompetencyID { get; set; } = string.Empty;
        
        [ValidateNever]
        public Proficiency Proficiencies { get; set; } = null!;
        [Required(ErrorMessage = "Please enter a Proficiency")]
        public string ProficiencyID { get; set; } = string.Empty;

        [ValidateNever]
        public Category Categories { get; set; } = null!;
        [Required(ErrorMessage = "Please enter a Category")]
        public string CategoryID { get; set; } = string.Empty;

    }
}

using System.ComponentModel.DataAnnotations;

namespace FormWise.WebApp.Models
{
    public class ReimbursementFormModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 250 characters.")]
        public string Description { get; set; }

        [Required]
        public IFormFile Receipt { get; set; }

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }

}

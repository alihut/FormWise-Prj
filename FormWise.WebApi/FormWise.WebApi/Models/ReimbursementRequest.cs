using System.ComponentModel.DataAnnotations;

namespace FormWise.WebApi.Models
{
    public class ReimbursementRequest
    {
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 250 characters.")]
        public string Description { get; set; }
        [Required]
        public IFormFile Receipt { get; set; }
    }
}

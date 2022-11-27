using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Core.Models
{
    public class CostViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Amount per year")]
        [Precision(18, 2)]
        [Range(0.00, 200000.00, ErrorMessage = "Amount must be a positive number and less than {2} digits")]
        public decimal Amount { get; set; }

        [Display(Name = "CostCategoryId")]
        public int? CostCategoryId { get; set; }

        public IEnumerable<GeneralCostTypeViewModel> GeneralCostType { get; set; }
            = new List<GeneralCostTypeViewModel>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Core.Models.GeneralCost
{
    public class CostViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Amount per year")]
        [Range(0.00, 9000000.00, ErrorMessage = "Amount must be a positive number and less than {2} digits")]
        public decimal Amount { get; set; }

        [Display(Name = "CostCategoryId")]
        public int? CostCategoryId { get; set; }
        public string DefaultCurrency { get; set; } = null!;
        public string CostCenterName { get; set; } = null!;
            
        //public IEnumerable<GeneralCostCenterViewModel> GeneralCostType { get; set; }
        //    = new List<GeneralCostCenterViewModel>();

    }
}

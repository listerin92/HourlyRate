using System.ComponentModel.DataAnnotations;


namespace HourlyRate.Core.Models.GeneralCost
{
    public class AddCostViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Amount per year")]
        [Range(0.00, 9000000.00, ErrorMessage = "Amount must be a positive number and less than {2} digits")]
        public decimal Amount { get; set; }

        [Display(Name = "Cost Category")]
        public int? CostCategoryId { get; set; }

        public IEnumerable<GeneralCostTypeViewModel> GeneralCostType { get; set; }
            = new List<GeneralCostTypeViewModel>();

        [Display(Name = "Cost Center")]
        public int? CostCenterId { get; set; }
        public IEnumerable<GeneralCostCenterViewModel> GeneralCostCenter { get; set; }
            = new List<GeneralCostCenterViewModel>();

    }
}

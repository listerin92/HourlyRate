using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Core.Models.GeneralCost
{
    public class CostCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;
        public int? CostCategoryId { get; set; }

        public IEnumerable<GeneralCostTypeViewModel> GeneralCostType { get; set; }
            = new List<GeneralCostTypeViewModel>();
    }
}

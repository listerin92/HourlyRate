using HourlyRate.Core.Models.Employee;
using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Core.Models.CostCenter
{
    public class AddCostCenterViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        public decimal FloorSpace { get; set; }
        public decimal AvgPowerConsumptionKwh { get; set; }
        
        [Required]
        [Display(Name = "AnnualHours")]
        public decimal AnnualHours { get; set; }

        [Required]
        [Display(Name = "AnnualChargeableHours")]
        public decimal AnnualChargeableHours { get; set; }

        [Required]
        [Display(Name = "Add Employee Department Wages")]
        public int DepartmentId { get; set; }
        public IEnumerable<EmployeeDepartmentModel> EmployeeDepartments { get; set; }
            = new List<EmployeeDepartmentModel>();
        [Display(Name = "Using Water")]
        public bool IsUsingWater { get; set; } = false;

        public bool IsActive { get; set; } = true;
    }
}

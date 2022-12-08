using HourlyRate.Infrastructure.Data.Models.Employee;
using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Core.Models.CostCenter
{
    public class CostCenterViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public string DefaultCurrency { get; set; } = null!;
        [Required]
        public decimal FloorSpace { get; set; }
        public decimal AvgPowerConsumptionKwh { get; set; }
        [Required]
        public decimal AnnualHours { get; set; }
        [Required]
        public decimal AnnualChargeableHours { get; set; }
        public int DirectAllocatedStuff { get; set; }
        public int? DepartmentId { get; set; }

        public Department Department { get; set; } = null!;
        public decimal TotalPowerConsumption { get; set; }

        [Required]
        public decimal DirectWagesCost { get; set; }
        public decimal DirectRepairCost { get; set; }
        public decimal DirectGeneraConsumablesCost { get; set; }
        public decimal DirectDepreciationCost { get; set; }
        public decimal TotalDirectCosts { get; set; }
        public decimal DirectElectricityCost { get; set; }
        public decimal RentCost { get; set; }

        public decimal TotalMixCosts { get; set; }

        /// <summary>
        /// TotalDirectCostOfAllCostCategories / CurrentCostCategoryDirectTotalCost
        /// </summary>
        public decimal TotalIndex { get; set; }

        /// <summary>
        /// TotalDirectCosOfCostCatUsingWater / CurrentCostCategoryUsingWaterDirectTotalCost
        /// </summary>
        public decimal WaterTotalIndex { get; set; }

        public decimal IndirectHeatingCost { get; set; }
        public decimal IndirectWaterCost { get; set; }
        public decimal IndirectTaxes { get; set; }
        public decimal IndirectPhonesCost { get; set; }
        public decimal IndirectOtherCost { get; set; }
        public decimal IndirectAdministrationWagesCost { get; set; }
        public decimal IndirectMaintenanceWagesCost { get; set; }
        public decimal IndirectDepreciationCost { get; set; }
        public decimal IndirectTotalCosts { get; set; }

        public decimal TotalCosts { get; set; }

        public decimal WagesPerMonth { get; set; }
        public decimal MachinesPerMonth { get; set; }
        public decimal OverheadsPerMonth { get; set; }

        public decimal WagesPerHour { get; set; }
        public decimal MachinesPerHour { get; set; }
        public decimal OverheadsPerHour { get; set; }
        public decimal TotalHourlyCostRate { get; set; }


    }
}

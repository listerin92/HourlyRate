﻿using HourlyRate.Core.Models.Employee;
using HourlyRate.Infrastructure.Data.Models.Employee;
using System.ComponentModel.DataAnnotations;
using HourlyRate.Core.Models.GeneralCost;

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
        public decimal AnnualHours { get; set; }
        [Required]
        public decimal AnnualChargeableHours { get; set; }
        public int DepartmentId { get; set; }
        public IEnumerable<EmployeeDepartmentModel> EmployeeDepartments { get; set; }
            = new List<EmployeeDepartmentModel>();
        [Display(Name = "Using Water")]
        public bool IsUsingWater { get; set; } = false;
    }
}
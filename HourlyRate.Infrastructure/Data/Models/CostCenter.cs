using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Infrastructure.Data.Models
{
    public class CostCenter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal FloorSpace { get; set; }

        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal AvgPowerConsumptionKwh { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal AnnualHours { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal AnnualChargeableHours { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; } = null!;

        public bool IsUsingWater { get; set; } = false;


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}

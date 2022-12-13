using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HourlyRate.Infrastructure.Data.Models.CostCategories;
using HourlyRate.Infrastructure.Data.Models.Costs;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Infrastructure.Data.Models
{
    public class Expenses
    {

        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }

        [ForeignKey(nameof(Employee))]
        public int? EmployeeId { get; set; }
        public Employee.Employee? Employee { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal Amount { get; set; }


        [ForeignKey(nameof(FinancialYear))]
        public int FinancialYearId { get; set; }
        public FinancialYear FinancialYear { get; set; } = null!;


        [ForeignKey(nameof(Consumable))]
        public int? ConsumableId { get; set; }
        public Consumable? Consumable { get; set; } = null!;


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;


        [ForeignKey(nameof(CostCategories))]
        public int? CostCategoryId { get; set; }

        public CostCategory? CostCategories { get; set; }

        [ForeignKey(nameof(CostCenter))]
        public int? CostCenterId { get; set; }

        public CostCenter? CostCenter { get; set; }
        public bool IsDeleted { get; set; }
    }
}

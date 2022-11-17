using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HourlyRate.Infrastructure.Models.Costs;
using HourlyRate.Infrastructure.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Infrastructure.Models
{
    public class Expenses
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Employee))]
        public int? EmployeeId { get; set; }

        public Employee.Employee Employee { get; set; } = null!;



        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        [ForeignKey(nameof(FinancialYear))]
        public int FinancialYearId { get; set; }
        public FinancialYear FinancialYear { get; set; } = null!;

        [ForeignKey(nameof(Rent))]
        public int? RentId { get; set; }
        public Rent? Rent { get; set; } = null!;

        [ForeignKey(nameof(Tax))]
        public int? TaxId { get; set; }
        public Tax? Tax { get; set; } = null!;

        [ForeignKey(nameof(Consumable))]
        public int? ConsumableId { get; set; }
        public Consumable? Consumable { get; set; } = null!;



        [ForeignKey(nameof(Company))] 
        public Guid? CompanyId { get; set; }

        public Company? Company { get; set; } = null!;
    }
}

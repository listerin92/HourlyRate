using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HourlyRate.Core.Models.Employee
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        [Required] 
        public decimal Amount { get; set; }

        [ForeignKey(nameof(FinancialYear))]
        public int FinancialYearId { get; set; }

        public FinancialYear FinancialYear { get; set; } = null!;
    }
}

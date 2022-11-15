using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HourlyRate.Core.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Infrastructure.Models.Employee
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }

        public Infrastructure.Models.Employee.Employee Employee { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        [ForeignKey(nameof(FinancialYear))]
        public int FinancialYearId { get; set; }

        public FinancialYear FinancialYear { get; set; } = null!;
    }
}

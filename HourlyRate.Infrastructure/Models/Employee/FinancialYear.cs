namespace HourlyRate.Infrastructure.Models.Employee
{
    public class FinancialYear
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; } = false;

    }
}

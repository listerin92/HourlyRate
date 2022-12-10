namespace HourlyRate.Areas.Admin.Models
{
    public class FinancialYearsViewModel
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<FinancialYearsViewModel> FinancialYears { get; set; }
            = new List<FinancialYearsViewModel>();
    }
}

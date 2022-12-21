namespace HourlyRate.Core.Models.Spektar
{
    public class SpektarCostCategoryViewModel
    {
        public string CostCategoryId { get; set; } = null!;
        public string CostCategory { get; set; } = null!;
        public double Wages { get; set; }
        public double Machines { get; set; }
        public double Overhead { get; set; }
        public double Total => Wages + Machines + Overhead;
    }
}

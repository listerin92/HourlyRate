using System.ComponentModel.DataAnnotations.Schema;

namespace HourlyRate.Infrastructure.Models.Costs
{
    public class Consumable
    {
        public int Id { get; set; }
        public string ArticleName { get; set; } = null!;

        [ForeignKey(nameof(Machine))]
        public int MachineId { get; set; }
        public Machine? Machine { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid? CompanyId { get; set; }

        public Company? Company { get; set; } = null!;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourlyRate.Core.Models
{
    public class EmployeeViewModelCurrency : EmployeeViewModel
    {
        public string DefaultCurrency { get; set; } = null!;

    }
}

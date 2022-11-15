using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models;
using HourlyRate.Infrastructure.Models.Employee;
using Microsoft.EntityFrameworkCore;
using WebShopDemo.Core.Data.Common;

namespace HourlyRate.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository _repo;

        public EmployeeService(IRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<EmployeeViewModel>> AllEmployees()
        {
            return await _repo.AllReadonly<Salary>()
                .Where(y => y.FinancialYear.Year == 2022)
                .Select(e => new EmployeeViewModel()
                {
                    Id = e.Id,
                    FirstName = e.Employee.FirstName,
                    LastName = e.Employee.LastName,
                    Salary = e.Amount
                })
                .ToListAsync();
        }
    }
}

using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models;
using HourlyRate.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HourlyRate.Infrastructure.Data.Common;
using HourlyRate.Infrastructure.Data.Models.Account;
using HourlyRate.Infrastructure.Data.Models.Employee;

namespace HourlyRate.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository _repo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(IRepository repo,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _repo = repo;
        }
        public async Task<IEnumerable<EmployeeViewModel>> AllEmployees()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return await _repo.AllReadonly<Expenses>()
                .Where(y => y.FinancialYear.Year == 2022)//TODO: get company id == userCompanyId
                .Select(e => new EmployeeViewModel()
                {
                    Id = e.Id,
                    FirstName = e.Employee!.FirstName,
                    LastName = e.Employee.LastName,
                    Salary = e.Amount
                })
                .ToListAsync();

        }

        public async Task<IEnumerable<EmployeeDepartmentModel>> AllDepartments()
        {
            return await _repo.AllReadonly<Department>()
                .OrderBy(c => c.Name)
                .Select(c => new EmployeeDepartmentModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<bool> DepartmentExists(int categoryId)
        {
            return await _repo.AllReadonly<Department>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> CreateEmployee(EmployeeViewModel model, Guid companyId)
        {
            var employee = new Employee()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CompanyId = companyId,
                JobTitle = model.JobTitle,
                DepartmentId = model.DepartmentId,
                IsEmployee = true
            };

            await _repo.AddAsync(employee);
            await _repo.SaveChangesAsync();

            return employee.Id;
        }

        public async Task CreateExpensesByEmployee(int employeeId, EmployeeViewModel employee,  Guid companyId)
        {
            var expense = new Expenses()
            {
                EmployeeId = employeeId,
                Amount = employee.Salary,
                CompanyId = companyId,
                FinancialYearId = 8 //TODO: implement financial years
            };
            await _repo.AddAsync(expense);
            await _repo.SaveChangesAsync();
        }
    }

}

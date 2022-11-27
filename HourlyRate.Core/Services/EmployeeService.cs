using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models;
using HourlyRate.Infrastructure.Data;
using HourlyRate.Infrastructure.Data.Common;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository _repo;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(IRepository repo
            , ApplicationDbContext dbContext
            )
        {
            _dbContext = dbContext;
            _repo = repo;
        }
        public async Task<IEnumerable<EmployeeViewModelCurrency>> AllEmployeesWithSalary(Guid companyId)
        {

            return await _repo.AllReadonly<Expenses>()
                .Where(y => y.FinancialYear.Year == 2022 && y.CompanyId == companyId && y.Employee.IsEmployee == true)//TODO: get company id == userCompanyId
                .Select(e => new EmployeeViewModelCurrency()
                {
                    Id = e.Employee!.Id,
                    FirstName = e.Employee!.FirstName,
                    LastName = e.Employee.LastName,
                    ImageUrl = e.Employee.ImageUrl,
                    Salary = e.Amount,
                    DefaultCurrency = e.Company.DefaultCurrency,
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

        public async Task<bool> DepartmentExists(int? departmentId)
        {
            return await _repo.AllReadonly<Department>()
                .AnyAsync(c => c.Id == departmentId);
        }

        public async Task<int> CreateEmployee(EmployeeViewModel model, Guid companyId)
        {
            var employee = new Employee()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CompanyId = companyId,
                JobTitle = model.JobTitle,
                ImageUrl = model.ImageUrl,
                DepartmentId = model.DepartmentId,
                IsEmployee = true
            };

            await _repo.AddAsync(employee);
            await _repo.SaveChangesAsync();

            return employee.Id;
        }

        public async Task CreateExpensesByEmployee(int employeeId, decimal amount, Guid companyId)
        {
            var expense = new Expenses()
            {
                EmployeeId = employeeId,
                Amount = amount,
                CompanyId = companyId,
                FinancialYearId = 8 //TODO: implement financial years
            };
            await _repo.AddAsync(expense);
            await _repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _repo.AllReadonly<Employee>()
                .AnyAsync(h => h.Id == id && h.IsEmployee);
        }

        public async Task Edit(int employeeId, EmployeeViewModel model, Guid companyId)
        {
            var employee = await _repo.GetByIdAsync<Employee>(employeeId);

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.CompanyId = companyId;
            employee.JobTitle = model.JobTitle;
            employee.ImageUrl = model.ImageUrl;
            employee.DepartmentId = model.DepartmentId;
            employee.IsEmployee = true;

            await _repo.SaveChangesAsync();

            var salary = GetEmployeeSalary(employeeId);
            var changeSalary = await _repo.GetByIdAsync<Expenses>(salary.Result.Id);

            changeSalary.Amount = model.Salary;
            await _repo.SaveChangesAsync();


        }

        public async Task<int> GetEmployeeDepartmentId(int employeeId)
        {
            var getEmployeeId = await _repo.GetByIdAsync<Employee>(employeeId);

            return (int)getEmployeeId.DepartmentId;

        }

        public async Task<Expenses> GetEmployeeSalary(int employeeId)
        {
            var salary = await _dbContext.Expenses.FirstAsync(s => s.EmployeeId == employeeId);

            return salary;
        }

        public async Task<EmployeeViewModel> EmployeeDetailsById(int id, Guid companyId)
        {
            return await _repo.AllReadonly<Employee>()
                .Where(e => e.IsEmployee && e.CompanyId == companyId)
                .Where(e => e.Id == id)
                .Select(e => new EmployeeViewModel()
                {
                    Id = id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    ImageUrl = e.ImageUrl,
                    JobTitle = e.JobTitle,
                    DepartmentId = e.DepartmentId,

                })
                .FirstAsync();
        }

        
        public async Task Delete(int employeeId)
        {
            var employee = await _repo.GetByIdAsync<Employee>(employeeId);
            employee.IsEmployee = false;

            await _repo.SaveChangesAsync();
        }
    }

}

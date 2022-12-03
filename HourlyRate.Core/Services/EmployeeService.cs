using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.Employee;
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

        private int ActiveFinancialYear()
        {
            return _repo.AllReadonly<FinancialYear>()
                .First(y => y.IsActive).Year;
        }
        private int ActiveFinancialYearId()
        {
            return _repo.AllReadonly<FinancialYear>()
                .First(y => y.IsActive).Id;
        }
        public async Task<IEnumerable<EmployeeViewModelCurrency>> AllEmployeesWithSalary(Guid companyId)
        {
            var currentYear = ActiveFinancialYear();

            //TODO: after seed need to set an active financial year, implement financial year in register!!!!
            var employees = _repo.AllReadonly<Expenses>()
                .Where(y => y.FinancialYear.Year == currentYear && y.CompanyId == companyId && y.Employee!.IsEmployee == true)
                .Select(e => new EmployeeViewModelCurrency()
                {
                    Id = e.Employee!.Id,
                    ImageUrl = e.Employee.ImageUrl,
                    FirstName = e.Employee!.FirstName,
                    LastName = e.Employee.LastName,
                    JobTitle = e.Employee.JobTitle,
                    Salary = e.Amount,
                    DefaultCurrency = e.Company.DefaultCurrency,
                    Department = e.Employee.Department!
                })
                .OrderBy(o => o.Department)
                .ToListAsync();

            return await employees;
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

        public async Task<int> CreateDepartment(AddEmployeeDepartmentViewModel model, Guid companyId)
        {
            var checkExist = _repo.AllReadonly<Department>()
                .FirstOrDefault(c => c.Name == model.Name)
                ?.Name;
            if (checkExist != null)
            {
                return -1;
            }

            var department = new Department()
            {
                Name = model.Name,
                CompanyId = companyId
            };

            await _repo.AddAsync(department);
            await _repo.SaveChangesAsync();
            return department.Id;
        }



        public async Task CreateExpensesByEmployee(int employeeId, decimal amount, Guid companyId)
        {
            var activeFinancialYearId = ActiveFinancialYearId();
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            var expense = new Expenses();

            try
            {
                var matchedCostCenter = _dbContext.CostCenters.FirstOrDefault(c => c.DepartmentId == employee.DepartmentId);

                {
                    expense.EmployeeId = employeeId;
                    expense.Amount = amount;
                    expense.CompanyId = companyId;
                    expense.FinancialYearId = activeFinancialYearId;
                    expense.CostCenterId = matchedCostCenter.Id;
                };
            }
            catch (Exception)
            {
                expense.EmployeeId = employeeId;
                expense.Amount = amount;
                expense.CompanyId = companyId;
                expense.FinancialYearId = activeFinancialYearId;
            }

            //find if Department is added to CostCenter                        


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

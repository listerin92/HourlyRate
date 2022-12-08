using HourlyRate.Core.Contracts;
using HourlyRate.Core.Services;
using HourlyRate.Infrastructure.Data;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace HourlyRate.UnitTests
{
    public class Tests
    {
        private ApplicationDbContext _dbContect;
        private IEnumerable<Expenses> expenses;
        private IEnumerable<Employee> employees;
        private IEnumerable<Department> departments;
        private IEnumerable<FinancialYear> financialYears;
        private ICostCenterService service;

        [OneTimeSetUp]
        public void TestInitialize()
        {

            expenses = new List<Expenses>()
            {
                new Expenses() { Id = 1, Amount = 500, EmployeeId = 1, FinancialYearId = 1},
                new Expenses() { Id = 2, Amount = 500, EmployeeId = 2, FinancialYearId = 1},
                new Expenses() { Id = 3, Amount = 500, EmployeeId = 3, FinancialYearId = 1},
            };
            employees = new List<Employee>()
            {
                new Employee() {Id = 1, DepartmentId = 1, FirstName = "Ivan", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 2, DepartmentId = 1, FirstName = "Petar", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 3, DepartmentId = 2, FirstName = "Stefan", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""}
            };
            departments = new List<Department>()
            {
                new Department() {Id = 1,Name = "Maintenance"},
                new Department() {Id = 2,Name = "Lamination"},
            };
            financialYears = new List<FinancialYear>()
            {
                new FinancialYear() { Id = 1, Year = 2022, IsActive = true }
            };


            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ExpensesInMemoryDb")
                .Options;

            _dbContect = new ApplicationDbContext(options);
            _dbContect.AddRange(expenses);
            _dbContect.AddRange(employees);
            _dbContect.AddRange(departments);
            _dbContect.SaveChanges();

            service = new CostCenterService(_dbContect);

        }


        [Test]
        public void AllDepartmentsTestCount()
        {
            var allDepartments = _dbContect.Departments;


            var result = service.AllDepartments();
            Assert.That(actual: result.Result.Count(), Is.EqualTo(expected: allDepartments.Count()));
        }


        [Test]
        public void AllDepartmentsTestName()
        {
            var allDepartments = _dbContect.Departments;


            var result = allDepartments.Where(n => n.Id == 1).Select(d => d.Name);
            
            Assert.That(actual: result,
                Is.EqualTo(expected: departments.Where(n => n.Id == 1).Select(d => d.Name)));
        }

        [Test]
        public void TotalSalaryMaintenanceDepartmentTest()
        {
            var allExpenses = _dbContect.Expenses;


            var result = service.TotalSalaryMaintenanceDepartment(allExpenses, 1);


            Assert.That(actual: result, Is.EqualTo(expected: 1000));
        }

    }
}
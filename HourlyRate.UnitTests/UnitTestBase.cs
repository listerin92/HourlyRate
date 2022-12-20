using HourlyRate.Infrastructure.Data;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.CostCategories;
using HourlyRate.Infrastructure.Data.Models.Employee;
using HourlyRate.Infrastructure.Spektar;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.UnitTests
{
    public class UnitTestBase
    {

        protected ApplicationDbContext? DbContext;
        protected Guid CompanyId;
        protected IEnumerable<Department>? Departments;
        protected SPEKTAR_NEWContext SpektarNewContext;
        private IEnumerable<Expenses>? _expenses;
        private IEnumerable<Employee>? _employees;
        private IEnumerable<CostCategory>? _costCategory;
        private IEnumerable<FinancialYear>? _financialYears;
        private IEnumerable<CostCenter>? _costCenter;



        [OneTimeSetUp]
        public void SetUpBase()
        {
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            CompanyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB");

            _costCategory = new List<CostCategory>()
            {
                new CostCategory() {Id = 1, Name = "Water", CompanyId = CompanyId},
                new CostCategory() {Id = 2, Name = "Power", CompanyId = CompanyId},
                new CostCategory() {Id = 3, Name = "Phones", CompanyId = CompanyId},
                new CostCategory() {Id = 4, Name = "Other", CompanyId = CompanyId},
                new CostCategory() {Id = 5, Name = "Administration", CompanyId = CompanyId},
                new CostCategory() {Id = 6, Name = "Rent", CompanyId = CompanyId},
                new CostCategory() {Id = 7, Name = "Direct Repairs", CompanyId = CompanyId},
                new CostCategory() {Id = 8, Name = "Direct Depreciation", CompanyId = CompanyId},
                new CostCategory() {Id = 9, Name = "Heating", CompanyId = CompanyId},
                new CostCategory() {Id = 10, Name = "Taxes", CompanyId = CompanyId},
                new CostCategory() {Id = 11, Name = "Indirect Depreciation", CompanyId = CompanyId},
            };

            _expenses = new List<Expenses>()
            {
                new Expenses() { Id = 1, Amount = 500, EmployeeId = 1, FinancialYearId = 1,CostCenterId = 1},
                new Expenses() { Id = 2, Amount = 500, EmployeeId = 2, FinancialYearId = 1,CostCenterId = 1},
                new Expenses() { Id = 3, Amount = 500, EmployeeId = 3, FinancialYearId = 1},
                new Expenses() { Id = 4, Amount = 500, EmployeeId = 4, FinancialYearId = 1,CostCenterId = 3},
                new Expenses() { Id = 5, Amount = 611, EmployeeId = 5, FinancialYearId = 1,CostCenterId = 3},
                new Expenses() { Id = 6, Amount = 666, CostCategoryId = 1, FinancialYearId = 1},
                new Expenses() { Id = 7, Amount = 666, CostCategoryId = 1, FinancialYearId = 1},
                new Expenses() { Id = 8, Amount = 1234, CostCategoryId = 1, FinancialYearId = 1},
                new Expenses() { Id = 9, Amount = 9999, CostCategoryId = 8, FinancialYearId = 1, CostCenterId = 1},
                new Expenses() { Id = 10, Amount = 5555, CostCategoryId = 6, FinancialYearId = 1},
                new Expenses() { Id = 11, Amount = 1111, CostCategoryId = 7, FinancialYearId = 1, CostCenterId = 1},
                new Expenses() { Id = 12, Amount = 2222, ConsumableId = 1, FinancialYearId = 1, CostCenterId = 1, IsDeleted = false},
                new Expenses() { Id = 13, Amount = 2222, ConsumableId = 1, FinancialYearId = 1, CostCenterId = 1, IsDeleted = true},

            };
            _employees = new List<Employee>()
            {
                new Employee() {Id = 1, DepartmentId = 1, FirstName = "Ivan", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 2, DepartmentId = 1, FirstName = "Petar", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 3, DepartmentId = 2, FirstName = "Stefan", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 4, DepartmentId = 3, FirstName = "Gancho", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 5, DepartmentId = 3, FirstName = "Dragancho", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = "", IsEmployee = false},
            };
            Departments = new List<Department>()
            {
                new Department() {Id = 1,Name = "Maintenance"},
                new Department() {Id = 2,Name = "Lamination"},
                new Department() {Id = 3,Name = "SM103"},
                new Department() {Id = 4,Name = "SM104"},
            };
            _financialYears = new List<FinancialYear>()
            {
                new FinancialYear() { Id = 1, Year = 2022, IsActive = true },
                new FinancialYear() { Id = 2, Year = 2023, IsActive = false },
            };

            _costCenter = new List<CostCenter>()
            {
                new CostCenter()
                {
                    Id = 1,
                    Name = "SM102-8P",
                    FloorSpace = 180,
                    AvgPowerConsumptionKwh = 80,
                    AnnualHours = 4000,
                    AnnualChargeableHours = 2000,
                    DepartmentId = 3,
                    TotalPowerConsumption = 160000,
                    DirectAllocatedStuff = 2,
                    DirectWagesCost = 100000,
                    DirectRepairCost = 123456,
                    DirectDepreciationCost = 55555,
                    TotalDirectCosts = 279011,
                    DirectElectricityCost = 0,
                    RentCost = 0,
                    TotalMixCosts = 279011,
                    IndirectHeatingCost =0,
                    TotalIndex = 0,
                    IsUsingWater = true,
                    WaterTotalIndex = 1,
                    IndirectWaterCost = 0,
                    IndirectTaxes = 0,
                    IndirectPhonesCost = 0,
                    IndirectAdministrationWagesCost = 0,
                    IndirectOtherCost = 0,
                    IndirectMaintenanceWagesCost = 0,
                    IndirectDepreciationCost = 0,
                    IndirectTotalCosts = 0,
                    TotalCosts = 0,
                    WagesPerMonth = 0,
                    MachinesPerMonth = 0,
                    OverheadsPerMonth = 0,
                    WagesPerHour = 0,
                    MachinesPerHour = 0,
                    OverheadsPerHour = 0,
                    TotalHourlyCostRate = 0,
                    IsActive = true,
                }
            };




            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ExpensesInMemoryDb").Options;
            DbContext = new ApplicationDbContext(options);
            DbContext.AddRange(_expenses);
            DbContext.AddRange(_employees);
            DbContext.AddRange(Departments);
            DbContext.AddRange(_financialYears);
            DbContext.AddRange(_costCategory);
            DbContext.AddRange(_costCenter);
            DbContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDownBase()
        {
            DbContext!.Dispose();
        }
    }
}

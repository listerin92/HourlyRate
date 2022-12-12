using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Core.Services;
using HourlyRate.Infrastructure.Data;
using HourlyRate.Infrastructure.Data.Models;
using HourlyRate.Infrastructure.Data.Models.CostCategories;
using HourlyRate.Infrastructure.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HourlyRate.UnitTests
{
    public class Tests
    {
        private ApplicationDbContext? _dbContext;
        private IEnumerable<Expenses>? expenses;
        private IEnumerable<Employee>? employees;
        private IEnumerable<Department>? departments;
        private IEnumerable<CostCategory>? costCategory;
        private IEnumerable<FinancialYear>? financialYears;
        private IEnumerable<CostCenter>? costCenter;
        private ICostCenterService service;
        private Guid companyId;
        private AddCostCenterViewModel modelAddCostCenter;
        private AddCostCenterViewModel modelAddCostCenterToEmployee;
        private List<CostCenter> allCostCenters;
        private CostCenter costCenter1;

        private DbSet<Expenses> allExpenses;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            companyId = Guid.Parse("4B609DF0-6F9C-4226-1BCE-08DAD4A028BB");

            costCategory = new List<CostCategory>()
            {
                new CostCategory() {Id = 1, Name = "Water", CompanyId = companyId},
                new CostCategory() {Id = 2, Name = "Power", CompanyId = companyId},
                new CostCategory() {Id = 3, Name = "Phones", CompanyId = companyId},
                new CostCategory() {Id = 4, Name = "Other", CompanyId = companyId},
                new CostCategory() {Id = 5, Name = "Administration", CompanyId = companyId},
                new CostCategory() {Id = 6, Name = "Rent", CompanyId = companyId},
                new CostCategory() {Id = 7, Name = "Direct Repairs", CompanyId = companyId},
                new CostCategory() {Id = 8, Name = "Direct Depreciation", CompanyId = companyId},
                new CostCategory() {Id = 9, Name = "Heating", CompanyId = companyId},
                new CostCategory() {Id = 10, Name = "Taxes", CompanyId = companyId},
                new CostCategory() {Id = 11, Name = "Indirect Depreciation", CompanyId = companyId},
            };

            expenses = new List<Expenses>()
            {
                new Expenses() { Id = 1, Amount = 500, EmployeeId = 1, FinancialYearId = 1},
                new Expenses() { Id = 2, Amount = 500, EmployeeId = 2, FinancialYearId = 1},
                new Expenses() { Id = 3, Amount = 500, EmployeeId = 3, FinancialYearId = 1},
                new Expenses() { Id = 4, Amount = 500, EmployeeId = 4, FinancialYearId = 1,CostCenterId = 3},
                new Expenses() { Id = 5, Amount = 611, EmployeeId = 5, FinancialYearId = 1,CostCenterId = 3},
                new Expenses() { Id = 6, Amount = 666, CostCategoryId = 1, FinancialYearId = 1},
                new Expenses() { Id = 7, Amount = 666, CostCategoryId = 1, FinancialYearId = 1},
                new Expenses() { Id = 8, Amount = 1234, CostCategoryId = 1, FinancialYearId = 1},
                new Expenses() { Id = 9, Amount = 9999, CostCategoryId = 8, FinancialYearId = 1, CostCenterId = 1},
                new Expenses() { Id = 10, Amount = 5555, CostCategoryId = 6, FinancialYearId = 1},
                new Expenses() { Id = 11, Amount = 1111, CostCategoryId = 7, FinancialYearId = 1, CostCenterId = 1},

            };
            employees = new List<Employee>()
            {
                new Employee() {Id = 1, DepartmentId = 1, FirstName = "Ivan", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 2, DepartmentId = 1, FirstName = "Petar", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 3, DepartmentId = 2, FirstName = "Stefan", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 4, DepartmentId = 3, FirstName = "Gancho", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
                new Employee() {Id = 5, DepartmentId = 3, FirstName = "Dragancho", LastName = "Ivanov", JobTitle = "asdf", ImageUrl = ""},
            };
            departments = new List<Department>()
            {
                new Department() {Id = 1,Name = "Maintenance"},
                new Department() {Id = 2,Name = "Lamination"},
                new Department() {Id = 3,Name = "SM103"},
                new Department() {Id = 4,Name = "SM104"},
            };
            financialYears = new List<FinancialYear>()
            {
                new FinancialYear() { Id = 1, Year = 2022, IsActive = true },
                new FinancialYear() { Id = 2, Year = 2023, IsActive = false },
            };

            costCenter = new List<CostCenter>()
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
                    TotalMixCosts = 0,
                    IndirectHeatingCost =0,
                    TotalIndex = 0,
                    IsUsingWater = true,
                    WaterTotalIndex = 0,
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

            modelAddCostCenter = new AddCostCenterViewModel()
            {
                Id = 2,
                Name = "SM103",
                FloorSpace = 130,
                AvgPowerConsumptionKwh = 80,
                AnnualHours = 4000,
                AnnualChargeableHours = 2000,
                DepartmentId = 3,
                IsUsingWater = true,
                IsActive = true,
            };
            modelAddCostCenterToEmployee = new AddCostCenterViewModel()
            {
                Id = 3,
                Name = "SM104",
                FloorSpace = 130,
                AvgPowerConsumptionKwh = 80,
                AnnualHours = 4000,
                AnnualChargeableHours = 2000,
                DepartmentId = 4,
                IsUsingWater = true,
                IsActive = true,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ExpensesInMemoryDb")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _dbContext.AddRange(expenses);
            _dbContext.AddRange(employees);
            _dbContext.AddRange(departments);
            _dbContext.AddRange(financialYears);
            _dbContext.AddRange(costCategory);
            _dbContext.AddRange(costCenter);
            _dbContext.SaveChanges();

            service = new CostCenterService(_dbContext);


            allCostCenters = _dbContext!.CostCenters.ToList();
            allExpenses = _dbContext!.Expenses;
            costCenter1 = _dbContext!.CostCenters.First(cc => cc.Id == 1);

        }


        [Test]
        public void AllDepartmentsTestCount()
        {
            var allDepartments = _dbContext!.Departments;


            var result = service.AllDepartments();
            Assert.That(actual: result.Result.Count(), Is.EqualTo(expected: allDepartments.Count()));
        }


        [Test]
        public void AllDepartmentsTestName()
        {
            var allDepartments = _dbContext!.Departments;


            var result = allDepartments.Where(n => n.Id == 1).Select(d => d.Name);

            Assert.That(actual: result,
                Is.EqualTo(expected: departments!.Where(n => n.Id == 1).Select(d => d.Name)));
        }

        [Test]
        public void AddCostCenterTest()
        {


            service.AddCostCenter(modelAddCostCenter, companyId);

            var costCenter2 = _dbContext!.CostCenters.First(n => n.Id == 2);

            Assert.That(actual: costCenter2.Name, Is.EqualTo("SM103"));
            Assert.That(actual: costCenter2.FloorSpace, Is.EqualTo(130.0m));
            Assert.That(actual: costCenter2.AvgPowerConsumptionKwh, Is.EqualTo(80));
            Assert.That(actual: costCenter2.AnnualHours, Is.EqualTo(4000));
            Assert.That(actual: costCenter2.AnnualChargeableHours, Is.EqualTo(2000));
            Assert.That(actual: costCenter2.DepartmentId, Is.EqualTo(3));
            Assert.That(actual: costCenter2.IsUsingWater, Is.EqualTo(true));
            Assert.That(actual: costCenter2.DirectAllocatedStuff, Is.EqualTo(2));
            Assert.That(actual: costCenter2.DirectWagesCost, Is.EqualTo(1111));
            Assert.That(actual: costCenter2.CompanyId, Is.EqualTo(companyId));
            Assert.That(actual: costCenter2.FinancialYearId, Is.EqualTo(1));
        }

        [Test]
        public void ActiveFinancialYearIdTest()
        {
            var result = service.ActiveFinancialYearId();
            Assert.That(actual: result, Is.EqualTo(expected: 1));
        }

        [Test]
        public void AddCostCenterToEmployeeTest()
        {


            service.AddCostCenterToEmployee(modelAddCostCenterToEmployee);

            var expensesByEmployeeForGivenCostCenter =
                _dbContext!.Expenses
                    .Where(e => e.CostCenterId == 3
                    && e.EmployeeId != null).Sum(s => s.Amount);
            Assert.That(actual: expensesByEmployeeForGivenCostCenter, Is.EqualTo(expected: 1111));

            var countOfEmployeesInGivenCostCenter =
                _dbContext.Expenses.Count(e => e.CostCenterId == 3
                                               && e.EmployeeId != null);

            Assert.That(actual: countOfEmployeesInGivenCostCenter, Is.EqualTo(expected: 2));
        }

        [Test]
        public void TotalSalaryMaintenanceDepartmentTest()
        {


            var result = service.TotalSalaryMaintenanceDepartment(allExpenses, 1);


            Assert.That(actual: result, Is.EqualTo(expected: 1000));
        }
        [Test]
        public void GetSumOfTotalIndirectCostOfCcTest()
        {
            
            var activeFinancialYearId = service.ActiveFinancialYearId();

            var result = service.GetSumOfTotalIndirectCostOfCc(allExpenses, activeFinancialYearId, 1);

            Assert.That(actual: result, Is.EqualTo(expected: 2566m));
        }

        [Test]
        public void SumTotalDirectCosts()
        {
            var result = service.SumTotalDirectCosts(allCostCenters);
            Assert.That(actual: result, Is.EqualTo(expected: 279011m));
        }


        [Test]
        public void UpdateAllCostCentersTest()
        {
            service.UpdateAllCostCenters(companyId);
        }

        [Test]
        public void SetWaterCostTest()
        {
            var directCostOfCcUsingWater = costCenter1.TotalDirectCosts;
            var totalWaterCost = 1000m;
            var result = service.SetWaterCost(costCenter1, directCostOfCcUsingWater, totalWaterCost);
            Assert.That(actual: result, Is.EqualTo(expected: 1000m));

        }

        [Test]
        public void CurrentCostCenterRentTest()
        {
            
            var totalRentSpace = service.TotalRentSpace(allCostCenters);

            var result = service.CurrentCostCenterRent(100000, totalRentSpace, costCenter1);
            Assert.That(actual: result, Is.EqualTo(expected: 100000m));

        }

        [Test]
        public void RentCostTotalTest()
        {
            var result = service.RentCostTotal(allExpenses, 6);
            Assert.That(actual: result, Is.EqualTo(expected: 5555));


        }

        [Test]
        public void ElectricityPricePerKwhIndirectlyCalculated()
        {
            var result = service.ElectricityPricePerKwhIndirectlyCalculated(100000, allCostCenters);
            Assert.That(actual: result, Is.EqualTo(expected: 0.625));

        }

        [Test]
        public void CurrentCostCenterDepreciationSumTest()
        {
            var result = service.CurrentCostCenterDepreciationSum(allExpenses, 1,
                1, costCenter1, 8);
            Assert.That(actual: result, Is.EqualTo(expected: 9999m));

        }

        [Test]
        public void CurrentCostCenterDirectRepairSumTest()
        {
            var result = service.CurrentCostCenterDirectRepairSum(allExpenses, 1, 1, costCenter1, 7);
            Assert.That(actual: result, Is.EqualTo(expected: 1111m));

        }

    }
}
using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models.CostCenter;
using HourlyRate.Core.Services;
using HourlyRate.Infrastructure.Data.Models;

namespace HourlyRate.UnitTests
{
    public class HourlyRateTests : UnitTestBase
    {
        private ICostCenterService _service = null!;
        private List<CostCenter> _allCostCenters = null!;
        private CostCenter _costCenter1 = null!;
        private AddCostCenterViewModel _modelAddCostCenterToEmployee = null!;
        private AddCostCenterViewModel _modelAddCostCenter = null!;


        private IQueryable<Expenses> _allExpenses = null!;

        [OneTimeSetUp]
        public void TestInitialize()
        {

            _service = new CostCenterService(DbContext!, SpektarNewContext!);


            _allCostCenters = DbContext!.CostCenters.ToList();
            _allExpenses = DbContext!.Expenses.Where(e => e.FinancialYearId == 1
                                                          && e.IsDeleted == false);
            _costCenter1 = DbContext!.CostCenters.First(cc => cc.Id == 1);


            _modelAddCostCenter = new AddCostCenterViewModel()
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

            _modelAddCostCenterToEmployee = new AddCostCenterViewModel()
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
        }


        [Test]
        public void AllDepartmentsTestCount()
        {
            var allDepartments = DbContext!.Departments;


            var result = _service.AllDepartments();
            Assert.That(actual: result.Result.Count(), Is.EqualTo(expected: allDepartments.Count()));
        }


        [Test]
        public void AllDepartmentsTestName()
        {
            var allDepartments = DbContext!.Departments;


            var result = allDepartments.Where(n => n.Id == 1).Select(d => d.Name);

            Assert.That(actual: result,
                Is.EqualTo(expected: Departments!.Where(n => n.Id == 1).Select(d => d.Name)));
        }

        [Test]
        public void AddCostCenterTest()
        {
                _service.AddCostCenter(_modelAddCostCenter, CompanyId);

            var costCenter2 = DbContext!.CostCenters.First(n => n.Id == 2);

            Assert.That(actual: costCenter2.Name, Is.EqualTo("SM103"));
            Assert.That(actual: costCenter2.FloorSpace, Is.EqualTo(130.0m));
            Assert.That(actual: costCenter2.AvgPowerConsumptionKwh, Is.EqualTo(80));
            Assert.That(actual: costCenter2.AnnualHours, Is.EqualTo(4000));
            Assert.That(actual: costCenter2.AnnualChargeableHours, Is.EqualTo(2000));
            Assert.That(actual: costCenter2.DepartmentId, Is.EqualTo(3));
            Assert.That(actual: costCenter2.IsUsingWater, Is.EqualTo(true));
            Assert.That(actual: costCenter2.DirectAllocatedStuff, Is.EqualTo(2));
            Assert.That(actual: costCenter2.DirectWagesCost, Is.EqualTo(1111));
            Assert.That(actual: costCenter2.CompanyId, Is.EqualTo(CompanyId));
            Assert.That(actual: costCenter2.FinancialYearId, Is.EqualTo(1));
        }

        [Test]
        public void ActiveFinancialYearIdTest()
        {
            var result = _service!.ActiveFinancialYearId();
            Assert.That(actual: result, Is.EqualTo(expected: 1));
        }

        [Test]
        public void AddCostCenterToEmployeeTest()
        {


            _service!.AddCostCenterToEmployeeExpenses(_modelAddCostCenterToEmployee!);

            var expensesByEmployeeForGivenCostCenter =
                DbContext!.Expenses
                    .Where(e => e.CostCenterId == 3
                    && e.EmployeeId != null).Sum(s => s.Amount);
            Assert.That(actual: expensesByEmployeeForGivenCostCenter, Is.EqualTo(expected: 1111));

            var countOfEmployeesInGivenCostCenter =
                DbContext.Expenses.Count(e => e.CostCenterId == 3
                                               && e.EmployeeId != null);

            Assert.That(actual: countOfEmployeesInGivenCostCenter, Is.EqualTo(expected: 2));
        }

        [Test]
        public void TotalSalaryMaintenanceDepartmentTest()
        {


            var result = _service!.TotalSalaryMaintenanceDepartment(_allExpenses!);


            Assert.That(actual: result, Is.EqualTo(expected: 1000));
        }
        [Test]
        public void GetSumOfTotalIndirectCostOfCcTest()
        {

            var activeFinancialYearId = _service!.ActiveFinancialYearId();

            var result = _service.GetSumOfTotalIndirectCostOfCc(_allExpenses!, 1);

            Assert.That(actual: result, Is.EqualTo(expected: 2566m));
        }

        [Test]
        public void SumTotalDirectCosts()
        {
            var result = _service!.SumTotalDirectMixCosts(_allCostCenters!);
            Assert.That(actual: result, Is.EqualTo(expected: 279011m));
        }


        [Test]
        public void UpdateAllCostCentersTest()
        {
            _service.UpdateAllCostCenters(CompanyId);
            //TODO: How to test this one ??????
        }

        [Test]
        public void SetWaterCostTest()
        {
            var totalWaterCost = 1000m;
            var result = _service.SetWaterCost(_costCenter1!, totalWaterCost);
            Assert.That(actual: result, Is.EqualTo(expected: 1000m));

        }

        [Test]
        public void CurrentCostCenterRentTest()
        {

            var totalRentSpace = _service!.TotalRentSpace(_allCostCenters!);

            var result = _service.CurrentCostCenterRent(100000, totalRentSpace, _costCenter1!);
            Assert.That(actual: result, Is.EqualTo(expected: 100000m));

        }

        [Test]
        public void RentCostTotalTest()
        {
            var result = _service.SumPerCostCategoryForAllCostCenters(_allExpenses!, 6);
            Assert.That(actual: result, Is.EqualTo(expected: 5555));


        }

        [Test]
        public void ElectricityPricePerKwhIndirectlyCalculated()
        {
            var result = _service.ElectricityPricePerKwhIndirectlyCalculated(100000, _allCostCenters!);
            Assert.That(actual: result, Is.EqualTo(expected: 0.625));

        }

        [Test]
        public void CurrentCostCenterDepreciationSumTest()
        {
            var result = _service.CurrentCostCenterDepreciationSum(_allExpenses!, _costCenter1!, 8);
            Assert.That(actual: result, Is.EqualTo(expected: 9999m));

        }

        [Test]
        public void CurrentCostCenterDirectRepairSumTest()
        {
            var result = _service.CurrentCostCenterDirectRepairSum(_allExpenses, _costCenter1, 7);
            Assert.That(actual: result, Is.EqualTo(expected: 1111m));

        }

        [Test]
        public void CurrentCostCenterConsumablesTotalTest()
        {
            var result = _service.CurrentCostCenterConsumablesTotal(_allExpenses, _costCenter1);
            Assert.That(actual: result, Is.EqualTo(expected: 2222m));

        }

        [Test]
        public void CurrentCostCenterEmployeesWagesSumTest()
        {
            var result = _service.CurrentCostCenterEmployeesWagesSum(_allExpenses, _costCenter1);
            Assert.That(actual: result, Is.EqualTo(expected: 1000m));

        }

        [Test]
        public void CurrentEmployeeCountTest()
        {
            var result = _service.CurrentEmployeeCount(_allExpenses, _costCenter1);
            Assert.That(actual: result, Is.EqualTo(expected: 2));

        }

    }
}
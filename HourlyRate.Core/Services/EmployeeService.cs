using HourlyRate.Core.Contracts;
using HourlyRate.Core.Models;
using HourlyRate.Infrastructure.Models;
using HourlyRate.Infrastructure.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebShopDemo.Core.Data.Common;

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
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return await _repo.AllReadonly<Expenses>()
                .Where(y => y.FinancialYear.Year == 2022 && y.UserId == userId)
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

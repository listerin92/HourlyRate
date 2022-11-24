using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using HourlyRate.Infrastructure.Data.Models.Account;
using HourlyRate.Infrastructure.Data;

namespace HourlyRate.Areas.Identity.Pages.Account.Manage
{
    public class CompanyInfo : PageModel
    {
        private readonly UserManager<UserIdentityExt> _userManager;
        private readonly SignInManager<UserIdentityExt> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public CompanyInfo(
            UserManager<UserIdentityExt> userManager
            , SignInManager<UserIdentityExt> signInManager
                , ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [TempData]
        public string StatusMessage { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? CompanyDescription { get; set; }
        public string CompanyEmail { get; set; } = null!;
        public string CompanyPhoneNumber { get; set; } = null!;
        public string DefaultCurrency { get; set; } = null!;

        public string VAT { get; set; } = null!;



        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public string CompanyName { get; set; } = null!;

            public string? CompanyDescription { get; set; }
            public string CompanyEmail { get; set; } = null!;
            public string CompanyPhoneNumber { get; set; } = null!;
            public string DefaultCurrency { get; set; } = null!;

            public string VAT { get; set; } = null!;
        }

        private Task LoadAsync(UserIdentityExt userIdentityExt)
        {
            Input = new InputModel
            {
                CompanyName = userIdentityExt.CompanyName,
                CompanyDescription = userIdentityExt.CompanyDescription,
                CompanyEmail = userIdentityExt.CompanyEmail,
                CompanyPhoneNumber = userIdentityExt.CompanyPhoneNumber,
                DefaultCurrency = userIdentityExt.DefaultCurrency,
                VAT = userIdentityExt.VAT,
                
            };
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load userIdentityExt with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load userIdentityExt with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);


            if (Input.CompanyName != dbUser?.CompanyName || 
                Input.CompanyDescription != dbUser.CompanyDescription ||
                Input.CompanyEmail != dbUser.CompanyEmail ||
                Input.CompanyPhoneNumber != dbUser.CompanyPhoneNumber ||
                Input.DefaultCurrency != dbUser.DefaultCurrency ||
                Input.VAT != dbUser.VAT)
            {
                user.CompanyName = Input.CompanyName;
                user.CompanyDescription = Input.CompanyDescription;
                user.CompanyEmail = Input.CompanyEmail;
                user.CompanyPhoneNumber = Input.CompanyPhoneNumber;
                user.DefaultCurrency = Input.DefaultCurrency;
                user.VAT = Input.VAT;

                var company = _dbContext.Companies.First(c => c.CompanyName == Input.CompanyName);
                company.CompanyName = Input.CompanyName;
                company.CompanyDescription = Input.CompanyDescription;
                company.CompanyEmail = Input.CompanyEmail;
                company.CompanyPhone = Input.CompanyPhoneNumber;
                company.DefaultCurrency = Input.DefaultCurrency;
                company.VAT = Input.VAT;

                _dbContext.Update(user);
                _dbContext.Update(company);
                await _dbContext.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }


    }
}

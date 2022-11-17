using HourlyRate.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using HourlyRate.Infrastructure.Models.Account;

namespace HourlyRate.Areas.Identity.Pages.Account.Manage
{
    public class CompanyInfo : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public CompanyInfo(
            UserManager<User> userManager
            , SignInManager<User> signInManager
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
        public string VAT { get; set; } = null!;



        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public string CompanyName { get; set; } = null!;

            public string? CompanyDescription { get; set; }
            public string CompanyEmail { get; set; } = null!;
            public string CompanyPhoneNumber { get; set; } = null!;
            public string VAT { get; set; } = null!;
        }

        private async Task LoadAsync(User user)
        {

            Input = new InputModel
            {
                CompanyName = user.CompanyName,
                CompanyDescription = user.CompanyDescription,
                CompanyEmail = user.CompanyEmail,
                CompanyPhoneNumber = user.CompanyPhoneNumber,
                VAT = user.VAT,
                
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
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
                Input.VAT != dbUser.VAT)
            {
                user.CompanyName = Input.CompanyName;
                user.CompanyDescription = Input.CompanyDescription;
                user.CompanyEmail = Input.CompanyEmail;
                user.CompanyPhoneNumber = Input.CompanyPhoneNumber;
                user.VAT = Input.VAT;

                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }


    }
}

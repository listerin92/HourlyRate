using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using HourlyRate.Infrastructure.Data.Models.Account;
using HourlyRate.Infrastructure.Data;

namespace HourlyRate.Areas.Identity.Pages.Account.Manage
{
    public class FirstLastNameModel : PageModel
    {
        private readonly UserManager<UserIdentityExt> _userManager;
        private readonly SignInManager<UserIdentityExt> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public FirstLastNameModel(
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

        public string FirstName { get; set; }
        public string LastName { get; set; }



        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [StringLength(20, MinimumLength = 4)]
            public string FirstName { get; set; } = null!;

            [StringLength(20, MinimumLength = 4)]
            public string LastName { get; set; } = null!;
        }

        private Task LoadAsync(UserIdentityExt userIdentityExt)
        {
            Input = new InputModel
            {
                FirstName = userIdentityExt.FirstName ?? "",
                LastName = userIdentityExt.LastName ?? ""
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

            var firstName = dbUser?.FirstName ?? "";
            var lastName = dbUser?.LastName ?? "";

            if (Input.FirstName != firstName || Input.LastName != lastName)
            {
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }


    }
}

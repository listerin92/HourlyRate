using HourlyRate.Core.Data;
using HourlyRate.Core.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HourlyRate.Areas.Identity.Pages.Account.Manage
{
    public class FirstLastNameModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public FirstLastNameModel(
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

        public string FirstName { get; set; }
        public string LastName { get; set; }



        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [StringLength(20, MinimumLength = 4)]
            public string FirstName { get; set; }

            [StringLength(20, MinimumLength = 4)]
            public string LastName { get; set; }
        }

        private async Task LoadAsync(User user)
        {

            Input = new InputModel
            {
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? ""
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

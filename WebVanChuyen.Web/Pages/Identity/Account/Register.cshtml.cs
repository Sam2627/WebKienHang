using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebVanChuyen.Models;

namespace WebVanChuyen.Web.Pages.Identity.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public RegisterModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");

            if(ModelState.IsValid)
            {
                var identity = new AppUser { UserName = Input.UserName, Email = Input.Password };
                var result = await _userManager.CreateAsync(identity, Input.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(identity, isPersistent: false);

                    return LocalRedirect(ReturnUrl);
                }
            }

            return Page();
        }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebVanChuyen.Models;

namespace WebVanChuyen.Web.Pages.Identity.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> signInManager;

        public async Task<IActionResult> OnPostAsync()
        {
            await signInManager.SignOutAsync();

            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToPage("/Index");
        }
    }
}

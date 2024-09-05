using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebVanChuyen.Models;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.Identity.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> SignInManager;

        //private IdentityUser _identityUser;

        public IEmployeeService EmployeeService { get; set; }

        public LoginModel(SignInManager<AppUser> signInManager, IEmployeeService employeeService) 
        {
            SignInManager = signInManager;
            EmployeeService = employeeService;
        }
        

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public EmployeeInfo Employee { get; set; } = new EmployeeInfo();

        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");
            /*
            if(ModelState.IsValid )
            {
                var result = await signInManager.PasswordSignInAsync(Input.UserName, Input.Password,false, lockoutOnFailure: false);

                if (result.Succeeded) return LocalRedirect(ReturnUrl);
            }
            */

            var userLogin = await SignInManager.UserManager.FindByNameAsync(Input.UserName);
            var isValid = await SignInManager.UserManager.CheckPasswordAsync(userLogin, Input.Password);
            if (isValid)
            {
                // Add Claims
                if(userLogin.EmployeeId != 0)
                {
                    Employee = await EmployeeService.GetEmployeeInfo(userLogin.EmployeeId);
                    var claims = new[]
                    {
                        new Claim("UserId", Employee.EmployeeId.ToString()),
                        new Claim("UserName", Employee.EmployeeName),
                        new Claim("UserPosition", Employee.EmployeePositionId.ToString()),
                        new Claim("WorkWarehouse", Employee.WorkWarehouseId)
                    };

                    await SignInManager.SignInWithClaimsAsync(userLogin, false, claims);
                }
                else
                {
                    await SignInManager.SignInAsync(userLogin, false);

                    /*
                    var claims = new[]
                    {
                         new Claim("UserName", userLogin.EmployeeName.ToString()),
                    };
                    
                    await signInManager.SignInWithClaimsAsync(userLogin, true, claims);
                    */
                }

                return LocalRedirect(ReturnUrl);
            }

            return Page();
        }
    }
}

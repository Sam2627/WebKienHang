using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebVanChuyen.Models
{
    public class AppUser : IdentityUser
    {
        //public string Password { get; set; }  // Remove bcs security
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
    }

    public class LoginInputModel
    {
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ")]
        public bool RememberMe { get; set; }
    }

    public class RegisterInputModel
    {
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

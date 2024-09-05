using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Api.Data;
using WebVanChuyen.Models;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Data
{
    public static class DataContextSeedUser
    {
        private static readonly DataContextWeb appDbContext;
        public static IEmployeeService EmployeeService { get; set; }

        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // Setting database
            var context = serviceProvider.GetService<DataContextUser>();

            //RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            string[] devRoles = new string[] { "Dev", "Admin", "Manager", "Staff", "Driver", "Shipper" };
            string[] adminRoles = new string[] { "Admin" };
            string[] managerRole = new string[] { "Manager" };
            string[] staffRole = new string[] { "Staff" };
            string[] driverRole = new string[] { "Driver" };
            string[] shipperRole = new string[] { "Shipper" };

            // Assign user role depend on EmployeeId
            int[] assignManagerRole = new int[] { 1, 4, 26, 10, 14, 18, 22, 26, 30, 34, 38, 42, 46, 50 };
            int[] assignStaffRole = new int[] { 2, 7, 5, 27, 11, 15, 19, 23, 31, 35, 39, 43, 47, 51  };
            int[] assignDriverRole = new int[] { 3, 8, 6, 9, 28, 29, 12, 16, 20, 24, 32, 36, 40, 44, 48, 52 };
            int[] assignShipperRole = new int[] { 54, 55, 56, 13, 17, 21, 25, 33, 37, 41, 45, 49, 53 };

            var userList = new List<AppUser>()
                {
                    // Seed Dev
                    new(){
                        UserName = "Developer",
                        //Password = "Developer",
                        EmployeeName = "Developer",
                        EmployeeId = 0,
                        EmailConfirmed = true,
                        //SecurityStamp = Guid.NewGuid().ToString("D"),
                        
                    },

                    // Seed Admin
                    new(){
                        UserName = "Admin",
                        EmployeeName = "Admin",
                        EmployeeId = 0,
                        EmailConfirmed = true,
                        
                    },

                    // Seed Employee AG-1
                    new(){
                        UserName = "Manager01@AG-1",
                        EmployeeName = "Ngoc Sa",
                        EmployeeId = 4,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@AG-1",
                        EmployeeName = "Bao Loc",
                        EmployeeId = 5,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@AG-1",
                        EmployeeName = "Thien Loc",
                        EmployeeId = 6,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver02@AG-1",
                        EmployeeName = "Bao Loc",
                        EmployeeId = 9,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@AG-1",
                        EmployeeName = "Nghia Loc",
                        EmployeeId = 56,
                        EmailConfirmed = true,
                    },

                    // Seed Employee BL-1
                    new(){
                        UserName = "Manager01@BL-1",
                        EmployeeName = "Ngoc Thanh",
                        EmployeeId = 10,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@BL-1",
                        EmployeeName = "Minh Long",
                        EmployeeId = 11,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@BL-1",
                        EmployeeName = "Be Ba",
                        EmployeeId = 12,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@BL-1",
                        EmployeeName = "Huu Phuoc",
                        EmployeeId = 13,
                        EmailConfirmed = true,
                    },

                    // Seed Employee BT-1
                    new(){
                        UserName = "Manager01@BT-1",
                        EmployeeName = "Ngoc Trinh",
                        EmployeeId = 14,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@BT-1",
                        EmployeeName = "Huu Nghia",
                        EmployeeId = 15,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@BL-1",
                        EmployeeName = "Huu Nghia",
                        EmployeeId = 16,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@BL-1",
                        EmployeeName = "Minh Thong",
                        EmployeeId = 17,
                        EmailConfirmed = true,
                    },

                    // Seed Employee CM-1
                    new(){
                        UserName = "Manager01@CM-1",
                        EmployeeName = "Binh Phuoc",
                        EmployeeId = 18,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@CM-1",
                        EmployeeName = "Trung Tin",
                        EmployeeId = 19,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@CM-1",
                        EmployeeName = "Quoc Bao",
                        EmployeeId = 20,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@CM-1",
                        EmployeeName = "Minh Sang",
                        EmployeeId = 21,
                        EmailConfirmed = true,
                    },

                    // Seed Employee CT-1
                    new (){
                        UserName = "Manager01@CT-1",
                        EmployeeName = "Anh Dung",
                        EmployeeId = 1,
                        EmailConfirmed = true,
                    },
                    new (){
                        UserName = "Staff01@CT-1",
                        EmployeeName = "Thanh Hang",
                        EmployeeId = 2,
                        EmailConfirmed = true,
                    },
                    new (){
                        UserName = "Staff02@CT-1",
                        EmployeeName = "Ngoc Bich",
                        EmployeeId = 7,
                        EmailConfirmed = true,
                    },
                    new (){
                        UserName = "Driver01@CT-1",
                        EmployeeName = "Quoc Bao",
                        EmployeeId = 3,
                        EmailConfirmed = true,
                    },
                    new (){
                        UserName = "Driver02@CT-1",
                        EmployeeName = "Minh Triet",
                        EmployeeId = 8,
                        EmailConfirmed = true,
                    },
                    new (){
                        UserName = "Shipper01@CT-1",
                        EmployeeName = "Vu Minh",
                        EmployeeId = 55,
                        EmailConfirmed = true,
                    },

                    // Seed Employee DT-1
                    new(){
                        UserName = "Manager01@DT-1",
                        EmployeeName = "Bao Thy",
                        EmployeeId = 22,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@DT-1",
                        EmployeeName = "Minh Anh",
                        EmployeeId = 23,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@DT-1",
                        EmployeeName = "Trung Thang",
                        EmployeeId = 24,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@DT-1",
                        EmployeeName = "Thien Phuoc",
                        EmployeeId = 25,
                        EmailConfirmed = true,
                    },

                    // Seed Employee HG-1
                    new (){
                        UserName = "Manager01@HG-1",
                        EmployeeName = "Ngoc Bao",
                        EmployeeId = 26,
                        EmailConfirmed = true,
                    },
                    new (){
                        UserName = "Staff01@HG-1",
                        EmployeeName = "Lan Anh",
                        EmployeeId = 27,
                        EmailConfirmed = true,
                    },
                    new (){
                        UserName = "Driver01@HG-1",
                        EmployeeName = "Hoai Bao",
                        EmployeeId = 28,
                        EmailConfirmed = true,
                    },
                    new (){
                        UserName = "Driver02@HG-1",
                        EmployeeName = "Quoc Nghia",
                        EmployeeId = 29,
                        EmailConfirmed = true,
                    },
                    new (){
                        UserName = "Shipper01@HG-1",
                        EmployeeName = "Bao Minh",
                        EmployeeId = 54,
                        EmailConfirmed = true,
                    },

                    // Seed Employee KG-1
                    new(){
                        UserName = "Manager01@KG-1",
                        EmployeeName = "Thu Thuy",
                        EmployeeId = 30,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@KG-1",
                        EmployeeName = "Bao An",
                        EmployeeId = 31,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@KG-1",
                        EmployeeName = "Van Duc",
                        EmployeeId = 32,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@KG-1",
                        EmployeeName = "Van Trong",
                        EmployeeId = 33,
                        EmailConfirmed = true,
                    },

                    // Seed Employee LA-1
                    new(){
                        UserName = "Manager01@LA-1",
                        EmployeeName = "Thu Ngoc",
                        EmployeeId = 34,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@LA-1",
                        EmployeeName = "Bao Binh",
                        EmployeeId = 35,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@LA-1",
                        EmployeeName = "Van Binh",
                        EmployeeId = 36,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@LA-1",
                        EmployeeName = "Van Minh",
                        EmployeeId = 37,
                        EmailConfirmed = true,
                    },

                    // Seed Employee ST-1
                    new(){
                        UserName = "Manager01@ST-1",
                        EmployeeName = "Minh Duc",
                        EmployeeId = 38,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@ST-1",
                        EmployeeName = "Hanh Ngoc",
                        EmployeeId = 39,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@ST-1",
                        EmployeeName = "Binh Minh",
                        EmployeeId = 40,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@ST-1",
                        EmployeeName = "Hoai Thu",
                        EmployeeId = 41,
                        EmailConfirmed = true,
                    },

                    // Seed Employee TG-1
                    new(){
                        UserName = "Manager01@TG-1",
                        EmployeeName = "Ngoc Hoa",
                        EmployeeId = 42,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@TG-1",
                        EmployeeName = "Ngoc Hoa",
                        EmployeeId = 43,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@TG-1",
                        EmployeeName = "Minh Trong",
                        EmployeeId = 44,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@TG-1",
                        EmployeeName = "Van Dung",
                        EmployeeId = 45,
                        EmailConfirmed = true,
                    },

                     // Seed Employee TV-1
                    new(){
                        UserName = "Manager01@TV-1",
                        EmployeeName = "Bao Long",
                        EmployeeId = 46,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@TV-1",
                        EmployeeName = "Hoang Kim",
                        EmployeeId = 47,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@TV-1",
                        EmployeeName = "Minh An",
                        EmployeeId = 48,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@TV-1",
                        EmployeeName = "Quoc Huy",
                        EmployeeId = 49,
                        EmailConfirmed = true,
                    },

                    // Seed Employee VL-1
                    new(){
                        UserName = "Manager01@TV-1",
                        EmployeeName = "Long Duc",
                        EmployeeId = 50,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Staff01@TV-1",
                        EmployeeName = "Kim Ngoc",
                        EmployeeId = 51,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Driver01@TV-1",
                        EmployeeName = "Hoang Duc",
                        EmployeeId = 52,
                        EmailConfirmed = true,
                    },
                    new(){
                        UserName = "Shipper01@TV-1",
                        EmployeeName = "Quoc Hai",
                        EmployeeId = 53,
                        EmailConfirmed = true,
                    },

                };

            foreach (var user in userList)
            {
                if (!_userManager.Users.Any(r => r.UserName == user.UserName))
                {
                    // Don't hash password manual let CreateAsync() method do it - BUG: password not match
                    /*
                    // Create hash password
                    var password = new PasswordHasher<AppUser>();
                    var hashed = password.HashPassword(user, user.Password);
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<AppUser>(context);
                    var result = userStore.CreateAsync(user);
                    */

                    // Add User
                    await _userManager.CreateAsync(user, user.UserName);

                    // Add UserRole
                    if(user.EmployeeId == 0 && user.UserName.Contains("Developer"))
                    {
                        await _userManager.AddToRolesAsync(user, devRoles);
                    }
                    else if (user.EmployeeId == 0 && user.UserName.Contains("Admin"))
                    {
                        await _userManager.AddToRolesAsync(user, adminRoles);
                    }
                    else if (assignManagerRole.Contains(user.EmployeeId))
                    {
                        await _userManager.AddToRolesAsync(user, managerRole);
                    }
                    else if (assignStaffRole.Contains(user.EmployeeId))
                    {
                        await _userManager.AddToRolesAsync(user, staffRole);
                    }
                    else if (assignDriverRole.Contains(user.EmployeeId))
                    {
                        await _userManager.AddToRolesAsync(user, driverRole);
                    }
                    else if (assignShipperRole.Contains(user.EmployeeId))
                    {
                        await _userManager.AddToRolesAsync(user, shipperRole);
                    }

                    // Add Claims
                    /*
                    List<Claim> claims = new List<Claim>();
                   foreach(var role in devRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    await _userManager.AddClaimsAsync(user, claims);
                    */

                    await context.SaveChangesAsync();
                }
            }
        }

        public static string SqlConn()
        {
            string strConnection = @"Server=(localdb)\\MSSQLLocalDB; database=WebKhoDb; Trust Server Certificate=True;";
            return strConnection;
        }

        public static async Task<Employee> EmployeePosition(int employeeId)
        {
            var result = (await EmployeeService.GetEmployee(employeeId));
            return result;
        }

    }
}

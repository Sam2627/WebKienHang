using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebVanChuyen.Web.Data
{
    public static class DataContextSeedRole
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContextUser(serviceProvider.GetRequiredService<DbContextOptions<DataContextUser>>()))
            {
                string[] roles = new string[] { "Dev", "Admin", "Manager", "Staff", "Driver", "Shipper" };

                var newrolelist = new List<IdentityRole>();

                foreach (string role in roles)
                {
                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        newrolelist.Add(new IdentityRole()
                        {
                            Name = role,
                            NormalizedName = role.ToUpper(),
                        });
                    }
                }
                context.Roles.AddRange(newrolelist);
                context.SaveChanges();
            }
        }
    }
}

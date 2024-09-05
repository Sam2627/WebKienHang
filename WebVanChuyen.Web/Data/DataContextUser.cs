using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Models;

namespace WebVanChuyen.Web.Data
{
    public class DataContextUser : IdentityDbContext
    {
        public DataContextUser(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pweb.API.Data
{
    public class PWEBAuthDbContext : IdentityDbContext
    {
        public PWEBAuthDbContext(DbContextOptions<PWEBAuthDbContext> options) : base(options)
        { 
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userRoleId = "100";

            var adminRoleId = "1000";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                },

                new IdentityRole 
                {
                    Id = adminRoleId,
                    ConcurrencyStamp= adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

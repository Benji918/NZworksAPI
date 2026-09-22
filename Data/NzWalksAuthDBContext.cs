using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZworks.Data
{
    public class NzWalksAuthDBContext : IdentityDbContext
    {
        public NzWalksAuthDBContext(DbContextOptions<NzWalksAuthDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "56947673-b045-441e-8229-45b14d81c694",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "f446eb96-a2f8-4133-825a-4d23442ebbf0"
                },
                new IdentityRole
                {
                    Id = "f446eb96-a2f8-4133-825a-4d23442ebbf0",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "f446eb96-a2f8-4133-825a-4d23442ebbf0"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NZworks.Models.Domain;

namespace NZworks.Data
{
    public class NzWalksDBContext : DbContext
    {
        public NzWalksDBContext(DbContextOptions<NzWalksDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}

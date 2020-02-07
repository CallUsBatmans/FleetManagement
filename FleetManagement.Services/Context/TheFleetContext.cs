using Microsoft.EntityFrameworkCore;
using TheFleet.Services.Domain;

namespace TheFleet.Services.Context
{
    public class TheFleetContext: DbContext
    {
        public TheFleetContext(DbContextOptions<TheFleetContext> options)
            :base(options)
        {}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(c => c.Id);
        }
    }
}
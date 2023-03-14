using Microsoft.EntityFrameworkCore;

namespace RegistrationAPI.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.AveragePowerConsumption)
                .HasColumnType("decimal(8,3)");

            modelBuilder.Entity<User>()
                .Property(u => u.DateOfBirth)
                .HasColumnType("date");
        }
    }
}

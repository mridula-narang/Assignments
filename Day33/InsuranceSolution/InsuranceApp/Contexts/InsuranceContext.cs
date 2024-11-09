using InsuranceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Contexts
{
    public class InsuranceContext : DbContext
    {
        public InsuranceContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<InsuranceClaim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Policies)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserName);

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.User)
                .WithMany(u => u.Policies)
                .HasForeignKey(p => p.UserName);

            modelBuilder.Entity<InsuranceClaim>()
                .HasOne<User>()
                .WithMany(u => u.Claims)
                .HasForeignKey(c => c.UserName);
        }
    }

}

using InsuraceClaimApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsuraceClaimApp.Contexts
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
                .HasKey(u => u.Username);

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.User)
                .WithMany(u => u.Policies)
                .HasForeignKey(p => p.UserName);

            //modelBuilder.Entity<InsuranceClaim>()
            //    .HasOne<Policy>()
            //    .WithMany(p => p.Claims)
            //    .HasForeignKey(c => c.PolicyNumber);

            modelBuilder.Entity<InsuranceClaim>()
                .HasKey(c => c.ClaimId);
        }
    }
}

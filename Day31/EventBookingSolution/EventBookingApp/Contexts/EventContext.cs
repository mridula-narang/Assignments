using EventBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApp.Contexts
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions contextOptions) : base(contextOptions)
        {
            
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Employee)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Bookings)
                .WithOne(b => b.Employee)
                .HasForeignKey(b => b.EmployeeId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Bookings)
                .WithOne(b => b.Event)
                .HasForeignKey(b => b.EventId);
        }

    }
}

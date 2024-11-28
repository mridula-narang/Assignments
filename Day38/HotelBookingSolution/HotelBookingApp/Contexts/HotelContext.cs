using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Contexts
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //one user has many bookings
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.Users)
                .HasForeignKey(b => b.UserId);

            //one hotel has many rooms
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);

            //one booking one payment
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithOne()
                .HasForeignKey<Payment>(p => p.BookingId);

            //one booking one cancellation policy
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.CancellationPolicy)
                .WithOne()
                .HasForeignKey<CancellationPolicy>(c => c.BookingId);

            //one room has multiple bookings
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Bookings)
                .WithOne(b => b.Rooms)
                .HasForeignKey(b => b.RoomId);

            //one hotel has many amenities
            modelBuilder.Entity<Hotel>()
                .HasMany(a => a.Amenities)
                .WithOne(h => h.Hotel);

        }
    }
}

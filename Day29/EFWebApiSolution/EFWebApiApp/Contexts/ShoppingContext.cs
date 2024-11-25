using EFWebApiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFWebApiApp.Contexts
{
    public class ShoppingContext : DbContext
    {
        public ShoppingContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => ci.SNo)
                .HasName("PK_CartItems");

            modelBuilder.Entity<Order>() //one customer many orders
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .HasConstraintName("FK_Orders_Customers");

            modelBuilder.Entity<OrderDetail>() //one product is present in many order details
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .HasConstraintName("FK_OrderDetails_Products");

            modelBuilder.Entity<OrderDetail>() //one order has many order details
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderNumber)
                .HasConstraintName("FK_OrderDetails_Orders");

            modelBuilder.Entity<Cart>() //one customer one cart 1-1 relationship
                .HasOne(c => c.Customer)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(c => c.CustomerId)
                .HasConstraintName("FK_Carts_Customers");

            modelBuilder.Entity<Product>() //one product has many product images
                .HasMany(p => (IEnumerable<ProductImage>)p.ProductImages)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .HasConstraintName("FK_ProductImages_Products");

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.Username)
                .HasConstraintName("FK_Customer_User");

        }
    }
}

using EF.Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF.Infra.Context
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products").HasKey(e => e.ProductId);
                entity.Property(e => e.ProductName).HasMaxLength(40);
                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
                entity.Property(e => e.UnitPrice).HasDefaultValue(0m);
                entity.Property(e => e.UnitsInStock).HasDefaultValue((short)0);

                entity.HasOne(d => d.Category).WithMany(e => e.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories").HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
                entity.Property(e => e.CategoryName).HasMaxLength(15);
                entity.Property(e => e.Picture).HasColumnType("image");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers").HasKey(e => e.CustomerId);
                entity.Property(e => e.CreatedDate).HasDefaultValue(DateTime.UtcNow);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders").HasKey(e => e.OrderId);
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.RequiredDate).HasColumnType("datetime");
                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer).WithMany(e => e.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Employee).WithMany(e => e.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("Order Details");
                entity.HasKey(e => new { e.OrderId, e.ProductId });
                entity.Property(e => e.Quantity).HasDefaultValue((short)1);

                entity.HasOne(d => d.Order).WithMany(e => e.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Product).WithMany(e => e.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees").HasKey(e => e.EmployeeId);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Regions").HasKey(e => e.RegionId);
            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.ToTable("Credit Cards");
                entity.HasKey(e => new { e.CardNumber, e.EmployeeId });

                entity.HasOne(d => d.Employee).WithMany(e => e.CreditCards)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }

    }
}

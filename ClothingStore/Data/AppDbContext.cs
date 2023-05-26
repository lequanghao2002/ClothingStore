using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ClothingStore.Models.Domain;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions) { } // constructor
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manage_Images)
                .WithMany(p => p.Product)
                .HasForeignKey( p => p.ID_MI);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Categories)
                .WithMany(p => p.Product)
                .HasForeignKey(p => p.ID_Category);

            modelBuilder.Entity<User>()
               .HasOne(u => u.Authorities)
               .WithMany(u => u.User)
               .HasForeignKey(u => u.ID_Authorize);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Products)
                .WithMany(o => o.Order)
                .HasForeignKey( o => o.ID_Product);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(o => o.Order)
                .HasForeignKey(o => o.ID_User);

            modelBuilder.Entity<Order_Detail>()
                .HasOne(od => od.Order)
                .WithMany(od => od.Order_Details)
                .HasForeignKey(od => od.ID_Order);

        }

        public DbSet<Authorities>? Authorities { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<Manage_Image>? Manage_Image { get; set; }
        public DbSet<Product>? Product { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet <Order>? Order { get; set; }
        public DbSet<Order_Detail>? Order_Detail { get; set; }
    }
}

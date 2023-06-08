using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ClothingStore.Data
{
    public class Clothing_Store_AuthDbContext:IdentityDbContext
    {
        public Clothing_Store_AuthDbContext(DbContextOptions<Clothing_Store_AuthDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create reader, writer and admin permission
            var AdminRoleId = "1";
            var ReaderRoleId = "2";
            var WriterRoleId = "3";
            base.OnModelCreating(modelBuilder);
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = AdminRoleId,
                    ConcurrencyStamp = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = ReaderRoleId,
                    ConcurrencyStamp= ReaderRoleId,
                    Name = "Read",
                    NormalizedName = "Read".ToUpper()
                },
                new IdentityRole
                {
                    Id = WriterRoleId,
                    ConcurrencyStamp= WriterRoleId,
                    Name = "Write",
                    NormalizedName = "Write".ToUpper()
                }
            };
            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var passwordhasder = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = passwordhasder.HashPassword(admin, "Admin123");
            admin.LockoutEnabled = true;
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            modelBuilder.Entity<IdentityUser>().HasData(admin);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = admin.Id,
                    RoleId = AdminRoleId
                }
            );
        }

    }
}

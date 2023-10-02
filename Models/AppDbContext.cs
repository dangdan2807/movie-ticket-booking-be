using Microsoft.EntityFrameworkCore;

namespace MovieTicketBookingBe.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<UserRole>? UserRoles { get; set; }
        public DbSet<TokenBlackList>? TokenBlackLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Định nghĩa cặp khóa kết hợp

            // Các cài đặt và quan hệ khác có thể được định nghĩa ở đây

            base.OnModelCreating(modelBuilder);
        }
    }
}

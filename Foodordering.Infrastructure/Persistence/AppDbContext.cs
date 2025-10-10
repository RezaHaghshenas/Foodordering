using Foodordering.Application.Common.Interfaces;
using Foodordering.Domain.Entities;
using FoodOrderingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foodordering.Infrastructure.Persistence
{
    public class AppDbContext : DbContext , IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }


        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<PasswordResetCode> PasswordResetCodes => Set<PasswordResetCode>();

        public DbSet<LoginHistory> loginHistories => Set<LoginHistory>();   

        public DbSet<Address> addresses => Set<Address>();          

        public DbSet<MenuItem> menuItems => Set<MenuItem>();

        public DbSet<Order> orders => Set<Order>();

        public DbSet<OrderItem> orderItems => Set<OrderItem>();

        public DbSet<Payment> payments => Set<Payment>();

        public DbSet<OrderReview> orderReviews => Set<OrderReview>();

        public DbSet<Restaurant> restaurant => Set<Restaurant>();

        public DbSet<Cart> carts => Set<Cart>();
        public DbSet<CartItem> cartItems => Set<CartItem>();






        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ------------------------------
            // User Entity Configuration
            // ------------------------------

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
                entity.Property(u => u.PasswordHash).IsRequired();

                entity.HasMany(e=> e.RefreshTokens)
                .WithOne(e=> e.User)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                // DomainEvents رو EF نباید map کنه
                entity.Ignore(u => u.DomainEvents);
            });

            // ------------------------------
            // RefreshToken Entity Configuration
            // ------------------------------

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.Id);

                entity.Property(rt => rt.TokenHash)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(rt => rt.DeviceId)
                    .HasMaxLength(100);

                entity.Property(rt => rt.DeviceName)
                    .HasMaxLength(200);

                entity.Property(rt => rt.CreatedByIp)
                    .HasMaxLength(45); // IPv6-safe

                entity.Property(rt => rt.UserAgent)
                    .HasMaxLength(500);

                // ✅ ایندکس برای جستجوی سریع
                entity.HasIndex(rt => new { rt.UserId, rt.DeviceId });
            });
        }
    }
}

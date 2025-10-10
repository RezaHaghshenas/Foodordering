// Application/Common/Interfaces/IAppDbContext.cs
using Foodordering.Domain.Entities;
using FoodOrderingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Foodordering.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }

       DbSet<PasswordResetCode> PasswordResetCodes { get; }

        DbSet<LoginHistory> loginHistories { get; }


        DbSet<Address> addresses { get;  }

         DbSet<Restaurant> restaurant { get; }

        DbSet<MenuItem> menuItems { get; }      

        DbSet<Order> orders { get; }        
        DbSet<OrderItem> orderItems { get; }    

        DbSet<Payment> payments { get; }    

        DbSet<OrderReview> orderReviews { get; }


        DbSet<Cart> carts { get; }
        public DbSet<CartItem> cartItems { get;  }



        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

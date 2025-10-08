// Application/Common/Interfaces/IAppDbContext.cs
using Foodordering.Domain.Entities;
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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

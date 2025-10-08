using Foodordering.Application.Common.DTOs;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class GetUserDetailsForAdminQueryHandler : IRequestHandler<GetUserDetailsForAdminQuery, UserDetailsDto>
    {
        private readonly IAppDbContext _context;

        public GetUserDetailsForAdminQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDetailsDto> Handle(GetUserDetailsForAdminQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.RefreshTokens)
                .Include(u => u.passwordResetCodes)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
                throw new Exception("کاربر یافت نشد");

            return new UserDetailsDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),
                IsActive = user.IsActive,
                RegisteredAt = user.RegisteredAt,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Notes = user.Notes,

                ActiveTokens = user.RefreshTokens
                    .Select(t => new RefreshTokenDto
                    {
                        Token = t.TokenHash,
                        CreatedAt = t.Created,
                        RevokedAt = t.Revoked
                    }).ToList(),

                PasswordResetCodes = user.passwordResetCodes
                    .Select(c => new PasswordResetCodeDto
                    {
                        Code = c.Code,
                        IsUsed = c.IsUsed,
                        ExpiresAt = c.ExpiresAt
                    }).ToList()
            };
        }
    }
}

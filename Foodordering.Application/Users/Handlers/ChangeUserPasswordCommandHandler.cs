using Foodordering.Application.Common.DTOs;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    // Application/Users/Handlers/ChangeUserPasswordCommandHandler.cs
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, AuthResultDto>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public ChangeUserPasswordCommandHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<AuthResultDto> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                throw new Exception("RefreshToken is required");
            }



            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            if (user == null) throw new Exception("User not found");

            var hash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.ChangePassword(hash);

            await _context.SaveChangesAsync(cancellationToken);
            // Revoke old tokens
            await _tokenService.RevokeAllRefreshTokenAsync(request.RefreshToken);

            // Generate new tokens
            var authResult = await _tokenService.GenerateTokens(
                user,
                request.DeviceId,
                request.DeviceName,
                request.IpAddress,
                request.UserAgent
            );

            // Optionally publish domain events
            foreach (var domainEvent in user.DomainEvents)
            {
                // Publish events later
            }

            return authResult;
        }
    }

}

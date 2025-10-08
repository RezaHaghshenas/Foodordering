using Foodordering.Application.Common.DTOs;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Commands;
using Foodordering.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand  , AuthResultDto >
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var mod = await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken, request.DeviceId);
            if (mod!= null )
            {
            return await _tokenService.GenerateTokens(mod, request.DeviceId, request.DeviceName, request.IpAddress, request.UserAgent);
            }
            else
            {
                throw new Exception("کاربری با این مشخصات پیدا نشد");
            }
        }
    }
}

using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Commands;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand , bool>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public LogoutUserCommandHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {

            if (await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken, request.DeviceId, request.IpAddress) == null)
            {
                throw new Exception("کاربری با این مشخصات پیدا نشد");
            }
            else
            {
                return true; 
            }

        }   
    }
}

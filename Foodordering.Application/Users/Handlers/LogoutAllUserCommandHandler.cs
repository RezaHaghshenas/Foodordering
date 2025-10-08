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
    public class LogoutAllUserCommandHandler : IRequestHandler<LogoutAllUserCommand , bool>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public LogoutAllUserCommandHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }


        public async Task<bool> Handle(LogoutAllUserCommand request, CancellationToken cancellationToken)
        {
            if (await _tokenService.RevokeAllRefreshTokenAsync(request.RefreshToken, request.DeviceId, request.IpAddress) == false)
            {
                throw new Exception("کاربری با این مشخصات پیدا نشد");
            }  
            return true; 
        }


    }
}

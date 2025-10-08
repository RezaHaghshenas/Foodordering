using Foodordering.Application.Common.DTOs;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Commands;
using Foodordering.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResultDto>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }


        public async Task<AuthResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users
    .FirstOrDefault(u => u.Email == request.EmailOrPhone || u.PhoneNumber == request.EmailOrPhone);



            if (user == null)
                throw new Exception("کاربری با این مشخصات پیدا نشد");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("پسورد اشتباه است");

            var authResult = await _tokenService.GenerateTokens(
    user,
    request.DeviceId,
    request.DeviceName,
    request.IpAddress,
    request.UserAgent
);
            var LoginHis = new LoginHistory(
            user.Id,
            request.DeviceId,
            request.DeviceName,
            request.IpAddress,
            request.UserAgent,
            true
          );

            _context.loginHistories.Add(LoginHis);
            await _context.SaveChangesAsync(cancellationToken);
            return authResult;

        }



    }
}
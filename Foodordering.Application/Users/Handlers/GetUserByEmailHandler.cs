using Foodordering.Application.Common.DTOs;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmail, UserDto>
    {
        private readonly IAppDbContext _context;

        public GetUserByEmailHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserByEmail request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null)
                throw new Exception("کاربر یافت نشد");

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),
                IsActive = user.IsActive,
                RegisteredAt = user.RegisteredAt,
                Notes = user.Notes,
                ProfilePictureUrl = user.ProfilePictureUrl,
            };
        }
    }
}


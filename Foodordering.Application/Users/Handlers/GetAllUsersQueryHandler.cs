using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Common.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using global::Foodordering.Application.Users.Queries;


namespace Foodordering.Application.Users.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IAppDbContext _context;

        public GetAllUsersQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.Role.ToString(),
                    IsActive = u.IsActive,
                    RegisteredAt = u.RegisteredAt , 
                    ProfilePictureUrl = u.ProfilePictureUrl,    
                    Notes =u.Notes
                })
                .ToListAsync(cancellationToken);
        }
    }
}


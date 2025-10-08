using Foodordering.Application.Common.DTOs;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Queries;
using Foodordering.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, List<UserDto>>
    {
        private readonly IAppDbContext _context;

        public SearchUsersQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var keyword = request.Keyword.ToLower();
                query = query.Where(u =>
                    u.Name.ToLower().Contains(keyword) ||
                    u.Email.ToLower().Contains(keyword) ||
                    u.PhoneNumber.Contains(keyword));
            }

            if (request.IsActive.HasValue)
                query = query.Where(u => u.IsActive == request.IsActive.Value);

            if (request.Role.HasValue)
                query = query.Where(u => (int)u.Role == request.Role.Value);

            var users = await query
                .OrderBy(u => u.Name)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.Role.ToString(),
                    RegisteredAt = u.RegisteredAt,
                    IsActive = u.IsActive , 
                    Notes = u.Notes,    
                    ProfilePictureUrl   = u.ProfilePictureUrl
                })
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}

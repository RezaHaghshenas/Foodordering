using Foodordering.Application.Common.DTOs;
using MediatR;
using System;

namespace Foodordering.Application.Users.Queries
{
    public class GetUserDetailsForAdminQuery : IRequest<UserDetailsDto>
    {
        public Guid UserId { get; set; }

        public GetUserDetailsForAdminQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}

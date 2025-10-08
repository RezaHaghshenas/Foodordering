using Foodordering.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace Foodordering.Application.Users.Queries
{
    public class GetUserLoginHistoryQuery : IRequest<List<LoginHistoryDto>>
    {
        public Guid UserId { get; set; }

        public GetUserLoginHistoryQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}

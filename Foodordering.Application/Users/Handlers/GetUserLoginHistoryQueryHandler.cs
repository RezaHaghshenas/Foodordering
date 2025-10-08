using Foodordering.Application.Common.DTOs;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class GetUserLoginHistoryQueryHandler : IRequestHandler<GetUserLoginHistoryQuery, List<LoginHistoryDto>>
    {
        private readonly IAppDbContext _context;

        public GetUserLoginHistoryQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoginHistoryDto>> Handle(GetUserLoginHistoryQuery request, CancellationToken cancellationToken)
        {
            var logs = await _context.loginHistories
                .Where(l => l.UserId == request.UserId)
                .OrderByDescending(l => l.LoggedInAt)
                .Take(30) // آخرین ۳۰ ورود اخیر
                .Select(l => new LoginHistoryDto
                {
                    LoggedInAt = l.LoggedInAt,
                    DeviceName = l.DeviceName,
                    IpAddress = l.IpAddress,
                    UserAgent = l.UserAgent,
                    IsSuccessful = l.IsSuccessful
                })
                .ToListAsync(cancellationToken);

            return logs;
        }
    }
}

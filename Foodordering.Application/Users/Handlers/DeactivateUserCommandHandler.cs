using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        private readonly IEventPublisher _eventPublisher;

        public DeactivateUserCommandHandler(IAppDbContext context, ITokenService tokenService, IEventPublisher eventPublisher)
        {
            _context = context;
            _tokenService = tokenService;
            _eventPublisher = eventPublisher;
        }



        public async Task<bool> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(F => F.Id == request.UserId);
            if (user == null)
            {
                throw new Exception("کاربری با این مشخصات پیدا نشد");
            }
            user.Deactivate();
            if (await _tokenService.RevokeAllRefreshTokenAsync(request.RefreshToken, request.DeviceId, request.IpAddress) == false)
            {
                throw new Exception("کاربری با این مشخصات پیدا نشد");
            }


            foreach (var domainEvent in user.DomainEvents)
            {
                await _eventPublisher.PublishAsync(domainEvent); // Kafka
            }
            user.DomainEvents.Clear();

            return true;
        }

    }
}

using BCrypt.Net;
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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        private readonly IEventPublisher _eventPublisher; 

        public RegisterUserCommandHandler(IAppDbContext context, ITokenService tokenService  , IEventPublisher eventPublisher)
        {
            _context = context;
            _tokenService = tokenService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User(
                request.Name,
                request.Email,
                request.PhoneNumber,
                passwordHash,
                request.Role
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

             var authResult=await _tokenService.GenerateTokens(user, request.DeviceId, request.DeviceName, request.IpAddress, request.UserAgent);

            foreach (var domainEvent in user.DomainEvents)
            {
                await _eventPublisher.PublishAsync(domainEvent); // Kafka
            }
            user.DomainEvents.Clear();

            return user.Id;
        }



    }
}

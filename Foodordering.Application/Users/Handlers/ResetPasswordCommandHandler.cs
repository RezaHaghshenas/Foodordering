using Foodordering.Application.Common.DTOs;
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

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        private readonly IEventPublisher _eventPublisher;

        public ResetPasswordCommandHandler(IAppDbContext context, ITokenService tokenService, IEventPublisher eventPublisher)
        {
            _context = context;
            _tokenService = tokenService;
            _eventPublisher = eventPublisher;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.NewPassword) || request.NewPassword.Length < 6)
                throw new Exception("رمز عبور باید حداقل ۶ کاراکتر باشد");

            var user = await _context.Users
                .Include(u => u.passwordResetCodes)
                .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);

            if (user == null)
                throw new Exception("کاربر یافت نشد");

            var code = user.passwordResetCodes
                .FirstOrDefault(c => c.Code == request.Code && !c.IsUsed && c.ExpiresAt > DateTime.UtcNow);

            if (code == null)
                throw new Exception("کد نامعتبر یا منقضی شده است");

            // ✅ تغییر پسورد
            var newHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.ResetPassword(newHash);
            code.MarkAsUsed();

            // ✅ حذف همه RefreshToken های فعال
            await _tokenService.RevokeRefreshTokenByIdAsync(user.Id);

            await _context.SaveChangesAsync(cancellationToken);

            // 🔔 بعداً اینجا میشه DomainEvent ها رو Dispatch کرد

            foreach (var domainEvent in user.DomainEvents)
            {
                await _eventPublisher.PublishAsync(domainEvent); // Kafka
            }
            user.DomainEvents.Clear();



            return true;
        }
    }
}

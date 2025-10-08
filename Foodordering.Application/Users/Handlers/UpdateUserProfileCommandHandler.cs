using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Foodordering.Application.Users.Handlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, bool>
    {
        private readonly IAppDbContext _context;

        public UpdateUserProfileCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
                throw new Exception("کاربر یافت نشد");

            // ✅ با توجه به private set، بهتره داخل دامنه متد مخصوص داشته باشیم
            user.UpdateProfile(
                profilePictureUrl: request.ProfilePictureUrl ?? user.ProfilePictureUrl,
                notes: request.Notes ?? user.Notes
            );

            // ✅ چون Name, Email و PhoneNumber از طریق دامنه ست نمی‌شن
            if (!string.IsNullOrWhiteSpace(request.Name))
                typeof(Foodordering.Domain.Entities.User)
                    .GetProperty(nameof(user.Name))!
                    .SetValue(user, request.Name.Trim());

            if (!string.IsNullOrWhiteSpace(request.Email))
                typeof(Foodordering.Domain.Entities.User)
                    .GetProperty(nameof(user.Email))!
                    .SetValue(user, request.Email.Trim());

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                typeof(Foodordering.Domain.Entities.User)
                    .GetProperty(nameof(user.PhoneNumber))!
                    .SetValue(user, request.PhoneNumber.Trim());

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

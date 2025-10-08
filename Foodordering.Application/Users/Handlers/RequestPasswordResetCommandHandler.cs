using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Commands;
using Foodordering.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar.Core;
using Kavenegar;

namespace Foodordering.Application.Users.Handlers
{
    public class RequestPasswordResetCommandHandler
       : IRequestHandler<RequestPasswordResetCommand, bool>
    {
        private readonly IAppDbContext _context;


        public RequestPasswordResetCommandHandler(IAppDbContext context)
        {
            _context = context;

        }

        public async Task<bool> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);
            if (user == null)
                throw new Exception("کاربر یافت نشد");

            // ✅ ساخت کد تصادفی ۶ رقمی
            var code = new Random().Next(100000, 999999).ToString();

            // ✅ ذخیره در جدول PasswordResetCodes
            var resetCode = new PasswordResetCode(user.Id, code, DateTime.UtcNow.AddMinutes(5));


            _context.PasswordResetCodes.Add(resetCode);
            await _context.SaveChangesAsync(cancellationToken);

            // ✅ ارسال پیامک


            var sender = "2000660110";
            var receptor = "09216063574";
            var message = ".وب سرویس پیام کوتاه کاوه نگار";
            var api = new Kavenegar.KavenegarApi("4F7350536C4A716B6B45754170666E364A4A617435617A72374D472B6D65534677464237537635545234553D");
            api.Send(sender, receptor, message);
            return true;
        }
    }

}

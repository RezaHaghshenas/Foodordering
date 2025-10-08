using FluentValidation;
using Foodordering.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }       // کد تأیید
        public string NewPassword { get; set; } // رمز جدید

        public string? DeviceId { get; set; }
        public string DeviceName { get; set; }

        public string IpAddress { get; set; }

    }




    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.NewPassword)
           .NotEmpty().WithMessage("پسورد جدید نمی‌تواند خالی باشد.")
           .MinimumLength(6).WithMessage("پسورد باید حداقل 6 کاراکتر داشته باشد.");


            // شماره موبایل ایران فقط اگر ارسال شده باشد
            When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber), () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .Matches(@"^09\d{9}$") // شروع با 09 و بعد 9 رقم دیگر
                    .WithMessage("شماره موبایل ایران معتبر نیست. مثال: 09123456789");
            });

            // IP فقط اگر ارسال شده باشد
            When(x => !string.IsNullOrWhiteSpace(x.IpAddress), () =>
            {
                RuleFor(x => x.IpAddress)
                    .Must(ip => System.Net.IPAddress.TryParse(ip, out _))
                    .WithMessage("IP معتبر نیست");
            });


            // اختیاری: بررسی DeviceId، DeviceName، IpAddress و UserAgent
            RuleFor(x => x.DeviceId)
                .MaximumLength(100).WithMessage("DeviceId طولانی است.");

            RuleFor(x => x.DeviceName)
                .MaximumLength(100).WithMessage("DeviceName طولانی است.");

            RuleFor(x => x.IpAddress)
                .MaximumLength(50).WithMessage("IpAddress طولانی است.");


        }
    }

}

using FluentValidation;
using Foodordering.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Commands
{
    public class RegisterUserCommand : IRequest<Guid> // userid
    {

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        // ✅ برای پشتیبانی از چند دستگاه همزمان:
        public string? DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }




    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {


            // ایمیل فقط اگر ارسال شده باشد
            When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress().WithMessage("ایمیل معتبر نیست");
            });


            RuleFor(x => x.Password)
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

            RuleFor(x => x.UserAgent)
                .MaximumLength(250).WithMessage("UserAgent طولانی است.");
        }
    }
}

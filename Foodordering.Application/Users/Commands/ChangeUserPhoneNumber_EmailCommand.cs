using FluentValidation;
using Foodordering.Application.Common.DTOs;
using MediatR;

namespace Foodordering.Application.Users.Commands
{
    public class ChangeUserPhoneNumber_EmailCommand : IRequest<AuthResultDto>
    {
        public Guid UserId { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string RefreshToken { get; set; }


        // ✅ برای پشتیبانی از چند دستگاه همزمان:
        public string? DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }


        public class ChangeUserPhoneNumber_EmailCommandValidator : AbstractValidator<ChangeUserPhoneNumber_EmailCommand>
        {
            public ChangeUserPhoneNumber_EmailCommandValidator()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("UserId نمی‌تواند خالی باشد");

                RuleFor(x => x.RefreshToken)
                    .NotEmpty().WithMessage("RefreshToken لازم است");

                // ایمیل فقط اگر ارسال شده باشد
                When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
                {
                    RuleFor(x => x.Email)
                        .EmailAddress().WithMessage("ایمیل معتبر نیست");
                });

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


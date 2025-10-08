using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Commands
{
    public class LogoutAllUserCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; }

        public string? DeviceId { get; set; }
        public string DeviceName { get; set; }

        public string IpAddress { get; set; }
    }


    public class LogoutAllUserCommandValidator : AbstractValidator<LogoutAllUserCommand>
    {
        public LogoutAllUserCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
           .NotEmpty().WithMessage("RefreshToken لازم است");


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

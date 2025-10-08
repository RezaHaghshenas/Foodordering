using FluentValidation;
using Foodordering.Application.Common.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResultDto>
    {
        public string RefreshToken { get; set; }

        public string? DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string UserAgent { get; set; }

        public string IpAddress { get; set; }
        
  
    }






    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
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

            RuleFor(x => x.UserAgent)
                .MaximumLength(250).WithMessage("UserAgent طولانی است.");
        }
    }

}

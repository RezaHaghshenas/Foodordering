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
    // Application/Users/Commands/ChangeUserPasswordCommand.cs
    public class ChangeUserPasswordCommand : IRequest<AuthResultDto>
    {
        public Guid UserId { get; set; }

        public string RefreshToken { get; set; }

        public string NewPassword { get; set; } = string.Empty;



        // ✅ برای پشتیبانی از چند دستگاه همزمان:
        public string? DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }



    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId نمی‌تواند خالی باشد.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("پسورد جدید نمی‌تواند خالی باشد.")
                .MinimumLength(6).WithMessage("پسورد باید حداقل 6 کاراکتر داشته باشد.");

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("RefreshToken ضروری است.");

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

using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Commands
{
    public class RequestPasswordResetCommand : IRequest<bool>
    {
        public string PhoneNumber { get; set; }
    }



    public class RequestPasswordResetCommandValidator : AbstractValidator<RequestPasswordResetCommand>
    {
        public RequestPasswordResetCommandValidator()
        {


       
            // شماره موبایل ایران فقط اگر ارسال شده باشد
            When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber), () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .Matches(@"^09\d{9}$") // شروع با 09 و بعد 9 رقم دیگر
                    .WithMessage("شماره موبایل ایران معتبر نیست. مثال: 09123456789");
            });



        }
    }

}

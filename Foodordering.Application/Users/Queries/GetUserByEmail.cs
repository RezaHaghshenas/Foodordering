using FluentValidation;
using Foodordering.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Queries
{

    public class GetUserByEmail : IRequest<UserDto>
    {
        public string Email { get; set; }
    }


    public class GetUserByEmailValidator : AbstractValidator<GetUserByEmail>
    {
        public GetUserByEmailValidator()
        {

            When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
                {
                    RuleFor(x => x.Email)
                        .EmailAddress().WithMessage("ایمیل معتبر نیست");
                });
        }

    }

}

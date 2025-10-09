using FluentValidation;
using Foodordering.Application.Common.DTOs.Address;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Commands
{
    public class MarkUserAddressDefaultCommand : IRequest<List<AddressDto>>
    {
        public Guid Address_Id { get; set; }
    }


    public class MarkAddressDefaultCommandValidator : AbstractValidator<MarkUserAddressDefaultCommand>
    {
        public MarkAddressDefaultCommandValidator()
        {
            RuleFor(x => x.Address_Id)
          .NotEmpty().WithMessage("Addreess_Id نمی‌تواند خالی باشد");
        }
    }
}

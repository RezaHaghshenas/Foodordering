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
    public class RemoveAddressCommand : IRequest<List<AddressDto>>
    {
        public Guid Address_Id { get; set; }
    }


    public class RemoveAddressCommandValidator : AbstractValidator<RemoveAddressCommand>
    {
        public RemoveAddressCommandValidator()
        {
            RuleFor(x => x.Address_Id)
          .NotEmpty().WithMessage("Addreess_Id نمی‌تواند خالی باشد");
        }
    }
}

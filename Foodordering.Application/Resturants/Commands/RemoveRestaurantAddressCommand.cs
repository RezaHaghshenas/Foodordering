using FluentValidation;
using Foodordering.Application.Common.DTOs.Address;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{


    public class RemoveRestaurantAddressCommand : IRequest<List<AddressDto>>
    {
        public Guid Address_Id { get; set; }
    }


    public class RemoveRestaurantAddressCommandValidator : AbstractValidator<RemoveRestaurantAddressCommand>
    {
        public RemoveRestaurantAddressCommandValidator()
        {
            RuleFor(x => x.Address_Id)
          .NotEmpty().WithMessage("Addreess_Id نمی‌تواند خالی باشد");
        }
    }
}

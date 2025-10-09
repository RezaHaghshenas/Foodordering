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
  
    public class MarkRestaurantAddressDefaultCommand : IRequest<List<AddressDto>>
    {
        public Guid Address_Id { get; set; }
    }


    public class MarkRestaurantAddressDefaultCommandValidator : AbstractValidator<MarkRestaurantAddressDefaultCommand>
    {
        public MarkRestaurantAddressDefaultCommandValidator()
        {
            RuleFor(x => x.Address_Id)
          .NotEmpty().WithMessage("Addreess_Id نمی‌تواند خالی باشد");
        }
    }
}

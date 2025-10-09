using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{
    public class DeactivateRestaurantCommand : IRequest<bool>
    {
        public Guid RestaurantId { get; set; }
    }


    public class DeactivateRestaurantCommandValidator : AbstractValidator<DeactivateRestaurantCommand>
    {
        public DeactivateRestaurantCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty();
        }

    }
}
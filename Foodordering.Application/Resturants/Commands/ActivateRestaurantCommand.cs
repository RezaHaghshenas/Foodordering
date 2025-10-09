using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{
    public class ActivateRestaurantCommand : IRequest<bool>
    {
        public Guid RestaurantId { get; set; }


    }

    public class ActivateRestaurantCommandValidator : AbstractValidator<ActivateRestaurantCommand>
    {
        public ActivateRestaurantCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty();
        }

    }
}

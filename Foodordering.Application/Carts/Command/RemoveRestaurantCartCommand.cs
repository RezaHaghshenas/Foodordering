using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class RemoveRestaurantCartCommand : IRequest<bool>
    {

        public Guid CartId { get; set; }

        public Guid RestaurantId { get; set; }


        public RemoveRestaurantCartCommand(Guid cartId, Guid restaurantId)
        {
            CartId = cartId;
            RestaurantId = restaurantId;
        }
    }
    public class RemoveRestaurantCartCommandValidator : AbstractValidator<RemoveRestaurantCartCommand>
    {
        public RemoveRestaurantCartCommandValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("شناسه سبد خرید الزامی است.");

            RuleFor(x => x.RestaurantId)
                .NotEmpty().WithMessage("شناسه رستوران الزامی است.");
        }
    }
}
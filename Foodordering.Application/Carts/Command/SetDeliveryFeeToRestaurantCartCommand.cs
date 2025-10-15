using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class SetDeliveryFeeToRestaurantCartCommand : IRequest<bool>
    {
        public Guid CartId { get; set; }
        public Guid RestaurantId { get; set; }
        public decimal DeliveryFee { get; set; }

        public SetDeliveryFeeToRestaurantCartCommand(Guid cartId, Guid restaurantId, decimal deliveryFee)
        {
            CartId = cartId;
            RestaurantId = restaurantId;
            DeliveryFee = deliveryFee;
        }
    }

public class SetDeliveryFeeToRestaurantCartCommandValidator : AbstractValidator<SetDeliveryFeeToRestaurantCartCommand>
    {
        public SetDeliveryFeeToRestaurantCartCommandValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("شناسه سبد خرید الزامی است.");

            RuleFor(x => x.RestaurantId)
                .NotEmpty().WithMessage("شناسه رستوران الزامی است.");

            RuleFor(x => x.DeliveryFee)
                .GreaterThanOrEqualTo(0).WithMessage("هزینه ارسال نمی‌تواند منفی باشد.")
                .LessThanOrEqualTo(500_000).WithMessage("هزینه ارسال بیش از حد مجاز است.");
        }
    }

}

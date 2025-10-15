using FluentValidation;
using MediatR;
using System;

namespace Foodordering.Application.Carts.Command
{
    public class ApplyDiscountToRestaurantCartCommand : IRequest<bool>
    {
        public Guid CartId { get; set; }
        public Guid RestaurantId { get; set; }
        public decimal DiscountAmount { get; set; }

        public ApplyDiscountToRestaurantCartCommand(Guid cartId, Guid restaurantId, decimal discountAmount)
        {
            CartId = cartId;
            RestaurantId = restaurantId;
            DiscountAmount = discountAmount;
        }
    }

    public class ApplyDiscountToRestaurantCartCommandValidator : AbstractValidator<ApplyDiscountToRestaurantCartCommand>
    {
        public ApplyDiscountToRestaurantCartCommandValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("شناسه سبد خرید الزامی است.");

            RuleFor(x => x.RestaurantId)
                .NotEmpty().WithMessage("شناسه رستوران الزامی است.");

            RuleFor(x => x.DiscountAmount)
                .GreaterThan(0).WithMessage("مقدار تخفیف باید بیشتر از صفر باشد.")
                .LessThanOrEqualTo(1_000_000).WithMessage("تخفیف بیش از حد مجاز است.");
        }
    }
}

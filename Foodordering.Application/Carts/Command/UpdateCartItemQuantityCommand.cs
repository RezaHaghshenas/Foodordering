using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class UpdateCartItemQuantityCommand : IRequest<bool>
    {
        public Guid CartId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid CartItemId { get; set; }
        public int NewQuantity { get; set; }

        public UpdateCartItemQuantityCommand(Guid cartId, Guid restaurantId, Guid cartItemId, int newQuantity)
        {
            CartId = cartId;
            RestaurantId = restaurantId;
            CartItemId = cartItemId;
            NewQuantity = newQuantity;
        }
    }


    public class UpdateCartItemQuantityCommandValidator : AbstractValidator<UpdateCartItemQuantityCommand>
    {
        public UpdateCartItemQuantityCommandValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("شناسه سبد خرید الزامی است.");

            RuleFor(x => x.RestaurantId)
                .NotEmpty().WithMessage("شناسه رستوران الزامی است.");

            RuleFor(x => x.CartItemId)
                .NotEmpty().WithMessage("شناسه آیتم الزامی است.");

            RuleFor(x => x.NewQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("تعداد نمی‌تواند منفی باشد.")
                .LessThanOrEqualTo(100).WithMessage("حداکثر تعداد مجاز ۱۰۰ عدد است.");
        }
    }



}

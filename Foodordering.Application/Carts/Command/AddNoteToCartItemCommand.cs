using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class AddNoteToCartItemCommand : IRequest<bool>
    {
        public Guid CartId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid CartItemId { get; set; }
        public string Note { get; set; }

        public AddNoteToCartItemCommand(Guid cartId, Guid restaurantId, Guid cartItemId, string note)
        {
            CartId = cartId;
            RestaurantId = restaurantId;
            CartItemId = cartItemId;
            Note = note;
        }
    }


public class AddNoteToCartItemCommandValidator : AbstractValidator<AddNoteToCartItemCommand>
    {
        public AddNoteToCartItemCommandValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("شناسه سبد خرید الزامی است.");

            RuleFor(x => x.RestaurantId)
                .NotEmpty().WithMessage("شناسه رستوران الزامی است.");

            RuleFor(x => x.CartItemId)
                .NotEmpty().WithMessage("شناسه آیتم الزامی است.");

            RuleFor(x => x.Note)
                .NotEmpty().WithMessage("یادداشت نمی‌تواند خالی باشد.")
                .MaximumLength(200).WithMessage("یادداشت نمی‌تواند بیش از ۲۰۰ کاراکتر باشد.");
        }
    }

}

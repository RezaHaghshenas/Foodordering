using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class AddCartItemCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }

        public AddCartItemCommand(Guid userId, Guid restaurantId, Guid menuItemId, int quantity)
        {
            UserId = userId;
            RestaurantId = restaurantId;
            MenuItemId = menuItemId;
            Quantity = quantity;
        }
    }

    public class AddCartItemCommandValidator : AbstractValidator<AddCartItemCommand>
    {
        public AddCartItemCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("شناسه کاربر نمی‌تواند خالی باشد.");

            RuleFor(x => x.RestaurantId)
                .NotEmpty().WithMessage("شناسه رستوران الزامی است.");

            RuleFor(x => x.MenuItemId)
                .NotEmpty().WithMessage("شناسه آیتم منو الزامی است.");

            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 100)
                .WithMessage("تعداد باید بین ۱ تا ۱۰۰ باشد.");
        }
    }


}

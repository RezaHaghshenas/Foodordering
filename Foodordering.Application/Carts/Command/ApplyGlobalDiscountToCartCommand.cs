using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class ApplyGlobalDiscountToCartCommand : IRequest<bool>
    {
        public Guid CartId { get; set; }

        public string? DiscountCode { get; set; }


        public ApplyGlobalDiscountToCartCommand(Guid cartId, string discountCode)
        {
            CartId = cartId;
            DiscountCode = discountCode; 
        }
    }

    public class ApplyGlobalDiscountToCartCommandValidator : AbstractValidator<ApplyGlobalDiscountToCartCommand>
    {
        public ApplyGlobalDiscountToCartCommandValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("شناسه سبد خرید الزامی است.");

        }
    }
}

using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class ClearCartCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }

        public Guid CartId { get; set; }

        public ClearCartCommand(Guid userId, Guid cartid)
        {
            UserId = userId;
            CartId = cartid;
        }
    }
    public class ClearCartCommandValidator : AbstractValidator<ClearCartCommand>
    {
        public ClearCartCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("شناسه کاربر الزامی است.");

            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("شناسه سبد خرید الزامی است.");
        }
    }

}
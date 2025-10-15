using FluentValidation;
using Foodordering.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class ConvertCartToOrdersCommand : IRequest<List<Guid>>
    {
        public Guid CartId { get; set; }
        public Address DeliveryAddress { get; set; }
        public string CustomerPhone { get; set; }

        public ConvertCartToOrdersCommand(Guid cartId, Address deliveryAddress, string customerPhone)
        {
            CartId = cartId;
            DeliveryAddress = deliveryAddress;
            CustomerPhone = customerPhone;
        }
    }


    public class ConvertCartToOrdersCommandValidator : AbstractValidator<ConvertCartToOrdersCommand>
    {
        public ConvertCartToOrdersCommandValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("شناسه سبد خرید الزامی است.");

            RuleFor(x => x.CustomerPhone)
                .NotEmpty().WithMessage("شماره تماس الزامی است.")
                .Matches(@"^\d{10,15}$").WithMessage("شماره تماس نامعتبر است.");

            RuleFor(x => x.DeliveryAddress)
                .NotNull().WithMessage("آدرس الزامی است.");
        }
    }

}

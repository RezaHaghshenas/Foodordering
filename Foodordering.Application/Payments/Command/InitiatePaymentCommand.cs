using FluentValidation;
using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Payments.Command
{
    public class InitiatePaymentCommand : IRequest<Guid>
    {
        public Guid CartId { get; set; }
        public PaymentMethod Method { get; set; }

        public InitiatePaymentCommand(Guid cartId, PaymentMethod method)
        {
            CartId = cartId;
            Method = method;
        }
    }

    public class InitiatePaymentCommandValidator : AbstractValidator<InitiatePaymentCommand>
    {
        public InitiatePaymentCommandValidator()
        {
            RuleFor(x => x.CartId).NotEmpty();
            RuleFor(x => x.Method).IsInEnum();
        }
    }


}

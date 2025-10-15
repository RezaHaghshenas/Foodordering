using FluentValidation;
using Foodordering.Domain.Events;
using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Payments.Command
{
    public class FailPaymentCommand : IRequest<bool>
    {
        public Guid PaymentId { get; set; }

        public FailPaymentCommand(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }

    public class FailPaymentCommandValidator : AbstractValidator<FailPaymentCommand>
    {
        public FailPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId).NotEmpty();
        }
    }

}

using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Payments.Command
{
    public class ConfirmPaymentCommand : IRequest<List<Guid>>
    {
        public Guid PaymentId { get; set; }
        public string GatewayTransactionId { get; set; }

        public ConfirmPaymentCommand(Guid paymentId, string gatewayTransactionId)
        {
            PaymentId = paymentId;
            GatewayTransactionId = gatewayTransactionId;
        }
    }

    public class ConfirmPaymentCommandValidator : AbstractValidator<ConfirmPaymentCommand>
    {
        public ConfirmPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId).NotEmpty();
            RuleFor(x => x.GatewayTransactionId).NotEmpty();
        }
    }


}

using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Payments.Command
{
    public class RefundPaymentCommand : IRequest<bool>
    {
        public Guid PaymentId { get; set; }
        public string Reason { get; set; }

        public RefundPaymentCommand(Guid paymentId, string reason)
        {
            PaymentId = paymentId;
            Reason = reason;
        }
    }

    public class RefundPaymentCommandValidator : AbstractValidator<RefundPaymentCommand>
    {
        public RefundPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("شناسه پرداخت الزامی است.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("علت بازپرداخت باید مشخص باشد.")
                .MinimumLength(5).WithMessage("علت باید حداقل ۵ کاراکتر باشد.");
        }
    }

}

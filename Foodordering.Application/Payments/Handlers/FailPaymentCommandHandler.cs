using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Orders.Commands;
using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodordering.Application.Payments.Command; 
namespace Foodordering.Application.Payments.Handlers
{
    public class FailPaymentCommandHandler : IRequestHandler<Command.FailPaymentCommand , bool>
    {
        private readonly IAppDbContext _context;

        public FailPaymentCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command.FailPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.payments
                .FirstOrDefaultAsync(p => p.Id == request.PaymentId, cancellationToken);

            if (payment == null)
                throw new InvalidOperationException("پرداخت یافت نشد");

            if (payment.Status != PaymentStatus.Pending)
                throw new InvalidOperationException("پرداخت در وضعیت قابل شکست نیست");

            payment.MarkFailed();
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}

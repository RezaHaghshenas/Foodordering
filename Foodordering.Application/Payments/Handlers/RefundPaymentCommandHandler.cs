using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Payments.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Payments.Handlers
{
    public class RefundPaymentCommandHandler : IRequestHandler<RefundPaymentCommand , bool>
    {
        private readonly IAppDbContext _context;

        public RefundPaymentCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.payments
                .FirstOrDefaultAsync(p => p.Id == request.PaymentId, cancellationToken);

            if (payment == null)
                throw new InvalidOperationException("پرداخت یافت نشد");

            payment.Refund(request.Reason);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}

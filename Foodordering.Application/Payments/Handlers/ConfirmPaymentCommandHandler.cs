using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Payments.Command;
using Foodordering.Domain.Entities;
using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Payments.Handlers
{
    public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, List<Guid>>
    {
        private readonly IAppDbContext _context;

        public ConfirmPaymentCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.payments
                .Include(p => p.cart)
                .FirstOrDefaultAsync(p => p.Id == request.PaymentId, cancellationToken);

        
            if (payment == null)
                throw new InvalidOperationException("پرداخت یافت نشد");

            if (payment.Status == PaymentStatus.Success)
                throw new InvalidOperationException("این پرداخت قبلاً تأیید شده است.");



            var user = await _context.Users.FirstOrDefaultAsync(fi => fi.Id == payment.cart.UserId);

            if (user == null)
                throw new InvalidOperationException("کاربری با این مشخصات پیدا نشد ");


            var user_Address = await _context.addresses.FirstOrDefaultAsync(f => f.UserId == payment.cart.UserId && f.IsDefault == true);

       



            if (user_Address == null)
                throw new InvalidOperationException("کاربر آدرس فعالی ندارد");

            payment.MarkSuccess(request.GatewayTransactionId);

          
            

            var orders = payment.cart.ToOrders(user_Address,user.PhoneNumber);
            foreach (var order in orders)
            {
                order.MarkAsPaid();
                _context.orders.Add(order);
            }

            payment.cart.Clear();
            await _context.SaveChangesAsync(cancellationToken);

            return orders.Select(o => o.Id).ToList();
        }
    }

}

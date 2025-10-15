using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Payments.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Foodordering.Domain.Entities;
using FoodOrdering.Domain.Entities;

public class InitiatePaymentCommandHandler : IRequestHandler<InitiatePaymentCommand, Guid>
{
    private readonly IAppDbContext _context;

    public InitiatePaymentCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(InitiatePaymentCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.carts
            .Include(c => c.RestaurantCarts)
                .ThenInclude(rc => rc.Items)
            .FirstOrDefaultAsync(c => c.Id == request.CartId, cancellationToken);

        if (cart == null)
            throw new InvalidOperationException("سبد خرید یافت نشد");

        var amount = cart.TotalPrice_AfterDiscount;// فرض بر اینکه متدی برای محاسبه کل مبلغ داری

        var payment = new Payment(cart.Id, amount, request.Method);
        _context.payments.Add(payment);

        await _context.SaveChangesAsync(cancellationToken);
        return payment.Id;
    }
}

using Foodordering.Application.Carts.Command;
using Foodordering.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Handlers
{
    public class ApplyGlobalDiscountToCartCommandHandler : IRequestHandler<ApplyGlobalDiscountToCartCommand, bool>
    {
        private readonly IAppDbContext _context;

        public ApplyGlobalDiscountToCartCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ApplyGlobalDiscountToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.carts
                .Include(c => c.RestaurantCarts)
                    .ThenInclude(rc => rc.Items)
                .FirstOrDefaultAsync(c => c.Id == request.CartId, cancellationToken);

            if (cart == null)
                throw new InvalidOperationException("سبد خرید یافت نشد");

            var totalPrice = cart.RestaurantCarts
                .SelectMany(rc => rc.Items)
                .Sum(i => i.Price);


            var discount_Amount = await _context.discountCodes.FirstOrDefaultAsync(d => d.Code == request.DiscountCode, cancellationToken);
            if (discount_Amount == null)
            {
                throw new InvalidOperationException("چنین کدی یافتد نشد ");
            }
            cart.DiscountCode_Id = discount_Amount.Id;
            if (discount_Amount.IsPercentage == false)
            {
                if (discount_Amount.Amount > totalPrice)
                {
                    throw new InvalidOperationException("مقدار تخفیف بیشتر از مجموع قیمت آیتم‌هاست");
                }
                cart.TotalPrice_AfterDiscount = (decimal)(totalPrice -  discount_Amount.Amount);   
            }
            else
            {
                if (discount_Amount.Amount >= 100)
                {
                    throw new InvalidOperationException("مقدار تخفیف بیشتر از مجموع قیمت آیتم‌هاست");
                }
                cart.TotalPrice_AfterDiscount = (decimal)(totalPrice - (discount_Amount.Amount * totalPrice / 100));
            }
                await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

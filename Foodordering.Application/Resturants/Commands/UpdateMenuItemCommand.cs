using FluentValidation;
using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{
    public class UpdateMenuItemCommand : IRequest<bool>
    {
        public Guid RestaurantId { get; set; }
        public Guid MenuItemId { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public MenuCategory? Category { get; set; }
        public int? PreparationTimeMinutes { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class UpdateMenuItemCommandValidator : AbstractValidator<UpdateMenuItemCommand>
    {
        public UpdateMenuItemCommandValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty();
            RuleFor(x => x.MenuItemId).NotEmpty();

            When(x => x.Price.HasValue, () =>
            {
                RuleFor(x => x.Price.Value).GreaterThan(0);
            });

            When(x => x.PreparationTimeMinutes.HasValue, () =>
            {
                RuleFor(x => x.PreparationTimeMinutes.Value).InclusiveBetween(1, 180);
            });
        }
    }
}

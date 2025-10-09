using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{
    public class RemoveMenuItemCommand : IRequest<bool>
    {
        public Guid RestaurantId { get; set; }

        public Guid MenuItemId { get; set; }
    }

    public class RemoveMenuItemCommandValidator : AbstractValidator<RemoveMenuItemCommand>
    {
        public RemoveMenuItemCommandValidator()
        {
            RuleFor(x => x.MenuItemId)
                .NotEmpty();

            RuleFor(x => x.RestaurantId)
         .NotEmpty();
        }

    }


}

using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{
    public class ActivateMenuItemCommand : IRequest<bool>
    {
        public Guid MenuItemId { get; set; }

        public Guid ResturantId { get; set; }
    }

    public class ActivateMenuItemCommandValidator : AbstractValidator<ActivateMenuItemCommand>
    {
        public ActivateMenuItemCommandValidator()
        {
            RuleFor(x => x.MenuItemId)
                .NotEmpty();

            RuleFor(x => x.ResturantId)
            .NotEmpty();
        }

    }


}

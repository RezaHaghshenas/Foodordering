using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{


    public class DeactivateMenuItemCommand : IRequest<bool>
    {
        public Guid ResturantId { get; set; }
        public Guid MenuItemId { get; set; }

    }

    public class DeactivateMenuItemCommandValidator : AbstractValidator<DeactivateMenuItemCommand>
    {
        public DeactivateMenuItemCommandValidator()
        {
            RuleFor(x => x.MenuItemId)
                .NotEmpty();

            RuleFor(x => x.ResturantId)
         .NotEmpty();
        }

    }

}

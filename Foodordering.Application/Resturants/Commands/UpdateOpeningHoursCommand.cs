using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{
    public class UpdateOpeningHoursCommand : IRequest
    {
        public Guid RestaurantId { get; set; }
        public string OpeningHours { get; set; }
    }

    public class UpdateOpeningHoursCommandValidator : AbstractValidator<UpdateOpeningHoursCommand>
    {
        public UpdateOpeningHoursCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty();

            RuleFor(x => x.OpeningHours)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}

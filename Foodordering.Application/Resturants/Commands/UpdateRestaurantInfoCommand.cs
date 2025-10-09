using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{
    public class UpdateRestaurantInfoCommand : IRequest<bool>
    {
        public Guid RestaurantId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string RestaurantPhone { get; set; }
    }

    public class UpdateRestaurantInfoCommandValidator : AbstractValidator<UpdateRestaurantInfoCommand>
    {
        public UpdateRestaurantInfoCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty().WithMessage("شناسه رستوران الزامی است.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام رستوران الزامی است.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.RestaurantPhone)
                .NotEmpty().WithMessage("شماره تماس الزامی است.")
                .Matches(@"^0\d{10}$").WithMessage("شماره تماس معتبر نیست.");
        }
    }
}

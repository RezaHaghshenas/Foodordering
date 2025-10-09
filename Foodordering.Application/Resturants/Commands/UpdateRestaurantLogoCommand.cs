using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{
    public class UpdateRestaurantLogoCommand : IRequest
    {
        public Guid RestaurantId { get; set; }
        public string LogoUrl { get; set; }
    }

    public class UpdateRestaurantLogoCommandValidator : AbstractValidator<UpdateRestaurantLogoCommand>
    {
        public UpdateRestaurantLogoCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty();

            RuleFor(x => x.LogoUrl)
                .NotEmpty()
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("آدرس لوگو معتبر نیست.");
        }
    }
}

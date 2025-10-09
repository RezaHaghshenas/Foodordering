using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Commands
{
    public class CreateRestaurantCommand : IRequest<Guid>
    {
        public required string OwnerName { get; set; }
        public required string OwnerFamily { get; set; }
        public required string OwnerNationalCode { get; set; }
        public required string OwnerPhone { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string RestaurantPhone { get; set; }

    }



    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(x => x.OwnerName)
                .NotEmpty().WithMessage("نام مالک الزامی است.")
                .MaximumLength(50);

            RuleFor(x => x.OwnerFamily)
                .NotEmpty().WithMessage("نام خانوادگی مالک الزامی است.")
                .MaximumLength(50);

            RuleFor(x => x.OwnerNationalCode)
                .NotEmpty().WithMessage("کد ملی الزامی است.")
                .Length(10).WithMessage("کد ملی باید ۱۰ رقم باشد.");

            RuleFor(x => x.OwnerPhone)
                .NotEmpty().WithMessage("شماره تماس مالک الزامی است.")
                .Matches(@"^09\d{9}$").WithMessage("شماره تماس معتبر نیست.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام رستوران الزامی است.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);


            RuleFor(x => x.RestaurantPhone)
                .NotEmpty().WithMessage("شماره تماس رستوران الزامی است.")
                .Matches(@"^0\d{10}$").WithMessage("شماره تماس معتبر نیست.");


        }
    }
}

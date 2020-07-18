using FluentValidation;
using RideShare.Web.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Validations
{
    public class TravelValidator : AbstractValidator<TravelRequestDto>
    {
        public TravelValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(500);
            RuleFor(x => x.StartingPoint).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.TargetPoint).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.TotalArmchair).NotNull().NotEmpty();
        }
    }
}

using FluentValidation;
using RideShare.Web.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Validations
{
    public class TravelUpdateValidator : AbstractValidator<TravelUpdateRequestDto>
    {
        public TravelUpdateValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(200).When(x=>!string.IsNullOrEmpty(x.Title));
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description));
            RuleFor(x => x.StartingPoint).NotNull().NotEmpty().MaximumLength(50).When(x => !string.IsNullOrEmpty(x.StartingPoint));
            RuleFor(x => x.TargetPoint).NotNull().NotEmpty().MaximumLength(50).When(x => !string.IsNullOrEmpty(x.TargetPoint));
            RuleFor(x => x.TotalArmchair).NotNull().NotEmpty().When(x => x.TotalArmchair != 0);
        }
    }
}

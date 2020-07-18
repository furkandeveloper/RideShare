using FluentValidation;
using RideShare.Web.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Validations
{
    public class UserUpdateValidator : AbstractValidator<UserRequestDto>
    {
        public const string regex = @"^[2-9]\d{2}-\d{3}-\d{4}$";
        public UserUpdateValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull().When(r=>!string.IsNullOrEmpty(r.Name));
            RuleFor(r => r.Surname).NotEmpty().NotNull().When(r => !string.IsNullOrEmpty(r.Surname));
            RuleFor(r => r.PhoneNumber).NotEmpty().NotNull().Matches(regex).When(r => !string.IsNullOrEmpty(r.PhoneNumber));
        }
    }
}

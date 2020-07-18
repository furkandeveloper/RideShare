using FluentValidation;
using RideShare.Web.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Validations
{
    public class UserValidator : AbstractValidator<UserRequestDto>
    {
        public const string regex = "^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$";
        public UserValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull();
            RuleFor(r => r.Surname).NotEmpty().NotNull();
            RuleFor(r => r.PhoneNumber).NotEmpty().NotNull().Matches(regex);
        }
    }
}

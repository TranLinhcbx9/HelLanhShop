using FluentValidation;
using HelLanhShop.Application.Authentications.DTOs;
using HelLanhShop.Application.Common.Validation.Base;
using HelLanhShop.Application.Common.Validation.Extensions;

namespace HelLanhShop.Application.Authentications.Validators
{
    public class RegisterRequestValidator : BaseValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator() 
        {
            RuleFor(x => x.UserName).NotEmptyTrim().WithMessage("Username is required.");
            RuleFor(x => x.Password).NotEmptyTrim().WithMessage("Password is required.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be valid.");
            RuleFor(x => x.Phone).OptionalPhoneNumber().WithMessage("Phone number must be valid.");
        }
    }
}

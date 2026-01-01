using FluentValidation;
using HelLanhShop.Application.Authentications.DTOs;
using HelLanhShop.Application.Common.Validation.Base;
using HelLanhShop.Application.Common.Validation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Authentications.Validators
{
    public class LoginRequestValidator : BaseValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserNameOrEmail).NotEmptyTrim().WithMessage("Username is required.");
            RuleFor(x => x.Password).NotEmptyTrim().WithMessage("Password is required.");
        }   
    }
}

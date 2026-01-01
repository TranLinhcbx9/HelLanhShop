using FluentValidation;
using HelLanhShop.Application.Authentications.DTOs;
using HelLanhShop.Application.Common.Validation.Base;
using HelLanhShop.Application.Common.Validation.Extensions;

namespace HelLanhShop.Application.Authentications.Validators
{
    public class RefreshRequestValidator : BaseValidator<RefreshRequestDto>
    {
        public RefreshRequestValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmptyTrim().WithMessage("Refresh token is required.");
        }
    }
}

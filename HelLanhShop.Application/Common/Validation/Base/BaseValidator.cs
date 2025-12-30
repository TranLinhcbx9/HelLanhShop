using FluentValidation;
using HelLanhShop.Application.Common.Enums;
using HelLanhShop.Application.Common.Models;

namespace HelLanhShop.Application.Common.Validation.Base
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        public Result ValidateToResult(T model)
        {
            var validationResult = Validate(model);

            if (validationResult.IsValid)
                return Result.Success();

            // Gộp message cho gọn (FE đọc được)
            var errorMessage = string.Join("; ",
                validationResult.Errors.Select(e =>
                    $"{e.PropertyName}: {e.ErrorMessage}")
            );

            return Result.Failure(
                error: errorMessage,
                errorType: ErrorType.Validation,
                errorCode: ErrorCode.VALIDATION_ERROR
            );
        }
    }
}

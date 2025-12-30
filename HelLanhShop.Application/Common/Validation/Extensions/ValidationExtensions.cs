using FluentValidation;
using System.Linq.Expressions;


namespace HelLanhShop.Application.Common.Validation.Extensions
{

    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> NotEmptyTrim<T>(
        this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty()
                .Must(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}

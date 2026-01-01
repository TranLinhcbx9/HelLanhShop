using FluentValidation;
using System.Linq.Expressions;
using System.Text.RegularExpressions;


namespace HelLanhShop.Application.Common.Validation.Extensions
{

    public static class ValidationExtensions
    {
        private static readonly Regex VnPhoneRegex = new(@"^(0|\+84)(3|5|7|8|9)\d{8}$", RegexOptions.Compiled);

        public static IRuleBuilderOptions<T, string> NotEmptyTrim<T>(
        this IRuleBuilder<T, string> rule)
        {
            return rule.Must(x => !string.IsNullOrWhiteSpace(x));
        }
        public static IRuleBuilderOptions<T, string?> OptionalPhoneNumber<T>(
        this IRuleBuilder<T, string?> rule)
        {
            return rule
                .Must(phone =>
                string.IsNullOrWhiteSpace(phone) || VnPhoneRegex.IsMatch(phone));
        }
        public static IRuleBuilderOptions<T, string?> RequiredPhone<T>(
        this IRuleBuilder<T, string?> rule)
        {
            return rule
                .Must(phone => !string.IsNullOrWhiteSpace(phone))
                .Must(phone => VnPhoneRegex.IsMatch(phone!));
        }
    }
}

using FluentValidation;
using System.Linq.Expressions;

namespace HelLanhShop.Application.Common.Validation.Base
{
    public abstract class EntityIdValidator<T> : BaseValidator<T>
    {
        protected void ValidateId(
            Expression<Func<T, int>> expr,
            string errorCode)
        {
            RuleFor(expr)
                .GreaterThan(0)
                .WithErrorCode(errorCode);
        }
    }
}

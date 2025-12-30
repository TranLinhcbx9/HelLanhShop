using FluentValidation;
using HelLanhShop.Application.Common.Models;
using System.Linq.Expressions;

namespace HelLanhShop.Application.Common.Validation.Base
{
    public abstract class PagingValidator<T> : BaseValidator<T> where T: PagingRequest
    {
        public PagingValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1).WithMessage("PageIndex must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100).WithMessage("PageSize must be greater than 0");
        }
    }
}

using FluentValidation;
using HelLanhShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Validation.Base
{
    public class FilterValidator<T> : BaseValidator<T> where T : BaseFilter
    {
        public FilterValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("Page Index must be greater than 0");
            RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(100).WithMessage("Page Size must be greater than 0");
        }
    }
}

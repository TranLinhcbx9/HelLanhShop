using FluentValidation;
using HelLanhShop.Application.Common.Validation.Base;
using HelLanhShop.Application.Common.Validation.Extensions;
using HelLanhShop.Application.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Products.Validators
{
    public class UpdateProductValidator : EntityIdValidator<UpdateProductDto>
    {
        public UpdateProductValidator() {
            RuleFor(x => x.Name)
                .NotEmptyTrim().WithMessage("Product Name is required.");
            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit Price must be greater than 0.");
            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Cost Price must be greater than or equal to 0.");
            //RuleFor(x => x.Stock)
            //    .GreaterThanOrEqualTo(0).When(x => x.Stock.HasValue).WithMessage("Stock must be greater than or equal to 0.");
        }
    }
}

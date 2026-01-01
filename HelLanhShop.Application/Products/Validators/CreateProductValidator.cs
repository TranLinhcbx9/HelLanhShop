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
    public class CreateProductValidator : BaseValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmptyTrim().WithMessage("Product Name is required")
                .MaximumLength(200).WithMessage("Name max length is 200");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit Price must be greater than 0");

            RuleFor(x => x.CostPrice)
                .GreaterThan(0).WithMessage("Cost Price must be greater than 0");

        }
    }
}

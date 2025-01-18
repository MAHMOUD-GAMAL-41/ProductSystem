using FluentValidation;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Validation
{
    public class ProductSearchDtoValidator : AbstractValidator<ProductSearchDto>
    {
        public ProductSearchDtoValidator()
        {
            RuleFor(x => x.MinPrice).GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
                .WithMessage("MinPrice must be a non-negative value.");

            RuleFor(x => x.MaxPrice).GreaterThanOrEqualTo(0).When(x => x.MaxPrice.HasValue)
                .WithMessage("MaxPrice must be a non-negative value.");

        }
    }
}

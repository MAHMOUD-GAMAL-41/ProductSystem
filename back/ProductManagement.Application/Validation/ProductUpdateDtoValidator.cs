using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Validation
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(p => !string.IsNullOrEmpty(p.Name));

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(p => !string.IsNullOrEmpty(p.Description));

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be a positive number.")
                .When(p => p.Price.HasValue);
          
        }
    }
}

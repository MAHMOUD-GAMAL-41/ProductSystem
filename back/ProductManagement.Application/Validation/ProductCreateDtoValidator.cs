
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Helpers;

namespace ProductManagement.Application.Validation
{


    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be a positive number.");

            

        }

    }

}

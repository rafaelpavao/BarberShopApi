using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
{
    public CreateProductCategoryCommandValidator()
    {
        RuleFor(p=>p.Name)
            .NotEmpty()
                .WithMessage("You should fill out a name")
            .MaximumLength(50)
                .WithMessage("The {PropertyName} shouldn't have more than 50 characteres");
    }
}
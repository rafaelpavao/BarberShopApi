using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand>
{
    public UpdateProductCategoryCommandValidator()
    {
        RuleFor(p=>p.Name)
            .NotEmpty()
                .WithMessage("You should fill out a name");
    }
}
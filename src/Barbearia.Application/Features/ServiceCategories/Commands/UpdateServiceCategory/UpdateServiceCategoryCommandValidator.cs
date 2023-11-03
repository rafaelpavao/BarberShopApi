using FluentValidation;

namespace Barbearia.Application.Features.ServiceCategories.Commands.UpdateServiceCategory;

public class UpdateServiceCategoryCommandValidator : AbstractValidator<UpdateServiceCategoryCommand>
{
    public UpdateServiceCategoryCommandValidator(){
        RuleFor(p=>p.Name)
            .NotEmpty()
                .WithMessage("You should fill out a name")
            .MaximumLength(80)
                .WithMessage("The name can only have 80 characters at most")
            .MinimumLength(1)
                .WithMessage("Service name cannot be empty");
    }
}
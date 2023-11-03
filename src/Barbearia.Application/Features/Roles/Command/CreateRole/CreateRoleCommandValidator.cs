using FluentValidation;

namespace Barbearia.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator(){
        RuleFor(p=>p.Name)
            .NotEmpty()
                .WithMessage("You should fill out a name")
            .MaximumLength(80)
                .WithMessage("The name can only have 80 characters at most")
            .MinimumLength(1)
                .WithMessage("The name cannot be empty");
    }
}
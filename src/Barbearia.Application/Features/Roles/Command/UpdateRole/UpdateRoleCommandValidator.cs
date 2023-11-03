using FluentValidation;

namespace Barbearia.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator(){
        RuleFor(p=>p.Name)
            .NotEmpty()
                .WithMessage("You should fill out a name")
            .MaximumLength(80)
                .WithMessage("The name can only have 80 characters at most")
            .MinimumLength(1)
                .WithMessage("The name cannot be empty");
    }
}
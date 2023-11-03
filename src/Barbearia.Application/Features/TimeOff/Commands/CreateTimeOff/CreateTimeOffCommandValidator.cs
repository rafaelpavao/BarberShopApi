using System.Data;
using System.Security;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Barbearia.Application.Features.TimesOff.Commands.CreateTimeOff;

public class CreateTimeOffCommandValidator : AbstractValidator<CreateTimeOffCommand>
{
    public CreateTimeOffCommandValidator()
    {

        RuleFor(t=>t.WorkingDayId)
            .NotEmpty()
                .WithMessage("You should fill working day id");

        RuleFor(t=>t.StartTime)
            .NotEmpty()
                .WithMessage("You should inform a start time");
                
        RuleFor(t=>t.FinishTime)
            .NotEmpty()
                .WithMessage("You should inform a finish time")
            .GreaterThan(t=>t.StartTime)
                .WithMessage("The finish time must be after the start time");

    }
}
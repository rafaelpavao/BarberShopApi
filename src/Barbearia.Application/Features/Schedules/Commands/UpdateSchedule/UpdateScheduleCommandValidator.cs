using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;

public class UpdateScheduleCommandValidator : AbstractValidator<UpdateScheduleCommand>
{
    public UpdateScheduleCommandValidator()
    {
        RuleFor(s => s.WorkingDayId)
             .NotEmpty()
                 .WithMessage("You should fill out a working day");

        RuleFor(s => s.Status)
            .NotEmpty()
                .WithMessage("You should fill out a status")
            .Must(CheckStatus)
                .WithMessage("Status must be 1 or 2");
    }

    private bool CheckStatus(int Status)
    {
        if (Status != 1 && Status != 2)
        {
            return false;
        }
        return true;
    }
}
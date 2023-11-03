using FluentValidation;

namespace Barbearia.Application.Features.WorkingDays.Commands.UpdateWorkingDay;
public class UpdateWorkingDayCommandValidator : AbstractValidator<UpdateWorkingDayCommand>
{
    public UpdateWorkingDayCommandValidator()
    {
        RuleFor(w=>w.PersonId)
            .NotEmpty()
                .WithMessage("You must inform person Id")
            .Must(CheckPerson)
                .WithMessage("You must inform valid person id");

        RuleFor(w=>w.WorkDate)
            .NotEmpty()
                .WithMessage("You must inform work date");

        RuleFor(w=>w.StartTime)
            .NotEmpty()
                .WithMessage("You must inform a start time");

        RuleFor(w=>w.FinishTime)
            .NotEmpty()
                .WithMessage("You must inform a finish time")
                .GreaterThan(w=>w.StartTime)
                    .WithMessage("Finish Time cant start before start time");

        
    }

    private bool CheckPerson(int personId)
    {
        if(personId<=0)
        {
            return false;
        }
        return true;
    }
}
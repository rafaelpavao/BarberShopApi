using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(a => a.ScheduleId)
                .NotEmpty()
                    .WithMessage("Should have a schedule");

            RuleFor(a => a.PersonId)
                .NotEmpty()
                    .WithMessage("Should have a customer");

            RuleFor(a => a.StartDate)
                .NotEmpty()
                    .WithMessage("start date cannot be empty");

            RuleFor(a => a.FinishDate)
                .NotEmpty()
                    .WithMessage("finish date cannot be empty")
                .GreaterThan(a=>a.StartDate)
                    .WithMessage("Finish time cant come before start time");

            RuleFor(a => a.Status)
                .NotEmpty()
                    .WithMessage("status cannot be empty")
                .Must(CheckStatus)
                    .WithMessage("Accepted status are between 1 to 3");

            RuleFor(a => a.StartServiceDate)
                .NotEmpty()
                    .WithMessage("start service date cannot be empty");

            RuleFor(a => a.FinishServiceDate)
                .NotEmpty()
                    .WithMessage("finish service date cannot be empty")
                .GreaterThan(a=>a.StartServiceDate)
                    .WithMessage("Finish time cant come before start time");
                    
            RuleFor(a => a.ServicesId)
                .NotEmpty()
                    .WithMessage("Should have at least one service");

        }
        private bool CheckStatus(int Status)
        {
            if (Status < 1 || Status > 3)
            {
                return false;
            }
            return true;
        }
    }
}

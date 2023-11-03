using Barbearia.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<CreateAppointmentCommandResponse>
    {
        public int ScheduleId { get; set; }
        public int PersonId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int Status { get; set; }
        public DateTime StartServiceDate { get; set; }
        public DateTime FinishServiceDate { get; set; }
        public DateTime ConfirmedDate { get; set; }
        public DateTime CancellationDate { get; set; }
        public List<int> ServicesId { get; set; } = new();
    }
}

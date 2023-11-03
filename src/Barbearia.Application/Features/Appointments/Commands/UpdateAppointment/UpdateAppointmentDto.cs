using Barbearia.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentDto
    {
        public int AppointmentId { get; set; }
        public int ScheduleId { get; set; }
        public int PersonId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int Status { get; set; }
        public DateTime StartServiceDate { get; set; }
        public DateTime FinishServiceDate { get; set; }
        public DateTime ConfirmedDate { get; set; }
        public DateTime CancellationDate { get; set; }
        public List<ServiceDto> Services { get; set; } = new();
    }
}

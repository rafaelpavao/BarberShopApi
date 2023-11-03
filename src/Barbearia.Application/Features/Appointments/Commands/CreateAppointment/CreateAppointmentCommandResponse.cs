using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandResponse : BaseResponse
    {
        public CreateAppointmentDto? Appointment { get; set; }
    }
}

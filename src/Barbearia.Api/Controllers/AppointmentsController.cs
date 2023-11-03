using Barbearia.Application.Features.Appointments.Commands.CreateAppointment;
using Barbearia.Application.Features.Appointments.Commands.DeleteAppointment;
using Barbearia.Application.Features.Appointments.Commands.UpdateAppointment;
using Barbearia.Application.Features.Appointments.Queries.GetAllAppointments;
using Barbearia.Application.Features.Appointments.Queries.GetAppointmentById;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : MainController
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllAppointmentsDto>>> GetAppointments ()
        {
            var getAllAppointmentsQuery = new GetAllAppointmentsQuery {  };

            var appointmentsToReturn = await _mediator.Send(getAllAppointmentsQuery);

            return Ok(appointmentsToReturn);
        }
        [HttpGet("{appointmentId}", Name = "getAppointmentById")]
        public async Task<ActionResult<GetAppointmentByIdDto?>> GetAppointmentById (int appointmentId)
        {
            var getAppointmentByIdQuery = new GetAppointmentByIdQuery { AppointmentId = appointmentId };

            var appointmentsToReturn = await _mediator.Send(getAppointmentByIdQuery);

            if(appointmentsToReturn == null) return NotFound();

            return Ok(appointmentsToReturn);
        }
        [HttpPost]
        public async Task<ActionResult<CreateAppointmentCommandResponse>> CreateAppointment (CreateAppointmentCommand createAppointmentCommand)
        {
            var createAppointmentCommandResponse = await _mediator.Send(createAppointmentCommand);

            if (!createAppointmentCommandResponse.IsSuccess)
                return HandleRequestError(createAppointmentCommandResponse);

            var appointmentForReturn = createAppointmentCommandResponse.Appointment;

            return CreatedAtRoute
            (
                "getAppointmentById",
                new { appointmentId = appointmentForReturn!.AppointmentId },
                appointmentForReturn
            );
        }
        [HttpDelete("{appointmentId}")]
        public async Task<ActionResult> DeleteAppointment(int appointmentId)
        {
            var deleteAppointmentCommand = new DeleteAppointmentCommand { AppointmentId = appointmentId };

            var result = await _mediator.Send(deleteAppointmentCommand);
            
            if(result == false) return NotFound();

            return NoContent();
        }
        [HttpPut("{appointmentId}")]
        public async Task<ActionResult> UpdateAppointment(int appointmentId, UpdateAppointmentCommand updateAppointmentCommand)
        {
            if (updateAppointmentCommand.AppointmentId != appointmentId) return BadRequest();

            var updateAppointmentCommandResponse = await _mediator.Send(updateAppointmentCommand);

            if (!updateAppointmentCommandResponse.IsSuccess) 
                return HandleRequestError(updateAppointmentCommandResponse);

            return NoContent();
        }
    }
}

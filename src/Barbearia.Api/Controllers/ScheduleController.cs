using Barbearia.Application.Features.Schedules.Commands.CreateSchedule;
using Barbearia.Application.Features.Schedules.Commands.DeleteSchedule;
using Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;
using Barbearia.Application.Features.Schedules.Queries.GetAllSchedules;
using Barbearia.Application.Features.Schedules.Queries.GetScheduleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/schedules")]
public class ScheduleController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<ScheduleController> _logger;

    public ScheduleController(IMediator mediator, ILogger<ScheduleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllSchedulesDto>>> GetAllSchedules()
    {
        var getAllSchedulesQuery = new GetAllSchedulesQuery { };

        var schedulesToReturn = await _mediator.Send(getAllSchedulesQuery);

        return Ok(schedulesToReturn);
    }

    [HttpGet("{scheduleId}", Name = "GetScheduleById")]
    public async Task<ActionResult<GetScheduleByIdDto>> GetScheduleById (int scheduleId)
    {
        var getScheduleByIdQuery = new GetScheduleByIdQuery { ScheduleId = scheduleId};

        var scheduleToReturn = await _mediator.Send(getScheduleByIdQuery);

        if(scheduleToReturn == null) return NotFound();

        return Ok(scheduleToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateScheduleCommandResponse>> CreateSchedule(CreateScheduleCommand createScheduleCommand)
    {
        var createScheduleCommandResponse = await _mediator.Send(createScheduleCommand);

        if(!createScheduleCommandResponse.IsSuccess)
            return HandleRequestError(createScheduleCommandResponse);

        var scheduleForReturn = createScheduleCommandResponse.Schedule;

        return CreatedAtRoute
        (
            "GetScheduleById",
            new { scheduleId = scheduleForReturn.ScheduleId},
            scheduleForReturn
        );
    }

    [HttpPut("{scheduleId}")]
    public async Task<ActionResult> UpdateSchedule(int scheduleId, UpdateScheduleCommand updateScheduleCommand)
    {
        if(updateScheduleCommand.ScheduleId != scheduleId) return BadRequest();

        var UpdateScheduleCommandResponse = await _mediator.Send(updateScheduleCommand);

        if(!UpdateScheduleCommandResponse.IsSuccess)
            return HandleRequestError(UpdateScheduleCommandResponse);

        return NoContent();
    }

    [HttpDelete("{scheduleId}")]
    public async Task<ActionResult> DeleteSchedule(int scheduleId)
    {
        var result = await _mediator.Send(new DeleteScheduleCommand { ScheduleId = scheduleId});

        if(!result) return NotFound();

        return NoContent();
    }
}
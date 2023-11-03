
using Barbearia.Application.Features.TimesOff.Commands.CreateTimeOff;
using Barbearia.Application.Features.TimesOff.Commands.DeleteTimeOff;
using Barbearia.Application.Features.TimesOff.Commands.UpdateTimeOff;
using Barbearia.Application.Features.TimesOff.Queries.GetTimeOffById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;


[ApiController]
[Route("api/timeoffs")]
public class TimeOffController : MainController
{
    private readonly IMediator _mediator;

    public TimeOffController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{timeOffId}", Name ="GetTimeOffById")]
    public async Task<ActionResult<GetTimeOffByIdDto>> GetTimeOffById(int timeOffId)
    {
        var getTimeOffByIdQuery = new GetTimeOffByIdQuery { TimeOffId = timeOffId };

        GetTimeOffByIdDto? timeOffToReturn = await _mediator.Send(getTimeOffByIdQuery);

        if (timeOffToReturn == null) return NotFound();

        return Ok(timeOffToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateTimeOffCommandResponse>> CreateSupplier(CreateTimeOffCommand createTimeOffCommand)
    {
        var createTimeOffCommandResponse = await _mediator.Send(createTimeOffCommand);


        if(!createTimeOffCommandResponse.IsSuccess)
            return HandleRequestError(createTimeOffCommandResponse);

        var timeOffForReturn = createTimeOffCommandResponse.TimeOff;


        return CreatedAtRoute
        (
            "GetTimeOffById",
            new { timeOffId = timeOffForReturn.TimeOffId },
            timeOffForReturn
        );
    }


    [HttpPut("{timeoffId}")]
    public async Task<ActionResult> UpdateTimeOff(int timeoffId, UpdateTimeOffCommand updateTimeOffCommand)
    {
        if(updateTimeOffCommand.TimeOffId != timeoffId) return BadRequest();

        var updateTimeOffCommandResponse = await _mediator.Send(updateTimeOffCommand);

        if(!updateTimeOffCommandResponse.IsSuccess)
        return HandleRequestError(updateTimeOffCommandResponse);

        return NoContent();
    }

    [HttpDelete("{timeoffId}")]
    public async Task<ActionResult> DeleteTimeOff(int timeoffId)
    {
        var result = await _mediator.Send(new DeleteTimeOffCommand { TimeOffId = timeoffId});

        if(!result) return NotFound();

        return NoContent();
    }


}
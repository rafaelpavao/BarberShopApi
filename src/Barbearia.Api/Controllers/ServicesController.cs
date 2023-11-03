using Barbearia.Application.Features.Services.Commands.CreateService;
using Barbearia.Application.Features.Services.Commands.DeleteService;
using Barbearia.Application.Features.Services.Commands.UpdateService;
using Barbearia.Application.Features.Services.Queries.GetServiceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/Services")]
public class ServicesController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<ServicesController> _logger;

    public ServicesController(IMediator mediator, ILogger<ServicesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{itemId}", Name = "GetServiceById")]
    public async Task<ActionResult<GetServiceByIdDto>> GetServiceById(int itemId)
    {
        var getServiceById = new GetServiceByIdQuery { ItemId = itemId };
        var ServiceToReturn = await _mediator.Send(getServiceById);

        if (ServiceToReturn == null) return NotFound();

        return Ok(ServiceToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateServiceCommandResponse>> CreateService(CreateServiceCommand createServiceCommand)
    {
        var CreateServiceCommandResponse = await _mediator.Send(createServiceCommand);

        if (!CreateServiceCommandResponse.IsSuccess)
            return HandleRequestError(CreateServiceCommandResponse);

        var ServiceForReturn = CreateServiceCommandResponse.Service;

        return CreatedAtRoute
        (
            "GetServiceById",
            new { itemId = ServiceForReturn.ItemId },
            ServiceForReturn
        );
    }

    [HttpPut("{itemId}")]
    public async Task<ActionResult> UpdateService(int itemId, UpdateServiceCommand updateServiceCommand)
    {
        if (updateServiceCommand.ItemId != itemId) return BadRequest();

        var updateServiceCommandResponse = await _mediator.Send(updateServiceCommand);

        if (!updateServiceCommandResponse.IsSuccess)
            return HandleRequestError(updateServiceCommandResponse);

        return NoContent();
    }

    [HttpDelete("{itemId}")]
    public async Task<ActionResult> DeleteService(int itemId)
    {
        var result = await _mediator.Send(new DeleteServiceCommand { ItemId = itemId });

        if (!result) return NotFound();

        return NoContent();
    }
}
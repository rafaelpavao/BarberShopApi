using Barbearia.Application.Features.Telephones.Commands.DeleteTelephone;
using Barbearia.Application.Features.Telephones.Query.GetTelephone;
using Barbearia.Application.Features.Telephones.Commands.CreateTelephone;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/persons/{personId}/telephones")]
public class TelephoneController : MainController
{
    private readonly IMediator _mediator;

    public TelephoneController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet(Name = "GetTelephone")]
    public async Task<ActionResult<IEnumerable<GetTelephoneQueryResponse>>> GetTelephone(int personId)
    {
        var getTelephoneQuery = new GetTelephoneQuery { PersonId = personId };
        var telephoneResponse = await _mediator.Send(getTelephoneQuery);

        if (!telephoneResponse.IsSuccess){
            return HandleRequestError(telephoneResponse);
        }

        return Ok(telephoneResponse.Telephones);
    }

    [HttpPost]
    public async Task<ActionResult<CreateTelephoneCommandResponse>> CreateTelephone(int personId, CreateTelephoneCommand createTelephoneCommand)
    {
        if (personId != createTelephoneCommand.PersonId) return BadRequest();

        var createTelephoneCommandResponse = await _mediator.Send(createTelephoneCommand);

        if (!createTelephoneCommandResponse.IsSuccess)
        {
            return HandleRequestError(createTelephoneCommandResponse);
        }

        var telephoneToReturn = createTelephoneCommandResponse.Telephone;

        return CreatedAtRoute(
            "GetTelephone",
            new
            {
                personId
            }, telephoneToReturn

        );
    }

    [HttpPut]
    public async Task<ActionResult> UpdateTelephone(int personId, UpdateTelephoneCommand updateTelephoneCommand)
    {
        if (personId != updateTelephoneCommand.PersonId) return BadRequest();

        var updateAddressCommandResponse = await _mediator.Send(updateTelephoneCommand);

        if (!updateAddressCommandResponse.IsSuccess)
        {
            return HandleRequestError(updateAddressCommandResponse);
        }
        return NoContent();

    }

    [HttpDelete("{telephoneId}")]
    public async Task<ActionResult> DeleteTelephone(int personId, int telephoneId)
    {
        var deleteTelephoneCommand = new DeleteTelephoneCommand { PersonId = personId, TelefoneId = telephoneId };
        var telephoneResponse = await _mediator.Send(deleteTelephoneCommand);

        if (!telephoneResponse.IsSuccess){
            return HandleRequestError(telephoneResponse);
        }

        return NoContent();
    }
}
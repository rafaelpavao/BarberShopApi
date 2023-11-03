using Barbearia.Application.Features.Addresses.Commands.CreateAddress;
using Barbearia.Application.Features.Addresses.Commands.DeleteAddress;
using Barbearia.Application.Features.Addresses.Commands.UpdateAddress;
using Barbearia.Application.Features.Addresses.Queries.GetAddress;
using Barbearia.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace Barbearia.Api.Controllers;

[Route("api/persons/{personId}/addresses")]
public class AddressesController : MainController
{
    private readonly IMediator _mediator;
    public AddressesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet(Name = "GetAddress")]
    public async Task<ActionResult<IEnumerable<GetAddressDto>>> GetAddress(int personId)
    {
        var getAddressQuery = new GetAddressQuery { PersonId = personId };
        var addressResponse = await _mediator.Send(getAddressQuery);

        if (!addressResponse.IsSuccess){
            return HandleRequestError(addressResponse);
        }

        return Ok(addressResponse.Addresses);
    }

    [HttpPost]
    public async Task<ActionResult<CreateAddressCommandResponse>> CreateAddress(int personId, [FromBody] CreateAddressCommand createAddressCommand)
    {
        if (personId != createAddressCommand.PersonId) return BadRequest();

        var createAddressCommandResponse = await _mediator.Send(createAddressCommand);

        if (!createAddressCommandResponse.IsSuccess)
            return HandleRequestError(createAddressCommandResponse);

        var addressForReturn = createAddressCommandResponse.Address;

        return CreatedAtRoute
        (
            "GetAddress",
            new
            {
                personId,
                addressId = addressForReturn.AddressId
            },
            addressForReturn
        );
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAddress(int personId, UpdateAddressCommand updateAddressCommand)
    {
        if (updateAddressCommand.PersonId != personId) return BadRequest();

        var updateAddressCommandResponse = await _mediator.Send(updateAddressCommand);

        if (!updateAddressCommandResponse.IsSuccess)
            return HandleRequestError(updateAddressCommandResponse);

        return NoContent();
    }

    [HttpDelete("{addressId}")]
    public async Task<ActionResult> DeleteAddress(int personId, int addressId)
    {
        var result = await _mediator.Send(new DeleteAddressCommand { PersonId = personId, AddressId = addressId });

        if (!result.IsSuccess){
            return HandleRequestError(result);
        }

        return NoContent();
    }
}
using Barbearia.Application.Features.Suppliers.Commands.CreateSupplier;
using Barbearia.Application.Features.Suppliers.Commands.DeleteSupplier;
using Barbearia.Application.Features.Suppliers.Commands.UpdateSupplier;
using Barbearia.Application.Features.Suppliers.Queries.GetSupplierById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/suppliers")]
public class SupplierController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<SupplierController> _logger;

    public SupplierController(IMediator mediator, ILogger<SupplierController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
    }

    [HttpGet("{supplierId}", Name ="GetSupplierById")]
    public async Task<ActionResult<GetSupplierByIdDto>> GetSupplierById(int supplierId)
    {
        var getSupplierByIdQuery = new GetSupplierByIdQuery { PersonId = supplierId };

        GetSupplierByIdDto? supplierToReturn = await _mediator.Send(getSupplierByIdQuery);

        if (supplierToReturn == null) return NotFound();

        return Ok(supplierToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateSupplierCommandResponse>> CreateSupplier(CreateSupplierCommand createSupplierCommand)
    {
        var createSupplierCommandResponse = await _mediator.Send(createSupplierCommand);


        if(!createSupplierCommandResponse.IsSuccess)
            return HandleRequestError(createSupplierCommandResponse);

        var supplierForReturn = createSupplierCommandResponse.Supplier;


        return CreatedAtRoute
        (
            "GetSupplierById",
            new { supplierId = supplierForReturn.PersonId },
            supplierForReturn
        );
    }

    [HttpPut("{supplierId}")]
    public async Task<ActionResult> UpdateSupplier(int supplierId, UpdateSupplierCommand updateSupplierCommand)
    {
        if(updateSupplierCommand.PersonId != supplierId) return BadRequest();

        var updateSupplierCommandResponse = await _mediator.Send(updateSupplierCommand);

        if(!updateSupplierCommandResponse.IsSuccess)
        return HandleRequestError(updateSupplierCommandResponse);

        return NoContent();
    }

    [HttpDelete("{supplierId}")]
    public async Task<ActionResult> DeleteSupplier(int supplierId)
    {
        var result = await _mediator.Send(new DeleteSupplierCommand { PersonId = supplierId});

        if(!result) return NotFound();

        return NoContent();
    }
    
}
using Barbearia.Application.Features.Customers.Commands.CreateCustomer;
using Barbearia.Application.Features.Customers.Queries.GetAllCustomers;
using Barbearia.Application.Features.Customers.Queries.GetCustomerById;
using Barbearia.Application.Features.Customers.Queries.GetCustomerWithOrdersById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Barbearia.Application.Features.Customers.Commands.UpdateCustomer;
using Barbearia.Application.Features.Customers.Commands.DeleteCustomer;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(IMediator mediator, ILogger<CustomersController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
    }



    [HttpGet("{customerId}", Name = "GetCustomerById")]
    public async Task<ActionResult<GetCustomerByIdDto>> GetCustomerById(int customerId)
    {
        var getCustomerByIdQuery = new GetCustomerByIdQuery { PersonId = customerId };

        var customerToReturn = await _mediator.Send(getCustomerByIdQuery);

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllCustomersDto>>> GetCustomers()
    {
        var getAllCustomersQuery = new GetAllCustomersQuery { }; 

        var customersToReturn = await _mediator.Send(getAllCustomersQuery); 

        return Ok(customersToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateCustomerCommandResponse>> CreateCustomer(CreateCustomerCommand createCustomerCommand)
    {
        var createCustomerCommandResponse = await _mediator.Send(createCustomerCommand);


        if(!createCustomerCommandResponse.IsSuccess)
            return HandleRequestError(createCustomerCommandResponse);

        var customerForReturn = createCustomerCommandResponse.Customer;


        return CreatedAtRoute
        (
            "GetCustomerById",
            new { customerId = customerForReturn.PersonId },
            customerForReturn
        );
    }

    [HttpPut("{customerId}")]
    public async Task<ActionResult> UpdateCustomer(int customerId, UpdateCustomerCommand updateCustomerCommand)
    {
        if(updateCustomerCommand.PersonId != customerId) return BadRequest();

        var updateCustomerCommandResponse = await _mediator.Send(updateCustomerCommand);

        if(!updateCustomerCommandResponse.IsSuccess)
        return HandleRequestError(updateCustomerCommandResponse);

        return NoContent();
    }

    [HttpDelete("{customerId}")]
    public async Task<ActionResult> DeleteCustomer(int customerId)
    {
        var result = await _mediator.Send(new DeleteCustomerCommand { PersonId = customerId});

        if(!result) return NotFound();

        return NoContent();
    }
    
    [HttpGet("with-orders/{customerId}")]
    public async Task<ActionResult<GetCustomerWithOrdersByIdDto>> GetCustomerWithOrdersById(int customerId)
    {
        var getCustomerWithOrdersByIdQuery = new GetCustomerWithOrdersByIdQuery {PersonId = customerId};

        var customerToReturn = await _mediator.Send(getCustomerWithOrdersByIdQuery);

        if(customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }
}
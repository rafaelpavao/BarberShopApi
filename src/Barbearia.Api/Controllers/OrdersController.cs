using Barbearia.Application.Features.Orders.Commands.CreateOrder;
using Barbearia.Application.Features.Orders.Commands.DeleteOrder;
using Barbearia.Application.Features.Orders.Commands.UpdateOrder;
using Barbearia.Application.Features.Orders.Queries.GetAllOrders;
using Barbearia.Application.Features.Orders.Queries.GetOrderById;
using Barbearia.Application.Features.Orders.Queries.GetOrderByNumber;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Barbearia.Api.Controllers;

[Route("api/orders")]
public class OrdersController : MainController
{
    private readonly IMediator _mediator;
    public OrdersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllOrdersDto>>> GetOrders()
    {
        var getAllOrdersQuery = new GetAllOrdersQuery { };

        var ordersToReturn = await _mediator.Send(getAllOrdersQuery);

        return Ok(ordersToReturn);
    }

    [HttpGet("{orderId}", Name = "GetOrderById")]
    public async Task<ActionResult<GetOrderByIdDto>> GetOrderById(int orderId)
    {
        var getOrderByIdQuery = new GetOrderByIdQuery { OrderId = orderId};

        var orderToReturn = await _mediator.Send(getOrderByIdQuery);

        if(orderToReturn == null) return NotFound();

        return Ok(orderToReturn);
    }

    [HttpGet("number/{number}", Name = "GetOrderByNumber")]
    public async Task<ActionResult<GetOrderByNumberDto>> GetOrderByNumber(int number)
    {
        var getOrderByNumberQuery = new GetOrderByNumberQuery { Number = number};

        var orderToReturn = await _mediator.Send(getOrderByNumberQuery);

        if(orderToReturn == null) return NotFound();

        return Ok(orderToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateOrderCommandResponse>> CreateOrder (CreateOrderCommand createOrderCommand)
    {
        var createOrderCommandResponse = await _mediator.Send(createOrderCommand);

        if(!createOrderCommandResponse.IsSuccess)
            return HandleRequestError(createOrderCommandResponse);

        

        var orderForReturn = createOrderCommandResponse.Order;

        return CreatedAtRoute
        (
            "GetOrderById",
            new { orderId = orderForReturn.OrderId},
            orderForReturn
        );
    }

    [HttpPut("{orderId}")]
    public async Task<ActionResult> UpdateOrder(int orderId, UpdateOrderCommand updateOrderCommand)
    {
        if (updateOrderCommand.OrderId != orderId) return BadRequest();

        var updateOrderCommandResponse = await _mediator.Send(updateOrderCommand);

        if (!updateOrderCommandResponse.IsSuccess)
            return HandleRequestError(updateOrderCommandResponse);
        
        return NoContent();
    }

    [HttpDelete("{orderId}")]
    public async Task<ActionResult> DeleteOrder(int orderId)
    {
        var result = await _mediator.Send(new DeleteOrderCommand { OrderId = orderId });

        if (!result) return NotFound();

        return NoContent();
    }
}
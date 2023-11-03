using System.Text.Json;
using Barbearia.Application.Features.OrdersCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/ordersCollection")]
public class OrdersCollectionController : MainController
{
    private readonly IMediator _mediator;

    public OrdersCollectionController(IMediator mediator){
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetOrdersCollectionQueryResponse>>> GetOrdersCollection(string? searchQuery = "", int pageNumber = 1, int pageSize = 5)
    {
        var getOrdersCollectionQuery = 
        new GetOrdersCollectionQuery
        {
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var requestResponse = await _mediator.Send(getOrdersCollectionQuery);

        if(!requestResponse.IsSuccess) 
            return HandleRequestError(requestResponse);

        var ordersToReturn = requestResponse.Orders;
        var paginationMetadata = requestResponse.PaginationMetadata;

        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(paginationMetadata));
        return Ok(ordersToReturn);
    }
}
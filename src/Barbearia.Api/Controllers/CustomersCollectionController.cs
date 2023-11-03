
using System.Text.Json;
using Barbearia.Application.Features.CustomersCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/customersCollection")]
public class CustomersCollectionController:MainController
{
    private readonly IMediator _mediator;

    public CustomersCollectionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult <IEnumerable <GetCustomersCollectionQueryResponse>>> GetCustomersCollection(
    string? searchQuery = "", int pageNumber = 1, int pageSize = 5)
    {
        var getCustomersCollectionQuery =
        new GetCustomersCollectionQuery
        {
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var requestResponse = await _mediator.Send(getCustomersCollectionQuery);
        
        if(!requestResponse.IsSuccess) 
            return HandleRequestError(requestResponse);

        var customersToReturn = requestResponse.Customers;
        var paginationMetadata = requestResponse.PaginationMetadata;
        
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(paginationMetadata));
        return Ok(customersToReturn);
    }

}

using System.Text.Json;
using Barbearia.Application.Features.SchedulesCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/SchedulesCollection")]
public class SchedulesCollectionController:MainController
{
    private readonly IMediator _mediator;

    public SchedulesCollectionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult <IEnumerable <GetSchedulesCollectionQueryResponse>>> GetSchedulesCollection(
    string? searchQuery = "", int pageNumber = 1, int pageSize = 5)
    {
        var getSchedulesCollectionQuery =
        new GetSchedulesCollectionQuery
        {
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var requestResponse = await _mediator.Send(getSchedulesCollectionQuery);
        
        if(!requestResponse.IsSuccess) 
            return HandleRequestError(requestResponse);

        var SchedulesToReturn = requestResponse.Schedules;
        var paginationMetadata = requestResponse.PaginationMetadata;
        
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(paginationMetadata));
        return Ok(SchedulesToReturn);
    }

}
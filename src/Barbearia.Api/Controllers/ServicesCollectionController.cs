using System.Text.Json;
using Barbearia.Application.Features.ServicesCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/ServicesCollection")]
public class ServicesCollectionController : MainController
{
    private readonly IMediator _mediator;

    public ServicesCollectionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetServicesCollectionQueryResponse>>> GetServicesCollection(string? searchQuery = "", int pageNumber = 1, int pageSize = 5)
    {
        var getServicesCollectionQuery =
        new GetServicesCollectionQuery
        {
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var requestResponse = await _mediator.Send(getServicesCollectionQuery);

        if (!requestResponse.IsSuccess)
            return HandleRequestError(requestResponse);

        var servicesToReturn = requestResponse.Services;
        var paginationMetadata = requestResponse.PaginationMetadata;

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
        return Ok(servicesToReturn);
    }
}
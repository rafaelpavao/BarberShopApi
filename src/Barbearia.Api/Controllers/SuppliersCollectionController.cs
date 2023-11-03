
using System.Text.Json;
using Barbearia.Application.Features.SuppliersCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/suppliersCollection")]
public class SuppliersCollectionController : MainController
{
    private readonly IMediator _mediator;

    public SuppliersCollectionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetSuppliersCollectionQueryResponse>>> GetSuppliersCollection(
    string? searchQuery = "", int pageNumber = 1, int pageSize = 5)
    {
        var getSuppliersCollectionQuery =
        new GetSuppliersCollectionQuery
        {
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var requestResponse = await _mediator.Send(getSuppliersCollectionQuery);

        if (!requestResponse.IsSuccess)
            return HandleRequestError(requestResponse);

        var SuppliersToReturn = requestResponse.Suppliers;
        var paginationMetadata = requestResponse.PaginationMetadata;

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
        return Ok(SuppliersToReturn);
    }

}
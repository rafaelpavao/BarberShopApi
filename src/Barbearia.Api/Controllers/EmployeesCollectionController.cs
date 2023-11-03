using System.Text.Json;
using Barbearia.Application.Features.EmployeesCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/employeesCollection")]
public class EmployeesCollectionController : MainController
{
    private readonly IMediator _mediator;

    public EmployeesCollectionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetEmployeesCollectionQueryResponse>>> GetEmployeesCollection(
    string? searchQuery = "", int pageNumber = 1, int pageSize = 5)
    {
        var getEmployeesCollectionQuery =
        new GetEmployeesCollectionQuery
        {
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var requestResponse = await _mediator.Send(getEmployeesCollectionQuery);

        if (!requestResponse.IsSuccess)
            return HandleRequestError(requestResponse);

        var employeesToReturn = requestResponse.Employees;
        var paginationMetadata = requestResponse.PaginationMetadata;

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
        return Ok(employeesToReturn);
    }

}
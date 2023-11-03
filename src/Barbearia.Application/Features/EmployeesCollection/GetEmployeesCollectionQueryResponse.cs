using Barbearia.Application.Models;
using MediatR;


namespace Barbearia.Application.Features.EmployeesCollection;

public class GetEmployeesCollectionQueryResponse : BaseResponse
{
    public IEnumerable<GetEmployeesCollectionDto> Employees{ get; set; } = default!;
    public PaginationMetadata? PaginationMetadata { get; set; }
    public GetEmployeesCollectionQueryResponse()
    {
        Errors = new Dictionary<string, string[]>();
    }
}
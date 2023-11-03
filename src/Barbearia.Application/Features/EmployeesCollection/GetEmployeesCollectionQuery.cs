
using MediatR;

namespace Barbearia.Application.Features.EmployeesCollection;

public class GetEmployeesCollectionQuery : IRequest<GetEmployeesCollectionQueryResponse>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
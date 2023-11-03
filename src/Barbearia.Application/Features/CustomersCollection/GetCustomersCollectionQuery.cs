
using MediatR;

namespace Barbearia.Application.Features.CustomersCollection;

public class GetCustomersCollectionQuery : IRequest<GetCustomersCollectionQueryResponse>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
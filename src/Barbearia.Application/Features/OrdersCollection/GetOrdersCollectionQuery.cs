
using MediatR;

namespace Barbearia.Application.Features.OrdersCollection;

public class GetOrdersCollectionQuery : IRequest<GetOrdersCollectionQueryResponse>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
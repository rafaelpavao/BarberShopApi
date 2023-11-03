using MediatR;

namespace Barbearia.Application.Features.ProductsCollection;

public class GetProductsCollectionQuery : IRequest<GetProductsCollectionQueryResponse>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
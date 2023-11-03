namespace Barbearia.Application.Features.SuppliersCollection;

using MediatR;

public class GetSuppliersCollectionQuery : IRequest<GetSuppliersCollectionQueryResponse>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
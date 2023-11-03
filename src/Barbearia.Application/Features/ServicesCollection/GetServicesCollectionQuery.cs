using MediatR;

namespace Barbearia.Application.Features.ServicesCollection;

public class GetServicesCollectionQuery : IRequest<GetServicesCollectionQueryResponse>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
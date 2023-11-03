using MediatR;

namespace Barbearia.Application.Features.SchedulesCollection;

public class GetSchedulesCollectionQuery : IRequest<GetSchedulesCollectionQueryResponse>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
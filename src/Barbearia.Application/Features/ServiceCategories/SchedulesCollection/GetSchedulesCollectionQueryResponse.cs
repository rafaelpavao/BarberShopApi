using Barbearia.Application.Models;
using MediatR;


namespace Barbearia.Application.Features.SchedulesCollection;

public class GetSchedulesCollectionQueryResponse : BaseResponse
{
    public IEnumerable<GetSchedulesCollectionDto> Schedules{ get; set; } = default!;
    public PaginationMetadata? PaginationMetadata { get; set; }
    public GetSchedulesCollectionQueryResponse()
    {
        Errors = new Dictionary<string, string[]>();
    }
}
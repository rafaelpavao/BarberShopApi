using MediatR;

namespace Barbearia.Application.Features.Schedules.Queries.GetScheduleById;

public class GetScheduleByIdQuery : IRequest<GetScheduleByIdDto>
{
    public int ScheduleId {get;set;}
}
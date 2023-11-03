using MediatR;

namespace Barbearia.Application.Features.Schedules.Queries.GetAllSchedules;

public class GetAllSchedulesQuery : IRequest<IEnumerable<GetAllSchedulesDto>>
{

}
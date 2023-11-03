using MediatR;

namespace Barbearia.Application.Features.WorkingDays.Query.GetWorkingDay;

public class GetWorkingDayQuery : IRequest<GetWorkingDayQueryResponse>
{
    public int PersonId {get; set;}
}
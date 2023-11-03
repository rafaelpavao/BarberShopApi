namespace Barbearia.Application.Features.WorkingDays.Query.GetWorkingDay;

public class GetWorkingDayQueryResponse : BaseResponse
{
    public IEnumerable<GetWorkingDayDto>? WorkingDay {get;set;}
}
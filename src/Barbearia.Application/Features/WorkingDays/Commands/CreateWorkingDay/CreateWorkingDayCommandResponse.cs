namespace Barbearia.Application.Features.WorkingDays.Commands.CreateWorkingDay;

public class CreateWorkingDayCommandResponse : BaseResponse
{
    public CreateWorkingDayDto WorkingDay {get;set;} = default!;
}
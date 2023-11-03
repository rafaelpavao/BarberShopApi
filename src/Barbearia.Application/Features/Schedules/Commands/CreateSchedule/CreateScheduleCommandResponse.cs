namespace Barbearia.Application.Features.Schedules.Commands.CreateSchedule;

public class CreateScheduleCommandResponse : BaseResponse
{
    public CreateScheduleDto Schedule {get; set;} = default!;    
}
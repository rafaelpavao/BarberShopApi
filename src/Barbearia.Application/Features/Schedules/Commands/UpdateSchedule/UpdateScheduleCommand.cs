using MediatR;

namespace Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;

public class UpdateScheduleCommand : IRequest<UpdateScheduleCommandResponse>
{
    public int ScheduleId {get;set;}
    public int WorkingDayId {get;set;}
    public int Status {get;set;}
}
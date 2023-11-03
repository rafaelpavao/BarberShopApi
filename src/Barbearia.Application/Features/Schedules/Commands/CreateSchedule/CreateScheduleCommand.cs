using MediatR;

namespace Barbearia.Application.Features.Schedules.Commands.CreateSchedule;

public class CreateScheduleCommand : IRequest<CreateScheduleCommandResponse>
{
    public int WorkingDayId {get;set;}
    public int Status {get;set;}
}
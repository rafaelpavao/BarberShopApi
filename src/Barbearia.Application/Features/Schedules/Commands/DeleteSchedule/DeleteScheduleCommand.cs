using MediatR;

namespace Barbearia.Application.Features.Schedules.Commands.DeleteSchedule;

public class DeleteScheduleCommand : IRequest<bool>
{
    public int ScheduleId{get;set;}
}
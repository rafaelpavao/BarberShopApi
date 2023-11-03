using Barbearia.Application.Models;

namespace Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;

public class UpdateScheduleDto
{
    public int ScheduleId{get;set;}
    public int WorkingDayId{get;set;}
    public int Status{get;set;}
}
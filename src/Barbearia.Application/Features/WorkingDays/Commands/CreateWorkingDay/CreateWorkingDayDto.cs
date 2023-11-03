using Barbearia.Application.Models;

namespace Barbearia.Application.Features.WorkingDays.Commands.CreateWorkingDay;

public class CreateWorkingDayDto
{
    public int WorkingDayId{get;set;}
    public int PersonId{get; set;}
    public DateOnly WorkDate{get; set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
}
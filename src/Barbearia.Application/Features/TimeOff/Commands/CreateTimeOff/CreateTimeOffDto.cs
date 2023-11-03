

namespace Barbearia.Application.Features.TimesOff.Commands.CreateTimeOff;

public class CreateTimeOffDto
{
    public int TimeOffId{get; set;}
    public int WorkingDayId{get;set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
}
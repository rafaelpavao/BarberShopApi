using Barbearia.Application.Models;

namespace Barbearia.Application.Features.TimesOff.Commands.UpdateTimeOff;

public class UpdateTimeOffDto
{
    public int TimeOffId{get; set;}
    public int WorkingDayId{get;set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
}
namespace Barbearia.Application.Models;

public class TimeOffForWorkingDayDto
{
    public int TimeOffId{get; set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
}
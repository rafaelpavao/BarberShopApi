using Barbearia.Domain.Entities;

namespace Barbearia.Application.Models;

public class WorkingDayDto
{
    public int WorkingDayId{get; set;}
    public int PersonId{get; set;}
    public PersonDto? Employee{get; set;}
    public DateOnly WorkDate{get; set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
}
    
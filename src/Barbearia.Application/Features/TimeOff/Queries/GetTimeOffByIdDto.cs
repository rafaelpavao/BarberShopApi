using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.TimesOff.Queries.GetTimeOffById;

public class GetTimeOffByIdDto
{
    public int TimeOffId{get; set;}
    public int WorkingDayId{get;set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
}
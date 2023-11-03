using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Schedules.Commands.CreateSchedule;

public class CreateScheduleDto
{
    public int ScheduleId{get;set;}
    public int WorkingDayId{get;set;}
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeStatus Status { get; set; }
}
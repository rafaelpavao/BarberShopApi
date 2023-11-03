using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Schedules.Queries.GetAllSchedules;

public class GetAllSchedulesDto
{
    public int ScheduleId{get;set;}    
    public WorkingDayDto? WorkingDay{get;set;}
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeStatus Status { get; set; }
}
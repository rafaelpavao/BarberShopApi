using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.SchedulesCollection;

public class GetSchedulesCollectionDto
{
    public int ScheduleId { get; set; }
    public WorkingDayDto? WorkingDay { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeStatus Status { get; set; }
}
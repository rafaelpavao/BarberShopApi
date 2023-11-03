using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.EmployeesCollection;

public class GetEmployeesCollectionDto
{
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;    
    public List<TelephoneDto> Telephones { get; set; } = new List<TelephoneDto>();

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeStatus Status { get; set; }

}
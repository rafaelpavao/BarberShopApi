using System.Text.Json.Serialization;
using Barbearia.Domain.Entities;
using static Barbearia.Domain.Entities.Telephone;

namespace Barbearia.Application.Models;

public class TelephoneDto
{
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TelephoneType Type { get; set; }
}
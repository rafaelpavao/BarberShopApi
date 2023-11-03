using System.Text.Json.Serialization;
using static Barbearia.Domain.Entities.Telephone;

namespace Barbearia.Application.Features.Telephones.Query.GetTelephone;

public class GetTelephoneDto
{
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;
       
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TelephoneType Type { get; set; }   
}
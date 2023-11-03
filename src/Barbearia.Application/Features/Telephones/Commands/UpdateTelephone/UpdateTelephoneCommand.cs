using System.Text.Json.Serialization;
using MediatR;
using static Barbearia.Domain.Entities.Telephone;

namespace Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;

public class UpdateTelephoneCommand : IRequest<UpdateTelephoneCommandResponse>
{
    public int PersonId { get; set; }
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TelephoneType Type { get; set; }
}
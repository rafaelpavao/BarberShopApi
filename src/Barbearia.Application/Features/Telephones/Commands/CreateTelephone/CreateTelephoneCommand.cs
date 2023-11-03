using System.Text.Json.Serialization;
using MediatR;
using static Barbearia.Domain.Entities.Telephone;

namespace Barbearia.Application.Features.Telephones.Commands.CreateTelephone;

public class CreateTelephoneCommand : IRequest<CreateTelephoneCommandResponse>
{
    public int PersonId { get; set; }
    public string Number { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TelephoneType Type { get; set; }
}
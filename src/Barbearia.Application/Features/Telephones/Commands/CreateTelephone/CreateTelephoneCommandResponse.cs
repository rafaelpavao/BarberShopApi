namespace Barbearia.Application.Features.Telephones.Commands.CreateTelephone;

public class CreateTelephoneCommandResponse : BaseResponse
{
    public CreateTelephoneDto Telephone {get;set;} = default!;
}
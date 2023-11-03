using MediatR;

namespace Barbearia.Application.Features.Telephones.Commands.DeleteTelephone;

public class DeleteTelephoneCommand : IRequest<DeleteTelephoneCommandResponse>{
    public int PersonId {get;set;}
    public int TelefoneId {get;set;}
}
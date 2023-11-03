using MediatR;

namespace Barbearia.Application.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommand : IRequest<DeleteAddressCommandResponse>
{
    public int PersonId {get;set;}
    public int AddressId {get;set;}
}
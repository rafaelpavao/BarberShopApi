using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, DeleteAddressCommandResponse>
{
    private readonly IPersonRepository _personRepository;

    public DeleteAddressCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<DeleteAddressCommandResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        DeleteAddressCommandResponse response = new();

        var personFromDatabase = await _personRepository.GetPersonByIdAsync(request.PersonId);
        if (personFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Person Not found in database" });
            return response;
        }

        var addressFromDatabase = personFromDatabase.Addresses.FirstOrDefault(a => a.AddressId == request.AddressId);
        if (addressFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("Addresses", new[] { "Addresses Not found in database" });
            return response;
        }

        _personRepository.DeleteAddress(personFromDatabase, addressFromDatabase);

        await _personRepository.SaveChangesAsync();
        
        return response;
    }
}
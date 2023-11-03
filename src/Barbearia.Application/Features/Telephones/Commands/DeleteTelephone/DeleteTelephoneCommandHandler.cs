using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Telephones.Commands.DeleteTelephone;

public class DeleteTelephoneCommandHandler : IRequestHandler<DeleteTelephoneCommand, DeleteTelephoneCommandResponse>
{
    private readonly IPersonRepository _personRepository;

    public DeleteTelephoneCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    public async Task<DeleteTelephoneCommandResponse> Handle(DeleteTelephoneCommand request, CancellationToken cancellationToken)
    {
        DeleteTelephoneCommandResponse response = new();
        
        var personFromDatabase = await _personRepository.GetPersonByIdAsync(request.PersonId);
        if (personFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Person Not found in database" });
            return response;
        }

        var telephoneFromDatabase = personFromDatabase.Telephones.FirstOrDefault(t => t.TelephoneId == request.TelefoneId);
        if (telephoneFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("Telephone", new[] { "Telephone Not found in database" });
            return response;
        }

        _personRepository.DeleteTelephone(personFromDatabase, telephoneFromDatabase);

        await _personRepository.SaveChangesAsync();

        return response;
    }
}
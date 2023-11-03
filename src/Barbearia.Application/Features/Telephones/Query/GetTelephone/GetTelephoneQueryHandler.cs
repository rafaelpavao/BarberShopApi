using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Telephones.Query.GetTelephone;

public class GetTelephoneQueryHandler : IRequestHandler<GetTelephoneQuery, GetTelephoneQueryResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetTelephoneQueryHandler(IPersonRepository personRepository, IMapper mapper){
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetTelephoneQueryResponse> Handle(GetTelephoneQuery request, CancellationToken cancellationToken)
    {
        GetTelephoneQueryResponse response = new();

        var personFromDatabase = await _personRepository.GetPersonByIdAsync(request.PersonId);
        if (personFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Person Not found in database" });
            return response;
        }

        var telephoneFromDatabase = personFromDatabase.Telephones;

        response.Telephones = _mapper.Map<IEnumerable<GetTelephoneDto>>(telephoneFromDatabase);

        return response;
    }
}
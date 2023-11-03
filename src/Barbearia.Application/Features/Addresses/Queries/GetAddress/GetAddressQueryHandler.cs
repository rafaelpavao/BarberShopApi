using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Addresses.Queries.GetAddress;

public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, GetAddressQueryResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetAddressQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    public async Task<GetAddressQueryResponse> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        GetAddressQueryResponse response = new();
        
        var personFromDatabase = await _personRepository.GetPersonByIdAsync(request.PersonId);
        if (personFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Person Not found in database" });
            return response;
        }

        var addressFromDatabase = personFromDatabase.Addresses;
        response.Addresses = _mapper.Map<IEnumerable<GetAddressDto>>(addressFromDatabase);

        return response;
    }
}
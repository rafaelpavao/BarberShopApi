using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    public async Task<GetCustomerByIdDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _personRepository.GetCustomerByIdAsync(request.PersonId);
        return _mapper.Map<GetCustomerByIdDto>(customerFromDatabase);
    }
}
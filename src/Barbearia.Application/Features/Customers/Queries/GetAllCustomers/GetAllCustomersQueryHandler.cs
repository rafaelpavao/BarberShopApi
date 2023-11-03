using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<GetAllCustomersDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllCustomersDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _personRepository.GetAllCustomersAsync();

        return _mapper.Map<IEnumerable<GetAllCustomersDto>>(customerFromDatabase);
    }
}
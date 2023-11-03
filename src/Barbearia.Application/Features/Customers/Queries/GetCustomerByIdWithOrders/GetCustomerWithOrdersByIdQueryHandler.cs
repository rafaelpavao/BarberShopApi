using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Customers.Queries.GetCustomerWithOrdersById;

public class GetCustomerWithOrdersByIdQueryHandler : IRequestHandler<GetCustomerWithOrdersByIdQuery, GetCustomerWithOrdersByIdDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetCustomerWithOrdersByIdQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerWithOrdersByIdDto> Handle(GetCustomerWithOrdersByIdQuery request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _personRepository.GetCustomerWithOrdersByIdAsync(request.PersonId);
        return _mapper.Map<GetCustomerWithOrdersByIdDto>(customerFromDatabase);
    }
}

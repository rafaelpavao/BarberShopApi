using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    public async Task<GetEmployeeByIdDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);
        return _mapper.Map<GetEmployeeByIdDto>(employeeFromDatabase);
    }
}
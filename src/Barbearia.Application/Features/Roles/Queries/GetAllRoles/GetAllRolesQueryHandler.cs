using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Roles.Queries.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<GetAllRolesDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetAllRolesQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllRolesDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var rolesFromDatabase = await _personRepository.GetAllRoles();

        return _mapper.Map<IEnumerable<GetAllRolesDto>>(rolesFromDatabase);
    }
}
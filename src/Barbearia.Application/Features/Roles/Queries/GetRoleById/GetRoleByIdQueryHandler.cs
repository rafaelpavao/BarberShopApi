using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, GetRoleByIdDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetRoleByIdQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetRoleByIdDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var roleFromDatabase = await _personRepository.GetRoleByIdAsync(request.RoleId);

        return _mapper.Map<GetRoleByIdDto>(roleFromDatabase);
    }
}
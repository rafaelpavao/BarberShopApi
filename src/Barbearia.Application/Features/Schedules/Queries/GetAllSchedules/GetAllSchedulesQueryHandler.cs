using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Schedules.Queries.GetAllSchedules;

public class GetAllSchedulesQueryHandler : IRequestHandler<GetAllSchedulesQuery, IEnumerable<GetAllSchedulesDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetAllSchedulesQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllSchedulesDto>> Handle(GetAllSchedulesQuery request, CancellationToken cancellationToken)
    {
        var SchedulesFromDatabase = await _personRepository.GetAllSchedulesAsync();

        return _mapper.Map<IEnumerable<GetAllSchedulesDto>>(SchedulesFromDatabase);
    }
}

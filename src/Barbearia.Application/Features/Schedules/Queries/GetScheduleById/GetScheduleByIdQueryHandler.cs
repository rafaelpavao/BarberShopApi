using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Schedules.Queries.GetScheduleById;

public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, GetScheduleByIdDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetScheduleByIdQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetScheduleByIdDto> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var ScheduleFromDatabase = await _personRepository.GetScheduleByIdAsync(request.ScheduleId);

        return _mapper.Map<GetScheduleByIdDto>(ScheduleFromDatabase);
    }
}

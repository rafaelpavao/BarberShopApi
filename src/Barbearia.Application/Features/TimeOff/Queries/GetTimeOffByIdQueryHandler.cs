using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.TimesOff.Queries.GetTimeOffById;

public class GetTimeOffByIdQueryHandler : IRequestHandler<GetTimeOffByIdQuery, GetTimeOffByIdDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetTimeOffByIdQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    public async Task<GetTimeOffByIdDto> Handle(GetTimeOffByIdQuery request, CancellationToken cancellationToken)
    {
        var timeOffFromDatabase = await _personRepository.GetTimeOffByIdAsync(request.TimeOffId);

        return _mapper.Map<GetTimeOffByIdDto>(timeOffFromDatabase);
    }
}
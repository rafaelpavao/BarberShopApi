using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.SchedulesCollection;

public class GetSchedulesCollectionQueryHandler : IRequestHandler<GetSchedulesCollectionQuery,GetSchedulesCollectionQueryResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetSchedulesCollectionQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetSchedulesCollectionQueryResponse> Handle(GetSchedulesCollectionQuery request, CancellationToken cancellationToken)
    {

        GetSchedulesCollectionQueryResponse response = new();

        var (scheduleToReturn, paginationMetadata) = await _personRepository.GetAllSchedulesAsync(request.SearchQuery, request.PageNumber,request.PageSize);

        response.Schedules = _mapper.Map<IEnumerable<GetSchedulesCollectionDto>>(scheduleToReturn);
        response.PaginationMetadata = paginationMetadata;

        return response;
    }
}
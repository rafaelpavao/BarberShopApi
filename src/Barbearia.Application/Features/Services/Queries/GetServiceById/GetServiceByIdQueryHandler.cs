using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Services.Queries.GetServiceById;

public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, GetServiceByIdDto>
{
    private readonly IItemRepository _ItemRepository;
    private readonly IMapper _mapper;

    public GetServiceByIdQueryHandler(IItemRepository ItemRepository, IMapper mapper)
    {
        _ItemRepository = ItemRepository;
        _mapper = mapper;
    }

    public async Task<GetServiceByIdDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var serviceFromDatabase = await _ItemRepository.GetServiceByIdAsync(request.ItemId);

        return _mapper.Map<GetServiceByIdDto>(serviceFromDatabase);
    }
}
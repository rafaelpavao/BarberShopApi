using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.ServicesCollection;

public class GetServicesCollectionQueryHandler : IRequestHandler<GetServicesCollectionQuery, GetServicesCollectionQueryResponse>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public GetServicesCollectionQueryHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<GetServicesCollectionQueryResponse> Handle(GetServicesCollectionQuery request, CancellationToken cancellationToken)
    {
        GetServicesCollectionQueryResponse response = new();

        var (serviceToReturn, paginationMetadata) = await _itemRepository.GetAllServicesAsync(request.SearchQuery, request.PageNumber, request.PageSize);
        response.Services = _mapper.Map<IEnumerable<GetServicesCollectionDto>>(serviceToReturn);
        response.PaginationMetadata = paginationMetadata;

        return response;
    }
}
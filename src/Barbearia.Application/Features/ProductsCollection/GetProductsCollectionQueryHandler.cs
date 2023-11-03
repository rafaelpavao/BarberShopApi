using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.ProductsCollection;

public class GetProductsCollectionQueryHandler : IRequestHandler<GetProductsCollectionQuery, GetProductsCollectionQueryResponse>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public GetProductsCollectionQueryHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<GetProductsCollectionQueryResponse> Handle(GetProductsCollectionQuery request, CancellationToken cancellationToken)
    {
        GetProductsCollectionQueryResponse response = new();

        var (productToReturn, paginationMetadata) = await _itemRepository.GetAllProductsAsync(request.SearchQuery, request.PageNumber, request.PageSize);
        response.Products = _mapper.Map<IEnumerable<GetProductsCollectionDto>>(productToReturn);
        response.PaginationMetadata = paginationMetadata;

        return response;
    }
}
using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsDto>>
{
    private readonly IItemRepository _ItemRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _ItemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllProductsDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productsFromDatabase = await _ItemRepository.GetAllProductsAsync();

        return _mapper.Map<IEnumerable<GetAllProductsDto>>(productsFromDatabase);
    }
}

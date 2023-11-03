using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.ProductCategories.Queries.GetAllProductCategories;

public class GetAllProductCategoriesQueryHandler : IRequestHandler<GetAllProductCategoriesQuery, IEnumerable<GetAllProductCategoriesDto>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public GetAllProductCategoriesQueryHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetAllProductCategoriesDto>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var productCategoriesFromDatabase = await _itemRepository.GetAllProductCategoriesAsync();
        return _mapper.Map<IEnumerable<GetAllProductCategoriesDto>>(productCategoriesFromDatabase);
    }
}
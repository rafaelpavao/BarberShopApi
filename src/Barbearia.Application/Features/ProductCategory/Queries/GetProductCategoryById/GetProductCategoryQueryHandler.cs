using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.ProductCategories.Queries.GetProductCategoryById;

public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, GetProductCategoryByIdDto>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public GetProductCategoryByIdQueryHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<GetProductCategoryByIdDto> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var productCategoryFromDatabase = await _itemRepository.GetProductCategoryByIdAsync(request.ProductCategoryId);
        return _mapper.Map<GetProductCategoryByIdDto>(productCategoryFromDatabase);
    }
}

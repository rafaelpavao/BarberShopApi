using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.ServiceCategories.Queries.GetServiceCategoryById;

public class GetServiceCategoryByIdQueryHandler : IRequestHandler<GetServiceCategoryByIdQuery, GetServiceCategoryByIdDto>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public GetServiceCategoryByIdQueryHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<GetServiceCategoryByIdDto> Handle(GetServiceCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var serviceCategoryFromDatabase = await _itemRepository.GetServiceCategoryByIdAsync(request.ServiceCategoryId);

        return _mapper.Map<GetServiceCategoryByIdDto>(serviceCategoryFromDatabase);
    }
}
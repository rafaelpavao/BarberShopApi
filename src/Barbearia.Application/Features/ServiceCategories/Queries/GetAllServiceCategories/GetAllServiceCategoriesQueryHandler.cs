using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.ServiceCategories.Queries.GetAllServiceCategories;

public class GetAllServiceCategoriesQueryHandler : IRequestHandler<GetAllServiceCategoriesQuery, IEnumerable<GetAllServiceCategoriesDto>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public GetAllServiceCategoriesQueryHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllServiceCategoriesDto>> Handle(GetAllServiceCategoriesQuery request, CancellationToken cancellationToken)
    {
        var serviceCategoriesFromDatabase = await _itemRepository.GetAllServiceCategory();

        return _mapper.Map<IEnumerable<GetAllServiceCategoriesDto>>(serviceCategoriesFromDatabase);
    }
}
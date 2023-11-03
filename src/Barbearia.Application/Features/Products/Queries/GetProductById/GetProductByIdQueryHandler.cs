using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdDto>
{
    private readonly IItemRepository _ItemRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IItemRepository ItemRepository, IMapper mapper)
    {
        _ItemRepository = ItemRepository;
        _mapper = mapper;
    }

    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var ProductFromDatabase = await _ItemRepository.GetProductByIdAsync(request.ItemId);

        return _mapper.Map<GetProductByIdDto>(ProductFromDatabase);
    }
}
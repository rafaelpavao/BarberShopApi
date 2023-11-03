using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;

namespace Barbearia.Application.Features.StockHistories.Queries.GetStockHistoryById;

public class GetStockHistoryByIdQueryHandler : IRequestHandler<GetStockHistoryByIdQuery, GetStockHistoryByIdDto>
{
    private readonly IItemRepository _ItemRepository;
    private readonly IMapper _mapper;

    public GetStockHistoryByIdQueryHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _ItemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<GetStockHistoryByIdDto> Handle(GetStockHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var stockFromDatabase = await _ItemRepository.GetStockHistoryByIdAsync(request.StockHistoryId);

        if(stockFromDatabase is StockHistoryOrder stockHistoryOrder)
        {
            var stockOrderToReturn = await _ItemRepository.GetStockHistoryOrderByIdAsync(request.StockHistoryId);
            return _mapper.Map<GetStockHistoryOrderDto>(stockOrderToReturn);
        }
        else
        {
            var stockSupplierToReturn = await _ItemRepository.GetStockHistorySupplierByIdAsync(request.StockHistoryId);
            return _mapper.Map<GetStockHistorySupplierDto>(stockSupplierToReturn);
        }

        // return _mapper.Map<GetStockHistoryByIdDto>(stockFromDatabase);
    }
}
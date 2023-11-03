using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.StockHistories.Commands.DeleteStockHistory;

public class DeleteStockHistoryCommandHandler : IRequestHandler<DeleteStockHistoryCommand, bool>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public DeleteStockHistoryCommandHandler(IItemRepository itemRepository, IMapper mapper){
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteStockHistoryCommand request, CancellationToken cancellationToken)
    {
        var StockHistoryFromDatabase = await _itemRepository.GetStockHistoryByIdAsync(request.StockHistoryId);

        if(StockHistoryFromDatabase == null) return false;

        _itemRepository.RemoveStockHistory(StockHistoryFromDatabase);

        return await _itemRepository.SaveChangesAsync();
    }
}
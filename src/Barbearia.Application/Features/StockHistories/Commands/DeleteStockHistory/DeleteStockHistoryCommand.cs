using MediatR;

namespace Barbearia.Application.Features.StockHistories.Commands.DeleteStockHistory;

public class DeleteStockHistoryCommand : IRequest<bool>
{
    public int StockHistoryId {get;set;}
}
namespace Barbearia.Application.Features.StockHistories.Commands.CreateStockHistory;

public class CreateStockHistoryCommandResponse : BaseResponse
{
    public CreateStockHistoryDto StockHistory {get;set;} = default!;
}
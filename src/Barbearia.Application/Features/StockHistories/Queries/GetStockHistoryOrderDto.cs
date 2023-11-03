using Barbearia.Application.Features.StockHistories.Queries.GetStockHistoryById;
using Barbearia.Application.Models;

namespace Barbearia.Application.Features.StockHistories.Queries;

public class GetStockHistoryOrderDto : GetStockHistoryByIdDto
{
    public int OrderId {get;set;}
    public OrderDto? Order {get;set;}
}
using Barbearia.Application.Features.StockHistories.Queries.GetStockHistoryById;
using Barbearia.Application.Models;

namespace Barbearia.Application.Features.StockHistories.Queries;

public class GetStockHistorySupplierDto : GetStockHistoryByIdDto
{
    public int PersonId {get;set;}
    public PersonDto? Supplier {get;set;}
}
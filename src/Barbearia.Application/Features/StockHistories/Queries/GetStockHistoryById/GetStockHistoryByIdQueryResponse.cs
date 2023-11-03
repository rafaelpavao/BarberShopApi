

namespace Barbearia.Application.Features.StockHistories.Queries.GetStockHistoryById;

public class GetStockHistoryByIdQueryResponse : BaseResponse
{
    public object StockHistory {get; set;} = new();

    public GetStockHistoryByIdQueryResponse()
    {
        Errors = new Dictionary<string, string[]>();
    }
}
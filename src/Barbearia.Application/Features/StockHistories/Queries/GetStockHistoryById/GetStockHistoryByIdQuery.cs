using MediatR;

namespace Barbearia.Application.Features.StockHistories.Queries.GetStockHistoryById;

public class GetStockHistoryByIdQuery : IRequest<GetStockHistoryByIdDto>
{
    public int StockHistoryId {get;set;}

}
using Barbearia.Application.Features.Suppliers.Commands.CreateSupplier;
using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.StockHistories.Commands.CreateStockHistory;

public class CreateStockHistoryCommand : IRequest<CreateStockHistoryCommandResponse>
{
    public int Operation {get;set;}
    public decimal CurrentPrice {get;set;}
    public int Amount {get;set;}
    public DateTime Timestamp {get;set;}
    public int LastStockQuantity {get;set;}
    public int PersonId {get;set;}
    public int ProductId {get;set;}
    public int OrderId {get;set;}
}
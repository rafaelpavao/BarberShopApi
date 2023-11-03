namespace Barbearia.Application.Models;

public class StockHistoryForSupplierDto
{
    public int StockHistoryId {get;set;}
    public int Operation {get;set;}
    public decimal CurrentPrice {get;set;}
    public int Amount {get;set;}
    public DateTime Timestamp {get;set;}
    public int LastStockQuantity {get;set;}
    public int ProductId {get;set;}
    public int OrderId {get;set;}
}
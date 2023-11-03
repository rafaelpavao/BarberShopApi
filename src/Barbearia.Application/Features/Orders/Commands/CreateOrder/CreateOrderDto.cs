using Barbearia.Application.Models;
using Barbearia.Application.Models.Appointments;
using Barbearia.Application.Models.Products;
using Barbearia.Application.Models.StockHistories;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderDto
{
    public int OrderId {get;set;}
    public int Number { get; set; }
    public int Status { get; set; }
    public DateTime BuyDate { get; set; }
    public int PersonId { get; set; }
    public List<StockHistoryOrderDto> StockHistoriesOrder { get; set; } = new();
    public List<ProductDto> Products { get; set; } = new();
    public List<AppointmentForGetOrderDto> Appointments { get; set; } = new();
}
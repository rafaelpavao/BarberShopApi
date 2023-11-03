using Barbearia.Application.Models;
using Barbearia.Application.Models.Appointments;
using Barbearia.Application.Models.Products;
using Barbearia.Application.Models.StockHistories;

namespace Barbearia.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdDto
{
    public int OrderId { get; set; }
    public int Number { get; set; }
    public int Status { get; set; }
    public DateTime BuyDate { get; set; }
    public PersonDto? Person { get; set; }
    public PaymentDto? Payment { get; set; }
    public List<StockHistoryOrderDto> StockHistoriesOrder { get; set; } = new();
    public List<ProductDto> Products { get; set; } = new();
    public List<AppointmentForGetOrderDto> Appointments { get; set; } = new();
}
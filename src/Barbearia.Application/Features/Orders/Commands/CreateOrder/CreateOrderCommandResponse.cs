namespace Barbearia.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandResponse : BaseResponse
{
    public CreateOrderDto Order {get; set;} = default!;    
}
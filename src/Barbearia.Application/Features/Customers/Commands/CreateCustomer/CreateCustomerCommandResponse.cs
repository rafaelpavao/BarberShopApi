namespace Barbearia.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandResponse : BaseResponse
{
    public CreateCustomerDto Customer {get; set;} = default!;    
}
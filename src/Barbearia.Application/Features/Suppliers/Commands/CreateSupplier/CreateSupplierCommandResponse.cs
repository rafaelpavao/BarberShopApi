namespace Barbearia.Application.Features.Suppliers.Commands.CreateSupplier;
public class CreateSupplierCommandResponse : BaseResponse
{
    public CreateSupplierDto Supplier {get; set;} = default!;    
}
namespace Barbearia.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandResponse : BaseResponse
{
    public CreateProductDto Product {get; set;} = default!;    
}
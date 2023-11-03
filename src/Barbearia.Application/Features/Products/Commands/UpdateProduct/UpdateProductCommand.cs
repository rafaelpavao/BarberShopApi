using MediatR;

namespace Barbearia.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<UpdateProductCommandResponse>
{
    public int ItemId {get;set;}
    public string Name {get;set;} = string.Empty;
    public decimal Price{get;set;}
    public string Description {get;set;} = string.Empty;
    public string Brand {get;set;} = string.Empty;
    public int Status{get; set;}
    public string SKU {get;set;} = string.Empty;//verificar se ja tem um sku no sistema ja pra não repetir
    public int QuantityInStock{get;set;}
    public int QuantityReserved {get;set;}
    public int ProductCategoryId { get; set; }
    public int PersonId {get;set;}
}
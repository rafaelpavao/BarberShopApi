using MediatR;

namespace Barbearia.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<bool>
{
    public int ItemId {get;set;}
}
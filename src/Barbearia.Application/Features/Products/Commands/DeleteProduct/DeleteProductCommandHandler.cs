using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IItemRepository _itemRepository;

    public DeleteProductCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var ProductFromDatabase = await _itemRepository.GetProductByIdAsync(request.ItemId);
        if(ProductFromDatabase == null) return false;
        _itemRepository.DeleteProduct(ProductFromDatabase);

        return await _itemRepository.SaveChangesAsync();
    }
}
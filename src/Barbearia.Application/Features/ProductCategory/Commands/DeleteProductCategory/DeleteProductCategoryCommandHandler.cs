using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, bool>
{
    private readonly IItemRepository _itemRepository;

    public DeleteProductCategoryCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategoryFromDatabase = await _itemRepository.GetProductCategoryByIdAsync(request.ProductCategoryId);

        if(productCategoryFromDatabase == null) return false;

        _itemRepository.RemoveProductCategory(productCategoryFromDatabase);

        return await _itemRepository.SaveChangesAsync();
    }
}

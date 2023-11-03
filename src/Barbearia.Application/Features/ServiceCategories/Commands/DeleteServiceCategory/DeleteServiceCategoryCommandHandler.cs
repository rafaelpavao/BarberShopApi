using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.ServiceCategories.Commands.DeleteServiceCategory;

public class DeleteServiceCategoryCommandHandler : IRequestHandler<DeleteServiceCategoryCommand, bool>
{
    private readonly IItemRepository _itemRepository;


    public DeleteServiceCategoryCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;

    }

    public async Task<bool> Handle(DeleteServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var serviceCategoriesEntity = await _itemRepository.GetServiceCategoryByIdAsync(request.ServiceCategoryId);
        if(serviceCategoriesEntity == null) return false;
        _itemRepository.DeleteServiceCategory(serviceCategoriesEntity);
        
        return await _itemRepository.SaveChangesAsync();
    }
}
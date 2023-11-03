using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Services.Commands.DeleteService;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, bool>
{
    private readonly IItemRepository _itemRepository;

    public DeleteServiceCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var serviceFromDatabase = await _itemRepository.GetServiceByIdAsync(request.ItemId);
        if(serviceFromDatabase == null) return false;
        _itemRepository.DeleteService(serviceFromDatabase);

        return await _itemRepository.SaveChangesAsync();
    }
}
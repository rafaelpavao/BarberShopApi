using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper){
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var OrderFromDatabase = await _orderRepository.GetOrderByIdAsync(request.OrderId);

        if(OrderFromDatabase == null) return false;

        _orderRepository.DeleteOrder(OrderFromDatabase);

        return await _orderRepository.SaveChangesAsync();
    }
}
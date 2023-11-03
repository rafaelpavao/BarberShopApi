using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Coupons.Commands.DeleteCoupon;

public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteCouponCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var couponToDelete = await _orderRepository.GetCouponByIdAsync(request.CouponId);

        if (couponToDelete == null) return false;

        _orderRepository.DeleteCoupon(couponToDelete);

        return await _orderRepository.SaveChangesAsync();
    }
}
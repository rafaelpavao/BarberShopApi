using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Coupons.Queries.GetCouponById;

public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, GetCouponByIdDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetCouponByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    
    public async Task<GetCouponByIdDto> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        var couponFromDatabase = await _orderRepository.GetCouponByIdAsync(request.CouponId);
        return _mapper.Map<GetCouponByIdDto>(couponFromDatabase);
    }
}
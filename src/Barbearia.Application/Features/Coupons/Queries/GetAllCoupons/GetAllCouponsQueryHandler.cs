using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;

public class GetAllCouponsQueryHandler : IRequestHandler<GetAllCouponsQuery, IEnumerable<GetAllCouponsDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetAllCouponsQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GetAllCouponsDto>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
    {
        var couponsFromDatabase = await _orderRepository.GetAllCoupons();
        return _mapper.Map<IEnumerable<GetAllCouponsDto>>(couponsFromDatabase);
    }
}
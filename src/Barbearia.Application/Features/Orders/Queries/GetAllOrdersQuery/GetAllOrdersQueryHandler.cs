using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<GetAllOrdersDto>>
{
    private readonly IOrderRepository _OrderRepository;
    private readonly IMapper _mapper;

    public GetAllOrdersQueryHandler(IOrderRepository OrderRepository, IMapper mapper)
    {
        _OrderRepository = OrderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllOrdersDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var OrdersFromDatabase = await _OrderRepository.GetAllOrdersAsync();

        return _mapper.Map<IEnumerable<GetAllOrdersDto>>(OrdersFromDatabase);
    }
}
using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdDto>
{
    private readonly IOrderRepository _OrderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository OrderRepository, IMapper mapper)
    {
        _OrderRepository = OrderRepository;
        _mapper = mapper;
    }

    public async Task<GetOrderByIdDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var OrderFromDatabase = await _OrderRepository.GetOrderByIdAsync(request.OrderId);

        return _mapper.Map<GetOrderByIdDto>(OrderFromDatabase);
    }
}
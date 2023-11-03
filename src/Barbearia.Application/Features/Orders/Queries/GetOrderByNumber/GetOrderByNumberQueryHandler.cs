using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Orders.Queries.GetOrderByNumber;

public class GetOrderByNumberQueryHandler : IRequestHandler<GetOrderByNumberQuery, GetOrderByNumberDto>
{
    private readonly IOrderRepository _OrderRepository;
    private readonly IMapper _mapper;

    public GetOrderByNumberQueryHandler(IOrderRepository OrderRepository, IMapper mapper)
    {
        _OrderRepository = OrderRepository;
        _mapper = mapper;
    }

    public async Task<GetOrderByNumberDto> Handle(GetOrderByNumberQuery request, CancellationToken cancellationToken)
    {
        var OrderFromDatabase = await _OrderRepository.GetOrderByNumberAsync(request.Number);

        return _mapper.Map<GetOrderByNumberDto>(OrderFromDatabase);
    }
}
using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.OrdersCollection;

public class GetOrdersCollectionQueryHandler : IRequestHandler<GetOrdersCollectionQuery,GetOrdersCollectionQueryResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersCollectionQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<GetOrdersCollectionQueryResponse> Handle(GetOrdersCollectionQuery request, CancellationToken cancellationToken)
    {
        GetOrdersCollectionQueryResponse response = new();

        var(orderToReturn, paginationMetadata) = await _orderRepository.GetAllOrdersAsync(request.SearchQuery, request.PageNumber, request.PageSize);
        response.Orders = _mapper.Map<IEnumerable<GetOrdersCollectionDto>>(orderToReturn);
        response.PaginationMetadata = paginationMetadata;

        return response;
    }
}
using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IItemRepository itemRepository, IPersonRepository personRepository, IMapper mapper, ILogger<CreateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _personRepository = personRepository;
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        CreateOrderCommandResponse response = new();

        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        var customerFromDatabase = await _personRepository.GetCustomerByIdAsync(request.PersonId);

        if (customerFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Customer not found." });
            return response;
        }

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        List<StockHistoryOrder> stockHistoryOrders = new List<StockHistoryOrder>();
        List<Product> Products = new List<Product>();
        List<Appointment> Appointments = new List<Appointment>();

        foreach (int stockHistoriesOrder in request.StockHistoriesOrderId)
        {
            var stockHistoryOrder = await _orderRepository.GetStockHistoryOrderToOrderByIdAsync(stockHistoriesOrder);
            if (stockHistoryOrder == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("stockHistoryOrder", new[] { "stockHistoryOrder not found in the database." });
                return response;
            }
            stockHistoryOrders.Add(stockHistoryOrder!);

        }

        foreach (int products in request.ProductsId)
        {
            var product = await _orderRepository.GetProductToOrderByIdAsync(products);
            if (product == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("product", new[] { "product not found in the database." });
                return response;
            }
            Products.Add(product!);

        }

        foreach (int appointments in request.AppointmentsId)
        {
            var appointment = await _orderRepository.GetAppointmentToOrderByIdAsync(appointments);
            if (appointment == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("appointment", new[] { "appointment not found in the database." });
                return response;
            }
            Appointments.Add(appointment!);

        }

        var orderEntity = _mapper.Map<Order>(request);

        foreach (var product in Products)
        {
            orderEntity.Products.Add(product);
        }
        foreach (var appointment in Appointments)
        {
            orderEntity.Appointments.Add(appointment);
        }
        foreach (var stockHistoryOrder in stockHistoryOrders)
        {
            orderEntity.StockHistoriesOrder.Add(stockHistoryOrder);
        }

        try
        {
            orderEntity.ValidateOrder();
        }
        catch(Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Order_Validation", new[] {"Error in order validation"});
            _logger.LogError(ex,"erro de validação em create order");
            return response;
        }

        _orderRepository.AddOrder(orderEntity);
        await _orderRepository.SaveChangesAsync();

        response.Order = _mapper.Map<CreateOrderDto>(orderEntity);
        return response;
    }
}
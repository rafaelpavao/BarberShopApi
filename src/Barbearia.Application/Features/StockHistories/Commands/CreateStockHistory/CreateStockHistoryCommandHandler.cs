using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.StockHistories.Commands.CreateStockHistory;

public class CreateStockHistoryCommandHandler : IRequestHandler<CreateStockHistoryCommand, CreateStockHistoryCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IPersonRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateStockHistoryCommandHandler> _logger;

    public CreateStockHistoryCommandHandler(IOrderRepository orderRepository, IPersonRepository customerRepository, IItemRepository itemRepository, IMapper mapper, ILogger<CreateStockHistoryCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateStockHistoryCommandResponse> Handle(CreateStockHistoryCommand request, CancellationToken cancellationToken)
    {
        CreateStockHistoryCommandResponse response = new();

        var validator = new CreateStockHistoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        var productFromDatbase = await _itemRepository.GetProductByIdAsync(request.ProductId);
        if (productFromDatbase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("ProductId", new[] { "Product not found." });
            return response;
        }

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        if(!(request.OrderId == 0) && !(request.PersonId == 0))
        {
            string[] error = {"More than one document informed"};
            response.Errors.Add("DocumentType", error);
            
            response.ErrorType = Error.BadRequestProblem;
            return response;
        }


        if (!(request.OrderId == 0)  && (request.Operation == 2 || request.Operation == 3))
        {
            var orderFromDatbase = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            if (orderFromDatbase == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("OrderId", new[] { "Order not found." });
                return response;
            }

            var stockHistoryOrderEntity = _mapper.Map<StockHistoryOrder>(request);

            try
            {
                stockHistoryOrderEntity.ValidateStockHistory();
            }
            catch (Exception ex)
            {
                response.ErrorType = Error.InternalServerErrorProblem;
                response.Errors.Add("StockHistory_Validation", new[] { "Error in stock history validation" });
                _logger.LogError(ex, "erro de validação em create stock history");
                return response;
            }


            _itemRepository.AddStockHistory(stockHistoryOrderEntity);
            await _itemRepository.SaveChangesAsync();
            response.StockHistory = _mapper.Map<CreateStockHistoryOrderDto>(stockHistoryOrderEntity);
        }
        else if (!(request.PersonId == 0) && (request.Operation == 1))
        {
            var supplierFromDatbase = await _customerRepository.GetSupplierByIdAsync(request.PersonId);
            if (supplierFromDatbase == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("PersonId", new[] { "Supplier not found." });
                return response;
            }

            var stockHistorySupplierEntity = _mapper.Map<StockHistorySupplier>(request);

            try
            {
                stockHistorySupplierEntity.ValidateStockHistory();
            }
            catch (Exception ex)
            {
                response.ErrorType = Error.InternalServerErrorProblem;
                response.Errors.Add("StockHistory_Validation", new[] { "Error in stock history validation" });
                _logger.LogError(ex, "erro de validação em create stock history");
                return response;
            }

            _itemRepository.AddStockHistory(stockHistorySupplierEntity);
            await _itemRepository.SaveChangesAsync();
            response.StockHistory = _mapper.Map<CreateStockHistorySupplierDto>(stockHistorySupplierEntity);
        }
        else
        {
            string[] error = { "Necessary keys not informed or invalid operation" };
            response.Errors.Add("DocumentType", error);

            response.ErrorType = Error.ValidationProblem;
            return response;
        }

        return response;

    }
}
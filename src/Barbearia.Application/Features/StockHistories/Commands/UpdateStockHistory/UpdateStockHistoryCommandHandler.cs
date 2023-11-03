using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.StockHistories.Commands.UpdateStockHistory;

public class UpdateStockHistoryCommandHandler : IRequestHandler<UpdateStockHistoryCommand, UpdateStockHistoryCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateStockHistoryCommandHandler> _logger;

    public UpdateStockHistoryCommandHandler(IOrderRepository orderRepository, IPersonRepository personRepository, IItemRepository itemRepository, IMapper mapper, ILogger<UpdateStockHistoryCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _personRepository = personRepository;
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateStockHistoryCommandResponse> Handle(UpdateStockHistoryCommand request, CancellationToken cancellationToken)
    {
        UpdateStockHistoryCommandResponse response = new();

        var validator = new UpdateStockHistoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request);



        if (!(request.OrderId == 0) && !(request.PersonId == 0))
        {
            string[] error = { "More than one document informed" };
            response.Errors.Add("DocumentType", error);

            response.ErrorType = Error.BadRequestProblem;
            return response;
        }

        var stockHistoryFromDatbase = await _itemRepository.GetStockHistoryByIdAsync(request.StockHistoryId);
        if (stockHistoryFromDatbase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("StockHistoryId", new[] { "StockHistory not found." });
            return response;
        }
        else if (stockHistoryFromDatbase is StockHistoryOrder stockHistoryOrder)
        {

            if (request.Operation != 2 && request.Operation != 3)
            {
                string[] error = { "wrong operation" };
                response.Errors.Add("DocumentType", error);

                response.ErrorType = Error.BadRequestProblem;
                return response;
            }

            if (request.OrderId == 0)
            {
                string[] error = { "No order informed" };
                response.Errors.Add("DocumentType", error);

                response.ErrorType = Error.BadRequestProblem;
                return response;
            }
            var orderFromDatbase = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            if (orderFromDatbase == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("OrderId", new[] { "Order not found." });
                return response;
            }

        }
        else if (stockHistoryFromDatbase is StockHistorySupplier stockHistorySupplier)
        {
            if (request.PersonId == 0)
            {
                string[] error = { "No supplier informed" };
                response.Errors.Add("DocumentType", error);

                response.ErrorType = Error.BadRequestProblem;
                return response;
            }
            if (!(request.Operation == 1))
            {
                string[] error = { "wrong operation" };
                response.Errors.Add("DocumentType", error);

                response.ErrorType = Error.BadRequestProblem;
                return response;
            }

            var supplierFromDatbase = await _personRepository.GetSupplierByIdAsync(request.PersonId);

            if (supplierFromDatbase == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("PersonId", new[] { "Supplier not found." });
                return response;
            }
        }


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

        _mapper.Map(request, stockHistoryFromDatbase);

        try
        {
            stockHistoryFromDatbase.ValidateStockHistory();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("StockHistory_Validation", new[] { "Error in stock history validation" });
            _logger.LogError(ex, "erro de validação em create stock history");
            return response;
        }

        await _itemRepository.SaveChangesAsync();

        return response;
    }
}
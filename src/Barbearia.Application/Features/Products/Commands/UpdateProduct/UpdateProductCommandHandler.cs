using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace Barbearia.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    private readonly IItemRepository _ItemRepository;
    private readonly IPersonRepository _customerRepository;
    private readonly IMapper _mapper;

    private readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(IItemRepository ItemRepository, IPersonRepository customerRepository, IMapper mapper
    , ILogger<UpdateProductCommandHandler> logger)
    {
        _ItemRepository = ItemRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        UpdateProductCommandResponse response = new UpdateProductCommandResponse();

        var ProductFromDatabase = await _ItemRepository.GetProductByIdAsync(request.ItemId);
        if (ProductFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("ItemId", new[] { "Product not found in the database." });
            return response;
        }

        var supplierFromDatabase = await _customerRepository.GetSupplierByIdAsync(request.PersonId);
        if (supplierFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Supplier not found in the database." });
            return response;
        }

        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, ProductFromDatabase);

        try
        {
            ProductFromDatabase.ValidateProduct();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Product_Validation", new[] { "Error in Product validation" });
            _logger.LogError(ex, "erro de validação em update Product");
            return response;
        }

        await _ItemRepository.SaveChangesAsync();

        return response;
    }
}
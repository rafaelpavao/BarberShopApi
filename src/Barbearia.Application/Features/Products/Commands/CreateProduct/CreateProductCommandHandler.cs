using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly IItemRepository _ItemRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(IItemRepository ItemRepository, IPersonRepository personRepository, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
    {
        _ItemRepository = ItemRepository;
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        CreateProductCommandResponse response = new();

        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        var supplierFromDatabase = await _personRepository.GetSupplierByIdAsync(request.PersonId);
        if (supplierFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Supplier not found in the database." });
            return response;
        }

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var ProductEntity = _mapper.Map<Product>(request);

        try
        {
            ProductEntity.ValidateProduct();
        }
        catch(Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Product_Validation", new[] {"Error in Product validation"});
            _logger.LogError(ex,"erro de validação em create Product");
            return response;
        }

        _ItemRepository.AddProduct(ProductEntity);
        await _ItemRepository.SaveChangesAsync();

        response.Product = _mapper.Map<CreateProductDto>(ProductEntity);
        return response;
    }
}
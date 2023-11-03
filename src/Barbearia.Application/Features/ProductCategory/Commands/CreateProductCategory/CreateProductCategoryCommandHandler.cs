using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, CreateProductCategoryCommandResponse>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductCategoryCommandHandler> _logger;

    public CreateProductCategoryCommandHandler(IItemRepository itemRepository, IMapper mapper, ILogger<CreateProductCategoryCommandHandler> logger)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateProductCategoryCommandResponse> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        CreateProductCategoryCommandResponse response = new();

        var validator = new CreateProductCategoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var ProductCategoryEntity = _mapper.Map<ProductCategory>(request);

        try
        {
            ProductCategoryEntity.ValidateProductCategory();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("ProductCategory_Validation", new[] { "Error in ProductCategory validation" });
            _logger.LogError(ex, "erro de validação em update ProductCategory");
            return response;
        }

        _itemRepository.AddProductCategory(ProductCategoryEntity);
        await _itemRepository.SaveChangesAsync();

        response.ProductCategory = _mapper.Map<CreateProductCategoryDto>(ProductCategoryEntity);
        return response;
    }
}
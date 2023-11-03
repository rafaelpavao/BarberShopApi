using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductCategoryCommand, UpdateProductCategoryCommandResponse>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductCategoryCommandHandler> _logger;

    public UpdateProductCategoryCommandHandler(IItemRepository itemRepository, IMapper mapper, ILogger<UpdateProductCategoryCommandHandler> logger)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateProductCategoryCommandResponse> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        UpdateProductCategoryCommandResponse response = new();

        var ProductCategoryFromDatabase = await _itemRepository.GetProductCategoryByIdAsync(request.ProductCategoryId);
        if (ProductCategoryFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("ProductCategoryId", new[] { "ProductCategory not found in the database." });
            return response;
        }

        var validator = new UpdateProductCategoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var ProductCategoryEntity = _mapper.Map(request, ProductCategoryFromDatabase);


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

        await _itemRepository.SaveChangesAsync();

        return response;
    }
}
using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.ServiceCategories.Commands.UpdateServiceCategory;

public class UpdateServiceCategoryCommandHandler:IRequestHandler<UpdateServiceCategoryCommand, UpdateServiceCategoryCommandResponse>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateServiceCategoryCommandHandler> _logger;

    public UpdateServiceCategoryCommandHandler(IItemRepository itemRepository, IMapper mapper, ILogger<UpdateServiceCategoryCommandHandler> logger)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateServiceCategoryCommandResponse> Handle(UpdateServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        UpdateServiceCategoryCommandResponse response = new();

        var serviceCategoryFromDatabase = await _itemRepository.GetServiceCategoryByIdAsync(request.ServiceCategoryId);
        if(serviceCategoryFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("ServiceCategoryId", new[] { "ServiceCategory not found in the database." });
            return response;
        }

        var validator = new UpdateServiceCategoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, serviceCategoryFromDatabase);

        try
        {
            serviceCategoryFromDatabase.ValidateServiceCategory();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("ServiceCategory_Validation", new[] { "Error in ServiceCategory validation" });
            _logger.LogError(ex, "erro de validação em update ServiceCategory");
            return response;
        }

        await _itemRepository.SaveChangesAsync();

        return response;
    }
}
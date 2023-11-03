using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.ServiceCategories.Commands.CreateServiceCategory;

public class CreateServiceCategoryCommandHandler : IRequestHandler<CreateServiceCategoryCommand, CreateServiceCategoryCommandResponse>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateServiceCategoryCommandHandler> _logger;

    public CreateServiceCategoryCommandHandler(IItemRepository itemRepository, IMapper mapper, ILogger<CreateServiceCategoryCommandHandler> logger)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateServiceCategoryCommandResponse> Handle(CreateServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        CreateServiceCategoryCommandResponse response = new();

        var validator = new CreateServiceCategoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var serviceCategoryEntiry = _mapper.Map<ServiceCategory>(request);

        try
        {
            serviceCategoryEntiry.ValidateServiceCategory();
        }
        catch(Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("ServiceCategory_Validation", new[] {"Error in ServiceCategory validation"});
            _logger.LogError(ex,"erro de validação em create ServiceCategory");
            return response;
        }

        _itemRepository.AddServiceCategory(serviceCategoryEntiry);
        await _itemRepository.SaveChangesAsync();

        response.ServiceCategory = _mapper.Map<CreateServiceCategoryDto>(serviceCategoryEntiry);
        return response;
    }
}
using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, CreateServiceCommandResponse>
{
    private readonly IItemRepository _ItemRepository;    
    private readonly IMapper _mapper;
    private readonly ILogger<CreateServiceCommandHandler> _logger;

    public CreateServiceCommandHandler(IItemRepository ItemRepository, IMapper mapper, ILogger<CreateServiceCommandHandler> logger)
    {
        _ItemRepository = ItemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateServiceCommandResponse> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        CreateServiceCommandResponse response = new();

        var validator = new CreateServiceCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        var sericeCategoryFromDatabase = await _ItemRepository.GetServiceCategoryByIdAsync(request.ServiceCategoryId);
        if (sericeCategoryFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("ServiceCategoryId", new[] { "ServiceCategory not found in the database." });
            return response;
        }

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var serviceEntity = _mapper.Map<Service>(request);

        try
        {
            serviceEntity.ValidateService();
        }
        catch(Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Service_Validation", new[] {"Error in Service validation"});
            _logger.LogError(ex,"erro de validação em create Service");
            return response;
        }

        _ItemRepository.AddService(serviceEntity);
        await _ItemRepository.SaveChangesAsync();

        response.Service = _mapper.Map<CreateServiceDto>(serviceEntity);
        return response;
    }
}
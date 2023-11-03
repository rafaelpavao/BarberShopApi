using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace Barbearia.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, UpdateServiceCommandResponse>
{
    private readonly IItemRepository _ItemRepository;
    private readonly IPersonRepository _customerRepository;
    private readonly IMapper _mapper;

    private readonly ILogger<UpdateServiceCommandHandler> _logger;

    public UpdateServiceCommandHandler(IItemRepository ItemRepository, IPersonRepository customerRepository, IMapper mapper
    , ILogger<UpdateServiceCommandHandler> logger)
    {
        _ItemRepository = ItemRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateServiceCommandResponse> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        UpdateServiceCommandResponse response = new UpdateServiceCommandResponse();

        var serviceFromDatabase = await _ItemRepository.GetServiceByIdAsync(request.ItemId);
        if (serviceFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("ItemId", new[] { "Service not found in the database." });
            return response;
        }

        var sericeCategoryFromDatabase = await _ItemRepository.GetServiceCategoryByIdAsync(request.ServiceCategoryId);
        if (sericeCategoryFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("ServiceCategoryId", new[] { "ServiceCategory not found in the database." });
            return response;
        }

        var validator = new UpdateServiceCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, serviceFromDatabase);

        try
        {
            serviceFromDatabase.ValidateService();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Service_Validation", new[] { "Error in Service validation" });
            _logger.LogError(ex, "erro de validação em update Service");
            return response;
        }

        await _ItemRepository.SaveChangesAsync();

        return response;
    }
}
using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.TimesOff.Commands.CreateTimeOff;


public class CreateTimeOffCommandHandler : IRequestHandler<CreateTimeOffCommand, CreateTimeOffCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateTimeOffCommandHandler> _logger;

    public CreateTimeOffCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<CreateTimeOffCommandHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateTimeOffCommandResponse> Handle(CreateTimeOffCommand request, CancellationToken cancellationToken)
    {
        CreateTimeOffCommandResponse response = new();

        var workingDayFromDatabase = await _personRepository.GetWorkingDayByIdAsync(request.WorkingDayId);
        if (workingDayFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("WorkingDayId", new[] { "WorkingDay not found in the database." });
            return response;
        }

        var validator = new CreateTimeOffCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var timeOffEntity = _mapper.Map<TimeOff>(request);

        // Validação do fornecedor
        try
        {
            timeOffEntity.ValidateTimeOff();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Supplier_Validation", new[] { "Error in supplier validation" });
            _logger.LogError(ex, "erro de validação em create supplier");
            return response;
        }


        _personRepository.AddTimeOff(timeOffEntity);
        await _personRepository.SaveChangesAsync();

        response.TimeOff = _mapper.Map<CreateTimeOffDto>(timeOffEntity);
        return response;
    }
}
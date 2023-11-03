using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Customers.Commands.CreateCustomer;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.TimesOff.Commands.UpdateTimeOff;

public class UpdateTimeOffCommandHandler : IRequestHandler<UpdateTimeOffCommand, UpdateTimeOffCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateTimeOffCommandHandler> _logger;

    public UpdateTimeOffCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<UpdateTimeOffCommandHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateTimeOffCommandResponse> Handle(UpdateTimeOffCommand request, CancellationToken cancellationToken)
    {
        UpdateTimeOffCommandResponse response = new();

        var timeOffFromDatabase = await _personRepository.GetTimeOffByIdAsync(request.TimeOffId);

        if (timeOffFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("TimeOffId", new[] { "TimeOff not found." });
            return response;
        }

        var workingDayFromDatabase = await _personRepository.GetWorkingDayByIdAsync(request.WorkingDayId);
        if (workingDayFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("WorkingDayId", new[] { "WorkingDay not found in the database." });
            return response;
        }

        var validator = new UpdateTimeOffCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, timeOffFromDatabase);

        // Validação do cliente
        try
        {
            timeOffFromDatabase.ValidateTimeOff();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("TimeOff_Validation", new[] { "Error in time off validation" });
            _logger.LogError(ex, "erro de validação em update time off");
            return response;
        }

        await _personRepository.SaveChangesAsync();

        return response;
    }
}
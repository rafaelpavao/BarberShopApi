using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;

public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, UpdateScheduleCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPersonRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateScheduleCommandHandler> _logger;

    public UpdateScheduleCommandHandler(IPersonRepository personRepository, IPersonRepository customerRepository, IMapper mapper, ILogger<UpdateScheduleCommandHandler> logger)
    {
        _personRepository = personRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateScheduleCommandResponse> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        UpdateScheduleCommandResponse response = new UpdateScheduleCommandResponse();

        var scheduleFromDatabase = await _personRepository.GetScheduleByIdAsync(request.ScheduleId);
        if (scheduleFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("ScheduleId", new[] { "Schedule not found in the database." });
            return response;
        }

        var workingDayFromDatabase = await _personRepository.GetWorkingDayByIdAsync(request.WorkingDayId);
        if (workingDayFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("WorkingDayId", new[] { "WorkingDay not found in the database." });
            return response;
        }

        var workingDayExists = await _personRepository.HasScheduleForWorkingDayAsync(request.WorkingDayId);

        if (workingDayExists)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("WorkingDayId", new[] { "A Schedule with the specified WorkingDay already exists in the database." });
            return response;
        }

        var validator = new UpdateScheduleCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, scheduleFromDatabase);

        try
        {
            scheduleFromDatabase.ValidateSchedule();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Schedule_Validation", new[] { "Error in Schedule validation" });
            _logger.LogError(ex, "erro de validação em update Schedule");
            return response;
        }

        await _personRepository.SaveChangesAsync();

        return response;
    }
}
using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.WorkingDays.Commands.CreateWorkingDay;

public class CreateWorkingDayCommandHandler : IRequestHandler<CreateWorkingDayCommand, CreateWorkingDayCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateWorkingDayCommandHandler> _logger;
    public CreateWorkingDayCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<CreateWorkingDayCommandHandler> logger)
    {
        _mapper = mapper;
        _personRepository = personRepository;
        _logger = logger;
    }
    public async Task<CreateWorkingDayCommandResponse> Handle(CreateWorkingDayCommand request, CancellationToken cancellationToken)
    {
        CreateWorkingDayCommandResponse response = new();

        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);
        if (employeeFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Person Not found in database" });
            return response;
        }

        var validator = new CreateWorkingDayCommandValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var workingDayEntity = _mapper.Map<WorkingDay>(request);

        try
        {
            workingDayEntity.ValidateWorkingDay();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Working_Day_Validation", new[] { "Error in working day validation" });
            _logger.LogError(ex, "erro de validação em create workingday");
            return response;
        }

        _personRepository.AddWorkingDay(employeeFromDatabase, workingDayEntity);
        await _personRepository.SaveChangesAsync();

        response.WorkingDay = _mapper.Map<CreateWorkingDayDto>(workingDayEntity);
        return response;
    }
}
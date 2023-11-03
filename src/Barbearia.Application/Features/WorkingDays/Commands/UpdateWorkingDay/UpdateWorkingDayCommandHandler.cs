using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.WorkingDays.Commands.UpdateWorkingDay;

public class UpdateWorkingDayCommandHandler : IRequestHandler<UpdateWorkingDayCommand, UpdateWorkingDayCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateWorkingDayCommandHandler> _logger;
    public UpdateWorkingDayCommandHandler(IPersonRepository personRepository, IMapper mapper, IValidator<UpdateWorkingDayCommand> validator
    , ILogger<UpdateWorkingDayCommandHandler> logger){
        _mapper = mapper;
        _personRepository = personRepository;
        _logger = logger;
    }
    public async Task<UpdateWorkingDayCommandResponse> Handle(UpdateWorkingDayCommand request, CancellationToken cancellationToken)
    {
        UpdateWorkingDayCommandResponse response = new();

        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);
        if(employeeFromDatabase == null){
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[]{"Employee not found in database"});
            return response;
        }

        var workingDayToUpdate = employeeFromDatabase.WorkingDays.FirstOrDefault(c => c.WorkingDayId == request.WorkingDayId);
        if(workingDayToUpdate == null){
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("working Day", new[]{"Working Day not found in database"});
            return response;
        }

        var validator = new UpdateWorkingDayCommandValidator();
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, workingDayToUpdate);

        try
        {
            workingDayToUpdate.ValidateWorkingDay();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("WorkingDay_Validation", new[] { "Error in working day validation" });
            _logger.LogError(ex, "erro de validação em create working day");
            return response;
        }

        await _personRepository.SaveChangesAsync();

        return response;
    }
}
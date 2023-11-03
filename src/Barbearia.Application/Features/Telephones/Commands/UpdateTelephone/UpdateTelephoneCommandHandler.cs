using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;

public class UpdateTelephoneCommandHandler : IRequestHandler<UpdateTelephoneCommand, UpdateTelephoneCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateTelephoneCommand> _validator;
    private readonly ILogger<UpdateTelephoneCommandHandler> _logger;
    public UpdateTelephoneCommandHandler(IPersonRepository personRepository, IMapper mapper, IValidator<UpdateTelephoneCommand> validator
    , ILogger<UpdateTelephoneCommandHandler> logger){
        _mapper = mapper;
        _personRepository = personRepository;
        _validator = validator;
        _logger = logger;
    }
    public async Task<UpdateTelephoneCommandResponse> Handle(UpdateTelephoneCommand request, CancellationToken cancellationToken)
    {
        UpdateTelephoneCommandResponse response = new();

        var personFromDatabase = await _personRepository.GetPersonByIdAsync(request.PersonId);
        if(personFromDatabase == null){
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("PersonId", new[]{"Customer not found in database"});
            return response;
        }

        var telephoneToUpdate = personFromDatabase.Telephones.FirstOrDefault(c => c.TelephoneId == request.TelephoneId);
        if(telephoneToUpdate == null){
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Telephone", new[]{"Telephone not found in database"});
            return response;
        }

        var validator = new UpdateTelephoneCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, telephoneToUpdate);

        try
        {
            telephoneToUpdate.ValidateTelephone();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Telephone_Validation", new[] { "Error in telephone validation" });
            _logger.LogError(ex, "erro de validação em create telephone");
            return response;
        }

        await _personRepository.SaveChangesAsync();

        return response;
    }
}
using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Telephones.Commands.CreateTelephone;

public class CreateTelephoneCommandHandler : IRequestHandler<CreateTelephoneCommand, CreateTelephoneCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateTelephoneCommandHandler> _logger;
    public CreateTelephoneCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<CreateTelephoneCommandHandler> logger)
    {
        _mapper = mapper;
        _personRepository = personRepository;
        _logger = logger;
    }
    public async Task<CreateTelephoneCommandResponse> Handle(CreateTelephoneCommand request, CancellationToken cancellationToken)
    {
        CreateTelephoneCommandResponse response = new();

        var personFromDatabase = await _personRepository.GetPersonByIdAsync(request.PersonId);
        if (personFromDatabase == null)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("PersonId", new[] { "Person Not found in database" });
            return response;
        }
        if (personFromDatabase.Telephones.Any() && (personFromDatabase.GetType() == typeof(Customer)))
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Telephone", new[] { "Only one telephone per customer" });
            return response;
        }        

        var validator = new CreateTelephoneCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var telephoneEntity = _mapper.Map<Telephone>(request);

        try
        {
            telephoneEntity.ValidateTelephone();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Telephone_Validation", new[] { "Error in telephone validation" });
            _logger.LogError(ex, "erro de validação em create telephone");
            return response;
        }

        _personRepository.AddTelephone(personFromDatabase, telephoneEntity);
        await _personRepository.SaveChangesAsync();

        response.Telephone = _mapper.Map<CreateTelephoneDto>(telephoneEntity);
        return response;
    }
}
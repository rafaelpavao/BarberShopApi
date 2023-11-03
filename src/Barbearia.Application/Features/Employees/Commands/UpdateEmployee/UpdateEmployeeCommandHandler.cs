using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Employees.Commands.CreateEmployee;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateEmployeeCommandHandler> _logger;



    public UpdateEmployeeCommandHandler(IPersonRepository personRepository, IItemRepository itemRepository, IMapper mapper, ILogger<UpdateEmployeeCommandHandler> logger)
    {
        _personRepository = personRepository;
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        UpdateEmployeeCommandResponse response = new();

        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);

        if (employeeFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Employee not found." });
            return response;
        }

        var validator = new UpdateEmployeeCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        List<Role> Roles = new List<Role>();
        List<Service> Services = new List<Service>();

        foreach (int roles in request.RolesId)
        {
            var roleFromDatabase = await _personRepository.GetRoleByIdAsync(roles);
            if (roleFromDatabase == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("roles", new[] { "roles not found in the database." });
                return response;
            }
            Roles.Add(roleFromDatabase!);

        }

        foreach (int services in request.ServicesId)
        {
            var servicesFromDatabase = await _personRepository.GetServiceByIdAsync(services);
            if (servicesFromDatabase == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("services", new[] { "services not found in the database." });
                return response;
            }
            Services.Add(servicesFromDatabase!);
        }

        _mapper.Map(request, employeeFromDatabase);

        foreach (var role in Roles)
        {
            employeeFromDatabase.Roles.Add(role);
        }
        foreach (var service in Services)
        {
            employeeFromDatabase.Services.Add(service);
        }

        // Validação do funcionario
        try
        {
            employeeFromDatabase.ValidateEmployee();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Employee_Validation", new[] { "Error in Employee validation" });
            _logger.LogError(ex, "erro de validação em create Employee");
            return response;
        }

        // Validação do número de telefone
        foreach (var telephone in request.Telephones)
        {
            var telephoneEntity = _mapper.Map<Telephone>(telephone);
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
        }

        // Validação do endereço
        foreach (var address in request.Addresses)
        {
            var addressEntity = _mapper.Map<Address>(address);
            try
            {
                addressEntity.ValidateAddress();
            }
            catch (Exception ex)
            {
                response.ErrorType = Error.InternalServerErrorProblem;
                response.Errors.Add("Address_Validation", new[] { "Error in address validation" });
                _logger.LogError(ex, "erro de validação em create address");
                return response;
            }
        }

        await _personRepository.SaveChangesAsync();

        return response;
    }
}
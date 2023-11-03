using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Suppliers.Commands.CreateSupplier;

public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, CreateSupplierCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSupplierCommandHandler> _logger;



    public CreateSupplierCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<CreateSupplierCommandHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateSupplierCommandResponse> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        CreateSupplierCommandResponse response = new();

        var validator = new CreateSupplierCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var supplierEntity = _mapper.Map<Supplier>(request);

        // Validação do fornecedor
        try
        {
            supplierEntity.ValidateSupplier();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Supplier_Validation", new[] { "Error in supplier validation" });
            _logger.LogError(ex, "erro de validação em create supplier");
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

        _personRepository.AddSupplier(supplierEntity);
        await _personRepository.SaveChangesAsync();

        response.Supplier = _mapper.Map<CreateSupplierDto>(supplierEntity);
        return response;
    }
}
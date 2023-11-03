using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, UpdateAddressCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAddressCommandHandler> _logger;

    public UpdateAddressCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<UpdateAddressCommandHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        UpdateAddressCommandResponse response = new UpdateAddressCommandResponse();

        var personFromDatabase = await _personRepository.GetPersonByIdAsync(request.PersonId);
        if (personFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Person not found in the database." });
            return response;
        }

        var addressToUpdate = personFromDatabase.Addresses.FirstOrDefault(a => a.AddressId == request.AddressId);

        if (addressToUpdate == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("AddressId", new[] { "Address not found for the specified PersonId." });
            return response;
        }

        var validator = new UpdateAddressCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, addressToUpdate);

        try
        {
            addressToUpdate.ValidateAddress();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Address_Validation", new[] { "Error in address validation" });
            _logger.LogError(ex, "erro de validação em create address");
            return response;
        }

        await _personRepository.SaveChangesAsync();

        return response;
    }
}
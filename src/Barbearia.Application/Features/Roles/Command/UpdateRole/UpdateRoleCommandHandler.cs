using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleCommandResponse>
{
    private readonly IPersonRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateRoleCommandHandler> _logger;

    public UpdateRoleCommandHandler(IPersonRepository customerRepository, IMapper mapper, ILogger<UpdateRoleCommandHandler> logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        UpdateRoleCommandResponse response = new();

        var roleFromDatabase = await _customerRepository.GetRoleByIdAsync(request.RoleId);
        if (roleFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("RoleId", new[] { "Role not found in the database." });
            return response;
        }

        var validator = new UpdateRoleCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, roleFromDatabase);

        try
        {
            roleFromDatabase.ValidateRole();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Role_Validation", new[] { "Error in Role validation" });
            _logger.LogError(ex, "erro de validação em update Role");
            return response;
        }

        await _customerRepository.SaveChangesAsync();

        return response;
    }
}
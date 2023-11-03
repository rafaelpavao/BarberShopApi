using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateRoleCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<CreateRoleCommandHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        CreateRoleCommandResponse response = new();

        var validator = new CreateRoleCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var roleEntiry = _mapper.Map<Role>(request);

        try
        {
            roleEntiry.ValidateRole();
        }
        catch(Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Role_Validation", new[] {"Error in Role validation"});
            _logger.LogError(ex,"erro de validação em create Role");
            return response;
        }

        _personRepository.AddRole(roleEntiry);
        await _personRepository.SaveChangesAsync();

        response.Role = _mapper.Map<CreateRoleDto>(roleEntiry);
        return response;
    }
}
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IPersonRepository _personRepository;


    public DeleteRoleCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;

    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var roleEntity = await _personRepository.GetRoleByIdAsync(request.RoleId);
        if(roleEntity == null) return false;
        _personRepository.DeleteRole(roleEntity);

        return await _personRepository.SaveChangesAsync();
    }
}
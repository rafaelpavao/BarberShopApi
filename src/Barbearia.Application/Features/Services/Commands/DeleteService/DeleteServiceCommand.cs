using MediatR;

namespace Barbearia.Application.Features.Services.Commands.DeleteService;

public class DeleteServiceCommand : IRequest<bool>
{
    public int ItemId {get;set;}
}
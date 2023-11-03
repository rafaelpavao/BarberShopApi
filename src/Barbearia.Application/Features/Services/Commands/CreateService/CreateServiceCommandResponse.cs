namespace Barbearia.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommandResponse : BaseResponse
{
    public CreateServiceDto Service {get; set;} = default!;    
}
using MediatR;

namespace Barbearia.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommand : IRequest<CreateServiceCommandResponse>
{
    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ServiceCategoryId { get; set; }
    public decimal Price { get; set; }   
    public int DurationMinutes { get; set; }
}
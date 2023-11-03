using System.Text.Json.Serialization;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Services.Commands.CreateService;

public class CreateServiceDto
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ServiceCategoryId { get; set; }
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }
}
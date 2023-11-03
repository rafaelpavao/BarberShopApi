using System.Text.Json.Serialization;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceDto
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ServiceCategoryId { get; set; }
    public decimal Price { get; set; }       
    public int DurationMinutes { get; set; }

}
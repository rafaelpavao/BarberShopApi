using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Services.Queries.GetServiceById;

public class GetServiceByIdDto
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ServiceCategoryId { get; set; }
    public int DurationMinutes { get; set; } 
    public decimal Price { get; set; }       
}
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Features.ServicesCollection;

public class GetServicesCollectionDto
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ServiceCategoryId { get; set; }
    //public ServiceCategory Name { get; set; }
    public decimal Price { get; set; }    
}
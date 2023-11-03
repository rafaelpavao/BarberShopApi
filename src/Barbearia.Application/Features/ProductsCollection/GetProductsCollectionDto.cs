using System.Text.Json.Serialization;
using Barbearia.Domain.Entities;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.ProductsCollection;

public class GetProductsCollectionDto
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int ProductCategoryId { get; set; }
    //public string Name { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeStatus Status { get; set; }
}
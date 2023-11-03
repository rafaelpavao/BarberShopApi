using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsDto
{
    public int ItemId {get;set;}
    public string Name {get;set;} = string.Empty;
    public string SKU {get;set;} = string.Empty;
    public int ProductCategoryId {get;set;}
    public ProductCategoryDto? productCategory {get;set;}
    public decimal Price {get;set;}
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeStatus Status { get; set; }
}
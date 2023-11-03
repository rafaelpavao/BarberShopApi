namespace Barbearia.Application.Models;
public class ProductForStockHistoryDto : ItemDto
{
    public string Brand {get;set;} = string.Empty;
    public int Status{get; set;}
    public string SKU {get;set;} = string.Empty;//verificar se ja tem um sku no sistema ja pra não repetir
    public int QuantityInStock{get;set;}
    public int QuantityReserved {get;set;}
    // public List<StockHistory> StockHistories { get; set; } = new();
    public int ProductCategoryId { get; set; }
    // public ProductCategoryForProductForStockHistoryDto? ProductCategory { get; set; }
    public int PersonId { get; set; }
    // public Person? Supplier { get; set; }
    // public List<Order> Orders { get; set; } = new();
    // public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
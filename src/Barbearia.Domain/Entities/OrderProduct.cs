namespace Barbearia.Domain.Entities;

public class OrderProduct
{

    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int ItemId { get; set; }
    public Product? Product { get; set; }
    
    private void CheckOrderId()
    {
        if (OrderId <= 0)
        {
            throw new ArgumentException("OrderId deve ser maior que zero.");
        }
    }
    
    private void CheckItemId()
    {
        if (ItemId <= 0)
        {
            throw new ArgumentException("ItemId deve ser maior que zero.");
        }
    }

    public void ValidateOrderProduct()
    {
        CheckOrderId();
        CheckItemId();
    }

}
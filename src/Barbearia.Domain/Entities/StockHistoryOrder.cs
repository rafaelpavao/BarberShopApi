namespace Barbearia.Domain.Entities;

public class StockHistoryOrder : StockHistory
{
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    private void CheckOrderId()
    {
        if (OrderId <= 0)
        {
            throw new ArgumentException("OrderId deve ser maior que zero.");
        }
    }

    private void CheckOperation()
    {
        if ((Operation != 2) || (Operation != 3))
        {
            throw new ArgumentException("Operação inválida. As operações para Order só podem ser 2-Removal, 3-Reserved.");
        }
    }

    private void CheckCurrentPrice()
    {
        if (CurrentPrice < 0)
        {
            throw new ArgumentException("Preço atual inválido. O preço atual deve ser maior que zero.");
        }
    }

    private void CheckAmount()
    {
        if (Amount < 0)
        {
            throw new ArgumentException("Quantidade inválida. A quantidade deve ser maior que zero.");
        }
    }

    private void CheckTimestamp()
    {
        if (Timestamp > DateTime.UtcNow)
        {
            throw new ArgumentException("O registro de histórico não pode estar no futuro.");
        }
    }


    public void ValidateStockHistoryOrder()
    {
        CheckOrderId();
        CheckOperation();
        CheckCurrentPrice();
        CheckAmount();
        CheckTimestamp();
    }
}
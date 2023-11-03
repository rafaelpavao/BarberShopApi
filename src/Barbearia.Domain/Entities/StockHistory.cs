namespace Barbearia.Domain.Entities;
public abstract class StockHistory{
    public int StockHistoryId {get;set;}
    public int Operation {get;set;}
    public decimal CurrentPrice {get;set;}
    public int Amount {get;set;}
    public DateTime Timestamp {get;set;}
    public int LastStockQuantity {get;set;}
    public int ProductId {get;set;}
    public Product? Product {get;set;}


    private void CheckOperation()
        {
            if (Operation < 0)
            {
                throw new ArgumentException("Operação inválida. A operação deve ser maior que zero.");
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

        public void ValidateStockHistory()
        {
            CheckOperation();
            CheckCurrentPrice();
            CheckAmount();
            CheckTimestamp();
        }
}
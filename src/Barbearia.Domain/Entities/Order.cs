using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Barbearia.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public int Number { get; set; }
    public int Status { get; set; }
    public DateTime BuyDate { get; set; }
    public int? PersonId { get; set; }
    public Person? Person { get; set; }
    public Payment? Payment { get; set; }
    public List<StockHistoryOrder> StockHistoriesOrder { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public List<Appointment> Appointments{get; set;} =new();

    private void CheckNumber()
    {
        if (Number == 0)
        {
            throw new ArgumentException("Numero não pode estar vazio.");
        }
        if (Number <= 0)
        {
            throw new ArgumentException("Número inválido. O Número deve ser maior que zero.");
        }
    }

    private void CheckCustomer()
    {
        if (PersonId == null)
        {
            throw new ArgumentException("O pedido deve estar associado a um cliente.");
        }
    }

    private void CheckStatus()
    {
        if(Status <1 || Status>3) throw new ArgumentException("Status não suportado");
    }

    private void CheckBuyDate()
    {
        if (!DateTime.TryParse(BuyDate.ToString(), out DateTime parsedDate))
        {
            throw new ArgumentException("A data de compra não está em um formato válido.");
        }
        if (parsedDate > DateTime.UtcNow)
        {
            throw new ArgumentException("A data de compra não pode ser no futuro.");
        }
    }

    public void ValidateOrder()
    {
        CheckStatus();
        CheckNumber();
        CheckCustomer();
        CheckBuyDate();
    }

}

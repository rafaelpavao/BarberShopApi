using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Security;

namespace Barbearia.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }
    public DateTime BuyDate { get; set; } 
    public Decimal GrossTotal { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public decimal NetTotal { get; set; }
    public int? CouponId { get; set; }
    public Coupon? Coupon { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; }




    private void CheckBuyDate()
    {
        if (!DateTime.TryParse(BuyDate.ToString(), out DateTime parsedDate))
        {
            throw new ArgumentException("A data deve ser válida.");
        }
        if (parsedDate > DateTime.UtcNow)
        {
            throw new ArgumentException("A data não pode ser no futuro.");
        }
    }

    private void CheckGrossTotal()
    {
        if (GrossTotal <= 0)
        {
            throw new ArgumentException("O valor bruto total deve ser maior ou igual a 0.");
        }
    }

    private void CheckPaymentMethod()
    {
        if (!(PaymentMethod == "Débito" || PaymentMethod == "Crédito" || PaymentMethod == "Dinheiro"))
        {
            throw new ArgumentException("Forma de pagamento não suportada.");
        }
    }

    private void CheckNetTotal()
    {
        if (NetTotal <= 0)
        {
            throw new ArgumentException("O valor líquido total deve ser maior ou igual a 0.");
        }
    }

    private void CheckOrder()
    {
        if (OrderId == null)
        {
            throw new ArgumentException("O pagamento deve estar associado a uma ordem.");
        }
    }

    public void ValidatePayment()
    {
        CheckBuyDate();
        CheckGrossTotal();
        CheckPaymentMethod();
        CheckNetTotal();
        CheckOrder();
    }

}


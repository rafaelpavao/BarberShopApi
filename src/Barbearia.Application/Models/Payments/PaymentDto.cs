namespace Barbearia.Application.Models;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public DateTime BuyDate { get; set; } 
    public Decimal GrossTotal { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public decimal NetTotal { get; set; }
    public int? CouponId { get; set; }
}
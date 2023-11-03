using MediatR;

namespace Barbearia.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommand : IRequest<CreatePaymentCommandResponse>
{
    public int PaymentId { get; set; }
    public DateTime BuyDate { get; set; } 
    public Decimal GrossTotal { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public decimal NetTotal { get; set; }
    public int? CouponId { get; set; }
    public int OrderId { get; set; }
}
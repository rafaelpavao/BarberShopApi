using MediatR;

namespace Barbearia.Application.Features.Payments.Commands.UpdatePayment;

public class UpdatePaymentCommand : IRequest<UpdatePaymentCommandResponse>
{
    public int OrderId { get; set; }
    public Decimal GrossTotal { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public decimal NetTotal { get; set; }
    public int? CouponId { get; set; }
}
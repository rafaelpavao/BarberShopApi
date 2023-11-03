namespace Barbearia.Domain.Entities;

public class Coupon
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = string.Empty;
    public int DiscountPercent { get; set; }//entre 1 e 100
    public DateTime CreationDate { get; set; }//creation date tem que ser antes de expiration
    public DateTime ExpirationDate { get; set; }
    public List<Payment> Payments { get; set; } = new();

    private void CheckDiscountPercent()
    {
        if (!(DiscountPercent >= 1 && DiscountPercent <= 100))
        {
            throw new ArgumentException("A taxa de desconto deve estar entre 1 e 100.");
        }
    }

    private void CheckCreationDate()
    {
        if (CreationDate > DateTime.UtcNow)
        {
            throw new ArgumentException("A data de criação não pode ser no futuro.");
        }
    }

    private void CheckExpirationDate()
    {
        if (CreationDate > ExpirationDate)
        {
            throw new ArgumentException("A data de expiração não pode ser anterior à data de criação.");
        }
    }

    public void ValidateCoupon()
    {
        CheckDiscountPercent();
        CheckCreationDate();
        CheckExpirationDate();
    }
}
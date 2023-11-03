namespace Barbearia.Domain.Entities;

public abstract class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;

    private void CheckPrice()
    {
        if (Price < 0)
        {
            throw new Exception("O preço do item não pode ser negativo.");
        }
    }

    public void ValidateItem()
    {
        CheckPrice();
    }
}
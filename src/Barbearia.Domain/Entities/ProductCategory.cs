namespace Barbearia.Domain.Entities;

public class ProductCategory
{
    public int ProductCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Product> Product { get; set; } = new();

    // Regra de negócio: O nome da categoria não pode estar vazio ou nulo.
    private void CheckName()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ArgumentException("O nome da categoria não pode estar vazio ou nulo.");
        }
    }    

    public void ValidateProductCategory()
    {
        CheckName();        
    }
}
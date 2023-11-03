using Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;

namespace Barbearia.Api.IntegrationTests.Utils.Fixtures;

[CollectionDefinition("ProductCategory")]
public class ProductCategoryCollection : ICollectionFixture<ProductCategoryDataGeneratingFixture> { }

public class ProductCategoryDataGeneratingFixture : IDisposable
{
    public int CreatedProductCategoryId;

    public CreateProductCategoryCommand GenerateValidCreateProductCategoryCommand()
    {
        return new CreateProductCategoryCommand
        {
            Name = "Higiene"
        };
    }

    public UpdateProductCategoryCommand GenerateValidUpdateProductCategoryCommand()
    {
        return new UpdateProductCategoryCommand()
        {
            ProductCategoryId = CreatedProductCategoryId,
            Name = "Bebidas"
        };
    }

    public void Dispose() { }
}
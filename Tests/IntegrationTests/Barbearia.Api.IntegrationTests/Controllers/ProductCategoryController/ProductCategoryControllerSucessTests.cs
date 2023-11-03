using System.Net;
using System.Net.Http.Json;
using Barbearia.Api.IntegrationTests.Utils.Factories;
using Barbearia.Api.IntegrationTests.Utils.Fixtures;
using Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Barbearia.Application.Features.ProductCategories.Queries.GetProductCategoryById;
using FluentAssertions;
using Xunit.Extensions.Ordering;

namespace Barbearia.Api.IntegrationTests.Controllers.ProductCategoryController;

[Collection(nameof(DatabaseCollection))]
public class ProductCategoryControllerSuccessTests : IClassFixture<BarbeariaWebApplicationFactory>, IClassFixture<ProductCategoryDataGeneratingFixture>
{
    private readonly BarbeariaWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private ProductCategoryDataGeneratingFixture _fixture;

    public ProductCategoryControllerSuccessTests(BarbeariaWebApplicationFactory factory, ProductCategoryDataGeneratingFixture fixture)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _fixture = fixture;
    }

    [Fact(DisplayName = "Create ProductCategory : WhenCommand Is Valid : Should Return CreatedAtRoute With Expected JSON")]
    [Trait("Category", "Integration Tests")]
    [Trait("Scenario", "Successful CRUD Tests")]

    [Order(1)]
    public async Task CreateProductCategory_WhenCommandIsValid_ShouldReturnCreatedAtRoute_WithExpectedJson()
    {
        // Arrange      
        var request = _fixture.GenerateValidCreateProductCategoryCommand();

        // Act
        var response = await _client.PostAsJsonAsync("api/ProductCategories", request);
        var ProductCategory = await response.Content.ReadFromJsonAsync<CreateProductCategoryDto>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        ProductCategory?.Should().NotBeNull();
        _fixture.CreatedProductCategoryId = ProductCategory!.ProductCategoryId;
    }

    [Fact(DisplayName = "Get ProductCategory By Id : When ProductCategory Is In Db : Should Return Ok ProductCategory")]
    [Trait("Category", "Integration Tests")]
    [Trait("Scenario", "Successful CRUD Tests")]
    [Order(2)]
    public async Task GetProductCategoryById_WhenProductCategoryIsInDb_ShouldReturnOkProductCategory()
    {
        // Arrange        
        var ProductCategoryId = _fixture.CreatedProductCategoryId;

        // Act
        var response = await _client.GetAsync($"api/ProductCategories/{ProductCategoryId}");
        var ProductCategory = await response.Content.ReadFromJsonAsync<GetProductCategoryByIdDto>();

        // Assert        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        ProductCategory?.ProductCategoryId.Should().Be(ProductCategoryId);
    }

    [Fact(DisplayName = "Update ProductCategory : When ProductCategory Is In Db : Should Return NoContent")]
    [Trait("Category", "Integration Tests")]
    [Trait("Scenario", "Successful CRUD Tests")]
    [Order(3)]
    public async Task UpdateProductCategory_WhenProductCategoryIsInDb_ShouldReturnNoContent()
    {
        // Arrange
        var ProductCategoryId = _fixture.CreatedProductCategoryId;
        var updateRequest = _fixture.GenerateValidUpdateProductCategoryCommand();

        // Act
        var response = await _client.PutAsJsonAsync($"api/ProductCategories/{ProductCategoryId}", updateRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact(DisplayName = "Delete ProductCategory : When ProductCategory Is In Db : Should Return NoContent")]
    [Trait("Category", "Integration Tests")]
    [Trait("Scenario", "Successful CRUD Tests")]
    [Order(4)]
    public async Task DeleteProductCategory_WhenProductCategoryIsInDb_ShouldReturnNoContent()
    {
        // Arrange        
        var ProductCategoryId = _fixture.CreatedProductCategoryId;

        // Act
        var response = await _client.DeleteAsync($"api/ProductCategories/{ProductCategoryId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
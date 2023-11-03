using Barbearia.Application.Features.Products.Commands.CreateProduct;
using Barbearia.Application.Features.Products.Commands.DeleteProduct;
using Barbearia.Application.Features.Products.Commands.UpdateProduct;
using Barbearia.Application.Features.Products.Queries.GetAllProducts;
using Barbearia.Application.Features.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllProductsDto>>> GetProducts()
    {
        var getAllProductsQuery = new GetAllProductsQuery{ };
        var productsToReturn = await _mediator.Send(getAllProductsQuery);

        return Ok(productsToReturn);
    }

    [HttpGet("{itemId}", Name ="GetProductById")]
    public async Task<ActionResult<GetProductByIdDto>> GetProductById(int itemId){
        var getProductById = new GetProductByIdQuery { ItemId = itemId };
        var productToReturn = await _mediator.Send(getProductById);

        if(productToReturn == null) return NotFound();

        return Ok(productToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductCommandResponse>> CreateProduct (CreateProductCommand createProductCommand)
    {
        var CreateProductCommandResponse = await _mediator.Send(createProductCommand);

        if(!CreateProductCommandResponse.IsSuccess)
            return HandleRequestError(CreateProductCommandResponse);

        var productForReturn = CreateProductCommandResponse.Product;

        return CreatedAtRoute
        (
            "GetProductById",
            new { itemId = productForReturn.ItemId},
            productForReturn
        );
    }

    [HttpPut("{itemId}")]
    public async Task<ActionResult> UpdateProduct(int itemId, UpdateProductCommand updateProductCommand)
    {
        if (updateProductCommand.ItemId != itemId) return BadRequest();

        var updateProductCommandResponse = await _mediator.Send(updateProductCommand);

        if (!updateProductCommandResponse.IsSuccess)
            return HandleRequestError(updateProductCommandResponse);
        
        return NoContent();
    }

    [HttpDelete("{itemId}")]
    public async Task<ActionResult> DeleteProduct(int itemId)
    {
        var result = await _mediator.Send(new DeleteProductCommand { ItemId = itemId });

        if (!result) return NotFound();

        return NoContent();
    }
}
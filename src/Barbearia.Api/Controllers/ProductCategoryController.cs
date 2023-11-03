using Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Barbearia.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;
using Barbearia.Application.Features.ProductCategories.Queries.GetAllProductCategories;
using Barbearia.Application.Features.ProductCategories.Queries.GetProductCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/productCategories")]
public class ProductCategoryController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductCategoryController> _logger;

    public ProductCategoryController(IMediator mediator, ILogger<ProductCategoryController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllProductCategoriesDto>>> GetAllProductCategories()
    {
        var getAllProductCategoriesQuery = new GetAllProductCategoriesQuery { };

        var ProductCategoriesToReturn = await _mediator.Send(getAllProductCategoriesQuery);

        return Ok(ProductCategoriesToReturn);
    }

    [HttpGet("{productCategoryId}", Name = "GetProductCategoryById")]
    public async Task<ActionResult<GetProductCategoryByIdDto>> GetProductCategoryById (int productCategoryId)
    {
        var getProductCategoryByIdQuery = new GetProductCategoryByIdQuery { ProductCategoryId = productCategoryId};

        var ProductCategoryToReturn = await _mediator.Send(getProductCategoryByIdQuery);

        if(ProductCategoryToReturn == null) return NotFound();

        return Ok(ProductCategoryToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductCategoryDto>> CreateProductCategory(CreateProductCategoryCommand createProductCategoryCommand)
    {
        var createProductCategoryCommandResponse = await _mediator.Send(createProductCategoryCommand);

        if(!createProductCategoryCommandResponse.IsSuccess)
            return HandleRequestError(createProductCategoryCommandResponse);

        var ProductCategoryForReturn = createProductCategoryCommandResponse.ProductCategory;

        return CreatedAtRoute
        (
            "GetProductCategoryById",
            new { productCategoryId = ProductCategoryForReturn.ProductCategoryId},
            ProductCategoryForReturn
        );
    }

    [HttpPut("{productCategoryId}")]
    public async Task<ActionResult> UpdateProductCategory(int productCategoryId, UpdateProductCategoryCommand updateProductCategoryCommand)
    {
        if(updateProductCategoryCommand.ProductCategoryId != productCategoryId) return BadRequest();

        var UpdateProductCategoryCommandResponse = await _mediator.Send(updateProductCategoryCommand);

        if(!UpdateProductCategoryCommandResponse.IsSuccess)
            return HandleRequestError(UpdateProductCategoryCommandResponse);

        return NoContent();
    }

    [HttpDelete("{productCategoryId}")]
    public async Task<ActionResult> DeleteProductCategory(int productCategoryId)
    {
        var result = await _mediator.Send(new DeleteProductCategoryCommand { ProductCategoryId = productCategoryId});

        if(!result) return NotFound();

        return NoContent();
    }
}
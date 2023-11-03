
using Barbearia.Application.Features.ServiceCategories.Queries.GetAllServiceCategories;
using Barbearia.Application.Features.ServiceCategories.Queries.GetServiceCategoryById;
using Barbearia.Application.Features.ServiceCategories.Commands.CreateServiceCategory;
using Barbearia.Application.Features.ServiceCategories.Commands.UpdateServiceCategory;
using Barbearia.Application.Features.ServiceCategories.Commands.DeleteServiceCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/serviceCategories")]
public class ServiceCategoriesController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<ServiceCategoriesController> _logger;

    public ServiceCategoriesController(IMediator mediator, ILogger<ServiceCategoriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllServiceCategoriesDto>>> GetServiceCategories()
    {
        var getAllServiceCategoriesQuery = new GetAllServiceCategoriesQuery{ };
        var serviceCategoriesToReturn = await _mediator.Send(getAllServiceCategoriesQuery);

        return Ok(serviceCategoriesToReturn);
    }

    [HttpGet("{serviceCategoryId}", Name ="GetServiceCategoryById")]
    public async Task<ActionResult<GetServiceCategoryByIdDto>> GetServiceCategoryById(int serviceCategoryId){
        var getServiceCategoryById = new GetServiceCategoryByIdQuery { ServiceCategoryId = serviceCategoryId };
        var serviceCategoryToReturn = await _mediator.Send(getServiceCategoryById);

        if(serviceCategoryToReturn == null) return NotFound();

        return Ok(serviceCategoryToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateServiceCategoryCommandResponse>> CreateServiceCategory (CreateServiceCategoryCommand createServiceCategoryCommand)
    {
        var CreateServiceCategoryCommandResponse = await _mediator.Send(createServiceCategoryCommand);

        if(!CreateServiceCategoryCommandResponse.IsSuccess)
            return HandleRequestError(CreateServiceCategoryCommandResponse);

        var serviceCategoryForReturn = CreateServiceCategoryCommandResponse.ServiceCategory;

        return CreatedAtRoute
        (
            "GetServiceCategoryById",
            new { serviceCategoryId = serviceCategoryForReturn.ServiceCategoryId},
            serviceCategoryForReturn
        );
    }

    [HttpPut("{serviceCategoryId}")]
    public async Task<ActionResult> UpdateServiceCategory(int serviceCategoryId, UpdateServiceCategoryCommand updateServiceCategoryCommand)
    {
        if (updateServiceCategoryCommand.ServiceCategoryId != serviceCategoryId) return BadRequest();

        var updateServiceCategoryCommandResponse = await _mediator.Send(updateServiceCategoryCommand);

        if (!updateServiceCategoryCommandResponse.IsSuccess)
            return HandleRequestError(updateServiceCategoryCommandResponse);
        
        return NoContent();
    }

    [HttpDelete("{serviceCategoryId}")]
    public async Task<ActionResult> DeleteServiceCategory(int serviceCategoryId)
    {
        var result = await _mediator.Send(new DeleteServiceCategoryCommand { ServiceCategoryId = serviceCategoryId });

        if (!result) return NotFound();

        return NoContent();
    }
}
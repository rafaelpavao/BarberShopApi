
using Barbearia.Application.Features.StockHistories.Commands.CreateStockHistory;
using Barbearia.Application.Features.StockHistories.Commands.DeleteStockHistory;
using Barbearia.Application.Features.StockHistories.Commands.UpdateStockHistory;
using Barbearia.Application.Features.StockHistories.Queries.GetStockHistoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/stockHistories")]
public class StockHistoryController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<StockHistoryController> _logger;

    public StockHistoryController(IMediator mediator, ILogger<StockHistoryController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
    }

    // [HttpGet]
    // public async Task<ActionResult<List<GetAllStockHistoriesDto>>> GetAllStockHistories()
    // {
    //     var getAllStockHistoriesQuery = new GetAllStockHistoriesQuery { };

    //     var stockHistoryToReturn = await _mediator.Send(getAllStockHistoriesQuery);

    //     if (stockHistoryToReturn == null) return NotFound();

    //     return Ok(stockHistoryToReturn);
    // }

    [HttpGet("{stockHistoryId}", Name ="GetStockHistoryById")]
    public async Task<ActionResult<GetStockHistoryByIdDto>> GetStockHistoryById(int stockHistoryId)
    {
        var getStockHistoryByIdQuery = new GetStockHistoryByIdQuery { StockHistoryId = stockHistoryId };

        var stockHistoryToReturn = await _mediator.Send(getStockHistoryByIdQuery);

        if (stockHistoryToReturn == null) return NotFound();

        return Ok(stockHistoryToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateStockHistoryCommandResponse>> CreateStockHistory(CreateStockHistoryCommand createStockHistoryCommand)
    {
        var createStockHistoryCommandResponse = await _mediator.Send(createStockHistoryCommand);

        if(!createStockHistoryCommandResponse.IsSuccess)
            return HandleRequestError(createStockHistoryCommandResponse);

        var stockHistoryForReturn = createStockHistoryCommandResponse.StockHistory;


        return CreatedAtRoute
        (
            "GetStockHistoryById",
            new { stockHistoryId = stockHistoryForReturn.StockHistoryId },
            stockHistoryForReturn
        );
    }

    [HttpPut("{stockHistoryId}")]
    public async Task<ActionResult> UpdateStockHistory(int stockHistoryId, UpdateStockHistoryCommand updateStockHistoryCommand)
    {
        if(updateStockHistoryCommand.StockHistoryId != stockHistoryId) return BadRequest();

        var updateStockHistoryCommandResponse = await _mediator.Send(updateStockHistoryCommand);

        if(!updateStockHistoryCommandResponse.IsSuccess)
        return HandleRequestError(updateStockHistoryCommandResponse);

        return NoContent();
    }

    [HttpDelete("{stockHistoryId}")]
    public async Task<ActionResult> DeleteStockHistory(int stockHistoryId)
    {
        var result = await _mediator.Send(new DeleteStockHistoryCommand { StockHistoryId = stockHistoryId});

        if(!result) return NotFound();

        return NoContent();
    }
    
}
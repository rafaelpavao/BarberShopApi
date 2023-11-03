using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Application.Features.Coupons.Commands.DeleteCoupon;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Application.Models.Coupons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[Route("api/coupons")]

public class CouponsController : MainController
{
    private readonly IMediator _mediator;
    
    public CouponsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet("{couponId}",Name = "GetCouponById")]
    public async Task<ActionResult<GetCouponByIdDto>> GetCouponById(int couponId)
    {
        var getCouponQuery = new GetCouponByIdQuery() { CouponId = couponId };
        var couponToReturn = await _mediator.Send(getCouponQuery);

        if (couponToReturn == null) return NotFound();

        return Ok(couponToReturn);
    }
    
    [HttpGet(Name = "GetAllCoupons")]
    public async Task<ActionResult<IEnumerable<GetAllCouponsDto>>> GetCoupons()
    {
        var getCouponsQuery = new GetAllCouponsQuery();
        var couponsToReturn = await _mediator.Send(getCouponsQuery);

        if (!couponsToReturn.Any()) return NotFound();

        return Ok(couponsToReturn);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateCouponCommandResponse>> CreateCoupon([FromBody] CreateCouponCommand createCouponCommand)
    {
        var createCouponCommandResponse = await _mediator.Send(createCouponCommand);

        if (!createCouponCommandResponse.IsSuccess)
            return HandleRequestError(createCouponCommandResponse);

        var couponForReturn = createCouponCommandResponse.Coupon;

        return CreatedAtRoute
        (
            "GetCouponById",
            new
            {
                couponId = couponForReturn.CouponId
            },
            couponForReturn
        );
    }
    
    [HttpPut("{couponId}")]
    public async Task<ActionResult> UpdateCoupon(int couponId ,UpdateCouponCommand updateCouponCommand)
    {
        if(updateCouponCommand.CouponId != couponId) return BadRequest();

        var updateCouponCommandResponse = await _mediator.Send(updateCouponCommand);

        if (!updateCouponCommandResponse.IsSuccess)
            return HandleRequestError(updateCouponCommandResponse);

        return NoContent();
    }

    [HttpDelete("{couponId}")]
    public async Task<ActionResult> DeleteCoupon(int couponId)
    {
        var result = await _mediator.Send(new DeleteCouponCommand {CouponId = couponId });

        if (!result) return NotFound();

        return NoContent();
    }
    
}
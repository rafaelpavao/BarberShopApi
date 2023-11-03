using MediatR;

namespace Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;

public class GetAllCouponsQuery : IRequest<IEnumerable<GetAllCouponsDto>>
{
    
}
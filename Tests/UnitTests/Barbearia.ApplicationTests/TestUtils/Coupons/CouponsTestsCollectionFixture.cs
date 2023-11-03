using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Application.Features.Coupons.Commands.DeleteCoupon;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Application.Models.Coupons;
using Barbearia.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace UnitTests.Barbearia.ApplicationTests.TestUtils.Coupons;

[CollectionDefinition(nameof(CouponsTestsCollection))]
public class CouponsTestsCollection : ICollectionFixture<CouponsTestsCollectionFixture>
{
}

public class CouponsTestsCollectionFixture
{
    public AutoMocker? Mocker;
    public CouponsTestsDataGeneratingFixture DataFixture = new ();

    public Mock<IOrderRepository> GenerateAndSetupOrderRepository()
    {
        var repo = new Mock<IOrderRepository>();
        repo.Setup( r => r.AddCoupon(It.IsAny<Coupon>())).Verifiable();
        repo.Setup(r => r.CouponExists(It.IsAny<string>())).ReturnsAsync(false);
        repo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true).Verifiable();
        repo.Setup(r => r.GetAllCoupons()).ReturnsAsync(DataFixture.GenerateValidCoupons(10));
        repo.Setup(r => r.DeleteCoupon(It.IsAny<Coupon>())).Verifiable();
        repo.Setup(r => r.GetCouponByIdAsync(It.IsAny<int>())).ReturnsAsync(DataFixture.GenerateValidCoupons(1).FirstOrDefault);

        return repo;
    }

    public Mock<IMapper> GenerateAndSetupMapper()
    {
        var mapper = new Mock<IMapper>();

        mapper.Setup(m => m.Map<Coupon>(It.IsAny<CouponDto>())).Returns(DataFixture.GenerateValidCoupons(1).FirstOrDefault!);
        mapper.Setup(m => m.Map<Coupon>(It.IsAny<CreateCouponCommand>())).Returns(DataFixture.GenerateValidCoupons(1).FirstOrDefault!);
        mapper.Setup(m => m.Map<CreateCouponDto>(It.IsAny<Coupon>())).Returns(DataFixture.GenerateValidCreateCouponDto());
        mapper.Setup(m => m.Map<UpdateCouponDto>(It.IsAny<Coupon>())).Returns(DataFixture.GenerateInvalidUpdateCouponDto());
        mapper.Setup(m => m.Map<GetCouponByIdDto>(It.IsAny<Coupon>())).Returns(DataFixture.GenerateValidGetCouponByIdDto());
        mapper.Setup(m => m.Map<IEnumerable<GetAllCouponsDto>>(It.IsAny<IEnumerable<Coupon>>())).Returns(DataFixture.GenerateValidGetAllCouponsDtos(10));

        return mapper;

    }

    public CreateCouponCommandHandler GenerateCreateCouponCommandHandler(IOrderRepository repo, IMapper mapper)
    {
        return new CreateCouponCommandHandler(repo, mapper,
            new Mock<ILogger<CreateCouponCommandHandler>>().Object);
    }
    
    public UpdateCouponCommandHandler GenerateUpdateCouponCommandHandler(IOrderRepository repo, IMapper mapper)
    {
        return new UpdateCouponCommandHandler(repo, mapper,
            new Mock<ILogger<UpdateCouponCommandHandler>>().Object);
    }
    
    public DeleteCouponCommandHandler GenerateDeleteCouponCommandHandler(IOrderRepository repo)
    {
        return new DeleteCouponCommandHandler(repo);
    }
    
    public GetAllCouponsQueryHandler GenerateGetAllCouponsQueryHandler(IOrderRepository repo, IMapper mapper)
    {
        return new GetAllCouponsQueryHandler(repo, mapper);
    }
    
    public GetCouponByIdQueryHandler GenerateGetCouponByIdQueryHandler(IOrderRepository repo, IMapper mapper)
    {
        return new GetCouponByIdQueryHandler(repo, mapper);
    }
    
}

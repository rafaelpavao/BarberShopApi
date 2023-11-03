using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Barbearia.ApplicationTests.TestUtils.Coupons;

namespace UnitTests.Barbearia.ApplicationTests.Features.Coupons.Queries.GetCouponById;


[Collection(nameof(CouponsTestsCollection))]
public class GetCouponByIdQueryHandlerTests : IClassFixture<CouponsTestsDataGeneratingFixture>
{
    
    private readonly CouponsTestsCollectionFixture _fixture;
    private readonly CouponsTestsDataGeneratingFixture _dataFixture;
    private Mock<IOrderRepository> _repo;
    private Mock<IMapper> _mapper;
    private GetCouponByIdQueryHandler _handler;

    public GetCouponByIdQueryHandlerTests(CouponsTestsCollectionFixture fixture, CouponsTestsDataGeneratingFixture dataFixture)
    {
        _fixture = fixture;
        _dataFixture = dataFixture;
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateGetCouponByIdQueryHandler(_repo.Object, _mapper.Object);
    }
    
    [Fact(DisplayName = "When Coupon Is In Db : Should Return Coupon")]
    [Trait("Category", "Get Coupon By Id Query Handler Tests")]
    public async Task Handle_WhenCouponIsInDb_ShouldReturnCoupon()
    {
        var query = new GetCouponByIdQuery(){CouponId = 1};
        
        var response = await _handler.Handle(query, CancellationToken.None);

        response.Should().BeAssignableTo<GetCouponByIdDto>();
        response.Should().NotBeNull();
    }
    
    [Fact(DisplayName = "When Coupon Is Not In Db : Should Return Null")]
    [Trait("Category", "Get Coupon By Id Query Handler Tests")]
    public async Task Handle_WhenCouponIsNotInDb_ShouldReturnNull()
    {
        var query = new GetCouponByIdQuery(){CouponId = 1};
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _mapper
            .Setup(m => m.Map<GetCouponByIdDto>(It.IsAny<Coupon>()))
            .Returns((GetCouponByIdDto)null);
        _repo
            .Setup(r => r.GetCouponByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Coupon)null);
        _handler = _fixture.GenerateGetCouponByIdQueryHandler(_repo.Object, _mapper.Object);
        
        var response = await _handler.Handle(query, CancellationToken.None);

        response.Should().BeNull();
    }
    
    [Fact(DisplayName = "When Method Is Called : Should Execute Dependencies Correctly")]
    [Trait("Category", "Get Coupon By Id Query Handler Tests")]
    public async Task Handle_WhenMethodIsCalled_ShouldExecuteDependenciesCorrectly()
    {
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateGetCouponByIdQueryHandler(_repo.Object, _mapper.Object);
        var query = new GetCouponByIdQuery(){CouponId = 1};

        var response = await _handler.Handle(query, CancellationToken.None);
        
        _mapper.Verify(m => m.Map<GetCouponByIdDto>(It.IsAny<Coupon>()), Times.Once);
        _repo.Verify(r => r.GetCouponByIdAsync(It.IsAny<int>()), Times.Once);
    }
}
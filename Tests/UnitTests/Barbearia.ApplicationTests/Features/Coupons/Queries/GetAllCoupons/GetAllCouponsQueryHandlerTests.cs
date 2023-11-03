using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Barbearia.ApplicationTests.TestUtils.Coupons;

namespace UnitTests.Barbearia.ApplicationTests.Features.Coupons.Queries.GetAllCoupons.SuccessfullTests;


[Collection(nameof(CouponsTestsCollection))]
public class GetAllCouponsQueryHandlerTests : IClassFixture<CouponsTestsDataGeneratingFixture>
{
    
    private readonly CouponsTestsCollectionFixture _fixture;
    private readonly CouponsTestsDataGeneratingFixture _dataFixture;
    private Mock<IOrderRepository> _repo;
    private Mock<IMapper> _mapper;
    private GetAllCouponsQueryHandler _handler;

    public GetAllCouponsQueryHandlerTests(CouponsTestsCollectionFixture fixture, CouponsTestsDataGeneratingFixture dataFixture)
    {
        _fixture = fixture;
        _dataFixture = dataFixture;
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateGetAllCouponsQueryHandler(_repo.Object, _mapper.Object);
    }
    
    [Fact(DisplayName = "When Db Has Coupons : Should Return Not Empty Coupon Collection")]
    [Trait("Category", "Get All Coupons Query Handler Tests")]
    public async Task Handle_WhenDbHasCoupons_ShouldReturnNotEmptyCouponCollection()
    {
        var query = new GetAllCouponsQuery();
        
        var response = await _handler.Handle(query, CancellationToken.None);

        response.Should().BeOfType<List<GetAllCouponsDto>>();
        response.Should().NotBeEmpty();
    }
    
    [Fact(DisplayName = "When Db Has No Coupons : Should Return Empty Coupon Collection")]
    [Trait("Category", "Get All Coupons Query Handler Tests")]
    public async Task Handle_WhenDbHasNoCoupons_ShouldReturnEmptyCouponCollection()
    {
        var query = new GetAllCouponsQuery();
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _mapper
            .Setup(m => m.Map<IEnumerable<GetAllCouponsDto>>(It.IsAny<IEnumerable<Coupon>>()))
            .Returns(new List<GetAllCouponsDto>());
        _repo
            .Setup(r => r.GetAllCoupons())
            .ReturnsAsync(new List<Coupon>());
        _handler = _fixture.GenerateGetAllCouponsQueryHandler(_repo.Object, _mapper.Object);
        
        var response = await _handler.Handle(query, CancellationToken.None);

        response.Should().BeOfType<List<GetAllCouponsDto>>();
        response.Should().BeEmpty();
    }
    
    [Fact(DisplayName = "When Method Is Called : Should Execute Dependencies Correctly")]
    [Trait("Category", "Get All Coupons Query Handler Tests")]
    public async Task Handle_WhenMethodIsCalled_ShouldExecuteDependenciesCorrectly()
    {
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateGetAllCouponsQueryHandler(_repo.Object, _mapper.Object);
        var query = new GetAllCouponsQuery();

        var response = await _handler.Handle(query, CancellationToken.None);
        
        _mapper.Verify(m => m.Map<IEnumerable<GetAllCouponsDto>>(It.IsAny<IEnumerable<Coupon>>()), Times.Once);
        _repo.Verify(r => r.GetAllCoupons(), Times.Once);
    }
}
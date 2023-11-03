using Barbearia.Application.Features;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Contracts.Repositories;

public interface IOrderRepository
{    
    Task<(IEnumerable<Order>,PaginationMetadata)> GetAllOrdersAsync(string? searchQuery,
         int pageNumber, int pageSize);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<Order?> GetOrderByNumberAsync(int number);

    Task<Payment?> GetPaymentAsync(int orderId);
    void AddOrder(Order order);
    void AddPayment(Order order, Payment payment);

    void DeletePayment(Order order, Payment payment);
    void DeleteOrder(Order order);    

    // COUPONS
    Task<IEnumerable<Coupon>> GetAllCoupons();
    Task<Coupon?> GetCouponByIdAsync(int couponId);
    void AddCoupon(Coupon coupon);
    void DeleteCoupon(Coupon coupon);

    Task<bool> CouponExistsAndIsActive(string couponCode);

    Task<bool> CouponExists(string couponCode);
    // COUPONS
    Task<bool> SaveChangesAsync();    
    
    //outros contexts
    public Task<Order?> GetOrderToOrderByIdAsync(int orderId);
    public Task<StockHistoryOrder?> GetStockHistoryOrderToOrderByIdAsync(int stockHistoryId);
    public Task<Appointment?> GetAppointmentToOrderByIdAsync(int appointmentId);
    public Task<Product?> GetProductToOrderByIdAsync(int productId);
}
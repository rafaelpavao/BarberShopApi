using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features;
using Barbearia.Domain.Entities;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(OrderContext OrderContext, IMapper mapper)
    {
        _context = OrderContext ?? throw new ArgumentNullException(nameof(OrderContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<(IEnumerable<Order>, PaginationMetadata)> GetAllOrdersAsync(string? searchQuery,
         int pageNumber, int pageSize)
    {
        IQueryable<Order> collection = _context.Orders
        .Include(o => o.Payment)
        .Include(o => o.Person);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim().ToLower();
            collection = collection.Where(
                o => o.Number.ToString().Contains(searchQuery)
                || o.Person != null
                && o.Person.Name.ToLower().Contains(name)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var orderToReturn = await collection
        .OrderBy(o => o.OrderId)
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToListAsync();

        return (orderToReturn, paginationMetadata);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
        .Include(o => o.Person)
        .Include(o => o.Payment)
        .Include(o => o.Products)
        .Include(o => o.Appointments)
        .Include(o => o.StockHistoriesOrder)
        .OrderBy(o => o.OrderId)
        .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
        .Include(o => o.Person)
        .Include(o => o.Payment)
        .Include(o => o.Products)
        .Include(o => o.Appointments)
        .Include(o => o.StockHistoriesOrder)
        .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<Order?> GetOrderToOrderByIdAsync(int orderId)
    {
        return await _context.Orders
        .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<StockHistoryOrder?> GetStockHistoryOrderToOrderByIdAsync(int stockHistoryId)
    {
        return await _context.StockHistories.OfType<StockHistoryOrder>()
        .FirstOrDefaultAsync(s => s.StockHistoryId == stockHistoryId);
    }

    public async Task<Appointment?> GetAppointmentToOrderByIdAsync(int appointmentId)
    {
        return await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
    }

    public async Task<Product?> GetProductToOrderByIdAsync(int productId)
    {
        return await _context.Products
        .FirstOrDefaultAsync(p => p.ItemId == productId);
    }

    public async Task<Order?> GetOrderByNumberAsync(int number)
    {
        return await _context.Orders
        .Include(o => o.Person)
        .Include(o => o.Payment)
        .Include(o => o.Products)
        .Include(o => o.Appointments)
        .Include(o => o.StockHistoriesOrder)
        .FirstOrDefaultAsync(o => o.Number == number);
    }

    public async Task<Payment?> GetPaymentAsync(int orderId)
    {
        return await _context.Payments
        .Include(p => p.Coupon)
        .FirstOrDefaultAsync(p => p.OrderId == orderId);
    }

    public void AddOrder(Order order)
    {
        _context.Orders.Attach(order);
    }

    public void AddPayment(Order order, Payment payment)
    {
        order.Payment = payment;
    }

    public void DeletePayment(Order order, Payment payment)
    {
        if (order.Payment != null)
        {
            _context.Payments.Remove(order.Payment);
        }
        order.Payment = null;
    }

    public void DeleteOrder(Order order)
    {
        // Excluir pagamentos associados sem cascade
        // var pagamentosParaExcluir = _context.Payments.Where(p => p.OrderId == order.OrderId);
        // _context.Payments.RemoveRange(pagamentosParaExcluir);

        _context.Orders.Remove(order);
        _context.Entry(order).State = EntityState.Detached;
    }

    public async Task<Coupon?> GetCouponByIdAsync(int couponId)
    {
        return await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == couponId);
    }

    public async Task<IEnumerable<Coupon>> GetAllCoupons()
    {
        return await _context.Coupons.OrderBy(c => c.CouponId).ToListAsync();
    }

    public async void AddCoupon(Coupon coupon)
    {
        await _context.Coupons.AddAsync(coupon);
    }

    public void DeleteCoupon(Coupon coupon)
    {
        _context.Coupons.Remove(coupon);
    }

    public async Task<bool> CouponExists(string couponCode)
    {
        return await _context.Coupons.AnyAsync(c => c.CouponCode == couponCode);
    }

    public async Task<bool> CouponExistsAndIsActive(string couponCode)
    {
        var couponFromDb = await _context.Coupons.Where(c => c.CouponCode == couponCode && c.ExpirationDate > DateTime.UtcNow).FirstOrDefaultAsync();
        if (couponFromDb == null)
        {
            return false;
        }

        return true;

    }    

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}